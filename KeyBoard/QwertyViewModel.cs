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

        public KbViewModel()
        {
            IsCapsLock = GetCapsLockState();
        }

        private static bool GetCapsLockState()
        {
            byte[] bs = new byte[256];
            GetKeyboardState(bs);
            return bs[KeyInterop.VirtualKeyFromKey(Key.CapsLock)] == 1;
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
            if (multiParameter.Parameter2)
            {
                keybd_event((byte)KeyInterop.VirtualKeyFromKey(multiParameter.Parameter1), 0, KEYEVENTF_KEYDOWN, IntPtr.Zero);
            }
            else
            {
                keybd_event((byte)KeyInterop.VirtualKeyFromKey(multiParameter.Parameter1), 0, KEYEVENTF_KEYUP, IntPtr.Zero);
            }
            await Task.CompletedTask;
        }
    }
}
