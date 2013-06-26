using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;
using log4net;

namespace Engine {
    public class Input : Base {
        private static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Form _window;
        private readonly DirectInput _di;
        private ulong _pressStamp;

        private readonly Keyboard _keyboard;
        private KeyboardState _keyState;
        private ulong[] _keyPressStamp = new ulong[256];

        private readonly Mouse _mouse;
        private MouseState _mouseState;
        private ulong[] _buttonPressStamp = new ulong[3];
        private Point _position;

        public Input(Form window) {
            _window = window;
            _di = new DirectInput();

            _keyboard = new Keyboard(_di);
            //_keyboard.SetCooperativeLevel(_window.Handle, CooperativeLevel.Background | CooperativeLevel.Nonexclusive);
            _keyboard.Acquire();
            Log.Info("Keyboard aquired");

            _mouse = new Mouse(_di);
            //_mouse.SetCooperativeLevel(_window.Handle, CooperativeLevel.Background | CooperativeLevel.Nonexclusive);
            _mouse.Acquire();
            Log.Info("Mouse aquired");

            _pressStamp = 0;

        }
        public void Release() {
            ReleaseCom(_di);
            ReleaseCom(_mouse);
            ReleaseCom(_keyboard);
        }

        private Result _result;
        public void Update() {
            //Log.Info("updating");
            while (true) {
                try {
                    _keyboard.Poll();
                    _keyState = _keyboard.GetCurrentState();
                    

                    if (Result.Last.IsSuccess) {
                        break;
                    }

                } catch (DirectInputException ex) {
                    Console.WriteLine(ex.Message);
                    _result = ex.ResultCode;
                    if (_result != ResultCode.InputLost && _result != ResultCode.NotAcquired) {
                        return;
                    }
                    if ((_result = (_keyboard.Acquire())).IsFailure) {
                        Log.Info("Failed to aquire keyboard");
                        //Log.Info(_result);
                        return;
                    }
                }


            }
            while (true) {
                try {
                    
                    _mouse.Poll();
                    _mouseState = _mouse.GetCurrentState();
                    break;

                } catch (DirectInputException ex) {
                    _result = ex.ResultCode;
                    if (_result != ResultCode.InputLost && _result != ResultCode.NotAcquired) {
                        return;
                    }
                    if (((_result = _mouse.Acquire())).IsFailure) {
                        Log.Info("Failed to aquire mouse");
                        Log.Info(_result);
                        return;
                    }
                }
            }
            _position = Cursor.Position;
            if (!_window.IsDisposed) {
                _position = _window.PointToClient(_position);
            }
            _pressStamp++;


        }
        public bool GetKeyPress(Key key, bool ignorePressStamp = false) {
            if (_keyState == null || !_keyState.IsPressed(key)) {
                return false;
            }

            var pressed = true;

            if (ignorePressStamp == false) {
                if (_keyPressStamp[(int)key] == _pressStamp - 1 || _keyPressStamp[(int)key] == _pressStamp) {
                    pressed = false;
                }
            }
            _keyPressStamp[(int)key] = _pressStamp;

            return pressed;
        }
        public bool GetButtonPress(char button, bool ignorePressStamp = false) {
            if (!_mouseState.IsPressed(button)) return false;

            var pressed = true;

            if (!ignorePressStamp) {
                if (_buttonPressStamp[button] == _pressStamp - 1 || _buttonPressStamp[button] == _pressStamp) {
                    pressed = false;
                }
            }
            _buttonPressStamp[button] = _pressStamp;

            return pressed;

        }

        public long GetPosX() {
            return _position.X;
        }
        public long GetPosY() {
            return _position.Y;
        }
        public long GetDeltaX() {
            return _mouseState.X;
        }
        public long GetDeltaY() {
            return _mouseState.Y;
        }
        public long GetDeltaWheel() {
            return _mouseState.Z;
        }
    }
}