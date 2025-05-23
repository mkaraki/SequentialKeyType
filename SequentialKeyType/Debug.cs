using System;
using System.Windows.Forms;

namespace SequentialKeyType
{
    public partial class Debug : Form
    {
        internal Debug(KEYBDINPUT[] keybdinputs)
        {
            InitializeComponent();

            string dbgStr = string.Empty;
            for (int i = 0; i < keybdinputs.Length; i++)
            {
                var keybdInput = keybdinputs[i];
                dbgStr += $"[{i}] vk: {keybdInput.wVk:X2}, sc: {keybdInput.wScan:X2}, flags: {keybdInput.dwFlags:X2}, time: {keybdInput.time}, extra: {keybdInput.dwExtraInfo}\r\n";
            }
            tbox_msg.Text = dbgStr;
        }

        private void Debug_Load(object sender, EventArgs e)
        {

        }
    }
}
