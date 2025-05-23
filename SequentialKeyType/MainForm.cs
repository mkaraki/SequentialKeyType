using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SequentialKeyType
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            combo_layout.Items.Clear();

            var layouts = KeyCodeUtils.GetUsableKeyboardLayouts().ToArray();

            if (layouts.Length == 0)
            {
                MessageBox.Show("No usable keyboard layouts found.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            List<uint> idxSearch = new List<uint>();

            foreach (var layout in layouts)
            {
                var id = layout.Item1;
                var name = layout.Item2;
                combo_layout.Items.Add($"{id:X4} - {name}");
                idxSearch.Add(id);
            }

            var currentLayout = KeyCodeUtils.GetCurrentKeyboardLayout();
            var currentIdx = Array.IndexOf(idxSearch.ToArray(), currentLayout);

            if (currentIdx == -1)
            {
                System.Diagnostics.Debug.WriteLine($"[ERR] No current layout {currentLayout:x8} exists in layout list.");
                MessageBox.Show(
                    $"Unexpected error. Current keyboard layout isn't in usable layout list. This may cause some problems.",
                    "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                combo_layout.SelectedIndex = currentIdx;
            }
        }

        private async void btn_type_Click(object sender, EventArgs e)
        {
            tbox_text.ReadOnly = true;
            num_delay.ReadOnly = true;
            combo_layout.Enabled = false;
            btn_type.Enabled = false;

            try
            {
                var packedKeyCodes = GetPackedKeyboardInput();
                if (packedKeyCodes == null)
                {
                    return;
                }

                var keyCodes = KeyCodeUtils.ExtractPackedKeyboardInputToSendInputFormat(packedKeyCodes).ToArray();

                var delay = (int)num_delay.Value;

                await Task.Delay(delay * 1000 /* Sec to ms */);

                foreach (var key in keyCodes)
                {
                    var result = KeyCodeUtils.SendKey(key);

                    if (result != 0)
                    {
                        MessageBox.Show($"Failed to send key. Error: 0x{result:X8}\r\nProcess Aborted.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    await Task.Delay(5);
                }

                /*
                 * This is most effective way to send keys. But this won't send big string correctly.
                 * And we decided to put some Delay between each key press.
                 *
                 * var result = KeyCodeUtils.SendKeys(keyCodes);
                 * if (result != 0)
                 * {
                 *     MessageBox.Show($"Failed to send keys. Error: {result:X8}", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 * }
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error occurred: {ex.Message}", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tbox_text.ReadOnly = false;
                num_delay.ReadOnly = false;
                combo_layout.Enabled = true;
                btn_type.Enabled = true;
            }
        }

        private void btn_debug_Click(object sender, EventArgs e)
        {
            tbox_text.ReadOnly = true;
            num_delay.ReadOnly = true;
            combo_layout.Enabled = false;
            btn_type.Enabled = false;

            try
            {
                var packedKeyCodes = GetPackedKeyboardInput();
                if (packedKeyCodes == null)
                {
                    return;
                }

                using (var form = new Debug(packedKeyCodes))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error occurred: {ex.Message}", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tbox_text.ReadOnly = false;
                num_delay.ReadOnly = false;
                combo_layout.Enabled = true;
                btn_type.Enabled = true;
            }
        }

        private KEYBDINPUT[] GetPackedKeyboardInput()
        {
            var text = tbox_text.Text;
            if (!uint.TryParse(combo_layout.Text.Split(' ')[0], NumberStyles.HexNumber, null, out var layout))
            {
                MessageBox.Show("Invalid keyboard layout.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Text cannot be empty.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var keyUtil = new KeyCodeUtils(layout);

            return keyUtil.GetVirtualKeyCodes(text).ToArray();
        }
    }
}
