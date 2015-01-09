#include <stdbool.h>
#include <stdlib.h>
#include <stdio.h>

// platform specific includes
#ifdef _WIN32
	#include "windows.h"
	#include "winreg.h"
#elif __unix__
	#error Platform compliation not implemented
#endif