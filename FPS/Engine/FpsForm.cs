using System.Windows.Forms;

namespace Engine {
    public class FpsForm:Form {
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WM_DESTROY = 0x0002;
        private const int WM_QUIT = 0x0012;

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case WM_ACTIVATEAPP:
                    Engine.GEngine.DeactiveFlag = (((int)m.WParam == 0));
                    return;
                case WM_DESTROY:
                case WM_QUIT:
                    Close();
                    Engine.GEngine.Quit();
                    return;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}