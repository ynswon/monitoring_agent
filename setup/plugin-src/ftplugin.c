//////////////////////////////////////////////////////////////////////////
// Copyright (c) 2002-2013 FabulaTech
// All rights reserved.
// Distributed under Non-disclosure agreement.
// http://www.fabulatech.com
//////////////////////////////////////////////////////////////////////////

#ifdef _MFC_VER

#include "afxwin.h"

#else

#include <windows.h>

#endif

#include "Plugin.h"

static HMODULE g_hPlugin;

BOOL
__cdecl
FtDataPluginInit()
{
	DWORD dwErr;

	g_hPlugin = LoadLibraryW(L"dummy.dll");

	if (NULL == g_hPlugin)
		return FALSE;

	do
	{
		Init = (PLUGIN_INIT)GetProcAddress(
			g_hPlugin, "Init");

		if (!Init)
			break;

		ProceedData = (PLUGIN_PROCEEDDATA)GetProcAddress(
			g_hPlugin, "ProceedData");

		if (!ProceedData)
			break;

		Cleanup = (PLUGIN_CLEANUP)GetProcAddress(
			g_hPlugin, "Cleanup");

		if (!Cleanup)
			break;

		return TRUE;

	} while(0);

	//
	dwErr = GetLastError();

	FtDataPluginClose();

	SetLastError(dwErr);

	return FALSE;
}

void
__cdecl
FtDataPluginClose()
{
	Init = NULL;
	ProceedData = NULL;
	Cleanup = NULL;

	FreeLibrary(g_hPlugin);

	g_hPlugin = NULL;
}
