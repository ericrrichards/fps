using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SlimDX;

namespace Engine {

    public class Base {
        public void ReleaseCom( ref ComObject p) {
            if (p != null && !p.Disposed) p.Dispose();
            p = null;
            
        }
    }

    public class EngineSetup {
        public IntPtr HInstance { get; set; }
        public string Name { get; set; }

        public EngineSetup() {
            HInstance = IntPtr.Zero;
            Name = "Application";
        }
    }

    public class FpsForm:Form {
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WM_DESTROY = 0x0002;
        private const int WM_QUIT = 0x0012;

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case WM_ACTIVATEAPP:
                    Engine.GEngine.DeactiveFlag = (((int)m.WParam != 0));
                    return;
                case WM_DESTROY:
                case WM_QUIT:
                    Close();
                    Engine.GEngine.Quit();
                    return;
                default:
                    base.WndProc(ref m);
                    return;
            }
        }
    }


    public class Engine : Base {
        private static volatile Engine _gEngine; 
        public static Engine GEngine { get { return _gEngine; }}


        private bool _loaded;
        private Form _window;
        private bool _deactive;
        private EngineSetup _setup;
        private bool _running;
        private long _lastTime;

        public Engine(EngineSetup setup = null) {
            _loaded = false;
            _setup = new EngineSetup();
            if (setup != null) {
                _setup = setup;
            }
            _gEngine = this;

            _window  = new FpsForm {Name = "WindowClass", Text = _setup.Name, FormBorderStyle = FormBorderStyle.FixedSingle, Size = new Size(800, 600)};

            _loaded = true;
            _running = true;
        }
        ~Engine() {
            if (_loaded) {
                
            }
            _window.Dispose();
        }

        public void Run() {
            if (_loaded) {
                _window.Show();
                _lastTime = Stopwatch.GetTimestamp();
                while (_running) {
                    Application.DoEvents();
                    if (!_deactive) {
                        var currentTime = Stopwatch.GetTimestamp();
                        
                        var elapsed = (currentTime - _lastTime)/Stopwatch.Frequency;
                        _lastTime = currentTime;
                    }
                }
                Application.Exit();
            }
        }

        public Form Window { get { return _window; } }
        public bool DeactiveFlag { set { _deactive = value; } }

        public void Quit() {
            _running = false;
        }
    }
}
