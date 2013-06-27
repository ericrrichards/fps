using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SlimDX.Direct3D9;

namespace Engine {
    public partial class DeviceEnumeration : Form {
        public static DeviceEnumeration GDeviceEnumeration { get; private set; }

        public bool Windowed { get { return _windowed; } }
        public bool VSync { get { return _vsync; } }
        public SlimDX.Direct3D9.DisplayMode SelectedDisplayMode { get { return _selectedDisplayMode; } }

        private readonly Script _settingsScript;
        private readonly AdapterDetails _adapter;
        private List<DisplayMode> _displayModes;
        private SlimDX.Direct3D9.DisplayMode _selectedDisplayMode;
        private bool _windowed;
        private bool _vsync;
        public DeviceEnumeration(Direct3D d3d) {
            InitializeComponent();
            _displayModes = new List<DisplayMode>();
            _settingsScript = new Script("DisplaySettings.txt");
            _adapter = d3d.GetAdapterIdentifier(0);

            var allowedFormats = new[] {
                Format.X1R5G5B5,
                Format.A1R5G5B5,
                Format.R5G6B5,
                Format.X8R8G8B8,
                Format.A8R8G8B8,
                Format.A2R10G10B10,
            };
            for (var af = 0; af < allowedFormats.Length; af++) {
                var format = allowedFormats[af];
                var modes = d3d.GetAdapterModeCount(0, format);
                for (var m = 0; m < modes; m++) {
                    var mode = d3d.EnumerateAdapterModes(0, format, m);
                    if (mode.Height < 600) continue;

                    var displayMode = new DisplayMode {
                        Mode = mode,
                        Bpp = af < 3 ? "16 bpp" : "32 bpp"
                    };
                    _displayModes.Add(displayMode);
                }
            }

            txtAdapterName.Text = _adapter.Description;
            txtDriverVersion.Text = _adapter.DriverVersion.ToString();

            if (_settingsScript.GetBool("windowed")) {
                _windowed = true;
                rbWindowed.Checked = true;
            } else {
                rbFullscreen.Checked = true;
                _windowed = false;
            }
            if (!_windowed) {
                // enable fullscreen options
                chkVSync.Enabled = true;
                cbColorFormat.Enabled = true;
                cbResolution.Enabled = true;
                cbRefresh.Enabled = true;

                chkVSync.Checked = _settingsScript.GetBool("vsync");

                ResetColorFormats();

                long selectedRes = _settingsScript.GetNumber("resolution");
                ResetResolution(selectedRes);

                long refresh = _settingsScript.GetNumber("refresh");
                ResetRefresh(refresh);
            }
        }

        private void ResetRefresh(long refresh) {
            cbRefresh.Items.Clear();
            foreach (var mode in _displayModes) {
                if (MakeLong(mode.Mode.Width, mode.Mode.Height) == ((KeyValuePair<long, string>) cbResolution.SelectedItem).Key) {
                    var text = string.Format("{0} Hz", mode.Mode.RefreshRate);
                    var i = cbRefresh.Items.Add(new KeyValuePair<int, string>(mode.Mode.RefreshRate, text));

                    if (mode.Mode.RefreshRate == refresh) {
                        cbRefresh.SelectedIndex = i;
                    }
                }
            }
        }

        private void ResetResolution(long selectedRes) {
            cbResolution.Items.Clear();
            foreach (var mode in _displayModes) {
                if (mode.Mode.Format == ((KeyValuePair<Format, String>) cbColorFormat.SelectedItem).Key) {
                    var text = string.Format("{0} x {1}", mode.Mode.Width, mode.Mode.Height);
                    var i = cbResolution.Items.Add(new KeyValuePair<long, string>(MakeLong(mode.Mode.Width, mode.Mode.Height), text));

                    if (selectedRes == MakeLong(mode.Mode.Width, mode.Mode.Height)) {
                        cbResolution.SelectedIndex = i;
                    }
                }
            }
        }

        private void ResetColorFormats() {
            cbColorFormat.Items.Clear();
            foreach (var mode in _displayModes) {
                var i = cbColorFormat.Items.Add(new KeyValuePair<Format, string>(mode.Mode.Format, mode.Bpp));
                if (_settingsScript.GetNumber("bpp") == (decimal) mode.Mode.Format) {
                    cbColorFormat.SelectedIndex = i;
                }
            }
        }

        private static long MakeLong(int a, int b) {
            return (a & 0xffff) | (((long)b & 0xffff) << 16);
        }

        private void btnOK_Click(object sender, EventArgs e) {
            _selectedDisplayMode = new SlimDX.Direct3D9.DisplayMode();
            _selectedDisplayMode.Width = LowWord(((KeyValuePair<long, string>) cbResolution.SelectedItem).Key);
            _selectedDisplayMode.Height = HighWord(((KeyValuePair<long, string>) cbResolution.SelectedItem).Key);
            _selectedDisplayMode.RefreshRate = ((KeyValuePair<int, string>) cbRefresh.SelectedItem).Key;
            _selectedDisplayMode.Format = ((KeyValuePair<Format, string>) cbColorFormat.SelectedItem).Key;
            _windowed = rbWindowed.Checked;
            _vsync = chkVSync.Checked;

            _settingsScript.AddVariable("windowed", VariableType.Bool, _windowed);
            _settingsScript.AddVariable("vsync", VariableType.Bool, _vsync);
            _settingsScript.AddVariable("bpp", VariableType.Number, (long)_selectedDisplayMode.Format);
            _settingsScript.AddVariable("resolution", VariableType.Number, MakeLong(_selectedDisplayMode.Width, _selectedDisplayMode.Width));
            _settingsScript.AddVariable("refresh", VariableType.Number, _selectedDisplayMode.RefreshRate);

            _settingsScript.SaveScript();
            
        }

        private static int LowWord(long a) {
            return (int) (a & 0x0000ffff);
        }
        private static int HighWord(long a) {
            return (int) ((a >> 16) & 0xffff);
        }

        private void cbColorFormat_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedRes = ((KeyValuePair<long, string>) cbResolution.SelectedItem).Key;
            ResetResolution(selectedRes);
            if (cbResolution.SelectedIndex < 0) {
                cbResolution.SelectedIndex = 0;
            }
        }

        private void cbResolution_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedRefresh = ((KeyValuePair<long, string>)cbRefresh.SelectedItem).Key;
            ResetRefresh(selectedRefresh);
            if (cbRefresh.SelectedIndex < 0) {
                cbRefresh.SelectedIndex = 0;
            }
        }

        private void rbWindowed_CheckedChanged(object sender, EventArgs e) {
            if (rbWindowed.Checked) {
                cbColorFormat.Items.Clear();
                cbResolution.Items.Clear();
                cbRefresh.Items.Clear();

                cbResolution.Enabled = false;
                cbColorFormat.Enabled = false;
                cbRefresh.Enabled = false;
                chkVSync.Enabled = false;
            } else {
                cbResolution.Enabled = true;
                cbColorFormat.Enabled = true;
                cbRefresh.Enabled = true;
                chkVSync.Enabled = true;
                ResetColorFormats();
                cbColorFormat.SelectedIndex = 0;
            }
        }
    }
    internal struct DisplayMode {
        public SlimDX.Direct3D9.DisplayMode Mode { get; set; }
        public string Bpp { get; set; }
    }
}
    
