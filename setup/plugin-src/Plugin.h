#pragma once

#include "PluginTypes.h"

#ifdef FTSERLIBPLUGIN_EXPORTS
#define FTSERLIBPLUGINDLL __declspec(dllexport)
#else
#define FTSERLIBPLUGINDLL __declspec(dllimport)
#endif

#ifdef __cplusplus
extern "C"
{
#endif

void
__cdecl
FtDataPluginClose();

BOOL
__cdecl
FtDataPluginInit();

typedef BOOL (__cdecl *PLUGIN_INIT)(wchar_t*, UINT, LPFNPLUGIN_WRITEDATA_CALLBACK);

PLUGIN_INIT Init;

typedef BOOL (__cdecl *PLUGIN_PROCEEDDATA)(
		__in UINT dwPortId,
		__in const void* lpBuffer,
		__in UINT lpcbBuffer);

PLUGIN_PROCEEDDATA ProceedData;

typedef BOOL (__cdecl *PLUGIN_CLEANUP)(
	__in UINT dwPortId);

PLUGIN_CLEANUP Cleanup;

#ifdef __cplusplus
}
#endif