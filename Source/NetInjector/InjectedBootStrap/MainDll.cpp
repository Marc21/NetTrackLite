#include "windows.h"
#include "MSCorEE.h"

void StartTheDotNetRuntime()
{
    // Bind to the CLR runtime..
    ICLRRuntimeHost *pClrHost = NULL;
    HRESULT hr = CorBindToRuntimeEx(
        NULL, L"wks", 0, CLSID_CLRRuntimeHost,
        IID_ICLRRuntimeHost, (PVOID*)&pClrHost);

    // Push the big START button shown above
    hr = pClrHost->Start();

    // Okay, the CLR is up and running in this (previously native) process.
    // Now call a method on our managed C# class library.
    DWORD dwRet = 0;
    hr = pClrHost->ExecuteInDefaultAppDomain(
        L"c:\\PathToYourManagedAssembly\\MyManagedAssembly.dll",
        L"MyNamespace.MyClass", L"MyMethod", L"MyParameter", &dwRet);

    // Optionally stop the CLR runtime (we could also leave it running)
    hr = pClrHost->Stop();

    // Don't forget to clean up.
    pClrHost->Release();
}

BOOL APIENTRY DllMain( 
	HANDLE hModule, 
	DWORD  ul_reason_for_call, 
	LPVOID lpReserved
	)
{
	BOOL bResult = TRUE;

	switch (ul_reason_for_call)
	{
		
		case DLL_PROCESS_ATTACH:
			// We disable thread notifications
			// Prevent the system from calling DllMain
			// when threads are created or destroyed.
			::DisableThreadLibraryCalls( (HINSTANCE)hModule );
			::MessageBox(0,"DLL_PROCESS_ATTACH","DLL_PROCESS_ATTACH",MB_OK);
			StartTheDotNetRuntime();

			break;

		// The attached process creates a new thread. 
		case DLL_THREAD_ATTACH: 
			//::MessageBox(0,"DLL_THREAD_ATTACH","DLL_THREAD_ATTACH",MB_OK);
			//::DoRTCInitialization();
			break; 

		// The thread of the attached process terminates.
		case DLL_THREAD_DETACH: 
			// Release the allocated memory for this thread.
			//::MessageBox(0,"DLL_THREAD_DETACH","DLL_THREAD_DETACH",MB_OK);
			break; 

		// DLL unload due to process termination or FreeLibrary. 
		case DLL_PROCESS_DETACH:
			//::MessageBox(0,"DLL_PROCESS_DETACH","DLL_PROCESS_DETACH",MB_OK);
			break;

	} // switch

	return TRUE;
    UNREFERENCED_PARAMETER(hModule); 
    UNREFERENCED_PARAMETER(lpReserved); 
}

