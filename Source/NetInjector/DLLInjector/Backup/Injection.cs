                   /*Classe d'injection de dll par Misugi
                              *fait le 28/12/07 à 01:35 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace DLLInjector
{
    public static class Injection
    {
        #region Win32

        [DllImport("KERNEL32.DLL")]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("KERNEL32.DLL")]
        private static extern bool Process32First(IntPtr hSnapShot, ref PROCESSENTRY32 pe);

        [DllImport("KERNEL32.DLL")]
        private static extern bool Process32Next(IntPtr Handle, ref PROCESSENTRY32 lppe);

        [DllImport("KERNEL32.DLL")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("KERNEL32.DLL")]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAdress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("KERNEL32.DLL")]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAdress, uint dwSize, uint dwFreeType);

        [DllImport("KERNEL32.DLL")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("KERNEL32.DLL")]
        private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr se, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, uint lpThreadId);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi)]
        private extern static IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("KERNEL32.DLL")]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("KERNEL32.DLL")]
        private static extern int GetLastError();

        [DllImport("KERNEL32.DLL")]
        private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliSeconds);

        private const uint TH32CS_SNAPPROCESS = 0x00000002;
        private const uint PROCESS_ALL_ACCESS = (uint)(0x000F0000 | 0x00100000 | 0xFFF);
        private const uint MEM_COMMIT = 0x1000;
        private const uint MEM_RESERVE = 0x2000;
        private const uint MEM_DECOMMIT = 0x4000;
        private const uint MEM_RELEASE = 0x8000;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;
        private const uint PAGE_READWRITE = 0X4;
        private const uint WAIT_ABANDONED = 0x00000080;
        private const uint WAIT_OBJECT_0 = 0x00000000;
        private const uint WAIT_TIMEOUT = 0x00000102;
        private const uint WAIT_FAILED = 0xFFFFFFFF;

        //Structure pe32
        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESSENTRY32
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
        private struct SECURITY_ATTRIBUTES
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

        //Fonction qui affiche les processus dans la listview et qui les met dans la liste
        public static void GetProcessList(ref ListView ProcessListView)
        {
            //on va dresser une liste de tout les processus ouverts
            IntPtr hSnapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);

            //structure qui va contenir le nom, pid etc... de chaque processus
            PROCESSENTRY32 Pe = new PROCESSENTRY32();
            Pe.dwSize = Marshal.SizeOf(Pe);

            //on commence par le premier processus
            bool ret = Process32First(hSnapshot, ref Pe);

            //on va boucler tout les processus
            while (ret)
            {
                //on ajoute le processus dans notre liste de processus
                ProcessList.Add(Pe);

                //on ajoute le processus dans notre listview
                ListViewItem Item = new ListViewItem(Pe.szExeFile); //son nom
                ProcessListView.Items.Add(Item);                    //ajout

                //prochain processus
                ret = Process32Next(hSnapshot, ref Pe);
            }

            //on ferme l'handle snapshot
            CloseHandle(hSnapshot);
        }

        //Fonction qui retourne l'id d'un processus à partir de son nom
        public static uint GetPIDbyName(string PName)
        {
            //on boucle tout les processus et on compare le nom du process[i] avec celui qu'on recherche
            foreach (PROCESSENTRY32 p in ProcessList)
            {
                if (string.Compare(p.szExeFile, PName) == 0) //si on l'a trouvé
                {
                    return p.th32ProcessID; //on retourne son id
                }
            }

            return 0; //processus non trouvé, on retourne 0
        }

        /// <summary>
        /// Fonction qui injecte une dll
        /// </summary>
        /// <param name="DllName">Nom de la dll qui va être injectée.</param>
        /// <param name="ProcessName">Nom du processus dans lequel la dll sera injectée.</param>
        public static bool StartInjection(string DllName, uint ProcessID)
        {
            try
            {
                IntPtr hProcess = new IntPtr(0); //openprocess
                IntPtr hModule = new IntPtr(0); //vritualAllocex
                IntPtr Injector = new IntPtr(0); //getprocadress
                IntPtr hThread = new IntPtr(0); //createremotethread
                int LenWrite = DllName.Length + 1;

                //on ouvre le processus avec tout les droits d'accés
                hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, ProcessID);

                //si il a bien été ouvert
                if (hProcess != IntPtr.Zero)
                {
                    //on va allouer de la mémoire
                    hModule = VirtualAllocEx(hProcess, IntPtr.Zero, (UIntPtr)LenWrite, MEM_COMMIT, PAGE_EXECUTE_READWRITE);

                    //si on a bien alloué de la mémoire
                    if (hModule != IntPtr.Zero)
                    {
                        //on va écrire le nom de la dll dans le process
                        ASCIIEncoding Encoder = new ASCIIEncoding();

                        //nombre de bytes écrits
                        int Written = 0;

                        //si on a bien écrit dans le process
                        if (WriteProcessMemory(hProcess, hModule, Encoder.GetBytes(DllName), LenWrite, Written))
                        {
                            //on va rechercher la fonction LoadLibrary qui va charger la dll
                            Injector = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

                            //si on a bien trouvé l'adresse
                            if (Injector != IntPtr.Zero)
                            {
                                //on lance le thread qui va s'executer dans l'espace mémoire du processus et charger la dll
                                hThread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, Injector, hModule, 0, 0);

                                //pas d'erreur avec le lancement du thread
                                if (hThread != IntPtr.Zero)
                                {
                                    //10 secondes
                                    uint Result = WaitForSingleObject(hThread, 10 * 1000);

                                    //...
                                    if (Result != WAIT_FAILED || Result != WAIT_ABANDONED
                                       || Result != WAIT_OBJECT_0 || Result != WAIT_TIMEOUT)
                                    {
                                        //on désalloc la mémoire allouée
                                        if (VirtualFreeEx(hProcess, hModule, 0, MEM_RELEASE))
                                        {
                                            //on regarde si l'handle du thread retourné n'est pas null
                                            if (hThread != IntPtr.Zero)
                                            {
                                                //injection réussie :]
                                                CloseHandle(hThread);
                                                MessageBox.Show("Injection réussie !", "Succés", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(e.Message, e.Source);
                return false;
            }
        }

    }
}
