using System;
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

            uint[] layouts = KeyCodeUtils.GetUsableKeyboardLayouts().ToArray();

            if (layouts.Length == 0)
            {
                MessageBox.Show("No usable keyboard layouts found.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            foreach (var layout in layouts)
            {
                combo_layout.Items.Add(layout.ToString("X8"));
            }

            var currentLayout = KeyCodeUtils.GetCurrentKeyboardLayout();
            var currentIdx = Array.IndexOf(layouts, currentLayout);

            if (currentIdx == -1)
            {
                MessageBox.Show(
                    "Unexpected error. Current keyboard layout isn't in usable layout list. This may cause some problems.",
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
                var text = tbox_text.Text;
                var delay = (int)num_delay.Value;
                if (!uint.TryParse(combo_layout.Text, NumberStyles.HexNumber, null, out var layout))
                {
                    MessageBox.Show("Invalid keyboard layout.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(text))
                {
                    MessageBox.Show("Text cannot be empty.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var keyUtil = new KeyCodeUtils(layout);

                var packedKeyCodes = keyUtil.GetVirtualKeyCodes(text).ToArray();
                var keyCodes = KeyCodeUtils.ExtractPackedKeyboardInputToSendInputFormat(packedKeyCodes).ToArray();

                await Task.Delay(delay * 1000 /* Sec to ms */);

                foreach (var key in keyCodes)
                {
                    var result = KeyCodeUtils.SendKey(key);

                    if (result != 0)
                    {
                        MessageBox.Show($"Failed to send key. Error: {result:X8}\r\nProcess Aborted.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var text = tbox_text.Text;
                var delay = (int)num_delay.Value;
                if (!uint.TryParse(combo_layout.Text, NumberStyles.HexNumber, null, out var layout))
                {
                    MessageBox.Show("Invalid keyboard layout.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(text))
                {
                    MessageBox.Show("Text cannot be empty.", "Sequential Key Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var keyUtil = new KeyCodeUtils(layout);

                var packedKeyCodes = keyUtil.GetVirtualKeyCodes(text).ToArray();

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
    }
}
