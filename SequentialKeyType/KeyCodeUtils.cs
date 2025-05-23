﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SequentialKeyType
{
    internal class KeyCodeUtils
    {
        [DllImport("user32.dll")]
        private static extern ushort VkKeyScanExW(char ch, UIntPtr dwhkl);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint cInputs, [MarshalAs(UnmanagedType.LPArray)] INPUT[] pInputs, int cbSize);

        public static IEnumerable<(uint, string)> GetUsableKeyboardLayouts()
        {
            foreach (InputLanguage locale in InputLanguage.InstalledInputLanguages)
            {
                yield return (
                    (uint)locale.Culture.KeyboardLayoutId,
                    locale.LayoutName
                );
            }
        }

        public static uint GetCurrentKeyboardLayout()
        {
            var lang = InputLanguage.CurrentInputLanguage;
            return (uint)lang.Culture.KeyboardLayoutId;
        }

        public uint LocaleId { get; }

        public KeyCodeUtils(uint localeId)
        {
            LocaleId = localeId;
        }

        public static IEnumerable<KEYBDINPUT> ExtractPackedKeyboardInputToSendInputFormat(IEnumerable<KEYBDINPUT> input)
        {
            foreach (var key in input)
            {
                if (key.wScan == 0)
                {
                    yield return key; // No scan code, no need to extract

                    yield return new KEYBDINPUT() // But need to add keyup
                    {
                        wVk = key.wVk,
                        wScan = 0, // No scan code
                        dwFlags = 0x0002, // KEYEVENTF_KEYUP
                        time = key.time,
                        dwExtraInfo = key.dwExtraInfo,
                    };

                    continue;
                }

                // If there are scan code.

                if ((key.wScan & 0b1) > 0) // SHIFT
                {
                    yield return new KEYBDINPUT()
                    {
                        wVk = 0x10,
                        wScan = 0,
                        dwFlags = 0,
                        time = key.time,
                        dwExtraInfo = key.dwExtraInfo,
                    };
                }

                if ((key.wScan & 0b10) > 0) // CTRL
                {
                    yield return new KEYBDINPUT()
                    {
                        wVk = 0x11,
                        wScan = 0,
                        dwFlags = 0,
                        time = key.time,
                        dwExtraInfo = key.dwExtraInfo,
                    };
                }

                if ((key.wScan & 0b100) > 0) // ALT
                {
                    yield return new KEYBDINPUT()
                    {
                        wVk = 0x12,
                        wScan = 0,
                        dwFlags = 0,
                        time = key.time,
                        dwExtraInfo = key.dwExtraInfo,
                    };
                }

                // ToDo: Support Hankaku key

                yield return new KEYBDINPUT()
                {
                    wVk = key.wVk,
                    wScan = 0, // No scan code
                    dwFlags = 0,
                    time = key.time,
                    dwExtraInfo = key.dwExtraInfo,
                };

                yield return new KEYBDINPUT()
                {
                    wVk = key.wVk,
                    wScan = 0, // No scan code
                    dwFlags = 0x0002, // KEYEVENTF_KEYUP
                    time = key.time,
                    dwExtraInfo = key.dwExtraInfo,
                };


                if ((key.wScan & 0b1) > 0) // SHIFT
                {
                    yield return new KEYBDINPUT()
                    {
                        wVk = 0x10,
                        wScan = 0,
                        dwFlags = 0x0002, // KEYEVENTF_KEYUP
                        time = key.time,
                        dwExtraInfo = key.dwExtraInfo,
                    };
                }

                if ((key.wScan & 0b10) > 0) // CTRL
                {
                    yield return new KEYBDINPUT()
                    {
                        wVk = 0x11,
                        wScan = 0,
                        dwFlags = 0x0002, // KEYEVENTF_KEYUP
                        time = key.time,
                        dwExtraInfo = key.dwExtraInfo,
                    };
                }

                if ((key.wScan & 0b100) > 0) // ALT
                {
                    yield return new KEYBDINPUT()
                    {
                        wVk = 0x12,
                        wScan = 0,
                        dwFlags = 0x0002, // KEYEVENTF_KEYUP
                        time = key.time,
                        dwExtraInfo = key.dwExtraInfo,
                    };
                }
            }
        }

        public KEYBDINPUT GetVirtualKeyCode(char ch)
        {
            // ToDo: Virtual Key method might not work in different keyboard layout VM or app. Should be checked.
            var key = VkKeyScanExW(ch, new UIntPtr(LocaleId));

            if (key == 0xFFFF /* Both high and low is -1 */)
            {
                throw new InvalidOperationException($"Unable to get key code for character '{ch}'.");
            }

            ushort keycode = (ushort)(key & 0xFF); // Low byte, virtual key code
            ushort scanCode = (ushort)((key >> 8) & 0xFF); // High byte, scan code

            var ret = new KEYBDINPUT
            {
                wVk = keycode,
                wScan = scanCode,
                dwFlags = 0,
                time = 100,
                //time = 0, // Let system decide
                dwExtraInfo = new UIntPtr((ulong)0),
            };

            return ret;
        }

        public IEnumerable<KEYBDINPUT> GetVirtualKeyCodes(string str)
        {
            // \r\n make double new line and \n will put unknown "9" before new line.
            // So use \r only.
            str = str
                .Replace("\r\n", "\r")
                .Replace("\n", "\r")
                ;

            foreach (var ch in str)
            {
                var key = GetVirtualKeyCode(ch);
                yield return key;
            }
        }

        public static int SendKey(KEYBDINPUT keybdInput)
        {
            INPUT input = new INPUT
            {
                type = 1, // Input type is keyboard
                ki = keybdInput
            };
            INPUT[] inputs = new INPUT[1];
            inputs[0] = input;
            var cnt = SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));

            return cnt == 1 ? 0 : Marshal.GetLastWin32Error();
        }

        public static int SendKeys(KEYBDINPUT[] keybdInputs)
        {
            uint seqCnt = (uint)keybdInputs.Length;
            INPUT[] inputs = new INPUT[keybdInputs.Length];
            for (int i = 0; i < keybdInputs.Length; i++)
            {
                inputs[i] = new INPUT
                {
                    type = 1, // Input type is keyboard
                    ki = keybdInputs[i]
                };
            }
            var cnt = SendInput(seqCnt, inputs, Marshal.SizeOf(typeof(INPUT)));
            return cnt == seqCnt ? 0 : Marshal.GetLastWin32Error();
        }

        private struct INPUT
        {
            internal uint type;
            internal KEYBDINPUT ki;
            private uint Unused;
            private uint Unused2;
        }
    }

    internal struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
}
