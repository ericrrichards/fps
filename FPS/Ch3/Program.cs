using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Engine;
using SlimDX.DirectInput;

namespace Ch3 {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var setup = new EngineSetup();
            setup.HInstance = Process.GetCurrentProcess().Handle;
            setup.Name = "Engine Control Test";
            setup.StateSetup = StateSetup;

            new Engine.Engine(setup);
            Engine.Engine.GEngine.Run();
        }

        private static void StateSetup() {
            Engine.Engine.GEngine.AddState(new TestState(), true);
        }
    }

    internal class TestState : State {
        public override void Update(float elapsed) {
            if (Engine.Engine.GEngine.Input.GetKeyPress(Key.Q)) {
                Engine.Engine.GEngine.Quit();
                
            }
        }
    }
}
