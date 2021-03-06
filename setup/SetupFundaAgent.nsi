
;프로그램 이름
!define PRODUCT_NAME "Funda Agent"
!define PRODUCT_VERSION "1.0"
!define PRODUCT_PUBLISHER "Funda."
!define PRODUCT_WEB_SITE "http://funda.co.kr"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\SmartUpdater.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; Check 64bit OS
!include "x64.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\orange-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\orange-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "license\license.txt"
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES

; Finish page
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "Korean"
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "SetupFundaAgent.exe"
InstallDir "$PROGRAMFILES\FundaAgent"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show


RequestExecutionLevel admin

; .닷넷 프레임워크 버전 체크
; 체크할 버전은 아래 define의 값을 설정하면 된다.
!define DOT_MAJOR "3"
!define DOT_MINOR "5"
var /GLOBAL dotNetVersion
Section "DotNetFrameWorkCheck" -Pre
        call IsDotNetInstalled
        pop $dotNetVersion
        StrCmp $dotNetVersion "NOK" 0 +5
        MessageBox MB_OK "닷넷 프레임워크 3.5 이상이 설치되어 있어야 합니다. 닷넷 프레임워크 3.5을 다운로드 합니다."
        SetOutPath "$DESKTOP"
        SetOverwrite on
        File "dotNetFx35setup.exe"
        ExecWait '$DESKTOP\dotNetFx35setup.exe'
        ;NSISdl::download /NOIEPROXY http://wepass.net/net2.0.exe $DESKTOP\net2.0.exe
        ;ExecWait '$DESKTOP\net2.0.exe'
        ;Abort
        return
SectionEnd

Section "MainSection" SEC01
  SetOutPath "$SYSDIR"
  SetOverwrite on
  File "SPS-Filter\filter.dll"
  SetOutPath "$INSTDIR"
  SetOverwrite on
  File "dll\ThoughtWorks.QRCode.dll"
  File "dll\Newtonsoft.Json.dll"
  File "dll\MonitoringAPIs.dll"
  File "dll\Microsoft.PointOfService.dll"
  File "dll\System.Data.SQLite.dll"
  File "dll\SQLite.Interop.dll"
  File "dll\SQLite.Designer.dll"
  File "dll\Newtonsoft.Json.xml"
  File "dll\System.Data.SQLite.dll"

  File "dll\jabber-net.dll"

;  File "ico\WepassSetupManagerIcon.ico"
  File "ico\wepass72.ico"
  File "ico\config.ico"
  
  File "Img\_msg_clerk01.jpg"
  File "Img\_msg_guest01.jpg"
  File "Img\bg_wepass.png"
  
  File "exe\TextPrint2COM.exe" 
  File "exe\3-1.TXT"
  File "exe\SmartUpdater.exe"
  File "exe\updateinfo.xml"
  File "exe\FundaAgent.exe"
  CreateShortCut "$DESKTOP\FundaAgent.lnk" "$INSTDIR\SmartUpdater.exe"
  CreateShortCut "$SMPROGRAMS\FundaAgent\FundaAgent.lnk" "$INSTDIR\SmartUpdater.exe"
;  File "lnk\WepassRemote.url"
;  CreateShortCut "$DESKTOP\스마트콘원격지원.lnk" "$INSTDIR\WepassRemote.url"
;  CreateShortCut "$SMPROGRAMS\스마트콘\스마트콘원격지원.lnk" "$INSTDIR\WepassRemote.url"
;  CreateShortCut "$SMPROGRAMS\스마트콘\SPS설치/삭제.lnk" "$INSTDIR\SPSInstall.msi"
;  CreateShortCut "$SMPROGRAMS\스마트콘\스마트콘 폴더.lnk" "$INSTDIR"
  CreateShortCut "$SMSTARTUP\FundaAgent.lnk" "$INSTDIR\SmartUpdater.exe"

  File "exe\SmartUpdater.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Run" "SmartUpdater" "$INSTDIR\SmartUpdater.exe"

  SetOverwrite off
  File "ini\config.ini"

  ; Bus Hound Driver
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\WPSettingManager.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name)는(은) 완전히 제거되었습니다."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "$(^Name)을(를) 제거하시겠습니까?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\SPSInstall.msi"
  Delete "$INSTDIR\config.ico"
  Delete "$INSTDIR\wepass72.ico" 
  Delete "$INSTDIR\SmartUpdater.exe" 
  Delete "$INSTDIR\FundaAgent.exe" 
  Delete "$INSTDIR\Microsoft.PointOfService.dll"
  Delete "$INSTDIR\MonitoringAPIs.dll"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\ThoughtWorks.QRCode.dll"
  Delete "$INSTDIR\jabber-net.dll"
  Delete "$INSTDIR\SPScore-Intel.msm"
  Delete "$INSTDIR\WepassRemote.url"

  
  Delete "$INSTDIR\System.Data.SQLite.dll"
  Delete "$INSTDIR\SQLite.Interop.dll"
  Delete "$INSTDIR\SQLite.Designer.dll"
  Delete "$INSTDIR\Newtonsoft.Json.xml"
  
  
  Delete "$INSTDIR\_msg_clerk01.jpg"
  Delete "$INSTDIR\_msg_guest01.jpg"
  Delete "$INSTDIR\bg_wepass.png"

  Delete "$DESKTOP\위패스.lnk"
  Delete "$DESKTOP\위패스원격지원.lnk"
  Delete "$SMPROGRAMS\위패스\위패스.lnk"
  Delete "$SMPROGRAMS\위패스\위패스등록.lnk"
;  Delete "$SMPROGRAMS\위패스\위패스설정.lnk"
;  Delete "$SMSTARTUP\위패스업데이트.lnk"
  Delete "$SMPROGRAMS\위패스\위패스원격지원.lnk"
  Delete "$SMPROGRAMS\위패스\SPS설치/삭제.lnk"
  Delete "$SMPROGRAMS\위패스\위패스 폴더.lnk"
  DeleteRegValue HKLM "Software\Microsoft\Windows\CurrentVersion\Run" "SmartUpdater"

  ; Bus Hound Driver
  /*Delete "$INSTDIR\BH\bhound7.inf"
  Delete "$INSTDIR\BH\bhound7.cat"
  Delete "$INSTDIR\BH\x64\bhound7.sys"
  Delete "$INSTDIR\BH\x86\bhound7.sys"*/



  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd



Function IsDotNetInstalled

  StrCpy $0 "0"
  StrCpy $1 "SOFTWARE\Microsoft\.NETFramework" ;registry entry to look in.
  StrCpy $2 0

  StartEnum:
    ;Enumerate the versions installed.
    EnumRegKey $3 HKLM "$1\policy" $2

    ;If we don't find any versions installed, it's not here.
    StrCmp $3 "" noDotNet notEmpty

    ;We found something.
    notEmpty:
      ;Find out if the RegKey starts with 'v'.
      ;If it doesn't, goto the next key.
      StrCpy $4 $3 1 0
      StrCmp $4 "v" +1 goNext
      StrCpy $4 $3 1 1

      ;It starts with 'v'.  Now check to see how the installed major version
      ;relates to our required major version.
      ;If it's equal check the minor version, if it's greater,
      ;we found a good RegKey.
      IntCmp $4 ${DOT_MAJOR} +1 goNext yesDotNetReg
      ;Check the minor version.  If it's equal or greater to our requested
      ;version then we're good.
      StrCpy $4 $3 1 3
      IntCmp $4 ${DOT_MINOR} yesDotNetReg goNext yesDotNetReg

    goNext:
      ;Go to the next RegKey.
      IntOp $2 $2 + 1
      goto StartEnum

  yesDotNetReg:
    ;Now that we've found a good RegKey, let's make sure it's actually
    ;installed by getting the install path and checking to see if the
    ;mscorlib.dll exists.
    EnumRegValue $2 HKLM "$1\policy\$3" 0
    ;$2 should equal whatever comes after the major and minor versions
    ;(ie, v1.1.4322)
    StrCmp $2 "" noDotNet
    ReadRegStr $4 HKLM $1 "InstallRoot"
    ;Hopefully the install root isn't empty.
    StrCmp $4 "" noDotNet
    ;build the actuall directory path to mscorlib.dll.
    StrCpy $4 "$4$3.$2\mscorlib.dll"
    IfFileExists $4 yesDotNet noDotNet

  noDotNet:
    ;Nope, something went wrong along the way.  Looks like the proper .NETFramework isn't installed.
    Push "NOK"
    Return

  yesDotNet:
    ;Everything checks out.  Go on with the rest of the installation.
    Push "OK"
    Return

FunctionEnd