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
                throw new InjectionException("Dll to inject not found :"+DllName);
            
            IntPtr hProcess = new IntPtr(0); // for openprocess
            IntPtr hModule = new IntPtr(0);  // for vritualAllocex
            IntPtr hKernel32Module = new IntPtr(0);  // for GetModuleHandle("kernel32.dll")
            IntPtr hLoadLDll = new IntPtr(0); // for getprocadress
            IntPtr hThread = new IntPtr(0);  // for createremotethread
            int LenWrite = DllName.Length + 1;

            hProcess = NativeInterop.OpenProcess(NativeInterop.PROCESS_ALL_ACCESS, false, ProcessID);

            if (hProcess == IntPtr.Zero)
                throw new InjectionException("Fail to open process");

            //memory allocation in target process
            hModule = NativeInterop.VirtualAllocEx(hProcess, IntPtr.Zero, (UIntPtr)LenWrite, NativeInterop.MEM_COMMIT, NativeInterop.PAGE_EXECUTE_READWRITE);

            if (hModule == IntPtr.Zero)
                throw new Exception("Fail to allocate memory on distant process");

            //write dll path to target process
            ASCIIEncoding Encoder = new ASCIIEncoding();
            int written=0;
            byte[] dllPathAsBytes=Encoder.GetBytes(DllName);
            //int length=dllPathAsBytes.Length+1;
            //byte[] memoryBuffer=new byte[length];
            //memoryBuffer.Initialize();
            //dllPathAsBytes.CopyTo(memoryBuffer,0);

            if (!NativeInterop.WriteProcessMemory(hProcess, hModule, dllPathAsBytes, LenWrite, written))
                throw new Exception("Fail to write memory (" + LenWrite + " bytes) on a distant process");

            // get proc address for LoadLibraryA
            hKernel32Module=NativeInterop.GetModuleHandle("kernel32.dll");
            if (hKernel32Module==IntPtr.Zero)
                throw new Exception("Fail to find kernel32.dll module");

            hLoadLDll = NativeInterop.GetProcAddress(hKernel32Module, "LoadLibraryA");

            if (hLoadLDll==IntPtr.Zero)
                throw new Exception("Fail to find proc address of LoadLibraryA in kernel32.dll");

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
    

            /// <summary>
        /// Fonction qui injecte une dll
        /// </summary>
        /// <param name="DllName">Nom de la dll qui va être injectée.</param>
        /// <param name="ProcessName">Nom du processus dans lequel la dll sera injectée.</param>
        public static bool StartInjection(uint ProcessID, string DllName)
        {
            if (!File.Exists(DllName))
                return false;

            //Injection.StartInjection( DllName,ProcessID);
            try
            {
                IntPtr hProcess = new IntPtr(0); //openprocess
                IntPtr hModule = new IntPtr(0); //vritualAllocex
                IntPtr Injector = new IntPtr(0); //getprocadress
                IntPtr hThread = new IntPtr(0); //createremotethread
                int LenWrite = DllName.Length + 1;

                //on ouvre le processus avec tout les droits d'accés
                hProcess = NativeInterop.OpenProcess(NativeInterop.PROCESS_ALL_ACCESS, false, ProcessID);

                //si il a bien été ouvert
                if (hProcess != IntPtr.Zero)
                {
                    //on va allouer de la mémoire
                    hModule = NativeInterop.VirtualAllocEx(hProcess, IntPtr.Zero, (UIntPtr)LenWrite, NativeInterop.MEM_COMMIT, NativeInterop.PAGE_EXECUTE_READWRITE);

                    //si on a bien alloué de la mémoire
                    if (hModule != IntPtr.Zero)
                    {
                        //on va écrire le nom de la dll dans le process
                        ASCIIEncoding Encoder = new ASCIIEncoding();

                        //nombre de bytes écrits
                        int Written = 0;

                        //si on a bien écrit dans le process
                        if (NativeInterop.WriteProcessMemory(hProcess, hModule, Encoder.GetBytes(DllName), LenWrite, Written))
                        {
                            //on va rechercher la fonction LoadLibrary qui va charger la dll
                            Injector = NativeInterop.GetProcAddress(NativeInterop.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

                            //si on a bien trouvé l'adresse
                            if (Injector != IntPtr.Zero)
                            {
                                //on lance le thread qui va s'executer dans l'espace mémoire du processus et charger la dll
                                hThread = NativeInterop.CreateRemoteThread(hProcess, IntPtr.Zero, 0, Injector, hModule, 0, 0);

                                //pas d'erreur avec le lancement du thread
                                if (hThread != IntPtr.Zero)
                                {
                                    //10 secondes
                                    uint Result = NativeInterop.WaitForSingleObject(hThread, 10 * 1000);

                                    //...
                                    if (Result != NativeInterop.WAIT_FAILED || Result != NativeInterop.WAIT_ABANDONED
                                       || Result != NativeInterop.WAIT_OBJECT_0 || Result != NativeInterop.WAIT_TIMEOUT)
                                    {
                                        //on désalloc la mémoire allouée
                                        if (NativeInterop.VirtualFreeEx(hProcess, hModule, 0, NativeInterop.MEM_RELEASE))
                                        {
                                            //on regarde si l'handle du thread retourné n'est pas null
                                            if (hThread != IntPtr.Zero)
                                            {
                                                //injection réussie :]
                                                NativeInterop.CloseHandle(hThread);
                                                return true;
                                            }
                                            else throw new Exception("Mauvais Handle du thread...injection échouée");
                                        }
                                        else throw new Exception("Problème libèration de mémoire...injection échouée");
                                    }
                                    else throw  new Exception("WaitForSingle échoué : " + Result.ToString() + "...injection échouée");
                                }
                                else throw new Exception("Problème au lancement du thread...injection échouée");
                            }
                            else throw new Exception("Adresse LoadLibraryA non trouvée...injection échouée");
                        }
                        else throw new Exception("Erreur d'écriture dans le processus...injection échouée");
                    }
                    else throw new Exception("Mémoire non allouée...injection échouée");
                }
                else throw new Exception("Processus non ouvert...injection échouée");

            }
            catch (Exception e)
            {
                //en cas d'erreur on affiche l'erreur et on arrête la fonction(pas d'injection)
                return false;
            }
        }

    }

}
