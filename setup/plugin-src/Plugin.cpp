#include "PluginStatic.h"
#include <map>
#include <cstdio>
#include <ctime>
#define BUFSIZE 1000
HANDLE g_hPipe;
int Timeoutcount = 10;
char* dir = "c:\\Program Files\\WePass\\WePassAutoQR\\log_filter\\";
LPTSTR lpszPipename = TEXT("\\\\.\\pipe\\wepassQRpipe"); 
//LPFNPLUGIN_WRITEDATA_CALLBACK lpfnWriteData;


void log(char* msg) {
	
	char path[512];
	char date[80];
	char strtime[80];
	time_t rawtime;	
	struct tm* timeinfo;
	time(&rawtime);
	timeinfo = localtime(&rawtime);
	strftime(date,80,"%Y-%m-%d",timeinfo);
	strftime(strtime,80,"%H:%M:%S",timeinfo);
	strcpy(path,dir);
	strcat(path,date);
	strcat(path,".txt");
	FILE* pf = fopen(path, "a+");
	if( pf!=NULL ) {
		//do something with the error	
		fprintf(pf,strtime);
		fprintf(pf," | ");
		fprintf(pf, msg);
		fprintf(pf,"\n");
		fclose(pf);

	}
}
void log(char* msg,int num){
	FILE* pf = fopen("c:\\log.txt", "a+");
	if( pf!=NULL ) {
		//do something with the error	
		fprintf(pf, msg,num);
		fprintf(pf,"\n");
		fclose(pf);

	}
}
BOOL _WriteFile(HANDLE hFile, void* buffer, DWORD size, DWORD* pdwWritten) {

	HANDLE hEvent;
	OVERLAPPED iAIO;
	ZeroMemory(&iAIO,sizeof(iAIO));
		hEvent = CreateEvent(NULL,TRUE,FALSE,NULL);
		if(hEvent){
			iAIO.hEvent = hEvent;
		}
	BOOL fSuccess = WriteFile( // send message
		g_hPipe,                  // pipe handle 
		buffer,             // message 
		size,              // message length 
		pdwWritten,            // bytes written 
		&iAIO);  
	if(!fSuccess && GetLastError() == 997){
		log("GetLastError() = 997");
		while(!GetOverlappedResult(g_hPipe,&iAIO,pdwWritten,FALSE))
		{
			if(GetLastError() == 996){
				log("sleep for 0.5 sec");
				Sleep(500);
			}
			else if(GetLastError() == 38){
				log("EOF");
				break;
			}
		}
	}
	else if(!fSuccess && GetLastError() != 997){
		ResetEvent(iAIO.hEvent);
		CloseHandle(hEvent);
		return FALSE;
	}
	ResetEvent(iAIO.hEvent);
	CloseHandle(hEvent);
	return TRUE;
}
BOOL write(HANDLE hPipe, char* lpvMessage,DWORD cbToWrite){
	BOOL fSuccess;
	DWORD cbWritten;
	fSuccess = _WriteFile( // send message
		hPipe,                  // pipe handle 
		lpvMessage,             // message 
		cbToWrite,              // message length 
		&cbWritten            // bytes written 
		);   
	return fSuccess;
}

BOOL _ReadFile(HANDLE hFile, void* buffer, DWORD size, DWORD* pdwRead) {

	HANDLE hEvent;
	OVERLAPPED iAIO;
	ZeroMemory(&iAIO,sizeof(iAIO));
		hEvent = CreateEvent(NULL,TRUE,FALSE,NULL);
		if(hEvent){
			iAIO.hEvent = hEvent;
		}
	BOOL fSuccess = ReadFile( 
		g_hPipe,    // pipe handle 
		buffer,    // buffer to receive reply 
		size,  // size of buffer 
		pdwRead,  // number of bytes read 
		&iAIO); 
	if(!fSuccess && GetLastError() == 997){
		int timer = 0;
		while(!GetOverlappedResult(g_hPipe,&iAIO,pdwRead,FALSE))
		{
			if(timer >= Timeoutcount){
				ResetEvent(iAIO.hEvent);
				CloseHandle(hEvent);
				log("timeout");
				return FALSE;
			}

			if(GetLastError() == 996){
				log("sleep for 0.5 sec");
				timer++;
				Sleep(500);
			}
			else if(GetLastError() == 38){
				log("EOF");
				break;
			}
		}
	}
	else if(!fSuccess && GetLastError() != 997){
		ResetEvent(iAIO.hEvent);
		CloseHandle(hEvent);
		return FALSE;
	}
	ResetEvent(iAIO.hEvent);
	CloseHandle(hEvent);
	return TRUE;
}

int read(void** ppInBuffer)
{
	DWORD cbRead;
	byte* size = new byte[2];
	log("readfile");
	BOOL fSuccess = _ReadFile( 
		g_hPipe,    // pipe handle 
		size,    // buffer to receive reply 
		2*sizeof(char),  // size of buffer 
		&cbRead  // number of bytes read 
		);  
	if(!fSuccess){
		log("1st readfile failed with error code %d",GetLastError());
		return -1;
	}


	unsigned int dataSize = size[0]*256 + size[1];
	log("Size of received data = %d",dataSize);
	*ppInBuffer = malloc(dataSize);
	//BOOL 
	
	fSuccess = _ReadFile( 
		g_hPipe,    // pipe handle 
		*ppInBuffer,    // buffer to receive reply 
		dataSize,  // size of buffer 
		&cbRead  // number of bytes read 
		); 
	
	if(!fSuccess){
		log("readfile failed with error code %d",GetLastError());
		return -1;
	}
	log("readfile success");
	return cbRead;

}
int writeAndRead(void* outBuffer, int outLen, void** inBuffer){

	DWORD cbRead = 0;
	// 보낸다.
	BOOL fSuccess = write(
		g_hPipe,
		(char*)outBuffer,
		outLen);

	if ( ! fSuccess) 
	{
		log("writefile failed with error code %d",GetLastError());
		*inBuffer = outBuffer;
		return outLen;
	}



	// 받는다.
	cbRead = read(inBuffer);

	if(cbRead == -1)
	{
		*inBuffer = outBuffer;
		return outLen;
	}
	/*if(inBuffer == NULL)
		log("inBuffer is NULL!");	
	else
		log((char*)*inBuffer);*/

	return cbRead;

}

//TODO: lock g_PortsMap (with mutex, etc.) when multy-thread access is required.

// map<lpdwPortId; lpfnWriteData>
std::map<UINT, void*> g_PortsMap;

FTSERLIBPLUGINDLL 
	BOOL 
	__cdecl
	Init(
	__in wchar_t *lpszPortName,
	__in UINT lpdwPortId,
	__in LPFNPLUGIN_WRITEDATA_CALLBACK lpfnWriteData)
{

	g_hPipe = CreateFile( 
		lpszPipename,   // pipe name 
		GENERIC_READ |  // read and write access 
		GENERIC_WRITE, 
		0,              // no sharing 
		NULL,           // default security attributes
		OPEN_EXISTING,  // opens existing pipe 
		FILE_FLAG_OVERLAPPED,              // default attributes 
		NULL);          // no template file 

	if((int)g_hPipe != -1)
	{


		// 파이프의 스테이트 설정
		DWORD dwMode = PIPE_READMODE_BYTE ; 
		BOOL fSuccess = SetNamedPipeHandleState( 
			g_hPipe,    // pipe handle 
			&dwMode,  // new pipe mode 
			NULL,     // don't set maximum bytes 
			NULL);    // don't set maximum time 

		if ( ! fSuccess) 
		{		
			//	_tprintf( TEXT("SetNamedPipeHandleState failed. GLE=%d\n"), GetLastError() ); 
		}
		

	}
	if (!lpfnWriteData)
		return FALSE;

	g_PortsMap[lpdwPortId] = lpfnWriteData;
	return TRUE;
}

FTSERLIBPLUGINDLL 
	BOOL 
	__cdecl
	ProceedData(
	__in UINT dwPortId,
	__in const void* lpBuffer,
	__in UINT lpcbBuffer)
{
	std::map<UINT, void*>::iterator iMap;
	iMap = g_PortsMap.find(dwPortId);
	LPFNPLUGIN_WRITEDATA_CALLBACK lpfnWriteData = NULL;
	if (iMap != g_PortsMap.end())
	{
		lpfnWriteData = (LPFNPLUGIN_WRITEDATA_CALLBACK)(iMap->second);

		// TODO: change data as you want
		// =========================================================================
		//
		size_t outBufCnt = lpcbBuffer;
		void *outBuf = NULL;
		BOOL bAllocated = FALSE;
		UINT outBufShift = 0;

		
		if((int)g_hPipe != -1)
		{
			outBufCnt = writeAndRead( (void*)lpBuffer, lpcbBuffer, &outBuf );

			if(outBufCnt <= 0){
			log("outBufCnt is 0!!!");
			}

		}
		else 
		{
			log("g_hPipe =-1");
			outBuf = malloc(outBufCnt);
			
			for(int i=0;i<outBufCnt;i++)
			{
				memcpy_s((byte*)outBuf + i, 1, (byte*)lpBuffer + i, 1);
			}
		}


		//=========================================================================

		// CALLBACK: write changed data to physical port
		
		if(outBuf == NULL){
			log("outBuf is null!");
			outBufCnt = 0;
			
		}
		else{
			log((char*)outBuf);
		}
		lpfnWriteData(dwPortId, outBuf, outBufCnt);

		// clear all temporary buffers (to avoid memory leaks)
		free(outBuf);
	}
	else
		return FALSE;
	return TRUE;
}

FTSERLIBPLUGINDLL
	BOOL 
	__cdecl
	Cleanup(
	__in UINT dwPortId)
{
	std::map<UINT, void*>::iterator iMap;
	iMap = g_PortsMap.find(dwPortId);
	if (iMap != g_PortsMap.end())
	{
		g_PortsMap.erase(iMap);
	}
	CloseHandle(g_hPipe); 
	return TRUE;
}

