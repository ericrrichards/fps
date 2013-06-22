using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Engine;

namespace Ch2 {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var setup = new EngineSetup() {HInstance = Process.GetCurrentProcess().Handle, Name = "Framework Test"};

            new Engine.Engine(setup);
            Engine.Engine.GEngine.Run();
        }
    }
}
