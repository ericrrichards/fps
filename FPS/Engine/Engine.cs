using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;

namespace Engine {

    public class Base {
        public void ReleaseCom( ComObject p) {
            if (p != null && !p.Disposed) p.Dispose();
            p = null;
            
        }
    }

    public delegate void StateSetup();
    public class EngineSetup {
        public IntPtr HInstance { get; set; }
        public string Name { get; set; }
        public StateSetup StateSetup { get; set; }

        public EngineSetup() {
            HInstance = IntPtr.Zero;
            Name = "Application";
            StateSetup = null;
        }
    }


    public class Engine : Base {
        private static volatile Engine _gEngine; 
        public static Engine GEngine { get { return _gEngine; }}


        private bool _loaded;
        private bool _deactive;
        private EngineSetup _setup;

        private readonly List<State> _states;
        private bool _stateChanged;

        // my adds
        private bool _running;
        private long _lastTime;

        public Engine(EngineSetup setup = null) {
            log4net.Config.XmlConfigurator.Configure();

            _loaded = false;
            _setup = new EngineSetup();
            if (setup != null) {
                _setup = setup;
            }
            _gEngine = this;

            Window  = new FpsForm {Name = "WindowClass", Text = _setup.Name, FormBorderStyle = FormBorderStyle.FixedSingle, Size = new Size(800, 600)};

            Window.Show();
            Window.Activate();
            _states = new List<State>();
            CurrentState = null;
            Input = new Input(Window);

            if (_setup.StateSetup != null) {
                _setup.StateSetup();
            }

            _loaded = true;
            _running = true;
        }
        public void Release() {
            if (_loaded) {
                if (CurrentState != null) {
                    CurrentState.Close(); 
                }
                _states.Clear();
                Input.Release();
            }
            Window.Dispose();
        }

        public void Run() {
            if (_loaded) {
                Window.Show();

                ViewerSetup viewer;

                _lastTime = Stopwatch.GetTimestamp();
                while (_running) {
                    Application.DoEvents();
                    if (!_deactive) {
                        var currentTime = Stopwatch.GetTimestamp();
                        
                        var elapsed = (currentTime - _lastTime)/Stopwatch.Frequency;
                        _lastTime = currentTime;

                        Input.Update();

                        if (Input.GetKeyPress(Key.Escape)) {
                            Quit();
                        }

                        if (CurrentState != null) {
                            CurrentState.RequestViewer( out viewer);
                        }
                        _stateChanged = false;
                        if (CurrentState != null) {
                            CurrentState.Update(elapsed);
                        }
                        if (_stateChanged) {
                            continue;
                        }
                     }
                }
                Release();
                Application.Exit();
            }
        }

        public Form Window { get; private set; }
        public bool DeactiveFlag { set { _deactive = value; } }

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
            var newState = _states.FirstOrDefault(s => s.ID == id);
            if (newState == null) return;
            if ( CurrentState != null) CurrentState.Close();
            CurrentState = newState;
            CurrentState.Load();

            _stateChanged = true;
        }

        public State CurrentState { get; private set; }
        public Input Input { get; private set; }

        public void Quit() {
            _running = false;
        }
    }
}
