#include "launcher.h"

//
// Initialize
//
void framework_init() {
	FreeConsole();
}

//
// Checks if .NET/Mono 4.0 is installed
//
bool framework_check() {
	HKEY hKey = NULL;
	LONG lRes = RegOpenKeyExW(HKEY_LOCAL_MACHINE, L"SOFTWARE\\Microsoft\\.NETFramework\\Policy\\v4.0", 0, KEY_READ, &hKey);

	// check if successful
	if (lRes == ERROR_SUCCESS) {
		return true;
	}

	return false;
}

//
// Asks the user if they want to install .net 4.0
//
bool framework_warning() {
	// ask user
	int result = MessageBoxA(NULL, "The Sling client requires .NET 4.0, do you want to install it now?", "Install?", MB_YESNO | MB_ICONQUESTION);

	// check if they want to install
	if (result == IDYES) {
		return true;
	} else {
		return false;
	}
}

//
// Install the .net framework
//
void framework_install() {
	// structs
	STARTUPINFO si;
    PROCESS_INFORMATION pi;

    ZeroMemory( &si, sizeof(si) );
    si.cb = sizeof(si);
    ZeroMemory( &pi, sizeof(pi) );

    // start installer
    if( !CreateProcessA( "redist/dotNetFx40.exe","/q /norestart",NULL, NULL,FALSE,0,NULL,NULL,&si,&pi)) 
    {
        MessageBoxA(NULL, "An error occured trying to install .NET 4.0, try reinstalling the application!", "Error", MB_OK | MB_ICONERROR);
        return;
    }

    // wait for exit
    WaitForSingleObject( pi.hProcess, INFINITE );

    // close handles
    CloseHandle( pi.hProcess );
    CloseHandle( pi.hThread );
}

void framework_launch() {
	// structs
	STARTUPINFO si;
    PROCESS_INFORMATION pi;

    ZeroMemory( &si, sizeof(si) );
    si.cb = sizeof(si);
    ZeroMemory( &pi, sizeof(pi) );

    // start installer
    if( !CreateProcessA( "Sling.Desktop.exe","",NULL, NULL,FALSE,0,NULL,NULL,&si,&pi)) 
    {
        MessageBoxA(NULL, "An error occured trying to launch Sling, try reinstalling the application!", "Error", MB_OK | MB_ICONERROR);
        return;
    }

    // close handles
    CloseHandle( pi.hProcess );
    CloseHandle( pi.hThread );
}

int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    return entry();
}