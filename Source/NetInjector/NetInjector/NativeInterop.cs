using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace NetInjector
{
    public static class NativeInterop
    {
        #region Win32

        [DllImport("KERNEL32.DLL")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("KERNEL32.DLL")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAdress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("KERNEL32.DLL")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAdress, uint dwSize, uint dwFreeType);

        [DllImport("KERNEL32.DLL")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("KERNEL32.DLL")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr se, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, uint lpThreadId);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi)]
        public extern static IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("KERNEL32.DLL")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("KERNEL32.DLL")]
        public static extern int GetLastError();

        [DllImport("KERNEL32.DLL")]
        public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliSeconds);

        public const uint TH32CS_SNAPPROCESS = 0x00000002;
        public const uint PROCESS_ALL_ACCESS = (uint)(0x000F0000 | 0x00100000 | 0xFFF);
        public const uint MEM_COMMIT = 0x1000;
        public const uint MEM_RESERVE = 0x2000;
        public const uint MEM_DECOMMIT = 0x4000;
        public const uint MEM_RELEASE = 0x8000;
        public const uint PAGE_EXECUTE_READWRITE = 0x40;
        public const uint PAGE_READWRITE = 0X4;
        public const uint WAIT_ABANDONED = 0x00000080;
        public const uint WAIT_OBJECT_0 = 0x00000000;
        public const uint WAIT_TIMEOUT = 0x00000102;
        public const uint WAIT_FAILED = 0xFFFFFFFF;

        //Structure pe32
        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESSENTRY32
        {
            public int dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        };

        //Structure Sa
        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        #endregion

        static List<PROCESSENTRY32> ProcessList; //Liste de tout les processus

        //Init de la liste des processus
        public static void InitProcessList()
        {
            ProcessList = new List<PROCESSENTRY32>();
        }

        //Vidage de la liste des processus
        public static void ClearProcessList()
        {
            //on vide tout
            ProcessList.Clear();
        }

    }
}
