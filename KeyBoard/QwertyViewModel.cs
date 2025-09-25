using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyBoard
{
    partial class KbViewModel : ObservableObject
    {
        // 导入Windows API函数
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);
        // 模拟按键按下和释放
        const int KEYEVENTF_KEYDOWN = 0;
        const int KEYEVENTF_KEYUP = 2;

        /// <summary>
        /// 获取键盘状态
        /// </summary>
        /// <param name="pbKeyState"></param>
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [ObservableProperty]
        private bool _isCapsLock;

        [ObservableProperty]
        private Brush _background = new SolidColorBrush(Colors.Transparent);

        [ObservableProperty]
        private bool _isCtrlPressed;

        [ObservableProperty]
        private bool _isAltPressed;

        [ObservableProperty]
        private bool _isShiftPressed;

        public KbViewModel()
        {
            IsCapsLock = GetCapsLockState();
            IsCtrlPressed = IsKeyPressed(Key.LeftCtrl) || IsKeyPressed(Key.RightCtrl);
            IsAltPressed = IsKeyPressed(Key.LeftAlt) || IsKeyPressed(Key.RightAlt);
            IsShiftPressed = IsKeyPressed(Key.LeftShift) || IsKeyPressed(Key.RightShift);
        }

        private static bool GetCapsLockState()
        {
            byte[] bs = new byte[256];
            GetKeyboardState(bs);
            return bs[KeyInterop.VirtualKeyFromKey(Key.CapsLock)] == 1;
        }

        private static bool IsKeyPressed(Key key)
        {
            byte[] keyStates = new byte[256];
            GetKeyboardState(keyStates);

            // 检查按键的高位是否被设置
            return (keyStates[KeyInterop.VirtualKeyFromKey(key)] & 0x80) != 0;
        }

        [RelayCommand]
        private static async Task KeyPassAsync(Key key)
        {
            keybd_event((byte)KeyInterop.VirtualKeyFromKey(key), 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
            keybd_event((byte)KeyInterop.VirtualKeyFromKey(key), 0, KEYEVENTF_KEYUP, IntPtr.Zero);
            await Task.CompletedTask;
        }

        [RelayCommand]
        private static async Task TogglePassAsync(MultiParameter multiParameter)
        {
            // 检查是否是需要左右一起按下的键
            if (multiParameter.Parameter1 == Key.LeftCtrl || multiParameter.Parameter1 == Key.RightCtrl)
            {
                if (multiParameter.Parameter2)
                {
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.LeftCtrl), 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.RightCtrl), 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
                }
                else
                {
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.LeftCtrl), 0, KEYEVENTF_KEYUP, IntPtr.Zero);
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.RightCtrl), 0, KEYEVENTF_KEYUP, IntPtr.Zero);
                }
            }
            else if (multiParameter.Parameter1 == Key.LeftAlt || multiParameter.Parameter1 == Key.RightAlt)
            {
                if (multiParameter.Parameter2)
                {
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.LeftAlt), 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.RightAlt), 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
                }
                else
                {
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.LeftAlt), 0, KEYEVENTF_KEYUP, IntPtr.Zero);
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.RightAlt), 0, KEYEVENTF_KEYUP, IntPtr.Zero);
                }
            }
            else if (multiParameter.Parameter1 == Key.LeftShift || multiParameter.Parameter1 == Key.RightShift)
            {
                if (multiParameter.Parameter2)
                {
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.LeftShift), 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.RightShift), 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
                }
                else
                {
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.LeftShift), 0, KEYEVENTF_KEYUP, IntPtr.Zero);
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(Key.RightShift), 0, KEYEVENTF_KEYUP, IntPtr.Zero);
                }
            }
            else
            {
                // 按原逻辑处理
                if (multiParameter.Parameter2)
                {
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(multiParameter.Parameter1), 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
                }
                else
                {
                    keybd_event((byte)KeyInterop.VirtualKeyFromKey(multiParameter.Parameter1), 0, KEYEVENTF_KEYUP, IntPtr.Zero);
                }
            }

            await Task.CompletedTask;
        }
    }
}
