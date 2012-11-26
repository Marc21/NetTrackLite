#include "StdAfx.h"
#include "Windows.h"


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
			::MessageBox(0,L"DLL_PROCESS_ATTACH",L"DLL_PROCESS_ATTACH",MB_OK);
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

