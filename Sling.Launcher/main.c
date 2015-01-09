// launcher includes
#include "launcher.h"

// framework functions
bool framework_check();
bool framework_warning();
void framework_install();
void framework_launch();

//
// Entry point
//
int entry() {
	framework_init();

	if (!framework_check()) {
		bool install = framework_warning();

		if (install) {
			framework_install();
			framework_launch();
		}
	} else {
		framework_launch();
	}

	return 0;
}

