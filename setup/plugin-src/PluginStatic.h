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

FTSERLIBPLUGINDLL 
BOOL 
__cdecl
Init(
		__in wchar_t *lpszPortName,
		__in UINT lpdwPortId,
		__in LPFNPLUGIN_WRITEDATA_CALLBACK lpfnWriteData);

FTSERLIBPLUGINDLL 
BOOL 
__cdecl
ProceedData(
		__in UINT dwPortId,
		__in const void* lpBuffer,
		__in UINT lpcbBuffer);

FTSERLIBPLUGINDLL
BOOL 
__cdecl
Cleanup(
	__in UINT dwPortId);

#ifdef __cplusplus
}
#endif