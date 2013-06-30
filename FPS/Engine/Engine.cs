using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.DirectInput;
using log4net.Config;
using Device = SlimDX.Direct3D9.Device;
using DeviceType = SlimDX.Direct3D9.DeviceType;

namespace Engine {
    using System.Threading;

    public class Base {
        protected void ReleaseCom(ComObject p) {
            if (p != null && !p.Disposed) p.Dispose();
            p = null;
        }
    }

    public delegate void StateSetup();

    public class EngineSetup {
        public EngineSetup() {
            HInstance = IntPtr.Zero;
            Name = "Application";
            StateSetup = null;
            Scale = 1.0f;
            TotalBackBuffers = 1;
        }

        public IntPtr HInstance { get; set; }
        public string Name { get; set; }
        public float Scale { get; set; }
        public byte TotalBackBuffers { get; set; }
        public StateSetup StateSetup { get; set; }
    }


    public class Engine : Base {
        private static volatile Engine _gEngine;


        private readonly bool _loaded;
        private readonly EngineSetup _setup;
        private readonly List<State> _states;
        private byte _currentBackBuffer;
        private bool _deactive;
        private Device _device;
        private SlimDX.Direct3D9.DisplayMode _displayMode;
        private long _lastTime;
        private bool _running;
        private Sprite _sprite;
        private bool _stateChanged;

        public Engine(EngineSetup setup = null) {
            XmlConfigurator.Configure();
            
            _loaded = false;
            _setup = new EngineSetup();
            if (setup != null) {
                _setup = setup;
            }
            _gEngine = this;

            var d3d = new Direct3D();
            var enumeration = new DeviceEnumeration(d3d);
            if (enumeration.ShowDialog() != DialogResult.OK) {
                ReleaseCom(d3d);
                return;
            }


            Window = new FpsForm {
                Name = "WindowClass", 
                Text = _setup.Name, 
                FormBorderStyle = enumeration.Windowed ? FormBorderStyle.FixedSingle : FormBorderStyle.None,
                Size = new Size(800, 600)
            };

            var pp = new PresentParameters {
                BackBufferWidth = enumeration.SelectedDisplayMode.Width, 
                BackBufferHeight = enumeration.SelectedDisplayMode.Height, 
                BackBufferFormat = enumeration.SelectedDisplayMode.Format, 
                BackBufferCount = _setup.TotalBackBuffers,
                SwapEffect = SwapEffect.Discard,
                DeviceWindowHandle = Window.Handle,
                Windowed =  enumeration.Windowed,
                EnableAutoDepthStencil =  true,
                AutoDepthStencilFormat = Format.D24S8,
                FullScreenRefreshRateInHertz = enumeration.SelectedDisplayMode.RefreshRate,
                PresentationInterval = enumeration.VSync ? PresentInterval.Default :  PresentInterval.Immediate,
                Multisample = MultisampleType.None,
                MultisampleQuality = 0,
                PresentFlags = PresentFlags.None
                
            };
            enumeration.Dispose();

            _device = new Device(d3d, 0, DeviceType.Hardware, Window.Handle, CreateFlags.MixedVertexProcessing, pp);

            ReleaseCom(d3d);

            _device.SetSamplerState(0, SamplerState.MagFilter, TextureFilter.Anisotropic);
            _device.SetSamplerState(0, SamplerState.MinFilter, TextureFilter.Anisotropic);
            _device.SetSamplerState(0, SamplerState.MipFilter, TextureFilter.Linear);

            var proj = Matrix.PerspectiveFovLH(
                (float) (Math.PI/4),
                (float)pp.BackBufferWidth/pp.BackBufferHeight,
                0.1f/_setup.Scale, 1000.0f/_setup.Scale
            );
            _device.SetTransform(TransformState.Projection, proj);
            _displayMode = new SlimDX.Direct3D9.DisplayMode {
                Width = pp.BackBufferWidth, 
                Height = pp.BackBufferHeight, 
                RefreshRate = pp.FullScreenRefreshRateInHertz, 
                Format = pp.BackBufferFormat
            };
            _currentBackBuffer = 0;

            _sprite = new Sprite(_device);


            Window.Show();
            Window.Activate();
            _states = new List<State>();
            CurrentState = null;

            ScriptManager = new ResourceManager<Script>();

            Input = new Input(Window);

            if (_setup.StateSetup != null) {
                _setup.StateSetup();
            }

            _loaded = true;
            _running = true;
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs) {
            throw new NotImplementedException();
        }

        private void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs threadExceptionEventArgs) {
            throw new NotImplementedException();
        }

        public static Engine GEngine {
            get { return _gEngine; }
        }

        public Form Window { get; private set; }

        public bool DeactiveFlag {
            set { _deactive = value; }
        }

        public State CurrentState { get; private set; }
        public Input Input { get; private set; }
        public ResourceManager<Script> ScriptManager { get; private set; }

        // my adds

        public void Release() {
            if (_loaded) {
                if (CurrentState != null) {
                    CurrentState.Close();
                }
                _states.Clear();
                Input.Release();
                ReleaseCom(_sprite);
                ReleaseCom( _device);

            }
            Window.Dispose();
        }

        public void Run() {
            if (_loaded) {
                Window.Show();

                ViewerSetup viewer = new ViewerSetup();

                _lastTime = Stopwatch.GetTimestamp();
                while (_running) {
                    Application.DoEvents();
                    if (!_deactive) {
                        long currentTime = Stopwatch.GetTimestamp();

                        long elapsed = (currentTime - _lastTime)/Stopwatch.Frequency;
                        _lastTime = currentTime;

                        Input.Update();

                        if (Input.GetKeyPress(Key.Escape)) {
                            Quit();
                        }

                        if (CurrentState != null) {
                            CurrentState.RequestViewer(out viewer);
                        }
                        _stateChanged = false;
                        if (CurrentState != null) {
                            CurrentState.Update(elapsed);
                        }
                        if (_stateChanged) {
                            continue;
                        }
                        try {
                            _device.Clear(viewer.ClearFlags, Color.White, 1.0f, 0);
                        } catch (Direct3D9Exception dex) {
                            
                        }
                        if (_device.BeginScene().IsSuccess) {
                            if (CurrentState != null) {
                                CurrentState.Render();
                            }
                            _device.EndScene();
                            _device.Present();
                            if (++_currentBackBuffer == _setup.TotalBackBuffers + 1) {
                                _currentBackBuffer = 0;
                            }
                        }
                    }
                }
                Release();
                Application.Exit();
            }
        }


        public void AddState(State state, bool change) {
            _states.Add(state);
            if (!change) return;
            if (CurrentState != null) {
                CurrentState.Close();
            }
            CurrentState = _states.Last();
            CurrentState.Load();
        }

        public void RemoveState(State state) {
            _states.Remove(state);
        }

        public void ChangeState(ulong id) {
            State newState = _states.FirstOrDefault(s => s.ID == id);
            if (newState == null) return;
            if (CurrentState != null) CurrentState.Close();
            CurrentState = newState;
            CurrentState.Load();

            _stateChanged = true;
        }


        public void Quit() {
            _running = false;
        }
    }
}