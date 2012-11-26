using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NetInjector
{
    public class Injector
    {
        public void Inject(uint ProcessID, string DllName)
        {
            if (!File.Exists(DllName))
                throw new InjectionException("Dll to inject not found :" + DllName);

            IntPtr hProcess = new IntPtr(0); // for openprocess
            IntPtr hModule = new IntPtr(0);  // for vritualAllocex
            IntPtr hKernel32Module = new IntPtr(0);  // for GetModuleHandle("kernel32.dll")
            IntPtr hLoadLDll = new IntPtr(0); // for getprocadress
            IntPtr hThread = new IntPtr(0);  // for createremotethread

            hProcess = NativeInterop.OpenProcess(NativeInterop.PROCESS_ALL_ACCESS, false, ProcessID);

            if (hProcess == IntPtr.Zero)
                throw new InjectionException("Fail to open process");

            //memory allocation in target process
            var Encoder = new UTF8Encoding();
            int written = 0;
            byte[] dllPathAsBytes = Encoder.GetBytes(DllName);
            int length = dllPathAsBytes.Length + 1;
            hModule = NativeInterop.VirtualAllocEx(hProcess, IntPtr.Zero, (UIntPtr)length, NativeInterop.MEM_COMMIT, NativeInterop.PAGE_EXECUTE_READWRITE);

            if (hModule == IntPtr.Zero)
                throw new Exception("Fail to allocate memory on distant process");

            //write dll path to target process

            byte[] memoryBuffer = new byte[length];
            memoryBuffer.Initialize();
            dllPathAsBytes.CopyTo(memoryBuffer, 0);

            if (!NativeInterop.WriteProcessMemory(hProcess, hModule, memoryBuffer, length, written))
                throw new Exception("Fail to write memory (" + length + " bytes) on a distant process");

            // get proc address for LoadLibraryA
            hKernel32Module = NativeInterop.GetModuleHandle("kernel32.dll");
            if (hKernel32Module == IntPtr.Zero)
                throw new Exception("Fail to find kernel32.dll module");

            hLoadLDll = NativeInterop.GetProcAddress(hKernel32Module, "LoadLibraryW");

            if (hLoadLDll == IntPtr.Zero)
                throw new Exception("Fail to find proc address of LoadLibraryW in kernel32.dll");

            //launching the remote thread to load the dll
            hThread = NativeInterop.CreateRemoteThread(hProcess, IntPtr.Zero, 0, hLoadLDll, hModule, 0, 0);

            if (hThread == IntPtr.Zero)
                throw new Exception("Fail to create a remote thread in the target process : check if your user is missing debug rights");

            //wait fro thread to start
            uint Result = NativeInterop.WaitForSingleObject(hThread, 5 * 1000);
            NativeInterop.VirtualFreeEx(hProcess, hModule, 0, NativeInterop.MEM_RELEASE);

            if (Result != NativeInterop.WAIT_FAILED || Result != NativeInterop.WAIT_ABANDONED
                || Result != NativeInterop.WAIT_OBJECT_0 || Result != NativeInterop.WAIT_TIMEOUT)
            {
                //injection succeeded
                NativeInterop.CloseHandle(hThread);
            }
            else
                throw new InjectionException("Remote thread created but failled to start");
        }
    }    
}
