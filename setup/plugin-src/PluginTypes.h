#pragma once

#include "windows.h"

typedef void(__cdecl *LPFNPLUGIN_WRITEDATA_CALLBACK)(
		__in UINT lpdwPortId, 
		__in const void* lpBuffer, 
		__in UINT lpcbBuffer);
