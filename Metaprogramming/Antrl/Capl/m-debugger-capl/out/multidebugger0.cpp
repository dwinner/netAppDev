#define CAPL_EVENT_IMPL
#ifndef WINVER
#define WINVER 0x0501 // WindowsXP ist Vorraussetzung! Notwendig für GetModuleHandleEx.
#endif

#ifndef _WIN32_WINNT
#define _WIN32_WINNT 0x0501 // WindowsXP ist Vorraussetzung! Notwendig für GetModuleHandleEx.
#endif

#define CAPL_PLUGIN_WRAPPER

#define _WINERROR_
#define NORASTEROPS

typedef short INT16;
typedef unsigned short UINT16;

#include <string>
#include <vector>
#include <string.h>
#include <math.h>
#include <Windows.h>

#undef NO_ERROR
#undef ERROR
#undef AF_INET6
#undef AF_INET
#undef INVALID_SOCKET
#undef TRUE
#undef FALSE

//####################################
//PluginApiVerbatimInclude
//####################################
#ifndef CAPLPLUGINAPI_H__INCLUDED
#define CAPLPLUGINAPI_H__INCLUDED

#include <vector>
#ifndef CAPL_DEFINES_H
#define CAPL_DEFINES_H

#ifndef QWORD_DEFINED
#define QWORD_DEFINED
typedef unsigned long long QWORD;
#endif

#ifndef CAPL_EXTERN_C
    #define CAPL_EXTERN_C extern "C" 
#endif

#define CAPL_CALL __stdcall
#ifndef USE_COM
    #ifdef CAPL_BUILD
        # define CAPL_EXPORT __declspec(dllexport)
    #else
        # define CAPL_EXPORT __declspec(dllimport)
    #endif
#else
    #define CAPL_EXPORT
#endif

#define CAPL_FUNC(ret, func) CAPL_EXTERN_C CAPL_EXPORT ret CAPL_CALL func
#define CAPL_METHOD(ret, func) virtual ret CAPL_CALL func
#define CAPL_METHOD_IMPL(ret, func) ret CAPL_CALL func
#define CAPL_HANDLE void*

#ifndef CAPL_PLUGIN_WRAPPER
   #include <stdexcept>

   namespace capl
   {
      // Bitte keine Inlinefunktion daraus machen! Andernfalls würden etwaige
      // __FILE__-Makros nicht mehr funktionieren.
      #define CAPL_THROW(classname, resourceId)\
      {\
         CString sTmp;\
         VERIFY(sTmp.LoadString(resourceId));\
         throw classname((LPCSTR)sTmp);\
      }

      // Bitte keine Inlinefunktion daraus machen! Andernfalls würden etwaige
      // __FILE__-Makros nicht mehr funktionieren.
      #define CAPL_THROW_P(classname, resourceId, __VA_ARGS__)\
      {\
         CString sTmp;\
         sTmp.Format(resourceId, __VA_ARGS__);\
         throw classname((LPCSTR)sTmp);\
      }


      // Erzeugt eine neue Exceptionklasse
      #define CREATE_EXCEPTION_CLASS(classname, base)\
         class classname : public base\
         {\
         public:\
            classname(const std::string& sErr) : base(sErr) {}\
            virtual ~classname() {}\
         }

      // Normale CAPL-Exception. Simulation muss bei dieser Exception nicht gestoppt werden.
      CREATE_EXCEPTION_CLASS(StdException, std::runtime_error);
      // Kritische CAPL-Exception. Simulation wird bei dieser Exception angehalten.
      CREATE_EXCEPTION_CLASS(CriticalException, std::runtime_error);


      typedef CriticalException EndlessLoopException;
   }
#endif //!CAPL_PLUGIN_WRAPPER 

#define CAPL_INT8 __int8
#define CAPL_UINT8 unsigned __int8
#define CAPL_INT16 __int16
#define CAPL_UINT16 unsigned __int16
#ifndef CAPL_INT32
#define CAPL_INT32 __int32
#endif
#define CAPL_UINT32 unsigned CAPL_INT32
#define CAPL_INT64 __int64
#define CAPL_UINT64 unsigned __int64
#define CAPL_DOUBLE double

#endif // CAPL_DEFINES_H
#include <string>
#include <windows.h>
//#ifdef USE_COM
//    #include "CAPLRT.COM.h"
//#endif

#ifndef USE_IREFERENCE
	#define REF(type) type&
#else
	//
	// CAPL over COM requires using IProperty<T> instead of T&
	//
	#define REF(type) IReference<type>&
#endif

namespace capl
{
    // Vorwärtsdeklarationen.
    class IAttribute;
    class IMessage;
    class ISignal;
    class ISignalVal;
    class ITimer;
    class IEnvVar;
    class ICanError;
    class IStart;
    class IStop;
    class IPreStart;
    class IPreStop;
    class IKey;
    class IErrorFrame;
    class IDiag;
    class IValueTable;
    class MessageEvent;
    class SignalEvent;
    class SignalUpdateEvent;
    class TimerEvent;
    class EnvVar;
    class IEnvVarEvent;
    class PreStartEvent;
    class StartEvent;
    class PreStopEvent;
    class StopEvent;
    class KeyEvent;
    class BusEvent;
    class ErrorFrameEvent;
    class IEthernetPacket;
    class EthernetPacketEvent;
    class EthernetStatusEvent;
    class DiagEvent;
    class IDiagResponse;
    class IDiagRequest;
    class DiagResponseEvent;
    class DiagRequestEvent;
    class ILinWakeupFrame;
    class LinWakeupFrameEvent;

    // Dient zur Optimierung von Message-GetSignal zugriffen.
    const CAPL_INT32 MAX_SIGNAL_NAME_LENGTH = 128;
    const CAPL_INT32 STRLEN_FOR_SIGNAL_NAME = MAX_SIGNAL_NAME_LENGTH + 1;

#define tSignalNameArr(varName) const char* varName

    inline LPCSTR GetSignalName(CAPL_UINT32 iIndex, tSignalNameArr(pSignalNameArr))
    {
        return &pSignalNameArr[iIndex * STRLEN_FOR_SIGNAL_NAME];
    }

    /**\brief Definiert den Status des Plugins.
     *
     * Wird in der API-Callack-Funktion tStatusFunc verwendet.
     */
    enum PluginStatus { Plugin_Running, Plugin_RunningNoEvents, Plugin_Stopped, Plugin_Error };

    /**\brief Definiert den Typ der Status-Funktion für das Plugin.
     *
     * Eine Funtkion diesen Typs wird beim Init-Aufruf des Plugins übergeben.
     * Wenn das Plugin seinen Status ändert, muss es die übergebene Funktion
     * mit dem neuen Status aufrufen.
     */
    typedef void (CAPL_CALL *tStatusFunc)(PluginStatus enStatus, HINSTANCE hDll);

    /**\brief Definiert den Typ der API-Init-Funktion des Plugins.
     *
     * Eine Funktion diesen Typs mit dem Name CAPL_boInit muss vom
     * Plugin exportiert werden.
     *
     * CAPL_boInit wird aufgerufen, wenn das Plugin vom CAPL-Loader
     * geladen wird (die CAPL-Plugin-Dll wurde zu diesem Zeitpunkt
     * bereits geladen.).
     *
     * Diese Funktion sollte true zurück geben, wenn die Initialisierung
     * erfolgreich war und somit der Ladeprozess erfolgreich abgeschlossen
     * werden kann.
     */
    typedef bool (CAPL_CALL *tInitFunc)(tStatusFunc, HWND);

    /**\brief Definiert den Typ der API-Init-Dependencies-Funktion des Plugins.
     *
     * Eine Funktion diesen Typs mit dem Name CAPL_boInitDependencies muss vom
     * Plugin exportiert werden.
     *
     * CAPL_boInitDependencies wird aufgerufen, wenn das Plugin vom CAPL-Loader
     * geladen wurde und alle gespeicherten Daten geladen wurden (CAPL_boInit
     * und CAPL_iLoad wurden bereits aufgerufen).
     *
     * Diese Funktion sollte true zurück geben, wenn die Initialisierung alle
     * abhängigen DLLs (z.B. eine CAPLDLL für BAP) erfolgreich war und somit
     * der Ladeprozess erfolgreich abgeschlossen werden kann.
     */
    typedef bool (CAPL_CALL *tInitDependenciesFunc)();

    /**\brief Definiert den Typ der API-Deinit-Funktion des Plugins.
     *
     * Eine Funktion diesen Typs mit dem Name CAPL_vDeinit muss vom
     * Plugin exportiert werden.
     *
     * CAPL_boDeinit wird aufgerufen, wenn das Plugin vom CAPL-Loader
     * entladen wird (die CAPL-Plugin-Dll wurde zu diesem Zeitpunkt
     * noch nicht entladen.).
     */
    typedef void (CAPL_CALL *tDeinitFunc)(void);


    /**\brief Definiert den Typ der API-GetDlls-Funktion des Plugins.
     *
     * Eine Funktion diesen Typs mit dem Name CAPL_boGetDll muss vom
     * Plugin exportiert werden.
     *
     * CAPL_boGetDll wird aufgerufen, wenn der CAPL-Loader Informationen
     * über die abhängigen CAPL-DLLs benötigt.
     *
     * Der erste Parameter definiert die Identifizierungsnummer der
     * abhängigen DLL. Der zweite Parameter ist ein Zeiger auf ein
     * Character-Buffer der die Größe MAX_PATH+1 besitzt.
     *
     * Diese Funktion sollte true zurück liefern, wenn diese
     * Identifizierungsnummer eine gültige Dll adressiert. Ansonsten
     * soll false zurück geliefert werden.
     * Die Identifizierungsnummer müsssen von 0 an direkt aufeinanderfolgend
     * vergeben werden!
     */
    typedef bool (CAPL_CALL *tGetDll)(int, char*);

    /**\brief Definiert den Typ der API-GetPluginName-Funktion des Plugins.
     *
     * Eine Funktion diesen Typs mit dem Namen CAPL_sGetPluginName kann vom
     * Plugin exportiert werden.
     *
     * CAPL_sGetPluginName wird aufgerufen, wenn der CAPL-Loader den PluginNamen
     * benötigt. Dieser wird bei der Erstellung des Plugins im CAPL-Converter angegeben
     * und mit in das Plugin kompiliert.
     * Der PluginName wird zur anzeige im Baum benutzt, sofern einer gesetzt ist.
     */
    typedef char* (CAPL_CALL *tGetPluginName)();

    /**\brief Definiert den Typ der API-GetVersionNumber-Funktion des Plugins.
     *
     * Eine Funktion diesen Typs mit dem Namen CAPL_sGetVersionNumber kann vom
     * Plugin exportiert werden.
     *
     * CAPL_sGetVersionNumber wird aufgerufen, wenn der CAPL-Loader die Versionsnummer
     * benötigt. Diese wird bei der Erstellung des Plugins im CAPL-Converter angegeben
     * und mit in das Plugin kompiliert.
     */
    typedef char* (CAPL_CALL *tGetVersionNumber)();

    /**\brief Definiert den Typ der API-GetDescription-Funktion des Plugins.
     *
     * Eine Funktion diesen Typs mit dem Namen CAPL_sGetDescription kann vom
     * Plugin exportiert werden.
     *
     * CAPL_sGetDescription wird aufgerufen, wenn der CAPL-Loader die Beschreibung
     * benötigt. Diese wird bei der Erstellung des Plugins im CAPL-Converter angegeben
     * und mit in das Plugin kompiliert.
     */
    typedef char* (CAPL_CALL *tGetDescription)();

    /**\brief Definiert den Typ der API-GetConfigFile-Funktion des Plugins.
     *
     * Eine Funktion diesen Typs mit dem Namen CAPL_vGetConfigFile kann vom
     * Plugin exportiert werden.
     *
     * CAPL_vGetConfigFile wird aufgerufen, wenn der CAPL-Loader die relativen Pfade
     * aktualisieren muss.
     */
    typedef void (CAPL_CALL *tGetConfigFile)(char *);

    /**\brief Definiert den Typ der API-GetConfigFile-Funktion des Plugins.
    *
    * Eine Funktion diesen Typs mit dem Namen CAPL_vSetConfigFile kann vom
    * Plugin exportiert werden.
    *
    * CAPL_vSetConfigFile wird aufgerufen, wenn der CAPL-Loader die relativen Pfade
    * aktualisieren muss.
    */
    typedef void (CAPL_CALL *tSetConfigFile)(const char *);

    /**\brief Definiert den Typ der MainTest-Funktion des Plugins.
    *
    * Eine Funktion diesen Typs mit dem Name MainTest wird vom Plugin exportiert, wenn es ein Test Modul ist.
    *
    * Diese Funktion kann von extern aufgerufen werden um die Tests durchzuführen.
    */
    typedef void (CAPL_CALL *tMainTest)();

    //  ______                _   _                 
    // |  ____|              | | (_)                
    // | |__ _   _ _ __   ___| |_ _  ___  _ __  ___ 
    // |  __| | | | '_ \ / __| __| |/ _ \| '_ \/ __|
    // | |  | |_| | | | | (__| |_| | (_) | | | \__ \
    // |_|   \__,_|_| |_|\___|\__|_|\___/|_| |_|___/
    //

    /**\breif Definiert die Stringresourcen die in der CAPL-RT definiert sind und über
     *    die Funktion capl::GetStringResource(CAPLPLUGIN_StringResources) von den
     *    Capl-Plugin-Generaten verwendet werden können.
     *
     * \remark Die Enumwerte sind wie die Stringresourcen benannt, wobei diese noch das
     *    "IDS_" am Anfang stehen haben.
     */
    enum CAPLPLUGIN_StringResource
    {
        CAPLPLUGIN_WARNING_OLD_DATA_TO_LOAD = 0,
        CAPLPLUGIN_WARNING_NEW_DATA_TO_LOAD,
        CAPLPLUGIN_ERROR_LOADING_FAILED,
        CAPLPLUGIN_ERROR_SAVING_FAILED
    };

    CAPL_FUNC(const char*, GetStringResource)(CAPLPLUGIN_StringResource stringResource);

    /**\brief Definiert die Rückgabewerte der CAPL-Runtime-Funktionen,
     *    die mit der CanEasy-API kommunizieren.
     */
    enum CAPLRT_ReturnValue
    {
        CAPLRT_OK,              // Aufruf wurde erfolgreich durchgeführt.
        CAPLRT_InvalideHandle,  // Es wurde ein ungültiges Handle übergeben.
        CAPLRT_InvalidParam,    // Es wurde ein ungültiger Parameter übergeben.
        CAPLRT_UnknownError     // Es ist ein unbekannter Fehler aufgetreten.
    };

    // Liefert den Pfad zum aktuellen Arbeitsbreich.
    CAPL_FUNC(void, GetWorkspace)(char* sWorkspace);
    const DWORD NotExist = 0xFFFFFFFF;

    struct FileInfo
    {
        char* file;       // Dateiname mit Endung
        char* path;       // nur die Verzeichnisse
        char* fileName;   // nur der Dateiname
        char* ext;        // nur die Dateiendung
        DWORD attr;       // File-Attributes (see GetFileAttributes)
    };

    // Der Aufrufer der Funktion muss den Speicher der zurückgegebenen File-Info-Struktur
    // freigeben (delete Member). Dafür kann der Benuzter die Funktion FreeFileInfo
    // benutzen.
    CAPL_FUNC(FileInfo, GetInfo)(const char* file);
    CAPL_FUNC(void, FreeFileInfo)(FileInfo& fileInfo);

    CAPL_FUNC(BOOL, SetCurrentDirectoryByFile)(const char* pcFile);
    CAPL_FUNC(BOOL, SaveWorkingDirectory)(char pcWorkingDirectory[MAX_PATH]);
    CAPL_FUNC(BOOL, RestoreWorkingDirectory)(const char *pcWokringDirectory);

    // Ermöglicht das Speichern in das übergebene Filemodul.
    // dwFileModule: wird beim Aufruf von CAPL_iSave übergeben.
    // pData: Zeiger auf die Daten, die im binärformat gespeichert werden sollen.
    // dwBytesToWrite: Anzahl der Bytes, die von pData aus gespeichert werden sollen.
    CAPL_FUNC(CAPLRT_ReturnValue, WriteModuleData)(DWORD dwFileModule, const void* pData,
        DWORD dwBytesToWrite);
    // Ermöglicht das Lesen aus dem übergebenen Filemodul.
    // dwFileModule: wird beim Aufruf von CAPL_iLoad übergeben.
    // pData: Zeiger auf einen Puffer, in welchen die im binärformat ausgelesen Daten geschrieben werden sollen.
    //        Der Puffer muss mindestens so groß sein, wie durch dwBytesToRead definiert.
    // dwBytesToRead: Anzahl der Bytes, die gelesen werden sollen.
    CAPL_FUNC(CAPLRT_ReturnValue, ReadModuleData)(DWORD dwFileModule, void* pData,
        DWORD dwBytesToRead);

    // Bestimmt aus der absoluten Pfadangabe und der gespeicherten Pfadangabe
    // des Arbeitsbereichs eine neue absolute relativ zum neuen Arbeitsbereich.
    CAPL_FUNC(void, GetPathRelativeToWorkspace)(char* sPath);

    enum ReportStyle { Report_Info, Report_Warning, Report_Error };
    CAPL_FUNC(void, Report)(const ReportStyle& reportStyle, const char* sWarning);

    // Setzt die Callback-Funktion für Status-Änderungen.
    CAPL_FUNC(void, SetStatusFunc)(tStatusFunc pFunc, HINSTANCE hDll);

    // Erzeugt/Löscht Message-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateMessageEvent)(MessageEvent* pEvent, DWORD u32FileIndex, const char* sFilter);
    CAPL_FUNC(void, DeleteMessageEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht Signal-Event.
    CAPL_FUNC(CAPL_HANDLE, CreateSignalEvent)(SignalEvent* pEvent, const char* sIds);
    CAPL_FUNC(CAPL_HANDLE, CreateSignalEventBySigVal)(SignalEvent* pEvent, ISignalVal& sIds);
    CAPL_FUNC(void, DeleteSignalEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht Signal-Event.
    CAPL_FUNC(CAPL_HANDLE, CreateSignalUpdateEvent)(SignalUpdateEvent* pEvent, const char* sIds);
    CAPL_FUNC(CAPL_HANDLE, CreateSignalUpdateEventBySigVal)(SignalUpdateEvent* pEvent, ISignalVal& sIds);
    CAPL_FUNC(void, DeleteSignalUpdateEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht Timer-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateTimerEvent)(TimerEvent* pEvent, ITimer* pTimer);
    CAPL_FUNC(void, DeleteTimerEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht Umgebungsvariablen-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateEnvVarEvent)(IEnvVarEvent* pEvent, const char* sId);
    CAPL_FUNC(void, DeleteEnvVarEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht Umgebungsvariablen-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateEnvVarUpdateEvent)(IEnvVarEvent* pEvent, const char* sId);
    CAPL_FUNC(void, DeleteEnvVarUpdateEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht PreStart-Events.
    CAPL_FUNC(CAPL_HANDLE, CreatePreStartEvent)(PreStartEvent* pEvent);
    CAPL_FUNC(void, DeletePreStartEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht Start-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateStartEvent)(StartEvent* pEvent);
    CAPL_FUNC(void, DeleteStartEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht PreStop-Events.
    CAPL_FUNC(CAPL_HANDLE, CreatePreStopEvent)(PreStopEvent* pEvent);
    CAPL_FUNC(void, DeletePreStopEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht Stop-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateStopEvent)(StopEvent* pEvent);
    CAPL_FUNC(void, DeleteStopEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht Key-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateCaplKeyEvent)(KeyEvent* pEvent, CAPL_INT32 caplKey, DWORD u32FileIndex);
    CAPL_FUNC(void, DeleteKeyEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht ErrorPassive-Events.
    enum BusEventType { BusOff = 1, Passive, Active, WarningLimit };
    CAPL_FUNC(CAPL_HANDLE, CreateBusEvent)(BusEvent* pEvent, BusEventType enState);
    CAPL_FUNC(void, DeleteBusEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht ErrorFrame-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateErrorFrameEvent)(ErrorFrameEvent* pEvent);
    CAPL_FUNC(void, DeleteErrorFrameEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht LinWakeupFrame-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateLinWakeupFrameEvent)(LinWakeupFrameEvent* pEvent);
    CAPL_FUNC(void, DeleteLinWakeupFrameEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht EthernetPacket-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateEthernetPacketEvent)(EthernetPacketEvent* pEvent, BYTE u8Channel);
    CAPL_FUNC(void, DeleteEthernetPacketEvent)(CAPL_HANDLE handle);
    CAPL_FUNC(CAPL_HANDLE, CreateEthernetStatusEvent)(EthernetStatusEvent* pEvent, BYTE u8Channel);
    CAPL_FUNC(void, DeleteEthernetStatusEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht Diag-Events.
    CAPL_FUNC(CAPL_HANDLE, CreateDiagEvent)(DiagEvent* pEvent);
    CAPL_FUNC(void, DeleteDiagEvent)(CAPL_HANDLE handle);
    CAPL_FUNC(CAPL_HANDLE, CreateDiagResponseEvent)(DiagResponseEvent* pEvent, const char* sId, const char* sStack);
    CAPL_FUNC(void, DeleteDiagResponseEvent)(CAPL_HANDLE handle);
    CAPL_FUNC(CAPL_HANDLE, CreateDiagRequestEvent)(DiagRequestEvent* pEvent, const char* sId, const char* sStack);
    CAPL_FUNC(void, DeleteDiagRequestEvent)(CAPL_HANDLE handle);

    // Erzeugt/Löscht OSEKTL-Events. <- Obsolete: For backward compatibility
    typedef void(_cdecl AIDA_DataFunction)(CAPL_INT32);
    CAPL_FUNC(CAPL_HANDLE, Create_AIDA_DataIndEvent)(AIDA_DataFunction* pFunc, CAPL_HANDLE hStack);
    CAPL_FUNC(void, Delete_AIDA_DataIndEvent)(CAPL_HANDLE hEvent);
    CAPL_FUNC(CAPL_HANDLE, Create_AIDA_DataConEvent)(AIDA_DataFunction* pFunc, CAPL_HANDLE hStack);
    CAPL_FUNC(void, Delete_AIDA_DataConEvent)(CAPL_HANDLE hEvent);

    // Erzeugt/Löscht CanTp-Events.   
    typedef void(_cdecl Transmission_Function)(CAPL_INT32, CAPL_UINT8*, CAPL_UINT32);
    CAPL_FUNC(CAPL_HANDLE, Create_ReceptionInd_Event)(Transmission_Function* pFunc, CAPL_HANDLE hStack);
    CAPL_FUNC(void, Delete_ReceptionInd_Event)(CAPL_HANDLE hEvent);
    CAPL_FUNC(CAPL_HANDLE, Create_SendCon_Event)(Transmission_Function* pFunc, CAPL_HANDLE hStack);
    CAPL_FUNC(void, Delete_SendCon_Event)(CAPL_HANDLE hEvent);

    // Erzeugt/Löscht Timer.
    CAPL_FUNC(ITimer*, CreateTimer)(bool boSeconds);
    CAPL_FUNC(void, DeleteTimer)(ITimer* pTimer);

    // Erzeugt/Löscht Botschaften.
    CAPL_FUNC(IMessage*, CreateMessage)(const char* sId);
    CAPL_FUNC(IMessage*, CreateMessageById)(DWORD dwMessageId, BYTE u8Channel, BYTE u8Type);//Type 1 is CAn, 2 is LIN
    CAPL_FUNC(void, DeleteMessage)(IMessage* pMsg);

    // Erzeugt/Löscht Umgebungsvariablen.
    CAPL_FUNC(IEnvVar*, CreateEnvVar)(const char* sId);
    CAPL_FUNC(IEnvVar*, CreateEnvVarByName)(const char* sName);

    CAPL_FUNC(void, DeleteEnvVar)(IEnvVar* pVar);

    // Erzeugt/Löscht CAPL-Threads.
	CAPL_FUNC(CAPL_HANDLE, CreateThread)();

    CAPL_FUNC(void, DeleteThread)(CAPL_HANDLE hThread);
    CAPL_FUNC(void, ResetCurrentThread)();

    // Erzeugt/Löscht Signalwerte.
    CAPL_FUNC(ISignalVal*, CreateSignalVal)(const char* sId);
    CAPL_FUNC(ISignalVal*, CreateTxRqSignalVal)(const char* sId);
    CAPL_FUNC(void, DeleteSignalVal)(ISignalVal* pVal);

    // Erzeugt/Löscht Signale.
    CAPL_FUNC(ISignal*, CreateSignal)(const char* sId);
    CAPL_FUNC(ISignal*, CreateTxRqSignal)(const char* sId);
    CAPL_FUNC(void, DeleteSignal)(ISignal* pVal);

    // Ermittelt/Löscht ein Attribut aus der Datenbasis und liefert das Interface darauf zurück.
    CAPL_FUNC(IAttribute*, GetAttribute)(const char* sId);
    CAPL_FUNC(void, DeleteAttribute)(IAttribute* pAttribute);

    CAPL_FUNC(void, SetTestFunction)(const char *sCAPLFunctionName);
    CAPL_FUNC(void, RemoveTestFunction)(const char *sCAPLFunctionName);
    CAPL_FUNC(void, SetExtendedErrorInfo)(CAPL_HANDLE pThread, bool boExtendedErrorInfo);

    // Erzeugt/Löscht EthernetPacket.
    CAPL_FUNC(IEthernetPacket*, CreateEthernetPacket)();
    CAPL_FUNC(void, DeleteEthernetPacket)(IEthernetPacket* pVal);

    // Erzeugt/Löscht Botschaften.
    CAPL_FUNC(IDiagResponse*, CreateDiagResponse)(const char* sId, const char* sStack);
    CAPL_FUNC(void, DeleteDiagResponse)(IDiagResponse* pMsg);
    CAPL_FUNC(IDiagRequest*, CreateDiagRequest)(const char* sId, const char* sStack);
    CAPL_FUNC(void, DeleteDiagRequest)(IDiagRequest* pMsg);

    // Erzeugt/Löscht LinWakeupFrame.
    CAPL_FUNC(ILinWakeupFrame*, CreateLinWakeupFrame)();
    CAPL_FUNC(void, DeleteLinWakeupFrame)(ILinWakeupFrame* pVal);

    //  _____       _             __                   
    // |_   _|     | |           / _|                  
    //   | |  _ __ | |_ ___ _ __| |_ __ _  ___ ___ ___ 
    //   | | | '_ \| __/ _ \ '__|  _/ _` |/ __/ _ \ __|
    //  _| |_| | | | |_  __/ |  | || (_| | (__  __\__ \
    // |_____|_| |_|\__\___|_|  |_| \__,_|\___\___|___/
    // 

    // Typen von Attributen
    enum AttributeTypes
    {
        TypeEmpty,
        TypeChar, TypeCharVector,
        TypeUChar, TypeUCharVector,
        TypeInt, TypeIntVector,
        TypeUInt, TypeUIntVector,
        TypeInt64, TypeInt64Vector,
        TypeUInt64, TypeUInt64Vector,
        TypeDouble, TypeDoubleVector,
        TypeString, TypeStringVector,
        TypePointer, TypePointerVector
    };

    // Interface für Attribute.
    class IAttribute
    {
    public:
        CAPL_METHOD(bool, ReleaseMe)() const = 0;
        CAPL_METHOD(AttributeTypes, GetType)() const = 0;

        CAPL_METHOD(int, GetInt)() const = 0;
        CAPL_METHOD(double, GetDouble)() const = 0;
        CAPL_METHOD(CAPL_INT32, GetLong)() const = 0;
        CAPL_METHOD(const char*, GetEnum)() const = 0;
        CAPL_METHOD(const char*, GetString)() const = 0;

        CAPL_METHOD(int, SetInt)(const int&) = 0;
        CAPL_METHOD(double, SetDouble)(const double&) = 0;
        CAPL_METHOD(CAPL_INT32, SetLong)(const CAPL_INT32&) = 0;
        CAPL_METHOD(const char*, SetEnum)(const char*&) = 0;
        CAPL_METHOD(const char*, SetString)(const char*&) = 0;
    };

#define ATTRIBUTE_HOLDER\
   CAPL_METHOD(IAttribute*, GetAttribute)(const char* sAttrName) = 0;

    // Interface für Properties.
    template<class T>
    class IProperty
    {
    public:
        virtual CAPL_CALL operator T() const = 0;
        CAPL_METHOD(T, operator=)(T value) = 0;
    };
	
#ifdef USE_IREFERENCE
	template<class T> 
	class IReference {  // Attension: this name is used in CaplComApiGen\PostAnalyzer.cs
    public:
        virtual CAPL_CALL operator T() const = 0;
        CAPL_METHOD(T, operator=)(T value) = 0;
	};
#endif

    // Interface für Botschaften.
    class IMessage
    {
    public:
        enum Direction { Rx, Tx, TxRequest }; // Übertragungsrichtung.

        CAPL_METHOD(void, Send)() = 0;
        CAPL_METHOD(IMessage*, GetMessage)(const char* sName) = 0;
        CAPL_METHOD(ISignal*, GetSignal)(CAPL_UINT32 uiIndex, tSignalNameArr(pSigNameArr)) = 0;

        CAPL_METHOD(REF(DWORD), ID)() = 0;
        CAPL_METHOD(REF(BYTE), CAN)() = 0;
        CAPL_METHOD(REF(BYTE), DLC)() = 0;

        CAPL_METHOD(REF(Direction), DIR)() = 0;
        CAPL_METHOD(REF(bool), RTR)() = 0;
        CAPL_METHOD(CAPL_INT32, TYPE)() const = 0;
        CAPL_METHOD(CAPL_INT32, MsgFlags)() const = 0;
        CAPL_METHOD(REF(QWORD), TIME)() = 0;
        CAPL_METHOD(REF(QWORD), Time_NS)() = 0;
        CAPL_METHOD(REF(bool), SIMULATED)() = 0;

        CAPL_METHOD(REF(BYTE), Byte)(BYTE byteIndex) = 0;
        CAPL_METHOD(REF(WORD), Word)(BYTE byteIndex) = 0;
        CAPL_METHOD(REF(DWORD), Dword)(BYTE byteIndex) = 0;
        CAPL_METHOD(REF(QWORD), Qword)(BYTE byteIndex = 0) = 0;
        CAPL_METHOD(REF(char), Char)(BYTE byteIndex) = 0;
        CAPL_METHOD(REF(short), Short)(BYTE byteIndex) = 0;
        CAPL_METHOD(REF(CAPL_INT32), Long)(BYTE byteIndex) = 0;
        CAPL_METHOD(void, CopyData)(IMessage& msg) = 0;
        CAPL_METHOD(BYTE*, GetData)() = 0;        

        ATTRIBUTE_HOLDER;

        CAPL_METHOD(REF(bool), BRS)() = 0;
        CAPL_METHOD(REF(bool), EDL)() = 0;
        CAPL_METHOD(REF(bool), FDF)() = 0;

        CAPL_METHOD(CAPL_INT32, SetSignalStartValues)() = 0;
    };

    // Interface für LinWakeupFrame.
    class ILinWakeupFrame
    {
    public:
        CAPL_METHOD(REF(QWORD), TIME)() = 0;
        CAPL_METHOD(REF(QWORD), Time_NS)() = 0;
        CAPL_METHOD(REF(DWORD), MsgOrigTime)() = 0;
        CAPL_METHOD(REF(BYTE), MsgTimeStampStatus)() = 0;
        CAPL_METHOD(REF(WORD), MsgChannel)() = 0;
        CAPL_METHOD(REF(BYTE), lin_Signal)() = 0;
        CAPL_METHOD(REF(BYTE), lin_External)() = 0;
        CAPL_METHOD(REF(QWORD), length_ns)() = 0;
        CAPL_METHOD(REF(QWORD), SOF)() = 0;
        CAPL_METHOD(void, CopyData)(ILinWakeupFrame& msg) = 0;
    };

    // Interface für phys und raw Properties.
    class IPhysRawValue
    {
    public:
        CAPL_METHOD(IProperty<double>&, ValuePhys)() = 0;
        CAPL_METHOD(IProperty<QWORD>&, ValueHex)() = 0;
    };

    // Interface für Signale.
    class ISignal : public IPhysRawValue
    {
    public:
        CAPL_METHOD(IMessage&, GetMessage)() const = 0;
        CAPL_METHOD(QWORD, GetHexValue)() const = 0;
        CAPL_METHOD(void, SetHexValue)(QWORD val) const = 0;
        CAPL_METHOD(DWORD, GetHexValueArray)(CAPL_UINT8* data, CAPL_INT32 dataLength) const = 0;
        CAPL_METHOD(DWORD, SetHexValueArray)(CAPL_UINT8* data, CAPL_INT32 dataLength) const = 0;

        ATTRIBUTE_HOLDER;
    };

    // Interface für direkten Signal-Zugriff.
    class ISignalVal : public IPhysRawValue
    {
    public:
        CAPL_METHOD(const char*, GetName)() const = 0;
        CAPL_METHOD(const char*, GetID)() const = 0;
        CAPL_METHOD(IPhysRawValue&, rx)() = 0;
        CAPL_METHOD(IPhysRawValue&, txrq)() = 0;
    };

    // Interface für eine Wertetabelle.
    class IValueTable
    {
    public:
        // Liefert den Wert eines Eintrags der Wertetabelle.
        CAPL_METHOD(CAPL_INT32, GetValue)() const = 0;
    };

    // Interface für Timer.
    class ITimer
    {
    public:
        CAPL_METHOD(void, Set)(DWORD u32Timer, DWORD u32Nano = 0) = 0;
        CAPL_METHOD(void, SetCyclic)(CAPL_INT32 firstDuration, CAPL_INT32 period = -1) = 0;
        CAPL_METHOD(void, Kill)() = 0;
        CAPL_METHOD(bool, UpdateTimer)() = 0;
        CAPL_METHOD(DWORD, GetTime)() const = 0;
        CAPL_METHOD(BOOL, GetActive)() const = 0;
    };

    class IStart
    {
    public:
        CAPL_METHOD(DWORD, GetTime)() const = 0;
        //TODO IMPL!
    };

    class IPreStart
    {
    public:
        CAPL_METHOD(DWORD, GetTime)() const = 0;
        //TODO IMPL!
    };

    class IPreStop
    {
    public:
        CAPL_METHOD(DWORD, GetTime)() const = 0;
        //TODO IMPL!
    };

    class IStop
    {
    public:
        CAPL_METHOD(DWORD, GetTime)() const = 0;
        //TODO IMPL!
    };

    class IKey
    {
    public:
        CAPL_METHOD(WORD, Key)() const = 0;
        CAPL_METHOD(DWORD, Modifier)() const = 0;

    };

    class IErrorFrame
    {
    public:
        CAPL_METHOD(QWORD, GetTimeNS)() const = 0;
        CAPL_METHOD(DWORD, GetTime)() const = 0;
        CAPL_METHOD(BYTE, GetChannel)() const = 0;
        //TODO IMPL!
    };

    // Interface für Umgebungsvariable.
    class IEnvVar
    {
    public:
        enum Type
        {
            Empty,
            Char, CharVector,
            UChar, UCharVector,
            Int, IntVector,
            UInt, UIntVector,
            Int64, Int64Vector,
            UInt64, UInt64Vector,
            Double, DoubleVector,
            String, StringVector,
            Pointer, PointerVector,
            Short, ShortVector,
            UShort, UShortVector
        };
        CAPL_METHOD(const char*, GetName)() const = 0;
        CAPL_METHOD(void, GetInt32)(CAPL_INT32& value) const = 0;
        CAPL_METHOD(void, GetDouble)(double& value) const = 0;
        CAPL_METHOD(void, SetInt32)(CAPL_INT32 value) = 0;
        CAPL_METHOD(void, SetDouble)(double value) = 0;
        CAPL_METHOD(void, GetInt64)(CAPL_INT64& value) const = 0;
        CAPL_METHOD(void, SetInt64)(CAPL_INT64 value) = 0;
        CAPL_METHOD(void, GetUInt32)(CAPL_UINT32& value) const = 0;
        CAPL_METHOD(void, SetUInt32)(CAPL_UINT32 value) = 0;
        CAPL_METHOD(void, GetUInt64)(CAPL_UINT64& value) const = 0;
        CAPL_METHOD(void, SetUInt64)(CAPL_UINT64 value) = 0;
        CAPL_METHOD(void, GetInt8)(CAPL_INT8& value) const = 0;
        CAPL_METHOD(void, SetInt8)(CAPL_INT8 value) = 0;
        CAPL_METHOD(void, GetUInt8)(CAPL_UINT8& value) const = 0;
        CAPL_METHOD(void, SetUInt8)(CAPL_UINT8 value) = 0;
        CAPL_METHOD(void, GetInt16)(CAPL_INT16& value) const = 0;
        CAPL_METHOD(void, SetInt16)(CAPL_INT16 value) = 0;
        CAPL_METHOD(void, GetUInt16)(CAPL_UINT16& value) const = 0;
        CAPL_METHOD(void, SetUInt16)(CAPL_UINT16 value) = 0;
        CAPL_METHOD(void, GetArray8)(CAPL_INT8& value, size_t row) const = 0;
        CAPL_METHOD(void, SetArray8)(CAPL_INT8 value, size_t row) = 0;
        CAPL_METHOD(void, GetArrayU8)(CAPL_UINT8& value, size_t row) const = 0;
        CAPL_METHOD(void, SetArrayU8)(CAPL_UINT8 value, size_t row) = 0;
        CAPL_METHOD(void, GetArray16)(CAPL_INT16& value, size_t row) const = 0;
        CAPL_METHOD(void, SetArray16)(CAPL_INT16 value, size_t row) = 0;
        CAPL_METHOD(void, GetArrayU16)(CAPL_UINT16& value, size_t row) const = 0;
        CAPL_METHOD(void, SetArrayU16)(CAPL_UINT16 value, size_t row) = 0;
        CAPL_METHOD(void, GetArray32)(CAPL_INT32& value, size_t row) const = 0;
        CAPL_METHOD(void, SetArray32)(CAPL_INT32 value, size_t row) = 0;
        CAPL_METHOD(void, GetArrayU32)(CAPL_UINT32& value, size_t row) const = 0;
        CAPL_METHOD(void, SetArrayU32)(CAPL_UINT32 value, size_t row) = 0;
        CAPL_METHOD(void, GetArray64)(CAPL_INT64& value, size_t row) const = 0;
        CAPL_METHOD(void, SetArray64)(CAPL_INT64 value, size_t row) = 0;
        CAPL_METHOD(void, GetArrayU64)(CAPL_UINT64& value, size_t row) const = 0;
        CAPL_METHOD(void, SetArrayU64)(CAPL_UINT64 value, size_t row) = 0;
        CAPL_METHOD(void, GetArrayD)(double& value, size_t row) const = 0;
        CAPL_METHOD(void, SetArrayD)(double value, size_t row) = 0;
        CAPL_METHOD(Type, GetType)() const = 0;
        CAPL_METHOD(CAPL_UINT64, Time_NS)() const = 0;

        ATTRIBUTE_HOLDER;
    };

    // Base Interface für DiagResponse und DiagRequest.
    class IDiagBase
    {
    public:
        CAPL_METHOD(CAPL_INT32, Initialize)() = 0;
        CAPL_METHOD(CAPL_INT32, Initialize)(const char serviceQualifier[]) = 0;

        CAPL_METHOD(CAPL_INT32, SetParameter)(char parameterName[], double newValue) = 0;
        CAPL_METHOD(CAPL_INT32, SetParameter)(CAPL_INT32 mode, char parameterName[], double newValue) = 0;
        CAPL_METHOD(CAPL_INT32, SetParameter)(char parameterName[], char newValue[]) = 0;

        CAPL_METHOD(CAPL_INT32, GetParameter)(char parameterName[], double output[1]) const = 0;
        CAPL_METHOD(double, GetParameter)(char parameterName[]) const = 0;
        CAPL_METHOD(CAPL_INT32, GetParameter)(CAPL_INT32 mode, char parameterName[], double output[1]) const = 0;
        CAPL_METHOD(double, GetParameter)(CAPL_INT32 mode, char parameterName[]) const = 0;
        CAPL_METHOD(CAPL_INT32, GetParameter)(char parameterName[], char buffer[], CAPL_UINT32 buffersize) const = 0;

        CAPL_METHOD(CAPL_INT32, SetPrimitiveData)(CAPL_UINT8 buffer[], CAPL_UINT32 buffersize) = 0;
        CAPL_METHOD(CAPL_INT32, GetPrimitiveData)(CAPL_UINT8 buffer[], CAPL_UINT32 buffersize) const = 0;

        CAPL_METHOD(CAPL_INT32, Resize)(CAPL_UINT32 byteCount) = 0;

        CAPL_METHOD(CAPL_INT32, GetPrimitiveSize)() const = 0;

        CAPL_METHOD(void, CopyData)(IDiagBase& msg) = 0;
        CAPL_METHOD(CAPL_INT32, GetParameterRaw)(const char parameter[], BYTE buffer[], CAPL_UINT32 buffersize) const = 0;
        CAPL_METHOD(CAPL_INT32, SetParameterRaw)(const char parameter[], BYTE buffer[], CAPL_UINT32 buffersize) const = 0;
    };

    // Interface für DiagResponse.
    class IDiagResponse : public IDiagBase
    {
    public:
#ifdef USE_COM
        using IDiagBase::Initialize;
#endif
        CAPL_METHOD(CAPL_INT32, Initialize)(const char serviceQualifier[], const char primitiveQualifier[]) = 0;
        CAPL_METHOD(CAPL_INT32, SendResponse)() = 0;
        CAPL_METHOD(CAPL_INT32, IsNegativeResponse)() const = 0;
        CAPL_METHOD(CAPL_INT32, GetResponseCode)() const = 0;
        CAPL_METHOD(CAPL_INT32, GetLastResponse)() const = 0;
        CAPL_METHOD(CAPL_INT32, IsPositiveResponse)() const = 0;
    };

    // Interface für DiagRequest.
    class IDiagRequest : public IDiagBase
    {
    public:
        CAPL_METHOD(CAPL_INT32, SendRequest)() = 0;
        CAPL_METHOD(CAPL_INT32, WaitForDiagResponse)(CAPL_UINT32 timeout) const = 0;
        CAPL_METHOD(CAPL_INT32, GetLastResponseCode)() const = 0;
    };

    // Interface für Bus-Fehler.
    class ICanError
    {
    public:
        CAPL_METHOD(BYTE, GetErrorCountRx)() const = 0;
        CAPL_METHOD(BYTE, GetErrorCountTx)() const = 0;
        CAPL_METHOD(BYTE, GetChannel)() const = 0;
    };

    //Interfaces for part value access
    class IByteValue
    {
    public:
        CAPL_METHOD(BYTE, operator=)(BYTE value) = 0;
        virtual __stdcall operator BYTE() = 0;
    };
    class IWordValue
    {
    public:
        CAPL_METHOD(WORD, operator=)(WORD value) = 0;
        virtual CAPL_CALL operator WORD() = 0;
    };
    class IDWordValue
    {
    public:
        CAPL_METHOD(DWORD, operator=)(DWORD value) = 0;
        virtual CAPL_CALL operator DWORD() = 0;
    };
    class IQWordValue
    {
    public:
        CAPL_METHOD(QWORD, operator=)(QWORD value) = 0;
        virtual CAPL_CALL operator QWORD() = 0;
    };
    class ICharValue
    {
    public:
        CAPL_METHOD(CAPL_INT8, operator=)(CAPL_INT8 value) = 0;
        virtual CAPL_CALL operator CAPL_INT8() = 0;
    };
    class IShortValue
    {
    public:
        CAPL_METHOD(CAPL_INT16, operator=)(CAPL_INT16 value) = 0;
        virtual CAPL_CALL operator CAPL_INT16() = 0;
    };
    class ILongValue
    {
    public:
        CAPL_METHOD(CAPL_INT32, operator=)(CAPL_INT32 value) = 0;
        virtual CAPL_CALL operator CAPL_INT32() = 0;
    };
    class ILongLongValue
    {
    public:
        CAPL_METHOD(CAPL_INT64, operator=)(CAPL_INT64 value) = 0;
        virtual CAPL_CALL operator CAPL_INT64() = 0;
    };
    // Basis Interface für Felder im IEthernetPacket.
    class IEthernetPacketProtocolField
    {
    public:
        CAPL_METHOD(CAPL_INT32, bitLength)() = 0;
        CAPL_METHOD(CAPL_INT32, bitOffset)() = 0;
        CAPL_METHOD(CAPL_INT32, byteLength)() = 0;
        CAPL_METHOD(CAPL_INT32, byteOffset)() = 0;

        CAPL_METHOD(IByteValue&, Byte)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(IWordValue&, Word)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(IDWordValue&, Dword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(IQWordValue&, Qword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(ICharValue&, Char)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(IShortValue&, Short)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(ILongValue&, Long)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(ILongLongValue&, LongLong)(CAPL_INT32 byteIndex) = 0;

        CAPL_METHOD(DWORD, SetData)(WORD offset, const void* source, WORD length) = 0;
        CAPL_METHOD(DWORD, GetData)(WORD offset, void* source, WORD length) = 0;
        CAPL_METHOD(CAPL_INT32, IsAvailable)() = 0;

        CAPL_METHOD(CAPL_INT32, ParseAddress)(const CAPL_INT8* addressAsString) = 0;
    };
    // Interface für Byte Felder im IEthernetPacket.
    class IEthernetPacketProtocolByteField : public IEthernetPacketProtocolField
    {
    public:
        CAPL_METHOD(BYTE, operator=)(BYTE value) = 0;
        virtual CAPL_CALL operator BYTE() = 0;
    };
    // Interface für Word Felder im IEthernetPacket.
    class IEthernetPacketProtocolWordField : public IEthernetPacketProtocolField
    {
    public:
        CAPL_METHOD(WORD, operator=)(WORD value) = 0;
        virtual CAPL_CALL operator WORD() = 0;
    };
    // Interface für DWord Felder im IEthernetPacket.
    class IEthernetPacketProtocolDWordField : public IEthernetPacketProtocolField
    {
    public:
        CAPL_METHOD(DWORD, operator=)(DWORD value) = 0;
        virtual CAPL_CALL operator DWORD() = 0;
    };
    // Interface für QWord Felder im IEthernetPacket.
    class IEthernetPacketProtocolQWordField : public IEthernetPacketProtocolField
    {
    public:
        CAPL_METHOD(QWORD, operator=)(QWORD value) = 0;
        virtual CAPL_CALL operator QWORD() = 0;
    };

    // Basis Interface für Protokolle im IEthernetPacket.
    class IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(CAPL_INT32, byteLength)() = 0;
        CAPL_METHOD(CAPL_INT32, byteOffset)() = 0;
        CAPL_METHOD(CAPL_INT32, nextOffset)() = 0;

        CAPL_METHOD(REF(BYTE), Byte)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(WORD), Word)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(DWORD), Dword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(QWORD), Qword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(char), Char)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(short), Short)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(CAPL_INT32), Long)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(CAPL_INT64), LongLong)(CAPL_INT32 byteIndex) = 0;

        CAPL_METHOD(DWORD, SetDataOffset)(WORD offset, const void* source, WORD length) = 0;
        CAPL_METHOD(DWORD, GetDataOffset)(WORD offset, void* source, WORD length) = 0;
        CAPL_METHOD(DWORD, SetData)(const void* source, WORD length) = 0;
        CAPL_METHOD(DWORD, GetData)(void* source, WORD length) = 0;

        CAPL_METHOD(CAPL_INT32, Init)() = 0;
        CAPL_METHOD(CAPL_INT32, IsAvailable)() = 0;
        CAPL_METHOD(CAPL_INT32, ResizeData)(WORD newLength) = 0;
        CAPL_METHOD(BYTE*, Data)() = 0;
        CAPL_METHOD(bool, CompletePacket)() = 0;
    };
    // Interface für IPv4 in IEthernetPacket.
    class IIPv4 : public IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(IEthernetPacketProtocolByteField&, version)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, ihl)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, dscp)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, ecn)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, length)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, identification)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, flags)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, offset)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, ttl)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, protocol)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, checksum)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, source)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, destination)() = 0;
    };
    // Interface für IPv6 in IEthernetPacket.
    class IIPv6 : public IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(IEthernetPacketProtocolByteField&, version)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, Class)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, flow)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, length)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, next)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, hopLimit)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, source)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, destination)() = 0;
    };
    // Interface für ICMIPv6 in IEthernetPacket.
    class IICMPv6 : public IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(IEthernetPacketProtocolByteField&, type)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, code)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, checksum)() = 0;
    };

    // Interface für RouterSolicitation NDP in IEthernetPacket.
    class INDPRouterSolicitation : public IEthernetPacketProtocol
    {
    public:
    };
    // Interface für RouterAdvertisement NDP in IEthernetPacket.
    class INDPRouterAdvertisement : public IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(IEthernetPacketProtocolByteField&, curHopLimit)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, managedAddrConfig)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, otherConfig)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, mobileHomeAgent)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, defaultRouterPrefs)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, neighborDiscoveryProxy)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, routerLifeTime)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, reachableTime)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, retransTime)() = 0;
    };
    // Interface für NeighborSolicitation NDP in IEthernetPacket.
    class INDPNeighborSolicitation : public IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, target)() = 0;
    };
    // Interface für NeighborAdvertisement NDP in IEthernetPacket.
    class INDPNeighborAdvertisement : public IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(IEthernetPacketProtocolByteField&, routerFlag)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, solicitedFlag)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, overrideFlag)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, target)() = 0;
    };
    // Interface für Redirect NDP in IEthernetPacket.
    class INDPRedirect : public IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, target)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, destination)() = 0;
    };
    // Interface für NDP in IEthernetPacket.
    class INDP
    {
    public:
        CAPL_METHOD(INDPRouterSolicitation&, routerSolicitation)() = 0;
        CAPL_METHOD(INDPRouterAdvertisement&, routerAdvertisement)() = 0;
        CAPL_METHOD(INDPNeighborSolicitation&, neighborSolicitation)() = 0;
        CAPL_METHOD(INDPNeighborAdvertisement&, neighborAdvertisement)() = 0;
        CAPL_METHOD(INDPRedirect&, redirect)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, target)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, destination)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, routerFlag)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, solicitedFlag)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, overrideFlag)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, curHopLimit)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, managedAddrConfig)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, otherConfig)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, mobileHomeAgent)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, defaultRouterPrefs)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, neighborDiscoveryProxy)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, routerLifeTime)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, reachableTime)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, retransTime)() = 0;
    };
    // Interface für TCP in IEthernetPacket.
    class ITCP : public IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(IEthernetPacketProtocolWordField&, source)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, destination)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, sequence)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, ackNumber)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, offset)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolByteField&, flags)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, window)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, checksum)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, pointer)() = 0;
    };
    // Interface für UDP in IEthernetPacket.
    class IUDP : public IEthernetPacketProtocol
    {
    public:
        CAPL_METHOD(IEthernetPacketProtocolWordField&, source)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, destination)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, length)() = 0;
        CAPL_METHOD(IEthernetPacketProtocolWordField&, checksum)() = 0;
    };
    // Interface für Ethernet Botschaften.
    class IEthernetPacket
    {
    public:

        CAPL_METHOD(void, Send)() = 0;

        CAPL_METHOD(QWORD, Time_NS)() const = 0;
        CAPL_METHOD(BYTE, DIR)() const = 0;
        CAPL_METHOD(REF(WORD), MsgChannel)() = 0;
        CAPL_METHOD(REF(WORD), Length)() = 0;
        CAPL_METHOD(DWORD, FCS)() const = 0;
        CAPL_METHOD(CAPL_INT64, FrameLen)() const = 0;
        CAPL_METHOD(CAPL_INT64, SOF)() const = 0;
        CAPL_METHOD(IProperty<WORD>&, Type)() = 0;
        CAPL_METHOD(REF(CAPL_INT32), TypeInternal)() = 0;
        CAPL_METHOD(REF(QWORD), Source)() = 0;
        CAPL_METHOD(REF(QWORD), Destination)() = 0;

        CAPL_METHOD(REF(BYTE), Byte)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(WORD), Word)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(DWORD), Dword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(QWORD), Qword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(char), Char)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(short), Short)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(CAPL_INT32), Long)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(REF(CAPL_INT64), LongLong)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(void, CopyData)(IEthernetPacket& msg) = 0;
        CAPL_METHOD(void, StartGenerator)(DWORD transmissionRate, DWORD numberOfFrames, DWORD counterByteOffset) = 0;
        CAPL_METHOD(void, StopGenerator)() = 0;

        CAPL_METHOD(CAPL_INT32, Clear)() = 0;
        CAPL_METHOD(CAPL_INT32, CompletePacket)() = 0;
        CAPL_METHOD(DWORD, GetData)(WORD offset, void* source, WORD length) = 0;
        CAPL_METHOD(WORD, GetProtocolErrorText)(char* buffer) = 0;
        CAPL_METHOD(CAPL_INT32, GetVlan)(WORD& tpid, WORD& tci) = 0;
        CAPL_METHOD(CAPL_INT32, GetVlanIndex)(DWORD vlanIndex, WORD& tpid, WORD& tci) = 0;
        CAPL_METHOD(DWORD, GetVlanId)() = 0;
        CAPL_METHOD(DWORD, GetVlanIdIndex)(DWORD vlanIndex) = 0;
        CAPL_METHOD(DWORD, GetVlanPriority)() = 0;
        CAPL_METHOD(DWORD, GetVlanPriorityIndex)(DWORD vlanIndex) = 0;
        CAPL_METHOD(WORD, HasProtocolError)() = 0;
        CAPL_METHOD(WORD, HasVlan)() = 0;
        CAPL_METHOD(DWORD, RemoveVlan)() = 0;
        CAPL_METHOD(DWORD, RemoveVlanIndex)(DWORD vlanIndex) = 0;
        CAPL_METHOD(DWORD, SetData)(WORD offset, const void* source, WORD length) = 0;
        CAPL_METHOD(CAPL_INT32, SetVlan)(WORD tpid, WORD tci) = 0;
        CAPL_METHOD(CAPL_INT32, SetVlanIndex)(DWORD vlanIndex, WORD tpid, WORD tci) = 0;
        CAPL_METHOD(DWORD, SetVlanId)(WORD vlanId) = 0;
        CAPL_METHOD(DWORD, SetVlanIdIndex)(DWORD vlanIndex, WORD vlanId) = 0;
        CAPL_METHOD(DWORD, SetVlanPriority)(WORD vlanPriority) = 0;
        CAPL_METHOD(DWORD, SetVlanPriorityIndex)(DWORD vlanIndex, WORD vlanPriority) = 0;


        CAPL_METHOD(IIPv4&, IPv4)() = 0;
        CAPL_METHOD(IIPv6&, IPv6)() = 0;
        CAPL_METHOD(IICMPv6&, ICMPv6)() = 0;
        CAPL_METHOD(INDP&, NDP)() = 0;
        CAPL_METHOD(ITCP&, TCP)() = 0;
        CAPL_METHOD(IUDP&, UDP)() = 0;
    };

    // Interface für Ethernet Status.
    class IEthernetStatus
    {
    public:

        CAPL_METHOD(LONG, Status)() const = 0;
        CAPL_METHOD(DWORD, Bitrate)() const = 0;
        CAPL_METHOD(REF(WORD), MsgChannel)() = 0;
        CAPL_METHOD(REF(WORD), HwChannel)() = 0;
        CAPL_METHOD(QWORD, Time_NS)() const = 0;
    };

    // __          __                              
    // \ \        / /                              
    //  \ \  /\  / / __ __ _ _ __  _ __   ___ _ __ 
    //   \ \/  \/ / '__/ _` | '_ \| '_ \ / _ \ '__|
    //    \  /\  /| | | (_| | |_) | |_) |  __/ |   
    //     \/  \/ |_|  \__,_| .__/| .__/ \___|_|   
    //                      | |   | |              
    //                      |_|   |_|              

    // Interface für Diagnose.
    class IDiag
    {
    public:
        CAPL_METHOD(CAPL_INT32, GetStatus)() const = 0;
    };

    // Wrapper-Klasse für den CAPL-Thread.
    class Thread
    {
    private:
        CAPL_HANDLE m_hThread;
    public:
        Thread(bool boExtendedErrorInfo = false)
        {
            m_hThread = CreateThread();
            SetExtendedErrorInfo(m_hThread, boExtendedErrorInfo);
        }

        virtual ~Thread()
        {
            DeleteThread(m_hThread);
        }

        CAPL_HANDLE GetCaplHandle()
        {
            return m_hThread;
        }
    };

    class TestInfo
    {
    private:
        TestInfo(const TestInfo&) { };

    protected:
        TestInfo() { };

        const char * m_sCAPLFunctionName;
    public:
        TestInfo(const char *sCAPLFunctionName)
            : m_sCAPLFunctionName(sCAPLFunctionName)
        {
            capl::SetTestFunction(m_sCAPLFunctionName);
        }

        ~TestInfo()
        {
            capl::RemoveTestFunction(m_sCAPLFunctionName);
        }
    };

//#ifdef USE_COM
    //#include "combase.h"
    //
    //#define CAPL_COMMETHOD_ONEVENT  
    ////virtual HRESULT STDMETHODCALLTYPE OnComEvent(IUnknown *sender) override

    //class BaseComEvent : public IDispatchImpl<ICAPLEvent>
    //{
    //public:
    //    STDMETHOD(QueryInterface)(REFIID refiid, void ** ppvObject) override
    //    {
    //        *ppvObject = (refiid == IID_ICAPLEvent) ? this : NULL;
    //        if (*ppvObject != NULL)
    //        {
    //            ((LPUNKNOWN)*ppvObject)->AddRef();
    //            return S_OK;
    //        }

    //        return IDispatchImpl<ICAPLEvent>::QueryInterface(refiid, ppvObject);
    //    }
    //};
//#else
    #define CAPL_COMMETHOD_ONEVENT
//#endif

    // Basis-Interface für ein CAPL-Event.
    template<class T>
    class IEvent
//#ifdef USE_COM
//        : public BaseComEvent
//#endif
    {
    public:
        IEvent() : m_hEvent(NULL) {}
        CAPL_METHOD(void, OnEvent)(T sender) = 0;
    protected:
        CAPL_HANDLE m_hEvent;
    };

    // Templatespezialisierung damit der GCC nicht meckert, da dieser den Typ
    // "T=void" in der Parameterliste von OnEvent akzeptiert.
    template<>
    class IEvent<void>
//#ifdef USE_COM
//        : public BaseComEvent
//#endif
    {
    public:
        IEvent() : m_hEvent(NULL) {}
        CAPL_METHOD(void, OnEvent)(void) = 0;
    protected:
        CAPL_HANDLE m_hEvent;
    };

    //  ______               _       
    // |  ____|             | |      
    // | |____   _____ _ __ | |_ ___ 
    // |  __\ \ / / _ \ '_ \| __/ __|
    // | |____ V /  __/ | | | |_\__ \
    // |______\_/ \___|_| |_|\__|___/
    //                               
    //

    // Botschaftsänderungs-Event.
    class MessageEvent : public IEvent<IMessage&>
    {
    public:
        MessageEvent()
        {
        }
        MessageEvent(DWORD u32FileIndex, const char* sFilter);
        virtual ~MessageEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // Signaländerungs-Event.
    class SignalEvent : public IEvent<ISignalVal&>
    {
    public:
        SignalEvent() 
        {
        }
        SignalEvent(const char* sIds);
        SignalEvent(ISignalVal& sig);
        virtual ~SignalEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // Signalupdate-Event.
    class SignalUpdateEvent : public IEvent<ISignalVal&>
    {
    public:
        SignalUpdateEvent(){}
        SignalUpdateEvent(const char* sIds);
        SignalUpdateEvent(ISignalVal& sig);
        virtual ~SignalUpdateEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // Timer-Event.
    class TimerEvent : public IEvent<ITimer&>
    {
        ITimer* m_pTimer;
    public:
        TimerEvent();
        virtual ~TimerEvent();
        CAPL_METHOD(void, Create)(bool boSecond);
        CAPL_METHOD(ITimer&, GetTimer)();
        CAPL_METHOD(void, Set)(DWORD u32Timer);
        CAPL_METHOD(void, SetCyclic)(CAPL_INT32 firstDuration, CAPL_INT32 period = -1);
        CAPL_METHOD(void, Kill)();
        CAPL_METHOD(DWORD, GetTime)() const;
        CAPL_COMMETHOD_ONEVENT;
        operator ITimer&()
        {
            return GetTimer();
        }
    };

    // Umgebungsvariablen-Event.

    class IEnvVarEvent : public IEvent<IEnvVar&>
    {
    public:
        CAPL_METHOD(void, SetEnvVar)(IEnvVar& envvar) = 0;
        CAPL_COMMETHOD_ONEVENT;
    };

    // PreStart-Event.
    class PreStartEvent : public IEvent<IPreStart&>
    {
    public:
        PreStartEvent();
        virtual ~PreStartEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // Start-Event.
    class StartEvent : public IEvent<IStart&>
    {
    public:
        StartEvent();
        virtual ~StartEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // PreStart-Event.
    class PreStopEvent : public IEvent<IPreStop&>
    {
    public:
        PreStopEvent();
        virtual ~PreStopEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // Stop-Event.
    class StopEvent : public IEvent<IStop&>
    {
    public:
        StopEvent();
        virtual ~StopEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // Key-Event.
    class KeyEvent : public IEvent<IKey&>
    {
    public:
        KeyEvent()
        {
        }
        KeyEvent(DWORD u32FileIndex, WORD u16Key, DWORD u32Modifier);
        KeyEvent(DWORD u32FileIndex, CAPL_INT32 caplKey);
        virtual ~KeyEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // ErrorPassive-Event.
    class BusEvent : public IEvent<ICanError&>
    {
    public:
        BusEvent() { }
        BusEvent(BusEventType enType);
        virtual ~BusEvent();
    };

    class ErrorBusOffEvent : public BusEvent
    {
    public:
        ErrorBusOffEvent();
        virtual ~ErrorBusOffEvent();
    };

    class ErrorPassiveEvent : public BusEvent
    {
    public:
        ErrorPassiveEvent();
        virtual ~ErrorPassiveEvent();
    };

    class ErrorActiveEvent : public BusEvent
    {
    public:
        ErrorActiveEvent();
        virtual ~ErrorActiveEvent();
    };
    class WarningLimitEvent : public BusEvent
    {
    public:
        WarningLimitEvent();
        virtual ~WarningLimitEvent();
    };

    // ErrorPassive-Event.
    class ErrorFrameEvent : public IEvent<IErrorFrame&>
    {
    public:
        ErrorFrameEvent();
        virtual ~ErrorFrameEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    class LinWakeupFrameEvent : public IEvent<ILinWakeupFrame&>
    {
    public:
        LinWakeupFrameEvent();
        virtual ~LinWakeupFrameEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // Diagnose-Event.
    class DiagEvent : public IEvent<IDiag&>
    {
    public:
        DiagEvent();
        virtual ~DiagEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    class DiagRequestEvent : public IEvent<IDiagRequest&>
    {
    public:
        DiagRequestEvent()
        {
        }
        DiagRequestEvent(const char* sIds);
        virtual ~DiagRequestEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    class DiagResponseEvent : public IEvent<IDiagResponse&>
    {
    public:
        DiagResponseEvent()
        {
        }
        DiagResponseEvent(const char* sIds);
        virtual ~DiagResponseEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // Ethernet-Event.
    class EthernetPacketEvent : public IEvent<IEthernetPacket&>
    {
    public:
        EthernetPacketEvent()
        {
        }
        EthernetPacketEvent(BYTE u8Channel);
        virtual ~EthernetPacketEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    class EthernetStatusEvent : public IEvent<IEthernetStatus&>
    {
    public:
        EthernetStatusEvent()
        {
        }
        EthernetStatusEvent(BYTE u8Channel);
        virtual ~EthernetStatusEvent();
        CAPL_COMMETHOD_ONEVENT;
    };

    // TimerArray-Event.
    class TimerArrayEvent 
    {
    public:
        class ChildEvent : public TimerEvent
        {
        public:
            TimerArrayEvent* m_parent;
            DWORD m_index;
            CAPL_METHOD_IMPL(void, OnEvent)(ITimer&)
            {
                m_parent->OnEvent(*m_parent, m_index);
            }
        };
    protected:
        std::vector<ChildEvent> m_Events;
    public:
        explicit TimerArrayEvent(CAPL_UINT32 size)
        {
            m_Events.resize(size);
            for (size_t i = 0; i < m_Events.size(); ++i)
            {
                m_Events[i].m_parent = this;
                m_Events[i].m_index = i;
            }
        }
        CAPL_METHOD_IMPL(void, Create)(bool boSecond)
        {
            for (size_t i = 0; i < m_Events.size(); ++i)
                m_Events[i].Create(boSecond);
        }
        ChildEvent& operator[](CAPL_UINT32 pos)
        {
            if (m_Events.size() <= pos)
                RaiseException(EXCEPTION_ARRAY_BOUNDS_EXCEEDED, 0, 0, NULL);
            return m_Events[pos];
        }
        CAPL_INT32 Size()const
        {
            return m_Events.size();
        }
        CAPL_METHOD(void, OnEvent)(TimerArrayEvent&, CAPL_UINT32) = 0;
    };

    static inline CAPL_INT32 elCountTimerArray(const TimerArrayEvent& timerArray)
    {
        return timerArray.Size();
    }


#ifdef CAPL_EVENT_IMPL
    // Botschaftsänderungs-Event.
    MessageEvent::MessageEvent(DWORD u32FileIndex, const char* sFilter)
    {
        m_hEvent = CreateMessageEvent(this, u32FileIndex, sFilter);
    }

    MessageEvent::~MessageEvent()
    {
        DeleteMessageEvent(m_hEvent);
    }

    SignalEvent::SignalEvent(const char* sIds)
    {
        m_hEvent = CreateSignalEvent(this, sIds);
    }

    SignalEvent::SignalEvent(ISignalVal& sig)
    {
        m_hEvent = CreateSignalEventBySigVal(this, sig);
    }

    SignalEvent::~SignalEvent()
    {
        DeleteSignalEvent(m_hEvent);
    }

    SignalUpdateEvent::SignalUpdateEvent(const char* sIds)
    {
        m_hEvent = CreateSignalUpdateEvent(this, sIds);
    }

    SignalUpdateEvent::SignalUpdateEvent(ISignalVal& sig)
    {
        m_hEvent = CreateSignalUpdateEventBySigVal(this, sig);
    }

    SignalUpdateEvent::~SignalUpdateEvent()
    {
        DeleteSignalUpdateEvent(m_hEvent);
    }

    TimerEvent::TimerEvent()
        : m_pTimer(NULL)
    {
    }

    TimerEvent::~TimerEvent()
    {
        DeleteTimerEvent(m_hEvent);
        DeleteTimer(m_pTimer);
    }

    CAPL_METHOD_IMPL(void, TimerEvent::Create)(bool boSeconds)
    {
        m_pTimer = CreateTimer(boSeconds);
        m_hEvent = CreateTimerEvent(this, m_pTimer);
    }

    CAPL_METHOD_IMPL(ITimer&, TimerEvent::GetTimer)()
    {
        return *m_pTimer;
    }
    CAPL_METHOD_IMPL(void, TimerEvent::Set)(DWORD u32Timer)
    {
        return m_pTimer->Set(u32Timer);
    }

    CAPL_METHOD_IMPL(void, TimerEvent::SetCyclic)(CAPL_INT32 firstDuration, CAPL_INT32 period)
    {
        return m_pTimer->SetCyclic(firstDuration, period);
    }

    CAPL_METHOD_IMPL(void, TimerEvent::Kill)()
    {
        return m_pTimer->Kill();
    }

    CAPL_METHOD_IMPL(DWORD, TimerEvent::GetTime)() const
    {
        return m_pTimer->GetTime();
    }

    PreStartEvent::PreStartEvent()
    {
        m_hEvent = CreatePreStartEvent(this);
    }

    PreStartEvent::~PreStartEvent()
    {
        DeletePreStartEvent(m_hEvent);
    }

    StartEvent::StartEvent()
    {
        m_hEvent = CreateStartEvent(this);
    }

    StartEvent::~StartEvent()
    {
        DeleteStartEvent(m_hEvent);
    }

    PreStopEvent::PreStopEvent()
    {
        m_hEvent = CreatePreStopEvent(this);
    }

    PreStopEvent::~PreStopEvent()
    {
        DeletePreStopEvent(m_hEvent);
    }

    StopEvent::StopEvent()
    {
        m_hEvent = CreateStopEvent(this);
    }

    StopEvent::~StopEvent()
    {
        DeleteStopEvent(m_hEvent);
    }

    KeyEvent::KeyEvent(DWORD u32FileIndex, CAPL_INT32 caplKey)
    {
        m_hEvent = CreateCaplKeyEvent(this, caplKey, u32FileIndex);
    }

    KeyEvent::~KeyEvent()
    {
        DeleteKeyEvent(m_hEvent);
    }

    BusEvent::BusEvent(BusEventType enType)
    {
        m_hEvent = CreateBusEvent(this, enType);
    }

    BusEvent::~BusEvent()
    {
        DeleteBusEvent(m_hEvent);
    }

    ErrorBusOffEvent::ErrorBusOffEvent() : BusEvent(BusOff) { }
    ErrorBusOffEvent::~ErrorBusOffEvent() { }

    ErrorPassiveEvent::ErrorPassiveEvent() : BusEvent(Passive) { }
    ErrorPassiveEvent::~ErrorPassiveEvent() { }

    ErrorActiveEvent::ErrorActiveEvent() : BusEvent(Active) { }
    ErrorActiveEvent::~ErrorActiveEvent() { }

    WarningLimitEvent::WarningLimitEvent() : BusEvent(WarningLimit) { }
    WarningLimitEvent::~WarningLimitEvent() { }

    ErrorFrameEvent::ErrorFrameEvent()
    {
        m_hEvent = CreateErrorFrameEvent(this);
    }

    ErrorFrameEvent::~ErrorFrameEvent()
    {
        DeleteErrorFrameEvent(m_hEvent);
    }

    LinWakeupFrameEvent::LinWakeupFrameEvent()
    {
        m_hEvent = CreateLinWakeupFrameEvent(this);
    }

    LinWakeupFrameEvent::~LinWakeupFrameEvent()
    {
        DeleteLinWakeupFrameEvent(m_hEvent);
    }

    EthernetPacketEvent::EthernetPacketEvent(BYTE u8Channel)
    {
        m_hEvent = CreateEthernetPacketEvent(this, u8Channel);
    }

    EthernetPacketEvent::~EthernetPacketEvent()
    {
        DeleteEthernetPacketEvent(m_hEvent);
    }

    EthernetStatusEvent::EthernetStatusEvent(BYTE u8Channel)
    {
        m_hEvent = CreateEthernetStatusEvent(this, u8Channel);
    }

    EthernetStatusEvent::~EthernetStatusEvent()
    {
        DeleteEthernetStatusEvent(m_hEvent);
    }


#endif

    CAPL_FUNC(void, cancelTimer)(capl::ITimer&);

    CAPL_FUNC(double, atodbl)(const char*);
    CAPL_FUNC(CAPL_INT32, atol)(const char*);
    CAPL_FUNC(void, ltoa)(CAPL_INT32, char*, CAPL_INT32);

    CAPL_FUNC(CAPL_INT32, _round)(double);
    CAPL_FUNC(CAPL_INT64, _round64)(double);

    CAPL_FUNC(double, _floor)(double x);
    CAPL_FUNC(double, _ceil)(double x);

    CAPL_FUNC(DWORD, random)(DWORD x);

    //CAPL_FUNC(double, _pow)(double, double); Wird auf pow abgebildet, welches durch math.h implementiert wird!
    //CAPL_FUNC(double, abs)(double); Wird durch math.h implementiert!
    //CAPL_FUNC(CAPL_INT32, abs)(CAPL_INT32); Wird durch math.h implementiert!
    //CAPL_FUNC(double, cos)(double); Wird durch math.h implementiert!
    //CAPL_FUNC(double, exp)(double); Wird durch math.h implementiert!
    //CAPL_FUNC(double, sin)(double); Wird durch math.h implementiert!
    //CAPL_FUNC(double, sqrt)(double); Wird durch math.h implementiert!

    CAPL_FUNC(void, loadPanel)(const char*); // Gibt es in CAPL nicht, nur zum Testen implementiert!
    CAPL_FUNC(void, openPanel)(const char*);
    CAPL_FUNC(void, closePanel)(const char*);
    CAPL_FUNC(void, SetControlBackColor)(const char* sPanel, const char* sControl, CAPL_INT32 color);
    CAPL_FUNC(void, SetControlForeColor)(const char* sPanel, const char* sControl, CAPL_INT32 color);
    CAPL_FUNC(void, SetControlColors)(const char* sPanel, const char* sControl, CAPL_INT32 backcolor, CAPL_INT32 textcolor);
    CAPL_FUNC(void, enableControl)(const char* sPanel, const char* sControl, CAPL_INT32 enable);
    CAPL_FUNC(void, SetControlVisibility)(const char* sPanel, const char* sControl, CAPL_INT32 visible);
    CAPL_FUNC(void, SetClockControlTimeByValues)(const char* sPanel, const char* sControl, CAPL_INT32 hours, CAPL_INT32 minutes, CAPL_INT32 seconds);
    CAPL_FUNC(void, SetClockControlTime)(const char* sPanel, const char* sControl, CAPL_INT32 time);
    CAPL_FUNC(void, ClockControlStart)(const char* sPanel, const char* sControl);
    CAPL_FUNC(void, ClockControlStop)(const char* sPanel, const char* sControl);
    CAPL_FUNC(void, ClockControlReset)(const char* sPanel, const char* sControl);

    CAPL_FUNC(CAPL_INT32, fileReadArray_Internal)(const char*, const char*, char*, short, const char*, const char*, const char*);
    CAPL_FUNC(double, fileReadFloat_Internal)(const char*, const char*, double, const char*, const char*, const char*);
    CAPL_FUNC(CAPL_INT32, fileReadInt_Internal)(const char*, const char*, CAPL_INT32, const char*, const char*, const char*);
    CAPL_FUNC(CAPL_INT32, fileReadString_Internal)(const char*, const char*, const char*, char*, CAPL_INT32, const char*, const char*, const char*);
    CAPL_FUNC(CAPL_INT32, fileWriteFloat_Internal)(const char*, const char*, double, const char*, const char*, const char*);
    CAPL_FUNC(CAPL_INT32, fileWriteInt_Internal)(const char*, const char*, CAPL_INT32, const char*, const char*, const char*);
    CAPL_FUNC(CAPL_INT32, fileWriteString_Internal)(const char*, const char*, const char*, const char*, const char*, const char*);

    CAPL_FUNC(void, getLocalTime)(CAPL_INT32*);
    CAPL_FUNC(void, getLocalTimeString)(char*);

    CAPL_FUNC(CAPL_INT32, getProfileArray_Internal)(const char*, const char*, char*, CAPL_INT32, const char*, const char*, const char*);
    CAPL_FUNC(double, getProfileFloat_Internal)(const char*, const char*, double, const char*, const char*, const char*);
    CAPL_FUNC(CAPL_INT32, getProfileInt_Internal)(const char*, const char*, CAPL_INT32, const char*, const char*, const char*);
    CAPL_FUNC(CAPL_INT32, getProfileString_Internal)(const char*, const char*, const char*, char*, CAPL_INT32, const char*, const char*, const char*);

    CAPL_FUNC(long, ErrorFrameCount)(BYTE channel);

    CAPL_FUNC(double, getSignal)(ISignal& var);
    CAPL_FUNC(void, setSignal)(ISignal& var, double value);
    CAPL_FUNC(double, getSignalByStringId)(const char* sId);
    CAPL_FUNC(void, setSignalByStringId)(const char* sId, double value);

    CAPL_FUNC(CAPL_INT64, getRawSignal)(ISignal& var);
    CAPL_FUNC(CAPL_INT32, getRawSignalArray)(ISignal& var, CAPL_UINT8* data, CAPL_INT32 dataLength);

    CAPL_FUNC(void, setRawSignal)(ISignal& var, CAPL_INT64 value);
    CAPL_FUNC(CAPL_INT32, setRawSignalArray)(ISignal& var, CAPL_UINT8* data, CAPL_INT32 dataLength);
    /////////////////////////////////////////////////////////////////////////////
    //            ___      __   _            
    //           | \ \    / /  | |           
    //  __ _  ___| |_ \  / /_ _| |_   _  ___ 
    // / _` |/ _ \ __\ \/ / _` | | | | |/ _ \
    //  (_| |  __/ |_ \  / (_| | | |_| |  __/
    // \__, |\___|\__| \/ \__,_|_|\__,_|\___|
    //  __/ |                                
    // |___/


    CAPL_FUNC(INT16, getValueIntByStringId)(const char* sId);
    CAPL_FUNC(CAPL_INT32, getValueLongByStringId)(const char* sId);
    CAPL_FUNC(double, getValueDoubleByStringId)(const char* sId);
    CAPL_FUNC(INT16, getValueIntByStringName)(const char* sId);
    CAPL_FUNC(CAPL_INT32, getValueLongByStringName)(const char* sId);
    CAPL_FUNC(double, getValueDoubleByStringName)(const char* sId);

    // ? getValue(name) ist in der WrapperApi definiert.

    CAPL_FUNC(INT16, getValueIntByObj)(IEnvVar& var);
    CAPL_FUNC(double, getValueDoubleByObj)(IEnvVar& var);
    CAPL_FUNC(CAPL_INT32, getValueLongByObj)(IEnvVar& var);

    CAPL_FUNC(CAPL_INT32, getValueStringByObjBufferSize)(IEnvVar& var, char* pBuffer, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, getValueStringByStringIdBufferSize)(const char* sId, char* pBuffer, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, getValueStringByStringNameBufferSize)(const char* sId, char* pBuffer, CAPL_INT32 size);

    CAPL_FUNC(CAPL_INT32, SysGetVariableDataByStringInternal)(const char* namespaceName, const char* variable, BYTE* pBuffer, CAPL_INT32 &size, CAPL_INT32 max);
    CAPL_FUNC(CAPL_INT32, SysGetVariableDataByObjInternal)(IEnvVar& var, BYTE* pBuffer, CAPL_INT32 &size, CAPL_INT32 max);

    template<class T>
    inline CAPL_INT32 SysGetVariableDataByString(const char* namespaceName, const char* variable, T& pBuffer, CAPL_INT32 &size)
    {
        return SysGetVariableDataByStringInternal(namespaceName, variable, pBuffer, size, sizeof(pBuffer));
    }
    template<class T>
    inline CAPL_INT32 SysGetVariableDataByObj(IEnvVar& var, T& pBuffer, CAPL_INT32 &size)
    {
        return SysGetVariableDataByObjInternal(var, pBuffer, size, sizeof(pBuffer));
    }

    CAPL_FUNC(CAPL_INT32, SysSetVariableStringByString)(const char* namespaceName, const char* variable, const char* pBuffer);
    CAPL_FUNC(CAPL_INT32, SysSetVariableStringByObj)(IEnvVar& var, const char* pBuffer);

    CAPL_FUNC(CAPL_INT32, SysGetVariableStringByString)(const char* namespaceName, const char* variable, char* pBuffer, CAPL_INT32 bufferSize);
    CAPL_FUNC(CAPL_INT32, SysGetVariableStringByObj)(IEnvVar& var, char* pBuffer, CAPL_INT32 bufferSize);

    CAPL_FUNC(CAPL_INT32, SysSetVariableDataByString)(const char* sNamespace, const char* sName, const BYTE* val, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, SysSetVariableDataByObj)(IEnvVar& var, const BYTE* val, CAPL_INT32 size);


    template<class T>
    CAPL_INT32 getValueStringByObj(IEnvVar& var, T& buffer) //T ist ein char[?]
    {
        return getValueStringByObjBufferSize(var, buffer, sizeof(buffer));
    }

    template<class T>
    CAPL_INT32 getValueStringByStringName(const char* sId, T& buffer) //T ist ein char[?]
    {
        return getValueStringByStringNameBufferSize(sId, buffer, sizeof(buffer));
    }
    template<class T>
    CAPL_INT32 getValueStringByStringId(const char* sId, T& buffer) //T ist ein char[?]
    {
        return getValueStringByStringIdBufferSize(sId, buffer, sizeof(buffer));
    }

    CAPL_FUNC(CAPL_INT32, getValueArrayByObjBufferSize)(IEnvVar& var, BYTE* pBuffer, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, getValueArrayByStringIdBufferSize)(const char* sId, BYTE* pBuffer, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, getValueArrayByStringNameBufferSize)(const char* sId, BYTE* pBuffer, CAPL_INT32 size);

    template<class T>
    CAPL_INT32 getValueArrayByObj(IEnvVar& var, T& buffer) //T ist ein BYTE[?]
    {
        return getValueArrayByObjBufferSize(var, buffer, sizeof(buffer));
    }

    template<class T>
    CAPL_INT32 getValueArrayByStringId(const char* sId, T& buffer) //T ist ein BYTE[?]
    {
        return getValueArrayByStringIdBufferSize(sId, buffer, sizeof(buffer));
    }
    template<class T>
    CAPL_INT32 getValueArrayByStringName(const char* sId, T& buffer) //T ist ein BYTE[?]
    {
        return getValueArrayByStringNameBufferSize(sId, buffer, sizeof(buffer));
    }
    CAPL_FUNC(CAPL_INT32, getValueArrayOffsetByObj)(IEnvVar& var, BYTE* pBuffer, CAPL_INT32 size, CAPL_INT32 offset);
    CAPL_FUNC(CAPL_INT32, getValueArrayOffsetByStringId)(const char* sId, BYTE* pBuffer, CAPL_INT32 size, CAPL_INT32 offset);
    CAPL_FUNC(CAPL_INT32, getValueArrayOffsetByStringName)(const char* sId, BYTE* pBuffer, CAPL_INT32 size, CAPL_INT32 offset);

    template<class T>
    CAPL_INT32 getValueArrayOffsetByObj(IEnvVar& var, T& buffer, CAPL_INT32 offset) //T ist ein BYTE[?]
    {
        return getValueArrayOffsetByObj(var, buffer, sizeof(buffer), offset);
    }

    template<class T>
    CAPL_INT32 getValueArrayOffsetByStringId(const char* sId, T& buffer, CAPL_INT32 offset) //T ist ein BYTE[?]
    {
        return getValueArrayOffsetByStringId(sId, buffer, sizeof(buffer), offset);
    }
    template<class T>
    CAPL_INT32 getValueArrayOffsetByStringName(const char* sId, T& buffer, CAPL_INT32 offset) //T ist ein BYTE[?]
    {
        return getValueArrayOffsetByStringName(sId, buffer, sizeof(buffer), offset);
    }

    CAPL_FUNC(CAPL_INT64, SysGetVariableLongLongByString)(const char* sNamespace, const char* sName);
    CAPL_FUNC(CAPL_INT64, SysGetVariableLongLongByObj)(IEnvVar& var);
    CAPL_FUNC(CAPL_INT32, SysGetVariableFloatArrayByString)(const char* sNamespace, const char* sName, double* val, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, SysGetVariableFloatArrayByObj)(IEnvVar& var, double* val, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, SysGetVariableLongArrayByString)(const char* sNamespace, const char* sName, CAPL_INT32* val, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, SysGetVariableLongArrayByObj)(IEnvVar& var, CAPL_INT32* val, CAPL_INT32 size);

    /////////////////////////////////////////////////////////////////////////////
    //              ___      __   _            
    //             | \ \    / /  | |           
    //  _ __  _   _| |_ \  / /_ _| |_   _  ___ 
    // | '_ \| | | | __\ \/ / _` | | | | |/ _ \
    // | |_) | |_| | |_ \  / (_| | | |_| |  __/
    // | .__/ \__,_|\__| \/ \__,_|_|\__,_|\___|
    // | |                                     
    // |_|


    CAPL_FUNC(void, putValueIntByStringId)(const char* sId, INT16 val);
    CAPL_FUNC(void, putValueLongByStringId)(const char* sId, CAPL_INT32 val);
    CAPL_FUNC(void, putValueDoubleByStringId)(const char* sId, double val);
    CAPL_FUNC(void, putValueIntByStringName)(const char* sId, INT16 val);
    CAPL_FUNC(void, putValueLongByStringName)(const char* sId, CAPL_INT32 val);
    CAPL_FUNC(void, putValueDoubleByStringName)(const char* sId, double val);

    CAPL_FUNC(void, putValueIntByObj)(IEnvVar& var, INT16 val);
    CAPL_FUNC(void, putValueLongByObj)(IEnvVar& var, CAPL_INT32 val);
    CAPL_FUNC(void, putValueDoubleByObj)(IEnvVar& var, double val);

    CAPL_FUNC(void, putValueStringByObj)(IEnvVar& var, const char* val);
    CAPL_FUNC(void, putValueStringByStringId)(const char* sId, const char* val);
    CAPL_FUNC(void, putValueStringByStringName)(const char* sId, const char* val);

    CAPL_FUNC(void, putValueArrayBufferSizeByObj)(IEnvVar& var, const BYTE* val, CAPL_INT32 size);
    CAPL_FUNC(void, putValueArrayBufferSizeByStringId)(const char* sId, const BYTE *val, CAPL_INT32 size);
    CAPL_FUNC(void, putValueArrayBufferSizeByStringName)(const char* sId, const BYTE *val, CAPL_INT32 size);

    template<class T>
    void putValueArrayByObj(IEnvVar& var, T& buffer)
    {
        putValueArrayBufferSizeByObj(var, buffer, sizeof(buffer));
    }

    template<class T>
    void putValueArrayByStringId(const char* sId, const T& buffer)
    {
        putValueArrayBufferSizeByStringId(sId, buffer, sizeof(buffer));
    }
    template<class T>
    void putValueArrayByStringName(const char* sId, const T& buffer)
    {
        putValueArrayBufferSizeByStringName(sId, buffer, sizeof(buffer));
    }


    CAPL_FUNC(CAPL_INT32, SysSetVariableFloatByString)(const char* sNamespace, const char* sName, double val);
    CAPL_FUNC(CAPL_INT32, SysSetVariableFloatByObj)(IEnvVar& var, double val);
    CAPL_FUNC(CAPL_INT32, SysSetVariableIntByString)(const char* sNamespace, const char* sName, CAPL_INT32 val);
    CAPL_FUNC(CAPL_INT32, SysSetVariableIntByObj)(IEnvVar& var, CAPL_INT32 val);
    CAPL_FUNC(CAPL_INT32, SysSetVariableLongLongByString)(const char* sNamespace, const char* sName, long long val);
    CAPL_FUNC(CAPL_INT32, SysSetVariableLongLongByObj)(IEnvVar& var, long long val);
    CAPL_FUNC(CAPL_INT32, SysSetVariableFloatArrayByString)(const char* sNamespace, const char* sName, const double* val, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, SysSetVariableFloatArrayByObj)(IEnvVar& var, const double* val, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, SysSetVariableLongArrayByString)(const char* sNamespace, const char* sName, const CAPL_INT32* val, CAPL_INT32 size);
    CAPL_FUNC(CAPL_INT32, SysSetVariableLongArrayByObj)(IEnvVar& var, const CAPL_INT32* val, CAPL_INT32 size);

    CAPL_FUNC(CAPL_INT32, SysGetVariableArrayLengthByString)(const char* sNamespace, const char* sName);
    CAPL_FUNC(CAPL_INT32, SysGetVariableArrayLengthByObj)(IEnvVar& var);

    template<class T>
    class IAccess
    {
    protected:
        virtual ~IAccess() {}
    public:
        IAccess() {}
        virtual T CAPL_CALL operator=(const T& value) = 0;
        virtual CAPL_CALL operator T()const = 0;
        virtual T CAPL_CALL operator++() = 0;
        virtual T CAPL_CALL operator++(int) = 0;
        virtual T CAPL_CALL operator--() = 0;
        virtual T CAPL_CALL operator--(int) = 0;
    };

    CAPL_FUNC(IAccess<char>&, GetSysVarArrayChar)(IEnvVar& var, size_t pos);
    CAPL_FUNC(IAccess<unsigned char>&, GetSysVarArrayUChar)(IEnvVar& var, size_t pos);
    CAPL_FUNC(IAccess<long>&, GetSysVarArrayLong)(IEnvVar& var, size_t pos);
    CAPL_FUNC(IAccess<unsigned long>&, GetSysVarArrayULong)(IEnvVar& var, size_t pos);
    CAPL_FUNC(IAccess<long long>&, GetSysVarArrayLongLong)(IEnvVar& var, size_t pos);
    CAPL_FUNC(IAccess<unsigned long long>&, GetSysVarArrayULongLong)(IEnvVar& var, size_t pos);
    CAPL_FUNC(IAccess<double>&, GetSysVarArrayDouble)(IEnvVar& var, size_t pos);
    CAPL_FUNC(IAccess<short>&, GetSysVarArrayShort)(IEnvVar& var, size_t pos);
    CAPL_FUNC(IAccess<unsigned short>&, GetSysVarArrayUShort)(IEnvVar& var, size_t pos);
    CAPL_FUNC(IAccess<float>&, GetSysVarArrayFloat)(IEnvVar& var, size_t pos);


    /////////////////////////////////////////////////////////////////////////////

    CAPL_FUNC(INT16, getValueSizeByStringName)(const char* sId);
    CAPL_FUNC(INT16, getValueSizeByStringId)(const char* sId);
    CAPL_FUNC(INT16, getValueSizeByObj)(IEnvVar& var);

    CAPL_FUNC(CAPL_INT32, isExtIdById)(DWORD dwMsgId);
    CAPL_FUNC(CAPL_INT32, isExtIdByObj)(capl::IMessage& m);
    CAPL_FUNC(CAPL_INT32, isStdIdById)(DWORD dwMsgId);
    CAPL_FUNC(CAPL_INT32, isStdIdByObj)(capl::IMessage& m);

    CAPL_FUNC(CAPL_INT32, MakeRGB)(CAPL_INT32, CAPL_INT32, CAPL_INT32);
    CAPL_FUNC(DWORD, mkExtId)(DWORD);
    CAPL_FUNC(void, outputInternal)(capl::IMessage&);
    CAPL_FUNC(void, outputEthernetPacket)(capl::IEthernetPacket&);

    CAPL_FUNC(CAPL_INT32, setBtr)(CAPL_INT32, BYTE, BYTE);
    CAPL_FUNC(CAPL_INT32, snprintf)(char*, CAPL_INT32, const char*, ...);
    CAPL_FUNC(void, stop)();

    // CAPL_FUNC(INT32, strlen)(char*); Wird durch String.h verwendet
    // CAPL_FUNC(void, strncat)(char*, char*, INT32); Wird durch String.h verwendet
    // CAPL_FUNC(INT32, strncmp)(char*, char*, INT32); Wird durch String.h verwendet
    CAPL_FUNC(void, strncpy)(char*, const char*, INT32);
    CAPL_FUNC(CAPL_INT32, strstr)(const char*, const char*);
    CAPL_FUNC(CAPL_INT32, strstr_off)(const char*, CAPL_INT32, const char*);

    CAPL_FUNC(CAPL_INT32, strncmp)(const char*, const char*, CAPL_INT32, CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, strncmp_off)(const char*, CAPL_INT32, const char*, CAPL_INT32, CAPL_INT32);
    CAPL_FUNC(void, strncpy_off)(char*, CAPL_INT32, const char*, CAPL_INT32);
    CAPL_FUNC(void, substr_cpy)(char*, const char*, CAPL_INT32, CAPL_INT32, CAPL_INT32);
    CAPL_FUNC(void, substr_cpy_off)(char*, CAPL_INT32, const char*, CAPL_INT32, CAPL_INT32, CAPL_INT32);

    CAPL_FUNC(double, SysGetVariableFloat)(const char*, const char*);
    CAPL_FUNC(CAPL_INT32, SysGetVariableInt)(const char*, const char*);
    CAPL_FUNC(CAPL_INT32, valOfId)(DWORD);
    CAPL_FUNC(CAPL_INT32, msgvalOfId)(capl::IMessage&);

    CAPL_FUNC(CAPL_INT32, writeProfileFloat_Internal)(const char*, const char*, double, const char*, const char*, const char*);
    CAPL_FUNC(CAPL_INT32, writeProfileInt_Internal)(const char*, const char*, CAPL_INT32, const char*, const char*, const char*);
    CAPL_FUNC(CAPL_INT32, writeProfileString_Internal)(const char*, const char*, const char*, const char*, const char*, const char*);

    // Bislang nicht unterstützte CAPL-Funktionen
    CAPL_FUNC(void, beep)(short, short);
    CAPL_FUNC(CAPL_INT32, canGetBusLoad)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetChipState)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetErrorCount)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetErrorRate)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetExtData)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetExtDataRate)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetExtRemote)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetExtRemoteRate)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetOverloadCount)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetOverloadRate)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetPeakLoad)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetRxErrorCount)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetStdData)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetStdDataRate)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetStdRemote)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetStdRemoteRate)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canGetTxErrorCount)(CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canSetChannelAcc)(CAPL_INT32, DWORD, DWORD);
    CAPL_FUNC(CAPL_INT32, canSetChannelMode)(CAPL_INT32, CAPL_INT32, CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, canSetChannelOutput)(CAPL_INT32, CAPL_INT32);

    CAPL_FUNC(CAPL_INT32, fileClose)(DWORD);
    CAPL_FUNC(CAPL_INT32, fileGetBinaryBlock)(BYTE*, CAPL_INT32, DWORD);
    CAPL_FUNC(CAPL_INT32, fileGetString)(char*, CAPL_INT32, DWORD);
    CAPL_FUNC(CAPL_INT32, fileGetStringSZ)(char*, CAPL_INT32, DWORD);
    CAPL_FUNC(CAPL_INT32, filePutString)(const char*, CAPL_INT32, DWORD);
    CAPL_FUNC(CAPL_INT32, fileRewind)(DWORD);
    CAPL_FUNC(CAPL_INT32, fileWriteBinaryBlock)(const BYTE*, CAPL_INT32, DWORD);
    CAPL_FUNC(CAPL_INT32, getCanOeAbsFilePath)(const char*, const char*, char*, CAPL_INT32);

    CAPL_FUNC(CAPL_INT32, getCardType)();
    CAPL_FUNC(short, getCardTypeEx)(short);
    CAPL_FUNC(CAPL_INT32, getChipType)(CAPL_INT32);
    CAPL_FUNC(short, getDrift)();
    CAPL_FUNC(short, GetEventSortingStatus)(capl::IMessage&);
    CAPL_FUNC(DWORD, GetFirstCANdbName)(char*, DWORD);
    CAPL_FUNC(short, getJitterMax)();
    CAPL_FUNC(short, getJitterMin)();
    CAPL_FUNC(CAPL_INT32, GetMessageAttrInt)(capl::IMessage&, const char*);
    CAPL_FUNC(DWORD, GetMessageName)(DWORD, DWORD, char*, DWORD);
    CAPL_FUNC(DWORD, GetNextCANdbName)(DWORD, char*, DWORD);
    CAPL_FUNC(short, getStartdelay)();
    CAPL_FUNC(void, halt)();
    CAPL_FUNC(void, inspect)();
    CAPL_FUNC(CAPL_INT32, isSimulated)();
    CAPL_FUNC(short, isStatisticAcquisitionRunning)();
    CAPL_FUNC(short, isTimerActive)(const capl::ITimer&);
    CAPL_FUNC(DWORD, keypressed)();
    CAPL_FUNC(double, MessageTimeNS)(capl::IMessage&);
    CAPL_FUNC(void, msgBeep)(CAPL_INT32);

    CAPL_FUNC(DWORD, openFileRead_Internal)(const char*, DWORD, const char*, const char*);
    CAPL_FUNC(DWORD, openFileWrite_Internal)(const char*, DWORD, const char*, const char*);

    CAPL_FUNC(void, DeleteControlContent)(const char*, const char*);
    CAPL_FUNC(void, putValueToControlString)(const char*, const char*, const char*);
    CAPL_FUNC(void, putValueToControlInt)(const char*, const char*, CAPL_INT32);
    CAPL_FUNC(void, putValueToControlIntParagraph)(const char*, const char*, CAPL_INT32, CAPL_INT32);
    CAPL_FUNC(void, putValueToControlIntHex)(const char*, const char*, CAPL_INT32, CAPL_INT32, CAPL_INT32);
    CAPL_FUNC(void, putValueToControlDouble)(const char*, const char*, double);
    CAPL_FUNC(void, putValueToControlDoubleParagraph)(const char*, const char*, double, CAPL_INT32);
    CAPL_FUNC(void, putValueToControlObj)(const char*, const char*, capl::IMessage&);
    CAPL_FUNC(void, putValueToControlObjParagraph)(const char*, const char*, capl::IMessage&, CAPL_INT32);
    CAPL_FUNC(void, putValueToControlObjHex)(const char*, const char*, capl::IMessage&, CAPL_INT32, CAPL_INT32);

    CAPL_FUNC(void, setPictureBoxImage)(const char*, const char*, const char*);

    CAPL_FUNC(void, resetCan)();
    CAPL_FUNC(void, ResetCanEx)(CAPL_INT32);
    CAPL_FUNC(void, runError)(CAPL_INT32, CAPL_INT32);
    CAPL_FUNC(CAPL_INT32, ScanBaudrateActive)(DWORD, DWORD, double, double, DWORD);
    CAPL_FUNC(CAPL_INT32, ScanBaudratePassive)(DWORD, DWORD, double, double, DWORD, DWORD);
    CAPL_FUNC(CAPL_INT32, setCanCabsMode)(CAPL_INT32, CAPL_INT32, CAPL_INT32, CAPL_INT32);

    CAPL_FUNC(void, setDrift)(short);
    CAPL_FUNC(void, setJitter)(short, short);
    CAPL_FUNC(void, setLogFileName)(const char*);
    CAPL_FUNC(void, setOcr)(CAPL_INT32, BYTE);
    CAPL_FUNC(void, setPostTrigger)(CAPL_INT32);
    CAPL_FUNC(void, setPreTrigger)(CAPL_INT32);
    CAPL_FUNC(void, setStartdelay)(short);
    CAPL_FUNC(void, setWriteDbgLevel)(WORD);

    CAPL_FUNC(void, SetControlPropertyString)(const char*, const char*, const char*, const char*);
    CAPL_FUNC(void, SetControlPropertyInt)(const char*, const char*, const char*, CAPL_INT32);
    CAPL_FUNC(void, SetControlPropertyDouble)(const char*, const char*, const char*, double);

    CAPL_FUNC(void, setTimer2P)(ITimer& timer, CAPL_INT32 time);
    CAPL_FUNC(void, setTimer3P)(ITimer& timer, CAPL_INT32 time, CAPL_INT32 time2);
    CAPL_FUNC(void, setTimerCyclic2P)(ITimer& timer, CAPL_INT32 period);
    CAPL_FUNC(void, setTimerCyclic3P)(ITimer& timer, CAPL_INT32 firstDuration, CAPL_INT32 period);
    CAPL_FUNC(void, startLogging)();
    CAPL_FUNC(void, startLogging1P)(const char*);
    CAPL_FUNC(void, startLogging2P)(const char*, CAPL_INT32);
    CAPL_FUNC(void, stopLogging)();
    CAPL_FUNC(void, stopLogging1P)(const char*);
    CAPL_FUNC(void, stopLogging2P)(const char*, CAPL_INT32);

    CAPL_FUNC(DWORD, StartMacroFile)(const char*);
    CAPL_FUNC(DWORD, StartReplayFile)(const char*);
    CAPL_FUNC(void, startStatisticAcquisition)();

    CAPL_FUNC(void, StopMacroFile)(DWORD);
    CAPL_FUNC(void, StopReplayFile)(DWORD);
    CAPL_FUNC(void, stopStatisticAcquisition)();
    CAPL_FUNC(void, sysExit)();
    CAPL_FUNC(void, sysMinimize)();

    CAPL_FUNC(CAPL_INT32, timeDiff)(capl::IMessage&, capl::IMessage&);
    CAPL_FUNC(DWORD, timeNow)();
    CAPL_FUNC(double, timeNowFloat)();
    CAPL_FUNC(double, timeNowNS)();
    CAPL_FUNC(CAPL_INT64, timeNowInt64)();
    CAPL_FUNC(CAPL_INT32, timeToElapse)(const capl::ITimer&);

    CAPL_FUNC(void, traceSetEventColors)(capl::IMessage&);
    CAPL_FUNC(void, trigger)();
    CAPL_FUNC(void, triggerEx)(const char*);

    CAPL_FUNC(DWORD, writeCreate)(const char*);
    CAPL_FUNC(void, writeDestroy)(DWORD);
    CAPL_FUNC(void, writeClear)(DWORD);
    CAPL_FUNC(void, write)(const char*, ...);
    CAPL_FUNC(void, writeEx)(DWORD, DWORD, const char*, ...);
    CAPL_FUNC(void, writeLineEx)(DWORD, DWORD, const char*, ...);

    CAPL_FUNC(void, writeTextBkgColor)(DWORD, DWORD, DWORD, DWORD);
    CAPL_FUNC(void, writeTextColor)(DWORD, DWORD, DWORD, DWORD);
    CAPL_FUNC(void, writeToLog)(const char*);
    CAPL_FUNC(void, writeToLogEx)(const char*);
    CAPL_FUNC(CAPL_INT32, writeDbgLevel)(CAPL_UINT32 priority, const char* format, ...);

    CAPL_FUNC(bool, validatePlugin)();
    // Swapt Bytes Intel/Motorola.
    CAPL_FUNC(WORD, swapWord)(WORD u16In);
    CAPL_FUNC(SHORT, swapInt)(SHORT i16In);
    CAPL_FUNC(DWORD, swapDWord)(DWORD u32In);
    CAPL_FUNC(CAPL_UINT64, swapQWord)(CAPL_UINT64 u64In);
    CAPL_FUNC(CAPL_INT32, swapLong)(CAPL_INT32 i32In);
    CAPL_FUNC(CAPL_INT64, swapInt64)(CAPL_INT64 i64In);

    // Zugriff auf Funktionen des Netzwerkmanagement.
    typedef void (NMHighFunction)(void);
    typedef void (NMHighRemoteFunction)(CAPL_INT32);
    typedef void (NMHighStateChangeFunction)(CAPL_INT32, CAPL_INT32);

    CAPL_FUNC(void, NMH_Init)(const char* sBusRef, const char* sEcuName, NMHighFunction* pSleep, NMHighFunction* pWakeup, NMHighFunction* pPrepareSleep, NMHighRemoteFunction* pRemote, NMHighStateChangeFunction* pStateChange, NMHighFunction* pStartInd);
    CAPL_FUNC(void, NMH_DeInit)(const char* sBusRef, const char* sEcuName);

    CAPL_FUNC(void, NMH_SetUserDataInternal)(const char* sBusRef, const char* sEcuName, const BYTE* pData, BYTE length);
    CAPL_FUNC(void, NMH_RequestBusComInternal)(const char* sBusRef, const char* sEcuName);
    CAPL_FUNC(void, NMH_ReleaseBusComInternal)(const char* sBusRef, const char* sEcuName);
    CAPL_FUNC(CAPL_INT32, NMH_PassiveStartUp)(const char* sBusRef, const char* sEcuName);

    CAPL_FUNC(CAPL_INT32, NMH_GetStationAddrInternal)(const char* sBusRef, const char* sEcuName);

    CAPL_FUNC(void, OSEKNM_Init)();
    CAPL_FUNC(void, OSEKNM_DeInit)();
    CAPL_FUNC(void, OSEKNM_DNMPutCmd)(const char* sBusStringIds, DWORD flag);
    CAPL_FUNC(void, OSEKNM_TalkNM)(const char* sBusStringIds);
    CAPL_FUNC(void, OSEKNM_SilentNM)(const char* sBusStringIds);
    CAPL_FUNC(void, OSEKNM_GotoMode_BusSleep)(const char* sBusStringIds);
    CAPL_FUNC(void, OSEKNM_GotoMode_Awake)(const char* sBusStringIds);

    CAPL_FUNC(CAPL_INT32, linSendWakeupInternal)(const char* sBusStringIds, CAPL_INT32 ttobrk, CAPL_INT32 count, CAPL_UINT32 length);

    CAPL_FUNC(CAPL_INT32, linChangeSchedTableInternal)(const char* sBusStringIds, CAPL_UINT32 tableIndex, CAPL_UINT32 slotIndex, CAPL_UINT32 onSlotIndex);

    CAPL_FUNC(CAPL_INT32, linSendSleepModFrmInternal)(const char* sBusStringIds, CAPL_INT32 silent, CAPL_INT32 restartScheduler, CAPL_INT32 wakeupIdentifier);
        
    CAPL_FUNC(CAPL_UINT32, linGetProtectedID)(CAPL_INT32 frameID);

    CAPL_FUNC(CAPL_INT32, IsRunningOnRemoteKernel)();

    CAPL_FUNC(CAPL_INT32, strtoul)(const char* s, CAPL_UINT32& result);
    CAPL_FUNC(CAPL_INT32, strtoul_index)(const char* s, CAPL_UINT32 startIndex, CAPL_UINT32& result);
    CAPL_FUNC(CAPL_INT32, strtoull)(const char* s, unsigned __int64& result);
    CAPL_FUNC(CAPL_INT32, strtoull_index)(const char* s, CAPL_UINT32 startIndex, unsigned __int64& result);
    CAPL_FUNC(CAPL_INT32, strstr_regex)(const char* s, const char* pattern);
    CAPL_FUNC(CAPL_INT32, strstr_regex_off)(const char* s, long offset, const char* pattern);
    CAPL_FUNC(CAPL_INT32, str_replace)(char* s, const char* searched, const char* replacement);
    CAPL_FUNC(CAPL_INT32, str_replace_off)(char* s, long startoffset, const char* replacement, long length);

    CAPL_FUNC(CAPL_INT32, getConfigurationName)(char buffer[], CAPL_UINT32 bufferLength);

    CAPL_FUNC(CAPL_INT32, convertTimestamp)(CAPL_UINT32 timestamp, CAPL_UINT32& days, CAPL_UINT8& hours, CAPL_UINT8& minutes, CAPL_UINT8& seconds, CAPL_UINT16& milliSeconds, CAPL_UINT16& microSeconds);
    CAPL_FUNC(CAPL_INT32, convertTimestampNS)(CAPL_UINT64 timestamp, CAPL_UINT32& days, CAPL_UINT8& hours, CAPL_UINT8& minutes, CAPL_UINT8& seconds, CAPL_UINT16& milliSeconds, CAPL_UINT16& microSeconds, CAPL_UINT16& nanoSeconds);


    CAPL_FUNC(CAPL_INT32, str_match_regex)(char s[], char pattern[]);

    CAPL_FUNC(void, _gcvtInternal)(double val, CAPL_INT32 digits, char* s, CAPL_INT32 length);
    template<class T>
    void _gcvt(double val, CAPL_INT32 digits, T& s)
    {
        _gcvtInternal(val, digits, s, sizeof(s));
    }

    CAPL_FUNC(CAPL_INT8, toUpperChar)(CAPL_INT8 c);
    CAPL_FUNC(CAPL_UINT32, toUpperInternal)(CAPL_INT8 dest[], CAPL_INT8 source[], CAPL_UINT32 bufferSize, CAPL_UINT32 sourceSize);
    template<class T>
    CAPL_UINT32 toUpper(CAPL_INT8 dest[], T source, CAPL_UINT32 bufferSize)
    {
        return toUpperInternal(dest, source, bufferSize, sizeof(source));
    }

    CAPL_FUNC(CAPL_UINT32, interpretAsDword)(float x);
    CAPL_FUNC(float, interpretAsFloat)(CAPL_UINT32 x);
    CAPL_FUNC(CAPL_UINT64, interpretAsQword)(double x);
    CAPL_FUNC(double, interpretAsDouble)(CAPL_UINT64 x);

    CAPL_FUNC(CAPL_UINT32, interpretAsDwordByObj)(IEnvVar& x);
    CAPL_FUNC(float, interpretAsFloatByObj)(IEnvVar& x);
    CAPL_FUNC(CAPL_UINT64, interpretAsQwordByObj)(IEnvVar& x);
    CAPL_FUNC(double, interpretAsDoubleByObj)(IEnvVar& x);

    ////////////////
    // Wiedergabe //
    ////////////////

    // Returns the state of the Replay block with the name pName.
    // Parameter: 
    //    pName, Name of the Replay block
    // Return:
    //    0: Replay Block is stopped (state: stopped)
    //    1: Execution of the Replay file was started (state: running)
    //    2: Execution of the Replay file was stopped (state: suspended)
    CAPL_FUNC(DWORD, ReplayState)(const char* pName);

    // Starts the Replay block with the name pName.
    // Parameter:
    //    pName, Name of the Replay block
    // Return:
    //    1: If successful
    //    0: If the Replay block does not exist or cannot be restarted
    CAPL_FUNC(DWORD, ReplayStart)(const char* pName);

    // Stops the Replay block with the name pName.                
    // Parameter:
    //    pName, Name of the Replay block.                                                                                               

    // Return:
    //    1: If successful                                                                                                      

    //    0: If the Replay block does not exist or cannot be restarted
    CAPL_FUNC(DWORD, ReplayStop)(const char* pName);

    // Suspends the Replay block with the name pName.
    // Parameter:
    //    pName, Name of the Replay block
    // Return:
    //    1: If successful
    //    0: If the Replay block does not exist or cannot be restarted
    CAPL_FUNC(DWORD, ReplaySuspend)(const char* pName);

    // Starts the Replay block with the name pName after it is suspended by ReplaySuspend.
    // Parameter:
    //   pName, Name of the Replay block.
    // Return:
    //   1: If successful
    //   0: If the Replay block does not exist or cannot be restarted
    CAPL_FUNC(DWORD, ReplayResume)(const char* pName);

    //////////////////////////////////
    // Interaction Layer (TrsCtrl2) //
    //////////////////////////////////

    // Initialisiert die IL.
    CAPL_FUNC(CAPL_INT32, ILControlInitInternal)(LPCSTR sEcuRef);
    CAPL_FUNC(CAPL_INT32, ILControlStartInternal)(LPCSTR sEcuRef);
    // Signaländerungen werden verworfen
    CAPL_FUNC(CAPL_INT32, ILControlStopInternal)(LPCSTR sEcuRef);
    // Signaländerungen werden gemerkt
    CAPL_FUNC(CAPL_INT32, ILControlWaitInternal)(LPCSTR sEcuRef);
    // Kl15 on
    CAPL_FUNC(CAPL_INT32, ILActivateClamp15Internal)(LPCSTR sEcuRef);
    // Kl15 off
    CAPL_FUNC(CAPL_INT32, ILDeactivateClamp15Internal)(LPCSTR sEcuRef);

    CAPL_FUNC(CAPL_INT32, ILControlSimulationOnInternal)(LPCSTR sEcuRef);
    CAPL_FUNC(CAPL_INT32, ILControlSimulationOffInternal)(LPCSTR sEcuRef);
    CAPL_FUNC(CAPL_INT32, ILSetMsgEventInternal)(LPCSTR sMsgRef);
    CAPL_FUNC(CAPL_INT32, ILFaultInjectionEnableMsg)(LPCSTR sMsgRef);
    CAPL_FUNC(CAPL_INT32, ILFaultInjectionDisableMsg)(LPCSTR sMsgRef);



    /////////////////////////////
    // Diagnose-Schnittstellen //
    /////////////////////////////

    CAPL_FUNC(CAPL_INT32, diagInitialize)(capl::IDiagBase& request);
    CAPL_FUNC(CAPL_INT32, diagInitializeQualivier)(capl::IDiagBase& request, char serviceQualifier[]);
    CAPL_FUNC(CAPL_INT32, diagInitializePrimitive)(capl::IDiagResponse& request, char serviceQualifier[], char primitiveQualifier[]);

    CAPL_FUNC(CAPL_INT32, diagSetParameterDouble)(capl::IDiagBase&, char parameterName[], double newValue);
    CAPL_FUNC(CAPL_INT32, diagSetParameterDoubleMode)(capl::IDiagBase&, CAPL_INT32 mode, char parameterName[], double newValue);
    CAPL_FUNC(CAPL_INT32, diagSetParameterString)(capl::IDiagBase&, char parameterName[], char newValue[]);

    CAPL_FUNC(CAPL_INT32, diagGetParameterDoubleParam)(capl::IDiagBase&, char parameterName[], double output[1]);
    CAPL_FUNC(double, diagGetParameterDouble)(capl::IDiagBase&, char parameterName[]);
    CAPL_FUNC(CAPL_INT32, diagGetParameterDoubleParamMode)(capl::IDiagBase&, CAPL_INT32 mode, char parameterName[], double output[1]);
    CAPL_FUNC(double, diagGetParameterDoubleMode)(capl::IDiagBase&, CAPL_INT32 mode, char parameterName[]);
    CAPL_FUNC(CAPL_INT32, diagGetParameterString)(capl::IDiagBase&, char parameterName[], char buffer[], CAPL_UINT32 buffersize);

    CAPL_FUNC(CAPL_INT32, diagSendRequest)(capl::IDiagRequest&);

    CAPL_FUNC(CAPL_INT32, diagSendResponse)(capl::IDiagResponse&);

    CAPL_FUNC(CAPL_INT32, diagGetPrimitiveData)(capl::IDiagBase&, BYTE buffer[], CAPL_UINT32 buffersize);
    CAPL_FUNC(CAPL_INT32, diagSetPrimitiveData)(capl::IDiagBase&, BYTE buffer[], CAPL_UINT32 buffersize);

    CAPL_FUNC(CAPL_INT32, diagGetPrimitiveSize)(capl::IDiagBase&);

    CAPL_FUNC(CAPL_INT32, diagIsNegativeResponse)(capl::IDiagResponse&);
    CAPL_FUNC(CAPL_INT32, diagIsPositiveResponse)(capl::IDiagResponse&);

    CAPL_FUNC(CAPL_INT32, diagGetResponseCode)(capl::IDiagResponse&);
    CAPL_FUNC(CAPL_INT32, diagGetLastResponseCode)(capl::IDiagRequest&);

    CAPL_FUNC(CAPL_INT32, diagGetLastResponse)(capl::IDiagResponse&);

    CAPL_FUNC(CAPL_INT32, diagGetParameterRaw)(capl::IDiagBase&, char parameter[], BYTE buffer[], CAPL_UINT32 buffersize);
    CAPL_FUNC(CAPL_INT32, diagSetParameterRaw)(capl::IDiagBase&, char parameter[], BYTE buffer[], CAPL_UINT32 buffersize);

    CAPL_FUNC(CAPL_INT32, testWaitForDiagResponse)(capl::IDiagRequest&, CAPL_UINT32 timeout);



    typedef void (OnDiagTiemoutFunction)();
    CAPL_FUNC(CAPL_INT32, diagSetTimeoutHandlerInternal)(OnDiagTiemoutFunction* pCallback, char stackName[]);
    CAPL_FUNC(CAPL_INT32, diagSetTimeoutHandlerObjInternal)(OnDiagTiemoutFunction* pCallback, capl::IDiagRequest&);

    CAPL_FUNC(CAPL_INT32, diagStartTesterPresentObj)(char ecuQualifier[]);

    CAPL_FUNC(CAPL_INT32, diagTestStack)(char stackvar[], char stackattr[]);

    /////////////////////////////
    //   Test-Schnittstellen   //
    /////////////////////////////

    CAPL_FUNC(CAPL_INT32, TestStepLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 1
    CAPL_FUNC(CAPL_INT32, TestStepHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 2
    CAPL_FUNC(CAPL_INT32, TestStep)(const char Identifier[], const char Description[], ...); // form 3
    CAPL_FUNC(CAPL_INT32, TestStepPassLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 4
    CAPL_FUNC(CAPL_INT32, TestStepPassHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 5
    CAPL_FUNC(CAPL_INT32, TestStepPassIdentifier)(const char Identifier[], const char Description[], ...); // form 6
    CAPL_FUNC(CAPL_INT32, TestStepPassDescription)(const char Description[]); // form 7
    CAPL_FUNC(CAPL_INT32, TestStepPass)(); // form 8
    CAPL_FUNC(CAPL_INT32, TestStepFailLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 9
    CAPL_FUNC(CAPL_INT32, TestStepFailHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // (10)
    CAPL_FUNC(CAPL_INT32, TestStepFailIdentifier)(const char Identifier[], const char Description[], ...); // form 11
    CAPL_FUNC(CAPL_INT32, TestStepFailDescription)(const char Description[]); // form 12
    CAPL_FUNC(CAPL_INT32, TestStepFail)(); // form 13
    CAPL_FUNC(CAPL_INT32, TestStepWarningLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 14
    CAPL_FUNC(CAPL_INT32, TestStepWarningHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 15
    CAPL_FUNC(CAPL_INT32, TestStepWarningIdentifier)(const char Identifier[], const char Description[], ...); // form 16
    CAPL_FUNC(CAPL_INT32, TestStepWarningDescription)(const char Description[]); // form 17
    CAPL_FUNC(CAPL_INT32, TestStepWarning)(); // form 18
    CAPL_FUNC(CAPL_INT32, TestStepInconclusiveLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 19
    CAPL_FUNC(CAPL_INT32, TestStepInconclusiveHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 20
    CAPL_FUNC(CAPL_INT32, TestStepInconclusiveIdentifier)(const char Identifier[], const char Description[], ...); // form 21
    CAPL_FUNC(CAPL_INT32, TestStepInconclusiveDescription)(const char Description[]); // form 22
    CAPL_FUNC(CAPL_INT32, TestStepInconclusive)(); // form 23
    CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystemLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 24
    CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystemHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 25
    CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystemIdentifier)(const char Identifier[], const char Description[], ...); // form 26
    CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystemDescription)(const char Description[]); // form 27
    CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystem)(); // form 28

    CAPL_FUNC(CAPL_INT32, TestWaitForTimeout)(CAPL_UINT32 aTimeout);
    CAPL_FUNC(CAPL_INT32, TestWaitForTesterConfirmation)(const char text[]);
    CAPL_FUNC(CAPL_INT32, TestWaitForTesterConfirmationTimeout)(const char text[], CAPL_UINT32 timeout);
    CAPL_FUNC(CAPL_INT32, TestWaitForTesterConfirmationHeading)(const char text[], CAPL_UINT32 timeout, const char heading[], const char resource[], const char resourceCaption[]);

    CAPL_FUNC(CAPL_INT32, TestStepBeginImportance)(CAPL_UINT32 Importance, const char Identifier[], const char Description[]);
    CAPL_FUNC(CAPL_INT32, TestStepBegin)(const char Identifier[], const char Description[]);

    CAPL_FUNC(CAPL_INT32, TestWaitForSignalMatchSig)(const ISignalVal& aSignal, float aCompareValue, CAPL_UINT32 aTimeout); // form 1
    CAPL_FUNC(CAPL_INT32, TestWaitForSignalMatchEnv)(const IEnvVar& aEnvVar, float aCompareValue, CAPL_UINT32 aTimeout); // form 2
    CAPL_FUNC(CAPL_INT32, TestWaitForSignalMatchEnvI64)(const IEnvVar& aEnvVar, CAPL_INT64 aCompareValue, CAPL_UINT32 aTimeout); // form 4

    CAPL_FUNC(CAPL_INT32, TestWaitForSignalInRangeSig)(const ISignalVal& aSignal, float aLowLimit, float aHighLimit, CAPL_UINT32 aTimeout); // form 1
    CAPL_FUNC(CAPL_INT32, TestWaitForSignalInRangeEnv)(const IEnvVar& aEnvVar, float aLowLimit, float aHighLimit, CAPL_UINT32 aTimeout); // form 2
    CAPL_FUNC(CAPL_INT32, TestWaitForSignalInRangeEnvI64)(const IEnvVar& aEnvVar, CAPL_INT64 aLowLimit, CAPL_INT64 aHighLimit, CAPL_UINT32 aTimeout); // form 4

    CAPL_FUNC(CAPL_INT32, TestReportAddEngineerInfo)(const char name[], const char description[], ...);
    CAPL_FUNC(CAPL_INT32, TestReportAddSetupInfo)(const char name[], const char description[], ...);
    CAPL_FUNC(CAPL_INT32, TestReportAddSUTInfo)(const char name[], const char description[], ...);
    CAPL_FUNC(CAPL_INT32, TestReportAddMiscInfo)(const char name[], const char description[], ...);

    CAPL_FUNC(CAPL_INT32, TestModuleTitle)(const char title[]);

    CAPL_FUNC(CAPL_INT32, TestModuleDescription)(const char description[]);

    CAPL_FUNC(CAPL_INT32, TestSupplyTextEvent)(const char aText[]);
    CAPL_FUNC(CAPL_INT32, TestWaitForTextEvent)(const char aText[], CAPL_UINT32 aTimeout);

    CAPL_FUNC(CAPL_INT32, TestWaitForEnvVar)(const IEnvVar& aEnvVar, DWORD aTimeout);
    CAPL_FUNC(CAPL_INT32, TestWaitForEnvVarName)(const char aEnvVarName[], DWORD aTimeout);
    CAPL_FUNC(CAPL_INT32, TestWaitForSysVar)(const IEnvVar& aEnvVar, DWORD aTimeout);
    CAPL_FUNC(CAPL_INT32, TestWaitForSysVarName)(const char aEnvVarName[], DWORD aTimeout);
    CAPL_FUNC(CAPL_INT32, TestWaitForMessage)(const char sRef[], DWORD aTimeout);
    CAPL_FUNC(CAPL_INT32, TestWaitForMessageIdInternal)(const char channelName[], DWORD aMessageId, DWORD aTimeout);
    CAPL_FUNC(CAPL_INT32, TestWaitForMessageAllInternal)(const char channelName[], DWORD aTimeout);

    CAPL_FUNC(CAPL_INT32, TestCaseTitle)(const char identifier[], const char title[]);
    CAPL_FUNC(CAPL_INT32, TestCaseDescription)(const char desc[]);

    CAPL_FUNC(CAPL_INT32, TestCaseComment)(const char aComment[]);
    CAPL_FUNC(CAPL_INT32, TestCaseCommentMessage)(const char aComment[], const IMessage& aMsg);
    CAPL_FUNC(CAPL_INT32, TestCaseCommentStr)(const char aComment[], const char aRawString[]);

    CAPL_FUNC(CAPL_INT32, TestReportFileName)(const char name[]);

    CAPL_FUNC(CAPL_INT32, TestJoinMessageEvent)(const char messageStringRef[]);
    CAPL_FUNC(CAPL_INT32, TestJoinMessageEventIdInternal)(const char channelName[], DWORD aMessageId);
    CAPL_FUNC(CAPL_INT32, TestJoinEnvVarEvent)(const IEnvVar& aEnvVar);
    CAPL_FUNC(CAPL_INT32, TestJoinTextEvent)(const char aText[]);
    CAPL_FUNC(CAPL_INT32, TestWaitForAnyJoinedEvent)(DWORD aTimeout);
    CAPL_FUNC(CAPL_INT32, TestWaitForAllJoinedEvents)(DWORD aTimeout);

    CAPL_FUNC(CAPL_INT32, TestGroupBegin)(const char title[], const char description[]);
    CAPL_FUNC(CAPL_INT32, TestGroupBeginIdent)(const char ident[], const char title[], const char description[]);

    CAPL_FUNC(CAPL_INT32, TestGroupEnd)();

    CAPL_FUNC(CAPL_INT32, TestGetWaitEventMsgData)(IMessage& aMsg);
    CAPL_FUNC(CAPL_INT32, TestGetWaitEventMsgDataIndex)(DWORD index, IMessage& aMsg);


    CAPL_FUNC(CAPL_INT32, DeferStopInternal)(LPCSTR busContext, DWORD maxDeferTime);
    CAPL_FUNC(void, CompleteStopInternal)(LPCSTR busContext);


    // CanTp-Funktionen
    CAPL_FUNC(CAPL_HANDLE, CanTpCreateConnectionInternal)(LPCSTR sBusRef, CAPL_INT32 mode, Transmission_Function* pSendConf, Transmission_Function* pReceptionInd);
    CAPL_FUNC(CAPL_INT32, CanTpCloseConnection)(CAPL_INT32 hConnection);
    CAPL_FUNC(CAPL_INT32, CanTpSetTxIdentifier)(CAPL_INT32 connection, CAPL_UINT32 txId);
    CAPL_FUNC(CAPL_INT32, CanTpSetRxIdentifier)(CAPL_INT32 connection, CAPL_UINT32 rxId);
    CAPL_FUNC(CAPL_INT32, CanTpSendData)(CAPL_INT32 connection, const CAPL_UINT8* pBuffer, CAPL_UINT32 nBytes);
    CAPL_FUNC(CAPL_INT32, ShowToManyConnectionsMessageBox)(LPCSTR nodeName, LPCSTR fileName);

    // Initialisiert die Diagnose
    CAPL_FUNC(CAPL_HANDLE, AIDA_InitInternal)(LPCSTR sBusRef, LPCSTR sConfig);


    // Deinitialisiert die Diagnose
    CAPL_FUNC(void, AIDA_DeInitInternal)(CAPL_HANDLE hStack);


    // Sendet eine Diagnose-Anforderung.
    CAPL_FUNC(void, AIDA_DataReqInternal)(CAPL_HANDLE hStack, const BYTE* pData, CAPL_INT32 lBufferSize);

    // Holt die empfangenen Daten von der DLL ab.
    CAPL_FUNC(void, AIDA_GetRxDataInternal)(CAPL_HANDLE hStack, BYTE* pData, CAPL_INT32 lBufferSize);

    // Spezifiziert die Sende-ID, auf der CanEasy Diagnose-Kommandos absetzt.
    CAPL_FUNC(void, OSEKTL_SetTxIdInternal)(CAPL_HANDLE hStack, DWORD txId);

    // Gibt die Empfangs-ID an, auf der CanEasy die angeforderten Diagnose-Daten empfangen soll.
    CAPL_FUNC(void, OSEKTL_SetRxIdInternal)(CAPL_HANDLE hStack, DWORD rxId);

    // Liefert die Sende-ID.
    CAPL_FUNC(CAPL_INT32, OSEKTL_GetTxIdInternal)(CAPL_HANDLE hStack);

    // Liefert die Empfangs-ID.
    CAPL_FUNC(CAPL_INT32, OSEKTL_GetRxIdInternal)(CAPL_HANDLE hStack);

    // Toggles to normal addressing
    CAPL_FUNC(void, OSEKTL_SetNrmlModeInternal)(CAPL_HANDLE hStack);

    // Toggles to mixed addressing
    CAPL_FUNC(void, OSEKTL_SetMixedModeInternal)(CAPL_HANDLE hStack);

    // Toggles to extended addressing
    CAPL_FUNC(void, OSEKTL_SetExtModeInternal)(CAPL_HANDLE hStack);

    CAPL_FUNC(void, OSEKTL_UseExtIdInternal)(CAPL_HANDLE hStack, BYTE val);
    CAPL_FUNC(void, OSEKTL_SetEcuNumberInternal)(CAPL_HANDLE hStack, BYTE val);
    CAPL_FUNC(void, OSEKTL_SetBSInternal)(CAPL_HANDLE hStack, BYTE val);
    CAPL_FUNC(void, OSEKTL_SetSTMINInternal)(CAPL_HANDLE hStack, BYTE val);
    CAPL_FUNC(void, OSEKTL_SetTgtAdrInternal)(CAPL_HANDLE hStack, CAPL_INT32 val);
    CAPL_FUNC(void, OSEKTL_SetUseFCInternal)(CAPL_HANDLE hStackm, BYTE val);
    CAPL_FUNC(void, OSEKTL_SetDLC8Internal)(CAPL_HANDLE hStack);
    CAPL_FUNC(void, OSEKTL_SetEvalAllFCInternal)(CAPL_HANDLE hStack);
    CAPL_FUNC(void, OSEKTL_SetTimeoutArInternal)(CAPL_HANDLE hStack, WORD timeout);
    CAPL_FUNC(void, OSEKTL_SetTimeoutAsInternal)(CAPL_HANDLE hStack, WORD timeout);
    CAPL_FUNC(void, OSEKTL_SetTimeoutBsInternal)(CAPL_HANDLE hStack, WORD timeout);
    CAPL_FUNC(void, OSEKTL_SetTimeoutCrInternal)(CAPL_HANDLE hStack, WORD timeout);

    // Callback-Funktionen für die RS232-Schnittstelle.
    typedef void(_cdecl R232ReceivedFunc)(CAPL_UINT32 port, const BYTE* pBuffer, CAPL_UINT32 size);
    typedef void(_cdecl R232ByteCallbackFunc)(CAPL_UINT32 port, CAPL_UINT32 datum, CAPL_UINT32 note);
    typedef void(_cdecl R232ByteCallbackFuncInt)(CAPL_INT32 port, CAPL_INT32 datum, CAPL_INT32 note);
    typedef void(_cdecl RS232OnErrorFunc)(CAPL_UINT32 port, CAPL_UINT32 errorFlags);
    typedef void(_cdecl RS232OnSendFunc)(CAPL_UINT32 port, const BYTE* pBuffer, CAPL_UINT32 size);

    // Funktionen für die RS232-Schnittstelle.
    CAPL_FUNC(DWORD, RS232OpenInternal)(DWORD port, RS232OnSendFunc* pRRS232OnSendFunc, R232ByteCallbackFunc* pByteCallbackFunc, RS232OnErrorFunc* pOnErrorFunc);
    CAPL_FUNC(DWORD, RS232OpenInternalInt)(DWORD port, RS232OnSendFunc* pRRS232OnSendFunc, R232ByteCallbackFuncInt* pByteCallbackFunc, RS232OnErrorFunc* pOnErrorFunc);
    CAPL_FUNC(DWORD, RS232Send)(DWORD port, const BYTE* pBuffer, DWORD number);
    CAPL_FUNC(DWORD, RS232WriteByte)(DWORD port, DWORD datum);
    CAPL_FUNC(DWORD, RS232WriteBlock)(DWORD port, const BYTE* pBuffer, DWORD number);
    CAPL_FUNC(DWORD, RS232Close)(DWORD port);
    CAPL_FUNC(DWORD, RS232CloseHandle)(DWORD port);
    CAPL_FUNC(DWORD, RS232ReceiveInternal)(DWORD port, BYTE* pBuffer, DWORD size, R232ReceivedFunc* pReceiveFunc);
    CAPL_FUNC(DWORD, RS232Configure)(DWORD port, DWORD baudrate, DWORD numberOfDataBits, DWORD numberOfStopBits, DWORD parity);
    CAPL_FUNC(DWORD, RS232SetSignalLine)(DWORD port, DWORD line, DWORD state);
    CAPL_FUNC(DWORD, RS232EscapeCommExt)(DWORD modemControl, DWORD port);
    CAPL_FUNC(DWORD, RS232EscapeCommFunc)(DWORD modemControl);
    CAPL_FUNC(DWORD, RS232SetHandshakeInternal)(DWORD port, DWORD handshake, DWORD XonLimit, DWORD XoffLimit, DWORD XonChar, DWORD XoffChar, DWORD timeout);

    // Benachrichtigt alle Node-Dlls, dass eine Ecu aktiv/inaktiv geschaltet wurde.
    CAPL_FUNC(void, NofityEcuStateChange)(LPCSTR ecuRef, bool boState);

    CAPL_FUNC(CAPL_INT32, sysExec)(const char* cmd, const char* params);
    CAPL_FUNC(CAPL_INT32, sysExecDir)(const char* cmd, const char* params, const char* directory);
    CAPL_FUNC(CAPL_INT32, sysExecCmd)(const char* cmd, const char* params);
    CAPL_FUNC(CAPL_INT32, sysExecCmdDir)(const char* cmd, const char* params, const char* directory);

    // Ethernet
    /*! \brief Brief description.
    *
    * \param ipv6Address  16 bytes
    */
    CAPL_FUNC(DWORD, IpGetAddressAsArray)(const char address[], BYTE ipv6Address[]/** ddd df s sf*/); // form 1
    CAPL_FUNC(DWORD, IpGetAddressAsNumber)(const char address[]); // form 1
    CAPL_FUNC(CAPL_INT32, IpGetAddressAsString)(DWORD numericAddress, char address[], DWORD count); // form 1
    CAPL_FUNC(CAPL_INT32, IpGetAddressAsStringIP6)(byte ipv6Address[], char address[], DWORD count); // form 2
    CAPL_FUNC(CAPL_INT32, IpGetLastError)(); // form 1
    CAPL_FUNC(CAPL_INT32, IpGetLastSocketError)(DWORD socket); // form 1
    CAPL_FUNC(CAPL_INT32, IpGetLastSocketErrorAsString)(DWORD socket, char text[], DWORD count); // form 1
    CAPL_FUNC(DWORD, IpGetAdapterCountInternal)(const char sNode[]);
    CAPL_FUNC(CAPL_INT32, IpGetAdapterAddressCountInternal)(const char sNode[], DWORD ifIndex, DWORD addressFamily);
    CAPL_FUNC(CAPL_INT32, IpGetAdapterAddressIPv4Internal)(const char sNode[], DWORD ifIndex, DWORD address[], DWORD count);
    CAPL_FUNC(CAPL_INT32, IpGetAdapterAddressIPv6Internal)(const char sNode[], DWORD ifIndex, byte ipv6Addresses[][16], DWORD count);
    CAPL_FUNC(CAPL_INT32, IpAddAdapterAddressIPv4Internal)(const char sNode[], DWORD ifIndex, DWORD address, DWORD mask);
    CAPL_FUNC(CAPL_INT32, IpAddAdapterAddressIPv6Internal)(const char sNode[], DWORD ifIndex, byte ipv6Addresses[], DWORD prefix);
    CAPL_FUNC(CAPL_INT32, IpRemoveAdapterAddressIPv4Internal)(const char sNode[], DWORD ifIndex, DWORD address, DWORD mask);
    CAPL_FUNC(CAPL_INT32, IpRemoveAdapterAddressIPv6Internal)(const char sNode[], DWORD ifIndex, byte ipv6Addresses[], DWORD prefix);
    CAPL_FUNC(CAPL_INT32, IpGetAdapterDescriptionInternal)(const char sNode[], DWORD ifIndex, char name[], DWORD count);
    CAPL_FUNC(CAPL_INT32, IpSetMulticastInterfaceInternal)(const char sNode[], DWORD socket, DWORD ifIndex);
    CAPL_FUNC(CAPL_INT32, IpJoinMulticastGroupIP4Internal)(const char sNode[], DWORD socket, DWORD ifIndex, DWORD address);
    CAPL_FUNC(CAPL_INT32, IpJoinMulticastGroupIP6Internal)(const char sNode[], DWORD socket, DWORD ifIndex, byte ipv6Address[]);


    CAPL_FUNC(CAPL_INT32, EthInitPacket)(); // form 1
    CAPL_FUNC(CAPL_INT32, EthInitPacketProt)(const char protocolDesignator[]); // form 2
    CAPL_FUNC(CAPL_INT32, EthInitPacketPacket)(const char protocolDesignator[], const char packetTypeDesignator[]); // form 3
    // long EthInitPacket(struct srcMacAddressStruct, struct dstMacAddressStruct, long ethernetType); // form 4
    CAPL_FUNC(CAPL_INT32, EthInitPacketArray)(BYTE srcMacAddress[6], BYTE dstMacAddress[6], CAPL_INT32 ethernetType); // form 5
    CAPL_FUNC(CAPL_INT32, EthInitPacketCopy)(CAPL_INT32 packetToCopy); // form 6
    CAPL_FUNC(CAPL_INT32, EthInitPacketData)(CAPL_INT32 rawDataLength, BYTE rawData[]); // form 7
    CAPL_FUNC(CAPL_INT32, EthInitProtocol)(CAPL_INT32 packet, const char protocolDesignator[]); // form 1
    CAPL_FUNC(CAPL_INT32, EthInitProtocolType)(CAPL_INT32 packet, const char protocolDesignator[], const char packetTypeDesignator[]); // form 2
    CAPL_FUNC(CAPL_INT32, EthGetLastError)(void); // form 1
    CAPL_FUNC(CAPL_INT32, EthGetLastErrorText)(DWORD bufferSize, char buffer[]); // form 1
    CAPL_FUNC(CAPL_INT32, EthAddToken)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[]);
    CAPL_FUNC(CAPL_INT32, EthRemoveToken)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[]);
    CAPL_FUNC(CAPL_INT32, EthGetTokenLengthBit)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[]);
    CAPL_FUNC(CAPL_INT32, EthGetTokenInt)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[]);
    CAPL_FUNC(CAPL_INT32, EthGetTokenData)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[], CAPL_INT32 length, void* buffer);
    CAPL_FUNC(CAPL_INT32, EthSetTokenData)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[], CAPL_INT32 length, const void* data); // form 1, 2, 3
    CAPL_FUNC(CAPL_INT32, EthSetTokenDataOffset)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[], CAPL_INT32 byteOffset, CAPL_INT32 length, const void* data); // form 4, 5, 6
    CAPL_FUNC(CAPL_INT32, EthSetTokenInt)(CAPL_INT32 packet, const char protocolDesignatorl[], const char tokenDesignator[], CAPL_INT32 value); // form 1
    CAPL_FUNC(CAPL_INT32, EthSetTokenIntOffset)(CAPL_INT32 packet, const char protocolDesignatorl[], const char tokenDesignator[], CAPL_INT32 byteOffset, CAPL_INT32 length, CAPL_INT32 networkByteOrder, CAPL_INT32 value); // form 2
    CAPL_FUNC(CAPL_INT32, EthResizeToken)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[], CAPL_INT32 newBitLength); // form 1
    CAPL_FUNC(CAPL_INT32, EthSetTokenSignalValue)(CAPL_INT32 packet, ISignalVal& signal, double physValue); // form 1
    CAPL_FUNC(CAPL_INT32, EthSetTokenSignalValueIndex)(CAPL_INT32 packet, ISignalVal& signal, double physValue, DWORD index); // form 2
    CAPL_FUNC(CAPL_INT32, EthCompletePacket)(CAPL_INT32 packet); // form 1
    CAPL_FUNC(CAPL_INT32, EthOutputPacketInternal)(const char* sChannelRef, CAPL_INT32 packet); // form 1
    CAPL_FUNC(CAPL_INT32, EthOutputPacketFcs)(CAPL_INT32 packet, DWORD fcs); // form 2
    CAPL_FUNC(CAPL_INT32, EthReleasePacket)(CAPL_INT32 packet); // form 1
    CAPL_FUNC(QWORD, EthGetMacAddressAsNumber)(const char macAddrStr[]); // form 1
    CAPL_FUNC(CAPL_INT32, EthGetMacAddressAsString)(QWORD macAddr, char buffer[], DWORD bufferLength); // form 1

    CAPL_FUNC(CAPL_INT32, EthStartPacketGenerator)(IEthernetPacket& txPacket, DWORD transmissionRate);
    CAPL_FUNC(CAPL_INT32, EthStartPacketGeneratorNumberOfFrames)(IEthernetPacket& txPacket, DWORD transmissionRate, DWORD numberOfFrames);
    CAPL_FUNC(CAPL_INT32, EthStartPacketGeneratorCounterByteOffset)(IEthernetPacket& txPacket, DWORD transmissionRate, DWORD numberOfFrames, DWORD counterByteOffset);
    CAPL_FUNC(CAPL_INT32, EthStopPacketGenerator)(IEthernetPacket& txPacket);

    CAPL_FUNC(DWORD, TcpOpenIP4Internal)(const char sNode[], DWORD address, DWORD port);
    CAPL_FUNC(DWORD, TcpOpenIP6Internal)(const char sNode[], const byte ipv6Address[], DWORD port);

    CAPL_FUNC(CAPL_INT32, TcpConnectIP4)(CAPL_UINT32 socket, CAPL_UINT32 address, CAPL_UINT32 port);
    CAPL_FUNC(CAPL_INT32, TcpConnectIP6)(CAPL_UINT32 socket, CAPL_UINT8 address[], CAPL_UINT32 port);
    CAPL_FUNC(CAPL_INT32, TcpClose)(CAPL_UINT32 socket);
    CAPL_FUNC(CAPL_INT32, TcpAccept)(CAPL_UINT32 socket);
    CAPL_FUNC(CAPL_INT32, TcpListen)(CAPL_UINT32 socket);

    CAPL_FUNC(DWORD, UdpOpenIP4Internal)(const char sNode[], DWORD address, DWORD port); // form 1
    CAPL_FUNC(DWORD, UdpOpenIP6Internal)(const char sNode[], const byte ipv6Address[], DWORD port); // form 2
    CAPL_FUNC(CAPL_INT32, UdpClose)(DWORD socket);

    // Ethernet UDP-Callback-Funktionen.
    typedef void (OnSendFunction)(CAPL_UINT32 socket, CAPL_INT32 result, void* buffer, CAPL_UINT32 size);
    typedef void (OnUdpReceiveFromFunctionIPv4)(CAPL_UINT32 socket, CAPL_INT32 result, CAPL_UINT32 address, CAPL_UINT32 port, void* buffer, CAPL_UINT32 size);
    typedef void (OnUdpReceiveFromFunctionIPv6)(CAPL_UINT32 socket, CAPL_INT32 result, CAPL_UINT8 address[], CAPL_UINT32 port, void* buffer, CAPL_UINT32 size);
    typedef void (OnTcpReceiveFunctionIPv4)(CAPL_UINT32 socket, CAPL_INT32 result, CAPL_UINT32 address, CAPL_UINT32 port, void* buffer, CAPL_UINT32 size);
    typedef void (OnTcpReceiveFunctionIPv6)(CAPL_UINT32 socket, CAPL_INT32 result, CAPL_UINT8 address[], CAPL_UINT32 port, void* buffer, CAPL_UINT32 size);

    typedef void (OnEthernetFunction)(CAPL_INT32 channel, CAPL_INT32 dir, CAPL_INT32 packet);

    CAPL_FUNC(CAPL_INT32, EthReceivePacketInternal)(OnEthernetFunction* pCallback);
    CAPL_FUNC(CAPL_INT32, EthReceiveRawPacketInternal)(OnEthernetFunction* pCallback, CAPL_INT32 flags, CAPL_UINT8 srcMacAddress[], CAPL_UINT8 dstMacAddress[6], CAPL_INT32 ethernetType);
    CAPL_FUNC(CAPL_INT32, EthGetThisData)(CAPL_INT32 offset, CAPL_INT32 bufferSize, void* pBuffer);

    CAPL_FUNC(CAPL_INT32, TcpSendInternal)(OnSendFunction* pCallback, DWORD socket, const void* buffer, DWORD size);
    CAPL_FUNC(CAPL_INT32, UdpSendToIP4Internal)(OnSendFunction* pCallback, DWORD socket, DWORD address, DWORD port, const void* buffer, DWORD size); // form 1, 3, 4
    CAPL_FUNC(CAPL_INT32, UdpSendToIP6Internal)(OnSendFunction* pCallback, DWORD socket, const byte ipv6Address[], DWORD port, const void* buffer, DWORD size); // form 2, 5, 6
    CAPL_FUNC(CAPL_INT32, UdpReceiveFromInternal)(OnUdpReceiveFromFunctionIPv4* pCallbackIPv4, OnUdpReceiveFromFunctionIPv6* pCallbackIPv6, DWORD socket, void* buffer, DWORD size);
    CAPL_FUNC(CAPL_INT32, TcpReceiveInternal)(OnTcpReceiveFunctionIPv4* pCallbackIPv4, OnTcpReceiveFunctionIPv6* pCallbackIPv6, DWORD socket, void* buffer, DWORD size);

    CAPL_FUNC(CAPL_INT32, GetComputerNameInternal)(char buffer[], DWORD bufferSize);

    struct DBLookupSigRet
    {
        bool valid;
        CAPL_UINT32 bitstart;
        CAPL_UINT32 bitcount;
        double offset;
        double factor;
        char unit[50];
        double minimum;
        double maximum;
        CAPL_INT64 DefaultValue;
        CAPL_INT64 NotValidLowerLimit;
        CAPL_INT64 NotValidUpperLimit;
        char signalgroup[250];
        char txpdu[250];
        operator CAPL_UINT8()const
        {
            return valid ? 1 : 0;
        }
    };
    CAPL_FUNC(DBLookupSigRet&, DBLookupSig)(capl::ISignal& sig);

    struct DBLookupMsgRet
    {
        bool valid;
        char Name[250];
        CAPL_INT32 DLC;
        char Transmitter[250];
        operator CAPL_UINT8()const
        {
            return valid ? 1 : 0;
        }
    };
    CAPL_FUNC(DBLookupMsgRet&, DBLookupMsg)(capl::IMessage& msg);

    CAPL_FUNC(CAPL_INT32, setSignalStartValues)(capl::IMessage& msg);

    template<class T>
    CAPL_UINT8& Byte(T arr, size_t pos)
    {
        if (pos + sizeof(CAPL_UINT8) > sizeof(arr))
            RaiseException(EXCEPTION_ARRAY_BOUNDS_EXCEEDED, 0, 0, NULL);

        return *(CAPL_UINT8*)(((BYTE*)arr) + pos);
    }
    template<class T>
    CAPL_INT8& Char(T arr, size_t pos)
    {
        if (pos + sizeof(CAPL_INT8) > sizeof(arr))
            RaiseException(EXCEPTION_ARRAY_BOUNDS_EXCEEDED, 0, 0, NULL);

        return *(CAPL_INT8*)(((BYTE*)arr) + pos);
    }
    template<class T>
    CAPL_UINT16& Word(T arr, size_t pos)
    {
        if (pos + sizeof(CAPL_UINT16) > sizeof(arr))
            RaiseException(EXCEPTION_ARRAY_BOUNDS_EXCEEDED, 0, 0, NULL);

        return *(CAPL_UINT16*)(((BYTE*)arr) + pos);
    }
    template<class T>
    CAPL_INT16& Short(T arr, size_t pos)
    {
        if (pos + sizeof(CAPL_INT16) > sizeof(arr))
            RaiseException(EXCEPTION_ARRAY_BOUNDS_EXCEEDED, 0, 0, NULL);

        return *(CAPL_INT16*)(((BYTE*)arr) + pos);
    }
    template<class T>
    CAPL_UINT32& Dword(T arr, size_t pos)
    {
        if (pos + sizeof(CAPL_UINT32) > sizeof(arr))
            RaiseException(EXCEPTION_ARRAY_BOUNDS_EXCEEDED, 0, 0, NULL);

        return *(CAPL_UINT32*)(((BYTE*)arr) + pos);
    }
    template<class T>
    CAPL_INT32& Long(T arr, size_t pos)
    {
        if (pos + sizeof(CAPL_INT32) > sizeof(arr))
            RaiseException(EXCEPTION_ARRAY_BOUNDS_EXCEEDED, 0, 0, NULL);

        return *(CAPL_INT32*)(((BYTE*)arr) + pos);
    }
    template<class T>
    CAPL_UINT64& Qword(T arr, size_t pos)
    {
        if (pos + sizeof(CAPL_UINT64) > sizeof(arr))
            RaiseException(EXCEPTION_ARRAY_BOUNDS_EXCEEDED, 0, 0, NULL);

        return *(CAPL_UINT64*)(((BYTE*)arr) + pos);
    }
    template<class T>
    CAPL_INT64& int64(T arr, size_t pos)
    {
        if(pos + sizeof(CAPL_INT64) > sizeof(arr))
            RaiseException(EXCEPTION_ARRAY_BOUNDS_EXCEEDED, 0, 0, NULL);

        return *(CAPL_INT64*)(((BYTE*)arr) + pos);
    }

    } //namespace capl


#endif //CAPLPLUGINAPI_H__INCLUDED

//####################################
//PluginApiVerbatimInclude DONE
//####################################
static char s_sCANOeConfigfile[MAX_PATH] = { "" };
CAPL_EXTERN_C __declspec(dllexport) void CAPL_CALL CAPL_vGetConfigFile(char* sFile)
{
   strcpy(sFile, s_sCANOeConfigfile);
}
CAPL_EXTERN_C __declspec(dllexport) void CAPL_CALL CAPL_vSetConfigFile(const char* sFile)
{
   strcpy(s_sCANOeConfigfile, sFile);
}
capl::Thread g_CaplThread(true); // CAPL-Thread für dieses Plugin erzeugen.
CAPL_HANDLE GetInternalCaplThread()
{
   return g_CaplThread.GetCaplHandle();
}
static const DWORD s_dwVersion = 4;
static HWND s_hMainWnd = NULL;

CAPL_EXTERN_C __declspec(dllexport) BOOL CAPL_CALL CAPL_boInit(capl::tStatusFunc pFunc, HWND hMainWnd)
{
   if(!capl::validatePlugin())
      return false;
   s_hMainWnd = hMainWnd;
   HMODULE hMod = NULL;
   GetModuleHandleEx(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | 
                     GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                     (LPCTSTR)CAPL_boInit,
                     &hMod);

   capl::SetStatusFunc(pFunc, (HINSTANCE)hMod);

   return true;
}

namespace capl
{
   bool Init();
   void Deinit();
   
   HMODULE GetThisModuleHandle()
   {
      HMODULE ret = NULL;
      GetModuleHandleEx(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | 
                        GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                        (LPCTSTR)CAPL_boInit,
                        &ret);

      return ret;
   }

   std::string GetThisFileName()
   {
      char fileNameBuffer[MAX_PATH];
      if(!GetModuleFileName(GetThisModuleHandle(), fileNameBuffer, MAX_PATH))
         return "d:\\work-files\\multidebugger\\out\\multidebugger.canapp";
      return fileNameBuffer;
   }

   std::string GetReadModuleDataErrorMsg()
   {
      std::string ret = GetThisFileName();
      ret.append(": ");
      ret.append(capl::GetStringResource(capl::CAPLPLUGIN_ERROR_LOADING_FAILED));
      return ret;
   }

   std::string GetWriteModuleDataErrorMsg()
   {
      std::string ret = GetThisFileName();
      ret.append(": ");
      ret.append(capl::GetStringResource(capl::CAPLPLUGIN_ERROR_SAVING_FAILED));
      return ret;
   }

   CAPL_INT32 CAPL_CALL getAbsFilePath(const char *relPath, char *absPath, CAPL_INT32 absPathLen)
   {
      return capl::getCanOeAbsFilePath(relPath, s_sCANOeConfigfile, absPath, absPathLen);
   }
};

CAPL_EXTERN_C __declspec(dllexport) BOOL CAPL_CALL CAPL_boInitDependencies()
{
   return capl::Init();
}

CAPL_EXTERN_C __declspec(dllexport) void CAPL_CALL CAPL_vDeInit()
{
   capl::Deinit();
}

CAPL_EXTERN_C __declspec(dllexport) bool CAPL_CALL CAPL_boGetDll(int nDll, char* pBuffer)
{
   return false;
}

CAPL_EXTERN_C __declspec(dllexport) char* CAPL_CALL CAPL_sGetPluginName()
{
   static char sPluginName[] = "";
   return sPluginName;
}

CAPL_EXTERN_C __declspec(dllexport) char* CAPL_CALL CAPL_sGetVersionNumber()
{
   static char sVersionNumber[] = "";
   return sVersionNumber;
}

CAPL_EXTERN_C __declspec(dllexport) char* CAPL_CALL CAPL_sGetDescription()
{
   static char sDescription[] = "";
   return sDescription;
}

namespace capl
{

}; //namespace capl

namespace capl
{

extern void Init0();

bool Init()
{
   bool boReturn = true; // Wird auf false gesetzt, wenn eine DLL nicht geladen werden konnte!
   Init0();
   ResetCurrentThread();
   return boReturn;
}

void Deinit()
{
}

//####################################
//PluginWrapperApiVerbatimInclude
//####################################
// Dieses File dient dazu in CAPL-Plugins integriert werden zu können.
// Dort können dann die Wrapperobjekte genutzt werden um auf die CAPLPluginApi
// in Form von Objekten zugreifen zu können.
// Damit dies Funktioniert, muss der Code vom Namespace "capl" umgeben werden.


//######################################################
// GCC-WRAPPER
//######################################################
#ifdef CAPL_PLUGIN_WRAPPER

#define ATTRIBUTE_WRAPPER_HOLDER(interfacePointer)\
      AttributeWrapper GetAttribute(const char* sAttrName) { return AttributeWrapper(interfacePointer->GetAttribute(sAttrName)); }

    class AttributeWrapper
    {
        IAttribute* m_pAttribute;
    public:
        explicit AttributeWrapper(IAttribute* pAttribute) : m_pAttribute(pAttribute) {}
        virtual ~AttributeWrapper()
        {
            if (m_pAttribute->ReleaseMe())
                capl::DeleteAttribute(m_pAttribute);
        }

        operator CAPL_INT8 () const { return (CAPL_INT8)m_pAttribute->GetInt(); }
        operator INT16 () const { return (INT16)m_pAttribute->GetInt(); }
        operator double() const { return m_pAttribute->GetDouble(); }
        operator CAPL_INT32 () const { return m_pAttribute->GetLong(); }
        operator CAPL_UINT8 () const { return (CAPL_UINT8)m_pAttribute->GetInt(); }
        operator UINT16 () const { return (UINT16)m_pAttribute->GetInt(); }
        operator CAPL_UINT32 () const { return (CAPL_UINT32)m_pAttribute->GetLong(); }
        operator const char* () const
        {
            switch (m_pAttribute->GetType())
            {
            case TypeString:
                return m_pAttribute->GetString();
            }
            return "";
        }
    };

    // Arithmetische Operatoren für Atttributzugriffe
    template<class T> T operator+(T t, AttributeWrapper a) { return t + (T)a; }
    template<class T> T operator-(T t, AttributeWrapper a) { return t - (T)a; }
    template<class T> T operator*(T t, AttributeWrapper a) { return t * (T)a; }
    template<class T> T operator/(T t, AttributeWrapper a) { return t / (T)a; }
    template<class T> T operator%(T t, AttributeWrapper a) { return t % (T)a; }
    template<class T> AttributeWrapper& operator+=(T& t, AttributeWrapper a) { a = t += (T)a; return a; }
    template<class T> AttributeWrapper& operator-=(T& t, AttributeWrapper a) { a = t -= (T)a; return a; }
    template<class T> AttributeWrapper& operator*=(T& t, AttributeWrapper a) { a = t *= (T)a; return a; }
    template<class T> AttributeWrapper& operator/=(T& t, AttributeWrapper a) { a = t /= (T)a; return a; }
    template<class T> AttributeWrapper& operator%=(T& t, AttributeWrapper a) { a = t %= (T)a; return a; }
    template<class T> T operator+(AttributeWrapper a, T t) { return t + (T)a; }
    template<class T> T operator-(AttributeWrapper a, T t) { return t - (T)a; }
    template<class T> T operator*(AttributeWrapper a, T t) { return t * (T)a; }
    template<class T> T operator/(AttributeWrapper a, T t) { return t / (T)a; }
    template<class T> T operator%(AttributeWrapper a, T t) { return t % (T)a; }

    // Vergleichsoperatoren  für Atttributzugriffe
    template<class T> bool operator==(T t, AttributeWrapper a) { return t == (T)a; }
    template<class T> bool operator!=(T t, AttributeWrapper a) { return t != (T)a; }
    template<class T> bool operator<=(T t, AttributeWrapper a) { return t <= (T)a; }
    template<class T> bool operator>=(T t, AttributeWrapper a) { return t >= (T)a; }
    template<class T> bool operator<(T t, AttributeWrapper a) { return t < (T)a; }
    template<class T> bool operator>(T t, AttributeWrapper a) { return t > (T)a; }

    template<class T> bool operator==(AttributeWrapper a, T t) { return t == (T)a; }
    template<class T> bool operator!=(AttributeWrapper a, T t) { return t != (T)a; }
    template<class T> bool operator<=(AttributeWrapper a, T t) { return t <= (T)a; }
    template<class T> bool operator>=(AttributeWrapper a, T t) { return t >= (T)a; }
    template<class T> bool operator<(AttributeWrapper a, T t) { return t < (T)a; }
    template<class T> bool operator>(AttributeWrapper a, T t) { return t > (T)a; }


    template<class T>
    class PropertyWrapper
    {
        IProperty<T>* m_pProp;
    public:
        explicit PropertyWrapper(IProperty<T>* pProp) : m_pProp(pProp) {}
        virtual ~PropertyWrapper() {}

        operator IProperty<T>&()
        {
            return *m_pProp;
        }

        CAPL_CALL operator T() const
        {
            return (T)(*m_pProp);
        }

        CAPL_METHOD_IMPL(T, operator=)(T value)
        {
            return (T)((*m_pProp) = value);
        }

        CAPL_METHOD_IMPL(PropertyWrapper<T>, operator=)(const PropertyWrapper<T>& value)
        {
            *m_pProp = (T)value;
            return *this;
        }

        // Prefix ++
        CAPL_METHOD_IMPL(T, operator++)()
        {
            return (T)(++(*m_pProp));
        }
        // Postfix ++
        CAPL_METHOD_IMPL(T, operator++)(int)
        {
            return (T)(++(*m_pProp));
        }

        // Prefix --
        CAPL_METHOD_IMPL(T, operator--)()
        {
            return (T)(--(*m_pProp));
        }
        // Postfix --
        CAPL_METHOD_IMPL(T, operator--)(int)
        {
            return (T)(--(*m_pProp));
        }
    };

    /* Der GCC hat anscheinend Probleme mit templates die Methoden aufrufen.
       Dies wäre eine schöne Lösung für den Zugriff auf Get/Set Methoden der Interfaces gewesen.
       Falls das Problem mit dem GCC behoben wurde, kann dieser Code wieder verwendet werden.
       Es wird jetzt erstmal direkt auf den Speicher zugegriffen, was kein Problem darstellen sollte,
       weil alle Zugriffe nur aus dem CAPL-Thread erfolgen.

       template<class Class, class T>
       class GetSetWrapper
       {
          typedef T (CAPL_CALL Class::*Get)() const;
          typedef void (CAPL_CALL Class::*Set)(T);
          Class* m_pItem;
          Get m_get;
          Set m_set;
          typedef GetSetWrapper<Class, T> ThisType;

       public:
          explicit GetSetWrapper(Class* pItem, Get getter, Set setter)
             : m_pItem(pItem)
             , m_get(getter)
             , m_set(setter)
          {
          }

          virtual ~GetSetWrapper()
          {
          }

          CAPL_CALL operator T() const
          {
             return (m_pItem->*m_get)();
          }

          CAPL_METHOD_IMPL(T, operator=)(T value)
          {
             (m_pItem->*m_set)(value);
             return (m_pItem->*m_get)();
          }

          CAPL_METHOD_IMPL(ThisType, operator=)(const ThisType& item)
          {
             *this = (T)item;
             return *this;
          }
       };

       template<class T>
       class MsgDataWrapper
       {
          typedef T (CAPL_CALL IMessage::*GetMethod)(BYTE byteIndex) const;
          typedef void (CAPL_CALL IMessage::*SetMethod)(BYTE byteIndex, T data);
          IMessage* m_pMsg;
          GetMethod m_get;
          SetMethod m_set;
          BYTE m_byteIndex;
          typedef MsgDataWrapper<T> ThisType;
       public:
          explicit MsgDataWrapper(IMessage* pMsg, GetMethod getter, SetMethod setter, BYTE byteIndex)
             : m_pMsg(pMsg)
             , m_get(getter)
             , m_set(setter)
             , m_byteIndex(byteIndex)
          {
          }

          virtual ~MsgDataWrapper()
          {
          }

          CAPL_CALL operator T() const
          {
             T data = (m_pMsg->*m_get)(m_byteIndex);
             return data;
          }

          CAPL_METHOD_IMPL(T, operator=)(T value)
          {
             (m_pMsg->*m_set)(m_byteIndex, value);
             return value;
          }

          CAPL_METHOD_IMPL(ThisType, operator=)(const ThisType& item)
          {
             *this = (T)item;
             return *this;
          }
       };
       */

    template<class T>
    class SigValueProp : public IProperty<T>
    {
        IProperty<QWORD>& m_Prop;
        BYTE m_Pos;
    public:
        SigValueProp(IProperty<QWORD>& prop)
            : m_Prop(prop)
            , m_Pos(0)
        {}
        void SetPos(BYTE pos)
        {
            m_Pos = pos;
        }
        CAPL_CALL operator T()const
        {
            return ((T*)&((QWORD&)m_Prop))[m_Pos];
        }
        T CAPL_CALL operator =(T value)
        {
            QWORD temp = (QWORD&)m_Prop;
            ((T*)&temp)[m_Pos] = value;
            m_Prop = temp;
            return ((T*)&((QWORD&)m_Prop))[m_Pos];
        }
    };

    class MessageWrapper;
    class SignalWrapper
    {
        ISignal* m_pSig;
        /* Definiert ob m_pSig durch die Erzeugung dieses Wrapper-Objektes
         * ebenfalls erzeugt wurde und somit auch von diesem wieder freigegeben
         * werden muss.
         */
        bool m_boDeleteSignal;

        SigValueProp<BYTE> m_ByteValue;
        SigValueProp<WORD> m_WordValue;
        SigValueProp<DWORD> m_DWordValue;
        SigValueProp<QWORD> m_QWordValue;
        SigValueProp<char> m_CharValue;
        SigValueProp<short> m_ShortValue;
        SigValueProp<CAPL_INT32> m_LongValue;
    public:
        explicit SignalWrapper(char* pcHandle)
            : m_pSig(CreateSignal(pcHandle))
            , m_boDeleteSignal(true)
            , m_ByteValue(m_pSig->ValueHex())
            , m_WordValue(m_pSig->ValueHex())
            , m_DWordValue(m_pSig->ValueHex())
            , m_QWordValue(m_pSig->ValueHex())
            , m_CharValue(m_pSig->ValueHex())
            , m_ShortValue(m_pSig->ValueHex())
            , m_LongValue(m_pSig->ValueHex())
        {
        }

        explicit SignalWrapper(ISignal& sig)
            : m_pSig(&sig)
            , m_boDeleteSignal(false)
            , m_ByteValue(m_pSig->ValueHex())
            , m_WordValue(m_pSig->ValueHex())
            , m_DWordValue(m_pSig->ValueHex())
            , m_QWordValue(m_pSig->ValueHex())
            , m_CharValue(m_pSig->ValueHex())
            , m_ShortValue(m_pSig->ValueHex())
            , m_LongValue(m_pSig->ValueHex())
        {
        }

        virtual ~SignalWrapper()
        {
            if (m_boDeleteSignal)
            {
                DeleteSignal(m_pSig);
            }
        }

        operator ISignal&()
        {
            return *m_pSig;
        }

        PropertyWrapper<double> ValuePhys()
        {
            return PropertyWrapper<double>(&m_pSig->ValuePhys());
        }

        PropertyWrapper<QWORD> ValueHex()
        {
            return PropertyWrapper<QWORD>(&m_pSig->ValueHex());
        }
    #ifndef REMOVE_SIGNAL_GETESSAGE
        MessageWrapper GetMessage() const;
    #endif

        operator QWORD () const
        {
            return m_pSig->ValueHex();
        }

        // Prefix ++
        QWORD operator++ ()
        {
            return (m_pSig->ValueHex() = ((QWORD)m_pSig->ValueHex()) + 1);
        }
        // Postfix ++
        QWORD operator++ (int)
        {
            return (m_pSig->ValueHex() = ((QWORD)m_pSig->ValueHex()) + 1);
        }

        // Prefix --
        QWORD operator-- ()
        {
            return (m_pSig->ValueHex() = ((QWORD)m_pSig->ValueHex()) - 1);
        }
        // Postfix --
        QWORD operator-- (int)
        {
            return (m_pSig->ValueHex() = ((QWORD)m_pSig->ValueHex()) - 1);
        }

        QWORD operator= (const QWORD& dIn)
        {
            return (m_pSig->ValueHex() = dIn);
        }

        PropertyWrapper<BYTE> Byte(BYTE byteIndex)
        {
            m_ByteValue.SetPos(byteIndex);
            return PropertyWrapper<BYTE>(&m_ByteValue);
        }
        PropertyWrapper<WORD> Word(BYTE byteIndex)
        {
            m_WordValue.SetPos(byteIndex);
            return PropertyWrapper<WORD>(&m_WordValue);
        }
        PropertyWrapper<DWORD> Dword(BYTE byteIndex)
        {
            m_DWordValue.SetPos(byteIndex);
            return PropertyWrapper<DWORD>(&m_DWordValue);
        }
        PropertyWrapper<char> Char(BYTE byteIndex)
        {
            m_CharValue.SetPos(byteIndex);
            return PropertyWrapper<char>(&m_CharValue);
        }
        PropertyWrapper<short> Short(BYTE byteIndex)
        {
            m_ShortValue.SetPos(byteIndex);
            return PropertyWrapper<short>(&m_ShortValue);
        }
        PropertyWrapper<CAPL_INT32> Long(BYTE byteIndex)
        {
            m_LongValue.SetPos(byteIndex);
            return PropertyWrapper<CAPL_INT32>(&m_LongValue);
        }
        PropertyWrapper<QWORD> Qword(BYTE byteIndex)
        {
            m_QWordValue.SetPos(byteIndex);
            return PropertyWrapper<QWORD>(&m_QWordValue);
        }

        ATTRIBUTE_WRAPPER_HOLDER(m_pSig);
    };
    // Interface für Botschaften.
    class MessageWrapper
    {
        IMessage* m_pMsg;
        /* Definiert ob m_pMsg durch die Erzeugung dieses Wrapper-Objektes
         * ebenfalls erzeugt wurde und somit auch von diesem wieder freigegeben
         * werden muss.
         */
        bool m_boDeleteMessage;
    public:
        BYTE TxReqCount;// Unused property dummy.

        explicit MessageWrapper(const char* sHandle = "")
            : m_pMsg(NULL)
            , m_boDeleteMessage(true)
            , TxReqCount(1)
        {
            m_pMsg = CreateMessage(sHandle);
        }

        explicit MessageWrapper(DWORD dwMessageId, BYTE u8Channel = 0, BYTE u8Type = 0)
            : m_pMsg(NULL)
            , m_boDeleteMessage(true)
        {
            m_pMsg = CreateMessageById(dwMessageId, u8Channel, u8Type);
        }

        explicit MessageWrapper(IMessage& msg)
            : m_pMsg(&msg)
            , m_boDeleteMessage(false)
        {
        }

        virtual ~MessageWrapper()
        {
            if (m_boDeleteMessage)
                DeleteMessage(m_pMsg);
        }

        operator IMessage&()
        {
            return *m_pMsg;
        }

        CAPL_METHOD_IMPL(void, Send)()
        {
            m_pMsg->Send();
        }
        CAPL_METHOD_IMPL(MessageWrapper, GetMessage)(const char* sName)
        {
            return MessageWrapper(*m_pMsg->GetMessage(sName));
        }
        CAPL_METHOD_IMPL(SignalWrapper, GetSignal)(CAPL_UINT32 uiIndex, tSignalNameArr(pSigNameArr))
        {
            return SignalWrapper(*m_pMsg->GetSignal(uiIndex, pSigNameArr));
        }

        CAPL_METHOD_IMPL(DWORD&, ID)()
        {
            return m_pMsg->ID();
        }
        CAPL_METHOD_IMPL(BYTE&, CAN)()
        {
            return m_pMsg->CAN();
        }
        CAPL_METHOD_IMPL(BYTE&, MsgChannel)()
        {
            return m_pMsg->CAN();
        }
        CAPL_METHOD_IMPL(BYTE&, DLC)()
        {
            return m_pMsg->DLC();
        }

        CAPL_METHOD_IMPL(IMessage::Direction&, DIR)()
        {
            return m_pMsg->DIR();
        }
        CAPL_METHOD_IMPL(bool&, RTR)()
        {
            return m_pMsg->RTR();
        }
        CAPL_METHOD_IMPL(bool&, EDL)()
        {
            return m_pMsg->EDL();
        }
        CAPL_METHOD_IMPL(bool&, FDF)()
        {
            return m_pMsg->FDF();
        }
        CAPL_METHOD_IMPL(bool&, BRS)()
        {
            return m_pMsg->BRS();
        }
        CAPL_METHOD_IMPL(CAPL_INT32, TYPE)() const
        {
            return m_pMsg->TYPE();
        }
        CAPL_METHOD_IMPL(CAPL_INT32, MsgFlags)() const
        {
            return m_pMsg->MsgFlags();
        }
        CAPL_METHOD_IMPL(QWORD&, TIME)()
        {
            return m_pMsg->TIME();
        }
        CAPL_METHOD_IMPL(QWORD&, Time_NS)()
        {
            return m_pMsg->Time_NS();
        }
        CAPL_METHOD_IMPL(bool&, SIMULATED)()
        {
            return m_pMsg->SIMULATED();
        }
        CAPL_METHOD_IMPL(BYTE&, Byte)(BYTE byteIndex)
        {
            return m_pMsg->Byte(byteIndex);
        }
        CAPL_METHOD_IMPL(WORD&, Word)(BYTE byteIndex)
        {
            return m_pMsg->Word(byteIndex);
        }
        CAPL_METHOD_IMPL(DWORD&, Dword)(BYTE byteIndex)
        {
            return m_pMsg->Dword(byteIndex);
        }
        CAPL_METHOD_IMPL(char&, Char)(BYTE byteIndex)
        {
            return m_pMsg->Char(byteIndex);
        }
        CAPL_METHOD_IMPL(short&, Short)(BYTE byteIndex)
        {
            return m_pMsg->Short(byteIndex);
        }
        CAPL_METHOD_IMPL(CAPL_INT32&, Long)(BYTE byteIndex)
        {
            return m_pMsg->Long(byteIndex);
        }
        CAPL_METHOD_IMPL(QWORD&, Qword)(BYTE byteIndex)
        {
            return m_pMsg->Qword(byteIndex);
        }

        IMessage& operator=(IMessage& m)
        {
            m_pMsg->CopyData(m);
            return *m_pMsg;
        }
        MessageWrapper& operator=(MessageWrapper& m)
        {
            m_pMsg->CopyData(*m.m_pMsg);
            return *this;
        }

        ATTRIBUTE_WRAPPER_HOLDER(m_pMsg);
    };


    #ifndef REMOVE_SIGNAL_GETESSAGE
    #ifdef CAPL_EVENT_IMPL
    MessageWrapper SignalWrapper::GetMessage() const
    {
        return MessageWrapper(m_pSig->GetMessage());
    }
    #endif
    #endif

    // Interface für Ethernet Botschaften.
    class EthernetPacketWrapper
    {
        IEthernetPacket* m_pMsg;
        /* Definiert ob m_pMsg durch die Erzeugung dieses Wrapper-Objektes
        * ebenfalls erzeugt wurde und somit auch von diesem wieder freigegeben
        * werden muss.
        */
        bool m_boDeleteMessage;
    public:
        explicit EthernetPacketWrapper()
            : m_pMsg(NULL)
            , m_boDeleteMessage(true)
        {
            m_pMsg = CreateEthernetPacket();
#ifdef FILE_SPECIFIC_FUNCTIONS
            MsgChannel() = s_asBusContext[s_u16BusContext].u8Channel;
#endif
        }

        explicit EthernetPacketWrapper(IEthernetPacket& msg)
            : m_pMsg(&msg)
            , m_boDeleteMessage(false)
        {
        }

        virtual ~EthernetPacketWrapper()
        {
            if (m_boDeleteMessage)
                DeleteEthernetPacket(m_pMsg);
        }

        operator IEthernetPacket&()
        {
            return *m_pMsg;
        }

        CAPL_METHOD_IMPL(void, Send)()
        {
            m_pMsg->Send();
        }

        CAPL_METHOD_IMPL(QWORD, Time_NS)() const
        {
            return m_pMsg->Time_NS();
        }
        CAPL_METHOD_IMPL(BYTE, DIR)() const
        {
            return m_pMsg->DIR();
        }
        CAPL_METHOD_IMPL(WORD&, MsgChannel)()
        {
            return m_pMsg->MsgChannel();
        }
        CAPL_METHOD_IMPL(WORD&, Length)()
        {
            return m_pMsg->Length();
        }
        CAPL_METHOD_IMPL(DWORD, FCS)() const
        {
            return m_pMsg->FCS();
        }
        CAPL_METHOD_IMPL(CAPL_INT64, FrameLen)() const
        {
            return m_pMsg->FrameLen();
        }
        CAPL_METHOD_IMPL(CAPL_INT64, SOF)() const
        {
            return m_pMsg->SOF();
        }
        CAPL_METHOD_IMPL(IProperty<WORD>&, TYPE)()
        {
            return m_pMsg->Type();
        }
        CAPL_METHOD_IMPL(QWORD&, Source)()
        {
            return m_pMsg->Source();
        }
        CAPL_METHOD_IMPL(QWORD&, Destination)()
        {
            return m_pMsg->Destination();
        }

        CAPL_METHOD_IMPL(BYTE&, Byte)(CAPL_INT32 byteIndex)
        {
            return m_pMsg->Byte(byteIndex);
        }
        CAPL_METHOD_IMPL(WORD&, Word)(CAPL_INT32 byteIndex)
        {
            return m_pMsg->Word(byteIndex);
        }
        CAPL_METHOD_IMPL(DWORD&, Dword)(CAPL_INT32 byteIndex)
        {
            return m_pMsg->Dword(byteIndex);
        }
        CAPL_METHOD_IMPL(QWORD&, Qword)(CAPL_INT32 byteIndex)
        {
            return m_pMsg->Qword(byteIndex);
        }
        CAPL_METHOD_IMPL(char&, Char)(CAPL_INT32 byteIndex)
        {
            return m_pMsg->Char(byteIndex);
        }
        CAPL_METHOD_IMPL(short&, Short)(CAPL_INT32 byteIndex)
        {
            return m_pMsg->Short(byteIndex);
        }
        CAPL_METHOD_IMPL(CAPL_INT32&, Long)(CAPL_INT32 byteIndex)
        {
            return m_pMsg->Long(byteIndex);
        }
        CAPL_METHOD_IMPL(CAPL_INT64&, LongLong)(CAPL_INT32 byteIndex)
        {
            return m_pMsg->LongLong(byteIndex);
        }
        CAPL_METHOD_IMPL(DWORD, SetData)(WORD offset, const void* source, WORD length)
        {
            return m_pMsg->SetData(offset, source, length);
        }
        template<class T>
        CAPL_METHOD_IMPL(DWORD, SetData)(WORD offset, const T& source)
        {
            return m_pMsg->SetData(offset, source, sizeof(T));
        }
        CAPL_METHOD_IMPL(DWORD, GetData)(WORD offset, void* source, WORD length)
        {
            return m_pMsg->GetData(offset, source, length);
        }
        template<class T>
        CAPL_METHOD_IMPL(DWORD, GetData)(WORD offset, T& source)
        {
            return m_pMsg->GetData(offset, source, sizeof(T));
        }

        CAPL_METHOD_IMPL(CAPL_INT32, Clear)()
        {
            return m_pMsg->Clear();
        }

        CAPL_METHOD_IMPL(CAPL_INT32, CompletePacket)()
        {
            return m_pMsg->CompletePacket();
        }

        CAPL_METHOD_IMPL(WORD, GetProtocolErrorText)(char* buffer)
        {
            return m_pMsg->GetProtocolErrorText(buffer);
        }

        CAPL_METHOD_IMPL(CAPL_INT32, GetVlan)(WORD& tpid, WORD& tci)
        {
            return m_pMsg->GetVlan(tpid, tci);
        }

        CAPL_METHOD_IMPL(CAPL_INT32, GetVlanIndex)(DWORD vlanIndex, WORD& tpid, WORD& tci)
        {
            return m_pMsg->GetVlanIndex(vlanIndex, tpid, tci);
        }

        CAPL_METHOD_IMPL(DWORD, GetVlanId)()
        {
            return m_pMsg->GetVlanId();
        }

        CAPL_METHOD_IMPL(DWORD, GetVlanIdIndex)(DWORD vlanIndex)
        {
            return m_pMsg->GetVlanIdIndex(vlanIndex);
        }

        CAPL_METHOD_IMPL(DWORD, GetVlanPriority)()
        {
            return m_pMsg->GetVlanPriority();
        }

        CAPL_METHOD_IMPL(DWORD, GetVlanPriorityIndex)(DWORD vlanIndex)
        {
            return m_pMsg->GetVlanPriorityIndex(vlanIndex);
        }

        CAPL_METHOD_IMPL(WORD, HasProtocolError)()
        {
            return m_pMsg->HasProtocolError();
        }

        CAPL_METHOD_IMPL(WORD, HasVlan)()
        {
            return m_pMsg->HasVlan();
        }

        CAPL_METHOD_IMPL(DWORD, RemoveVlan)()
        {
            return m_pMsg->RemoveVlan();
        }

        CAPL_METHOD_IMPL(DWORD, RemoveVlanIndex)(DWORD vlanIndex)
        {
            return m_pMsg->RemoveVlanIndex(vlanIndex);
        }

        CAPL_METHOD_IMPL(CAPL_INT32, SetVlan)(WORD tpid, WORD tci)
        {
            return m_pMsg->SetVlan(tpid, tci);
        }

        CAPL_METHOD_IMPL(CAPL_INT32, SetVlanIndex)(DWORD vlanIndex, WORD tpid, WORD tci)
        {
            return m_pMsg->SetVlanIndex(vlanIndex, tpid, tci);
        }

        CAPL_METHOD_IMPL(DWORD, SetVlanId)(WORD vlanId)
        {
            return m_pMsg->SetVlanId(vlanId);
        }

        CAPL_METHOD_IMPL(DWORD, SetVlanIdIndex)(DWORD vlanIndex, WORD vlanId)
        {
            return m_pMsg->SetVlanIdIndex(vlanIndex, vlanId);
        }

        CAPL_METHOD_IMPL(DWORD, SetVlanPriority)(WORD vlanPriority)
        {
            return m_pMsg->SetVlanPriority(vlanPriority);
        }

        CAPL_METHOD_IMPL(DWORD, SetVlanPriorityIndex)(DWORD vlanIndex, WORD vlanPriority)
        {
            return m_pMsg->SetVlanPriorityIndex(vlanIndex, vlanPriority);
        }



        CAPL_METHOD_IMPL(IIPv4&, IPv4)()
        {
            return m_pMsg->IPv4();
        }

        CAPL_METHOD_IMPL(IIPv6&, IPv6)()
        {
            return m_pMsg->IPv6();
        }

        CAPL_METHOD_IMPL(IICMPv6&, ICMPv6)()
        {
            return m_pMsg->ICMPv6();
        }

        CAPL_METHOD_IMPL(INDP&, NDP)()
        {
            return m_pMsg->NDP();
        }

        CAPL_METHOD_IMPL(ITCP&, TCP)()
        {
            return m_pMsg->TCP();
        }

        CAPL_METHOD_IMPL(IUDP&, UDP)()
        {
            return m_pMsg->UDP();
        }


        IEthernetPacket& operator=(IEthernetPacket& m)
        {
            m_pMsg->CopyData(m);
            return *m_pMsg;
        }
        EthernetPacketWrapper& operator=(EthernetPacketWrapper& m)
        {
            m_pMsg->CopyData(*m.m_pMsg);
            return *this;
        }
    };

    // Interface für Ethernet Status.
    class EthernetStatusWrapper
    {
        IEthernetStatus* m_pMsg;
    public:
        explicit EthernetStatusWrapper(IEthernetStatus& msg)
            : m_pMsg(&msg)
        {
        }

        virtual ~EthernetStatusWrapper()
        {
        }

        operator IEthernetStatus&()
        {
            return *m_pMsg;
        }

        CAPL_METHOD_IMPL(LONG, Status)() const
        {
            return m_pMsg->Status();
        }
        CAPL_METHOD_IMPL(DWORD, Bitrate)() const
        {
            return m_pMsg->Bitrate();
        }
        CAPL_METHOD_IMPL(WORD&, MsgChannel)()
        {
            return m_pMsg->MsgChannel();
        }
        CAPL_METHOD_IMPL(WORD&, HwChannel)()
        {
            return m_pMsg->HwChannel();
        }
        CAPL_METHOD_IMPL(QWORD, Time_NS)() const
        {
            return m_pMsg->Time_NS();
        }

    };

    // Wrapper für phys und raw Properties.
    class PhysRawValue
    {
        IPhysRawValue& m_val;
    public:
        PhysRawValue(IPhysRawValue& val)
            : m_val(val)
        {
        }

        PropertyWrapper<double> ValuePhys()
        {
            return PropertyWrapper<double>(&m_val.ValuePhys());
        }

        PropertyWrapper<QWORD> ValueHex()
        {
            return PropertyWrapper<QWORD>(&m_val.ValueHex());
        }

        double operator=(double val)
        {
            m_val.ValuePhys() = val;
            return val;
        }

        operator double() const
        {
            return m_val.ValuePhys();
        }
    };

    // Wrapper für direkten Signal-Zugriff.
    class SignalVal
    {
        ISignalVal* m_pSigVal;
        /* Definiert ob m_pSig durch die Erzeugung dieses Wrapper-Objektes
         * ebenfalls erzeugt wurde und somit auch von diesem wieder freigegeben
         * werden muss.
         */
        bool m_boDeleteSignal;
    public:
        SignalVal(const char* sId, bool boTxRq = false)
            : m_boDeleteSignal(true)
            , m_pSigVal(NULL)
        {
            if (boTxRq)
                m_pSigVal = CreateTxRqSignalVal(sId);
            else
                m_pSigVal = CreateSignalVal(sId);
        }

        explicit SignalVal(ISignalVal& sig)
            : m_pSigVal(&sig)
            , m_boDeleteSignal(false)
        {
        }

        SignalVal(const SignalVal& sig)
            : m_pSigVal(sig.m_pSigVal)
            , m_boDeleteSignal(false)
        {
        }

        ~SignalVal()
        {
            if (m_boDeleteSignal)
                DeleteSignalVal(m_pSigVal);
        }

        operator ISignalVal&()
        {
            return *m_pSigVal;
        }

        PhysRawValue rx()
        {
            return PhysRawValue(m_pSigVal->rx());
        }

        PhysRawValue txrq()
        {
            return PhysRawValue(m_pSigVal->txrq());
        }

        PropertyWrapper<double> ValuePhys()
        {
            return PropertyWrapper<double>(&m_pSigVal->ValuePhys());
        }

        PropertyWrapper<QWORD> ValueHex()
        {
            return PropertyWrapper<QWORD>(&m_pSigVal->ValueHex());
        }

        double operator=(double val)
        {
            m_pSigVal->txrq().ValuePhys() = val;
            return val;
        }

        operator double() const
        {
            return m_pSigVal->ValuePhys();
        }
    };

    static double getSignal(SignalVal& sigVal)
    {
        return (double)sigVal;
    }

    static void setSignal(SignalVal& sigVal, double dVal)
    {
        sigVal = dVal;
    }

    static QWORD getRawSignal(SignalVal& sigVal)
    {
        return sigVal.ValueHex();
    }

    static void setRawSignal(SignalVal& sigVal, QWORD val)
    {
        sigVal.ValueHex() = val;
    }

    static void getRawSignalArray(SignalVal& sigVal, CAPL_UINT8* data, CAPL_INT32 dataLength)
    {        
        QWORD qwVal = sigVal.ValueHex();
        if (dataLength > sizeof(qwVal))
            dataLength = sizeof(qwVal);
        memcpy(data, &qwVal, dataLength);
    }

    static void setRawSignalArray(SignalVal& sigVal, QWORD val, CAPL_UINT8* data, CAPL_INT32 dataLength)
    {
        QWORD qwVal = 0;
        if (dataLength > sizeof(qwVal))
            dataLength = sizeof(qwVal);
        memcpy(&qwVal, data, dataLength);
        sigVal.ValueHex() = qwVal;
    }

    // Wrapper für Timer.
    class TimerWrapper
    {
        ITimer* m_pTimer;
        bool m_boDeleteTimer;
    public:
        TimerWrapper(ITimer& timer)
            : m_pTimer(&timer)
            , m_boDeleteTimer(false)
        {
        }

        TimerWrapper(bool boSeconds)
            : m_boDeleteTimer(true)
        {
            m_pTimer = CreateTimer(boSeconds);
        }

        ~TimerWrapper()
        {
            if (m_boDeleteTimer)
                DeleteTimer(m_pTimer);
        }

        operator ITimer&()
        {
            return *m_pTimer;
        }

        CAPL_METHOD_IMPL(void, Set)(DWORD u32Timer)
        {
            m_pTimer->Set(u32Timer);
        }
        CAPL_METHOD_IMPL(void, Kill)()
        {
            m_pTimer->Kill();
        }
        CAPL_METHOD_IMPL(bool, UpdateTimer)()
        {
            m_pTimer->UpdateTimer();
        }
        CAPL_METHOD_IMPL(DWORD, GetTime)() const
        {
            return m_pTimer->GetTime();
        }
        CAPL_METHOD_IMPL(void, SetCyclic)(CAPL_INT32 firstDuration, CAPL_INT32 period)
        {
            m_pTimer->SetCyclic(firstDuration, period);
        }
        CAPL_METHOD_IMPL(void, SetCyclic)(CAPL_INT32 period)
        {
            m_pTimer->SetCyclic(period, -1);
        }
    };

    class StartWrapper
    {
        IStart* m_pStart;
    public:
        StartWrapper(IStart& start)
            : m_pStart(&start)
        {
        }

        operator IStart&()
        {
            return *m_pStart;
        }

        CAPL_METHOD_IMPL(DWORD, GetTime)() const
        {
            return m_pStart->GetTime();
        }
    };

    class PreStartWrapper
    {
        IPreStart* m_pPreStart;
    public:
        PreStartWrapper(IPreStart& preStart)
            : m_pPreStart(&preStart)
        {
        }

        operator IPreStart&()
        {
            return *m_pPreStart;
        }

        CAPL_METHOD_IMPL(DWORD, GetTime)() const
        {
            return m_pPreStart->GetTime();
        }
    };

    class PreStopWrapper
    {
        IPreStop* m_pPreStop;
    public:
        PreStopWrapper(IPreStop& preStop)
            : m_pPreStop(&preStop)
        {
        }

        operator IPreStop&()
        {
            return *m_pPreStop;
        }

        CAPL_METHOD_IMPL(DWORD, GetTime)() const
        {
            return m_pPreStop->GetTime();
        }
    };

    class StopWrapper
    {
        IStop* m_pStop;
    public:
        StopWrapper(IStop& stop)
            : m_pStop(&stop)
        {
        }

        operator IStop&()
        {
            return *m_pStop;
        }

        CAPL_METHOD_IMPL(DWORD, GetTime)() const
        {
            return m_pStop->GetTime();
        }
    };

    class KeyWrapper
    {
        IKey* m_pKey;
    public:
        KeyWrapper(IKey& key)
            : m_pKey(&key)
        {
        }

        operator IKey&()
        {
            return *m_pKey;
        }

        operator WORD()
        {
            return m_pKey->Key();
        }
    };

    class CanErrorWrapper
    {
        ICanError* m_pCanError;
    public:
        BYTE errorCountTX;
        BYTE errorCountRX;
        BYTE CAN;
        CanErrorWrapper(ICanError& canError)
            : m_pCanError(&canError)
            , errorCountTX(m_pCanError->GetErrorCountTx())
            , errorCountRX(m_pCanError->GetErrorCountRx())
            , CAN(m_pCanError->GetChannel())
        {
        }

        virtual ~CanErrorWrapper() { }

        operator ICanError&()
        {
            return *m_pCanError;
        }

        CAPL_METHOD(BYTE, GetErrorCountRx)() const
        {
            return m_pCanError->GetErrorCountRx();
        }

        CAPL_METHOD(BYTE, GetErrorCountTx)() const
        {
            return m_pCanError->GetErrorCountTx();
        }

        CAPL_METHOD(BYTE, GetChannel)() const
        {
            return m_pCanError->GetChannel();
        }
    };

    typedef ICanError IErrorBusOff;
    typedef ICanError IErrorPassive;
    typedef ICanError IErrorActive;
    typedef ICanError IWarningLimit;

    typedef CanErrorWrapper ErrorBusOffWrapper;
    typedef CanErrorWrapper ErrorPassiveWrapper;
    typedef CanErrorWrapper ErrorActiveWrapper;
    typedef CanErrorWrapper WarningLimitWrapper;

    class ErrorFrameWrapper
    {
        IErrorFrame* m_pErrorFrame;
    public:
        QWORD time_ns;
        DWORD time;
        BYTE CAN;
        BYTE ecc;//ToDo: <- noch nicht unterstützt.
        ErrorFrameWrapper(IErrorFrame& errorFrame)
            : m_pErrorFrame(&errorFrame)
            , time_ns(m_pErrorFrame->GetTimeNS())
            , time(m_pErrorFrame->GetTime())
            , CAN(m_pErrorFrame->GetChannel())
            , ecc(0)
        {
        }

        operator IErrorFrame&()
        {
            return *m_pErrorFrame;
        }

        CAPL_METHOD_IMPL(QWORD, GetTimeNS)() const
        {
            return m_pErrorFrame->GetTimeNS();
        }
        CAPL_METHOD_IMPL(DWORD, GetTime)() const
        {
            return m_pErrorFrame->GetTime();
        }
        CAPL_METHOD_IMPL(BYTE, GetChannel)() const
        {
            return m_pErrorFrame->GetChannel();
        }
    };

    class LinWakeupFrameWrapper
    {
        ILinWakeupFrame* m_pLinWakeupFrame;
    public:
        LinWakeupFrameWrapper(ILinWakeupFrame& frame)
            : m_pLinWakeupFrame(&frame)
        {
        }

        operator ILinWakeupFrame&()
        {
            return *m_pLinWakeupFrame;
        }

        CAPL_METHOD_IMPL(QWORD, TimeNS)() const
        {
            return m_pLinWakeupFrame->Time_NS();
        }
        CAPL_METHOD_IMPL(DWORD, Time)() const
        {
            return m_pLinWakeupFrame->TIME();
        }

        CAPL_METHOD_IMPL(DWORD&, MsgOrigTime)()
        {
            return m_pLinWakeupFrame->MsgOrigTime();
        }

        CAPL_METHOD_IMPL(BYTE&, MsgTimeStampStatus)()
        {
            return m_pLinWakeupFrame->MsgTimeStampStatus();
        }

        CAPL_METHOD_IMPL(WORD&, MsgChannel)()
        {
            return m_pLinWakeupFrame->MsgChannel();
        }

        CAPL_METHOD_IMPL(BYTE&, lin_Signal)()
        {
            return m_pLinWakeupFrame->lin_Signal();
        }

        CAPL_METHOD_IMPL(BYTE&, lin_External)()
        {
            return m_pLinWakeupFrame->lin_External();
        }

        CAPL_METHOD_IMPL(QWORD&, length_ns)()
        {
            return m_pLinWakeupFrame->length_ns();
        }

        CAPL_METHOD_IMPL(QWORD&, SOF)()
        {
            return m_pLinWakeupFrame->SOF();
        }

    };

    // Interface für Array Werte von Umgebungsvariable.
    template<class T>
    class EnvVarArrayVal
    {
        IEnvVar* m_pEnvVar;
        size_t m_iNo;
    public:
        EnvVarArrayVal()
            : m_pEnvVar(0)
            , m_iNo(0)
        {
        }
        EnvVarArrayVal(const EnvVarArrayVal& val)
            : m_pEnvVar(val.m_pEnvVar)
            , m_iNo(val.m_iNo)
        {
        }
        explicit EnvVarArrayVal(IEnvVar& envVar, size_t iNo)
            : m_pEnvVar(&envVar)
            , m_iNo(iNo)
        {
        }
        CAPL_CALL operator T() const
        {
            double value;
            m_pEnvVar->GetArrayD(value, m_iNo);
            return (T)value;
        }

        CAPL_METHOD_IMPL(T, operator=)(T value)
        {
            double v = (double)value;
            m_pEnvVar->SetArrayD(v, m_iNo);
            m_pEnvVar->GetArrayD(v, m_iNo);
            return (T)v;
        }

        EnvVarArrayVal& operator=(const EnvVarArrayVal& a) { *this = (T)a; return *this; }
        bool operator==(const EnvVarArrayVal& a) { return (T)*this == (T)a; }
        bool operator!=(const EnvVarArrayVal& a) { return (T)*this != (T)a; }
        bool operator<=(const EnvVarArrayVal& a) { return (T)*this <= (T)a; }
        bool operator>=(const EnvVarArrayVal& a) { return (T)*this >= (T)a; }
        bool operator<(const EnvVarArrayVal& a) { return (T)*this < (T)a; }
        bool operator>(const EnvVarArrayVal& a) { return (T)*this > (T)a; }

        EnvVarArrayVal& operator++()  // Prefix
        {
            T val = *this;
            val++;
            *this = val;
            return *this;
        }

        EnvVarArrayVal& operator--()  // Prefix
        {
            T val = *this;
            val--;
            *this = val;
            return *this;
        }

        T operator++(int) // Postfix
        {
            T tmp = *this;
            tmp++;
            *this = tmp;
            return tmp;
        }

        T operator--(int) // Postfix
        {
            T tmp = *this;
            tmp--;
            *this = tmp;
            return tmp;
        }
    };
    template<>
    inline CAPL_CALL EnvVarArrayVal<CAPL_INT8>::operator CAPL_INT8() const
    {
        CAPL_INT8 value;
        m_pEnvVar->GetArray8(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_METHOD_IMPL(CAPL_INT8, EnvVarArrayVal<CAPL_INT8>::operator=)(CAPL_INT8 value)
    {
        m_pEnvVar->SetArray8(value, m_iNo);
        m_pEnvVar->GetArray8(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_CALL EnvVarArrayVal<CAPL_UINT8>::operator CAPL_UINT8() const
    {
        CAPL_UINT8 value;
        m_pEnvVar->GetArrayU8(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_METHOD_IMPL(CAPL_UINT8, EnvVarArrayVal<CAPL_UINT8>::operator=)(CAPL_UINT8 value)
    {
        m_pEnvVar->SetArrayU8(value, m_iNo);
        m_pEnvVar->GetArrayU8(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_CALL EnvVarArrayVal<CAPL_INT16>::operator CAPL_INT16() const
    {
        CAPL_INT16 value;
        m_pEnvVar->GetArray16(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_METHOD_IMPL(CAPL_INT16, EnvVarArrayVal<CAPL_INT16>::operator=)(CAPL_INT16 value)
    {
        m_pEnvVar->SetArray16(value, m_iNo);
        m_pEnvVar->GetArray16(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_CALL EnvVarArrayVal<CAPL_UINT16>::operator CAPL_UINT16() const
    {
        CAPL_UINT16 value;
        m_pEnvVar->GetArrayU16(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_METHOD_IMPL(CAPL_UINT16, EnvVarArrayVal<CAPL_UINT16>::operator=)(CAPL_UINT16 value)
    {
        m_pEnvVar->SetArrayU16(value, m_iNo);
        m_pEnvVar->GetArrayU16(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_CALL EnvVarArrayVal<CAPL_INT32>::operator CAPL_INT32() const
    {
        CAPL_INT32 value;
        m_pEnvVar->GetArray32(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_METHOD_IMPL(CAPL_INT32, EnvVarArrayVal<CAPL_INT32>::operator=)(CAPL_INT32 value)
    {
        m_pEnvVar->SetArray32(value, m_iNo);
        m_pEnvVar->GetArray32(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_CALL EnvVarArrayVal<CAPL_UINT32>::operator CAPL_UINT32() const
    {
        CAPL_UINT32 value;
        m_pEnvVar->GetArrayU32(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_METHOD_IMPL(CAPL_UINT32, EnvVarArrayVal<CAPL_UINT32>::operator=)(CAPL_UINT32 value)
    {
        m_pEnvVar->SetArrayU32(value, m_iNo);
        m_pEnvVar->GetArrayU32(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_CALL EnvVarArrayVal<CAPL_INT64>::operator CAPL_INT64() const
    {
        CAPL_INT64 value;
        m_pEnvVar->GetArray64(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_METHOD_IMPL(CAPL_INT64, EnvVarArrayVal<CAPL_INT64>::operator=)(CAPL_INT64 value)
    {
        m_pEnvVar->SetArray64(value, m_iNo);
        m_pEnvVar->GetArray64(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_CALL EnvVarArrayVal<CAPL_UINT64>::operator CAPL_UINT64() const
    {
        CAPL_UINT64 value;
        m_pEnvVar->GetArrayU64(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_METHOD_IMPL(CAPL_UINT64, EnvVarArrayVal<CAPL_UINT64>::operator=)(CAPL_UINT64 value)
    {
        m_pEnvVar->SetArrayU64(value, m_iNo);
        m_pEnvVar->GetArrayU64(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_CALL EnvVarArrayVal<double>::operator double() const
    {
        double value;
        m_pEnvVar->GetArrayD(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_METHOD_IMPL(double, EnvVarArrayVal<double>::operator=)(double value)
    {
        m_pEnvVar->SetArrayD(value, m_iNo);
        m_pEnvVar->GetArrayD(value, m_iNo);
        return value;
    }
    template<>
    inline CAPL_CALL EnvVarArrayVal<float>::operator float() const
    {
        double value;
        m_pEnvVar->GetArrayD(value, m_iNo);
        return (float)value;
    }
    template<>
    inline CAPL_METHOD_IMPL(float, EnvVarArrayVal<float>::operator=)(float value)
    {
        double v = (double)value;
        m_pEnvVar->SetArrayD(v, m_iNo);
        m_pEnvVar->GetArrayD(v, m_iNo);
        return (float)v;
    }
    // Interface für Umgebungsvariable.
    class EnvVarWrapperBase
    {
    protected:
        IEnvVar* m_pEnvVar;
        /* Definiert ob m_pEnvVar durch die Erzeugung dieses Wrapper-Objektes
         * ebenfalls erzeugt wurde und somit auch von diesem wieder freigegeben
         * werden muss.
         */
        bool m_boDeleteEnvVar;
    public:
        explicit EnvVarWrapperBase(const char* sId)
            : m_boDeleteEnvVar(true)
        {
            m_pEnvVar = CreateEnvVar(sId);
        }

        explicit EnvVarWrapperBase(IEnvVar& envVar)
            : m_pEnvVar(&envVar)
            , m_boDeleteEnvVar(false)
        {
        }

        EnvVarWrapperBase& AssignNewEnvVar(const char* sName)
        {
            if (m_boDeleteEnvVar)
                DeleteEnvVar(m_pEnvVar);
            m_boDeleteEnvVar = true;
            m_pEnvVar = CreateEnvVarByName(sName);
            return *this;
        }

        virtual ~EnvVarWrapperBase()
        {
            if (m_boDeleteEnvVar)
                DeleteEnvVar(m_pEnvVar);
        }

        IEnvVar::Type GetType()const
        {
            return m_pEnvVar->GetType();
        }

        CAPL_METHOD(CAPL_UINT64, Time_NS)() const
        {
            return m_pEnvVar->Time_NS();
        }

        operator IEnvVar&()
        {
            return *m_pEnvVar;
        }

        const char* GetName() const
        {
            return m_pEnvVar->GetName();
        }

        ATTRIBUTE_WRAPPER_HOLDER(m_pEnvVar);
    };

    // Interface für Umgebungsvariable.
    class EnvVarWrapper : public EnvVarWrapperBase
    {
    public:
        explicit EnvVarWrapper(const char* sId)
            : EnvVarWrapperBase(sId)
        {
        }

        explicit EnvVarWrapper(IEnvVar& envVar)
            : EnvVarWrapperBase(envVar)
        {
        }

        explicit EnvVarWrapper(const EnvVarWrapperBase& envVar)
            : EnvVarWrapperBase(envVar)
        {
        }

        virtual ~EnvVarWrapper()
        {
        }

    //#ifndef VC_BUILD
    //    operator size_t() const
    //    {
    //        CAPL_UINT32 value;
    //        m_pEnvVar->GetUInt32(value);
    //        return value;
    //    }
    //#endif

        operator bool() const
        {
            CAPL_INT32 value;
            m_pEnvVar->GetInt32(value);
            return value != false;
        }

        operator CAPL_INT8() const
        {
            CAPL_INT8 value;
            m_pEnvVar->GetInt8(value);
            return value;
        }

        operator CAPL_UINT8() const
        {
            CAPL_UINT8 value;
            m_pEnvVar->GetUInt8(value);
            return value;
        }

        operator CAPL_INT16() const
        {
            CAPL_INT16 value;
            m_pEnvVar->GetInt16(value);
            return value;
        }

        operator CAPL_UINT16() const
        {
            CAPL_UINT16 value;
            m_pEnvVar->GetUInt16(value);
            return value;
        }

        operator CAPL_INT32() const
        {
            CAPL_INT32 value;
            m_pEnvVar->GetInt32(value);
            return value;
        }

        operator CAPL_UINT32() const
        {
            CAPL_UINT32 value;
            m_pEnvVar->GetUInt32(value);
            return value;
        }

        operator CAPL_INT64() const
        {
            CAPL_INT64 value;
            m_pEnvVar->GetInt64(value);
            return value;
        }

        operator CAPL_UINT64() const
        {
            CAPL_UINT64 value;
            m_pEnvVar->GetUInt64(value);
            return value;
        }

        operator CAPL_DOUBLE() const
        {
            CAPL_DOUBLE value;
            m_pEnvVar->GetDouble(value);
            return value;
        }

    //#ifndef VC_BUILD
    //    operator int() const
    //    {
    //        CAPL_DOUBLE value;
    //        m_pEnvVar->GetDouble(value);
    //        return value;
    //    }
    //#endif

        CAPL_INT32 operator=(CAPL_INT8 value)
        {
            m_pEnvVar->SetInt32(value);
            return value;
        }

        CAPL_UINT8 operator=(CAPL_UINT8 value)
        {
            m_pEnvVar->SetInt32(value);
            return value;
        }

        CAPL_INT16 operator=(CAPL_INT16 value)
        {
            m_pEnvVar->SetInt32(value);
            return value;
        }

        CAPL_UINT16 operator=(CAPL_UINT16 value)
        {
            m_pEnvVar->SetInt32(value);
            return value;
        }

        CAPL_INT32 operator=(CAPL_INT32 value)
        {
            m_pEnvVar->SetInt32(value);
            return value;
        }

    //#ifndef VC_BUILD
    //    CAPL_INT32 operator=(int value)
    //    {
    //        m_pEnvVar->SetInt32(value);
    //        return value;
    //    }
    //#endif

        CAPL_UINT32 operator=(CAPL_UINT32 value)
        {
            m_pEnvVar->SetUInt32(value);
            return value;
        }

        CAPL_INT64 operator=(CAPL_INT64 value)
        {
            m_pEnvVar->SetInt64(value);
            return value;
        }

        CAPL_UINT64 operator=(CAPL_UINT64 value)
        {
            m_pEnvVar->SetUInt64(value);
            return value;
        }

        double operator=(double value)
        {
            m_pEnvVar->SetDouble(value);
            return value;
        }

        template<class T>
        EnvVarWrapperBase& Decrement()
        {
            T val = *this;
            val--;
            *this = val;
            return *this;
        }

        template<class T>
        EnvVarWrapperBase& Increment()
        {
            T val = *this;
            val++;
            *this = val;
            return *this;
        }

        EnvVarWrapperBase& operator++()  // Prefix
        {
            switch (GetType())
            {
            case IEnvVar::Char:     return Increment<CAPL_INT8>();
            case IEnvVar::UChar:    return Increment<CAPL_UINT8>();
            case IEnvVar::Short:    return Increment<CAPL_INT16>();
            case IEnvVar::UShort:   return Increment<CAPL_UINT16>();
            case IEnvVar::Int:      return Increment<CAPL_INT32>();
            case IEnvVar::UInt:     return Increment<CAPL_UINT32>();
            case IEnvVar::Int64:    return Increment<CAPL_INT64>();
            case IEnvVar::UInt64:   return Increment<CAPL_UINT64>();
            case IEnvVar::Double:   return Increment<double>();
            }

            return *this;
        }

        EnvVarWrapperBase& operator--()  // Prefix
        {
            switch (GetType())
            {
            case IEnvVar::Char:     return Decrement<CAPL_INT8>();
            case IEnvVar::UChar:    return Decrement<CAPL_UINT8>();
            case IEnvVar::Short:    return Decrement<CAPL_INT16>();
            case IEnvVar::UShort:   return Decrement<CAPL_UINT16>();
            case IEnvVar::Int:      return Decrement<CAPL_INT32>();
            case IEnvVar::UInt:     return Decrement<CAPL_UINT32>();
            case IEnvVar::Int64:    return Decrement<CAPL_INT64>();
            case IEnvVar::UInt64:   return Decrement<CAPL_UINT64>();
            case IEnvVar::Double:   return Decrement<double>();
            }

            return *this;
        }

        EnvVarWrapperBase operator++(int) // Postfix
        {
            EnvVarWrapperBase tmp = *this;
            ++*this;
            return tmp;
        }

        EnvVarWrapperBase operator--(int) // Postfix
        {
            EnvVarWrapperBase tmp = *this;
            --*this;
            return tmp;
        }
    };
    // Interface für Umgebungsvariable.
    template<class T>
    class EnvVarWrapperT : public EnvVarWrapperBase
    {
    public:
        explicit EnvVarWrapperT(const char* sId)
            : EnvVarWrapperBase(sId)
        {
        }

        explicit EnvVarWrapperT(IEnvVar& envVar)
            : EnvVarWrapperBase(envVar)
        {
        }

        explicit EnvVarWrapperT(const EnvVarWrapperBase& envVar)
            : EnvVarWrapperBase(envVar)
        {
        }

        virtual ~EnvVarWrapperT()
        {
        }

        operator T() const
        {
            double v = 0;
            m_pEnvVar->GetDouble(v);
            return (T)v;
        }

        T operator=(T value)
        {
            double v = (double)value;
            m_pEnvVar->SetDouble(v);
            return (T)v;
        }


        EnvVarArrayVal<T> operator[](size_t pos)const
        {
            switch (GetType())
            {
            case IEnvVar::String:
            case IEnvVar::CharVector:
            case IEnvVar::UCharVector:
            case IEnvVar::IntVector:
            case IEnvVar::UIntVector:
            case IEnvVar::Int64Vector:
            case IEnvVar::UInt64Vector:
            case IEnvVar::DoubleVector:
            case IEnvVar::ShortVector:
            case IEnvVar::UShortVector:
                return EnvVarArrayVal<T>(*m_pEnvVar, pos);
            default:
                return EnvVarArrayVal<T>();
            }
        }

        EnvVarWrapperBase& operator++()  // Prefix
        {
            T val = *this;
            val++;
            *this = val;
            return *this;
        }

        EnvVarWrapperBase& operator--()  // Prefix
        {
            T val = *this;
            val--;
            *this = val;
            return *this;
        }

        EnvVarWrapperBase operator++(int) // Postfix
        {
            EnvVarWrapperBase tmp = *this;
            ++*this;
            return tmp;
        }

        EnvVarWrapperBase operator--(int) // Postfix
        {
            EnvVarWrapperBase tmp = *this;
            --*this;
            return tmp;
        }
    };
    template<>
    inline EnvVarWrapperT<CAPL_INT8>::operator CAPL_INT8() const
    {
        CAPL_INT8 value;
        m_pEnvVar->GetInt8(value);
        return value;
    }
    template<>
    inline CAPL_INT8 EnvVarWrapperT<CAPL_INT8>::operator=(CAPL_INT8 value)
    {
        m_pEnvVar->SetInt8(value);
        return value;
    }
    template<>
    inline EnvVarWrapperT<CAPL_UINT8>::operator CAPL_UINT8() const
    {
        CAPL_UINT8 value;
        m_pEnvVar->GetUInt8(value);
        return value;
    }
    template<>
    inline CAPL_UINT8 EnvVarWrapperT<CAPL_UINT8>::operator=(CAPL_UINT8 value)
    {
        m_pEnvVar->SetUInt8(value);
        return value;
    }
    template<>
    inline EnvVarWrapperT<CAPL_INT16>::operator CAPL_INT16() const
    {
        CAPL_INT16 value;
        m_pEnvVar->GetInt16(value);
        return value;
    }
    template<>
    inline CAPL_INT16 EnvVarWrapperT<CAPL_INT16>::operator=(CAPL_INT16 value)
    {
        m_pEnvVar->SetInt16(value);
        return value;
    }
    template<>
    inline EnvVarWrapperT<CAPL_UINT16>::operator CAPL_UINT16() const
    {
        CAPL_UINT16 value;
        m_pEnvVar->GetUInt16(value);
        return value;
    }
    template<>
    inline CAPL_UINT16 EnvVarWrapperT<CAPL_UINT16>::operator=(CAPL_UINT16 value)
    {
        m_pEnvVar->SetUInt16(value);
        return value;
    }
    template<>
    inline EnvVarWrapperT<CAPL_INT32>::operator CAPL_INT32() const
    {
        CAPL_INT32 value;
        m_pEnvVar->GetInt32(value);
        return value;
    }
    template<>
    inline CAPL_INT32 EnvVarWrapperT<CAPL_INT32>::operator=(CAPL_INT32 value)
    {
        m_pEnvVar->SetInt32(value);
        return value;
    }
    template<>
    inline EnvVarWrapperT<CAPL_UINT32>::operator CAPL_UINT32() const
    {
        CAPL_UINT32 value;
        m_pEnvVar->GetUInt32(value);
        return value;
    }
    template<>
    inline CAPL_UINT32 EnvVarWrapperT<CAPL_UINT32>::operator=(CAPL_UINT32 value)
    {
        m_pEnvVar->SetUInt8(value);
        return value;
    }
    template<>
    inline EnvVarWrapperT<CAPL_INT64>::operator CAPL_INT64() const
    {
        CAPL_INT64 value;
        m_pEnvVar->GetInt64(value);
        return value;
    }
    template<>
    inline CAPL_INT64 EnvVarWrapperT<CAPL_INT64>::operator=(CAPL_INT64 value)
    {
        m_pEnvVar->SetInt64(value);
        return value;
    }
    template<>
    inline EnvVarWrapperT<CAPL_UINT64>::operator CAPL_UINT64() const
    {
        CAPL_UINT64 value;
        m_pEnvVar->GetUInt64(value);
        return value;
    }
    template<>
    inline CAPL_UINT64 EnvVarWrapperT<CAPL_UINT64>::operator=(CAPL_UINT64 value)
    {
        m_pEnvVar->SetUInt64(value);
        return value;
    }
    template<>
    inline EnvVarWrapperT<double>::operator double() const
    {
        double value;
        m_pEnvVar->GetDouble(value);
        return value;
    }
    template<>
    inline double EnvVarWrapperT<double>::operator=(double value)
    {
        m_pEnvVar->SetDouble(value);
        return value;
    }
    template<>
    inline EnvVarWrapperT<float>::operator float() const
    {
        double value;
        m_pEnvVar->GetDouble(value);
        return value;
    }
    template<>
    inline float EnvVarWrapperT<float>::operator=(float value)
    {
        m_pEnvVar->SetDouble(value);
        return value;
    }

    // Arithmetische Operatoren für Umgebungsvariablen-Attributzugriffe
    template<class T> T operator+(T t, EnvVarWrapper a) { reutrn(t + (T)a); }
    template<class T> T operator-(T t, EnvVarWrapper a) { reutrn(t - (T)a); }
    template<class T> T operator*(T t, EnvVarWrapper a) { reutrn(t * (T)a); }
    template<class T> T operator/(T t, EnvVarWrapper a) { reutrn(t / (T)a); }
    template<class T> T operator%(T t, EnvVarWrapper a) { reutrn(t % (T)a); }
    template<class T> T operator&(T t, EnvVarWrapper a) { reutrn(t & (T)a); }
    template<class T> T operator<<(T t, EnvVarWrapper a) { reutrn(t << (T)a); }
    template<class T> T operator>>(T t, EnvVarWrapper a) { reutrn(t >> (T)a); }
    template<class T> EnvVarWrapper& operator+=(T& t, EnvVarWrapper a) { a = (t += (T)a); return a; }
    template<class T> EnvVarWrapper& operator-=(T& t, EnvVarWrapper a) { a = (t -= (T)a); return a; }
    template<class T> EnvVarWrapper& operator*=(T& t, EnvVarWrapper a) { a = (t *= (T)a); return a; }
    template<class T> EnvVarWrapper& operator/=(T& t, EnvVarWrapper a) { a = (t /= (T)a); return a; }
    template<class T> EnvVarWrapper& operator%=(T& t, EnvVarWrapper a) { a = (t %= (T)a); return a; }
    template<class T> EnvVarWrapper& operator&=(T& t, EnvVarWrapper a) { a = (t &= (T)a); return a; }
    template<class T> T operator+(EnvVarWrapper a, T t) { return (t + (T)a); }
    template<class T> T operator-(EnvVarWrapper a, T t) { return ((T)a - t); }
    template<class T> T operator*(EnvVarWrapper a, T t) { return (t * (T)a); }
    template<class T> T operator/(EnvVarWrapper a, T t) { return ((T)a / t); }
    template<class T> T operator%(EnvVarWrapper a, T t) { return ((T)a % t); }
    template<class T> T operator&(EnvVarWrapper a, T t) { return (t & (T)a); }
    template<class T> T operator<<(EnvVarWrapper a, T t) { return ((T)a << t); }
    template<class T> T operator>>(EnvVarWrapper a, T t) { return ((T)a >> t); }

    // Vergleichsoperatoren  für Umgebungsvariablen-Attributzugriffe
    template<class T> bool operator==(T t, EnvVarWrapper a) { return t == (T)a; }
    template<class T> bool operator!=(T t, EnvVarWrapper a) { return t != (T)a; }
    template<class T> bool operator<=(T t, EnvVarWrapper a) { return t <= (T)a; }
    template<class T> bool operator>=(T t, EnvVarWrapper a) { return t >= (T)a; }
    template<class T> bool operator<(T t, EnvVarWrapper a) { return t < (T)a; }
    template<class T> bool operator>(T t, EnvVarWrapper a) { return t > (T)a; }

    template<class T> bool operator==(EnvVarWrapper a, T t) { return t == (T)a; }
    template<class T> bool operator!=(EnvVarWrapper a, T t) { return t != (T)a; }
    template<class T> bool operator<=(EnvVarWrapper a, T t) { return t <= (T)a; }
    template<class T> bool operator>=(EnvVarWrapper a, T t) { return t >= (T)a; }
    template<class T> bool operator<(EnvVarWrapper a, T t) { return t < (T)a; }
    template<class T> bool operator>(EnvVarWrapper a, T t) { return t > (T)a; }

    // Da der Rückgabetyp dynamisch ermitteln werden können muss, wird hier ein Macro verwendet.
    // Aufgrund des dynamischen Rückgabetyps sind Polymorphie, Templates und Funktionszeiger 
    // keine Lösungen.
    // Name darf nur einmal verwendet werden, für den Fall dass etwas wie acNames[i++] übergeben
    // wird.
    static EnvVarWrapper tempEnvVar("");
    #define getValue(name)(\
          tempEnvVar.AssignNewEnvVar(name).GetType() == IEnvVar::Double ? ((CAPL_DOUBLE)tempEnvVar) : \
                                tempEnvVar.GetType() == IEnvVar::Char ? ((CAPL_INT8)tempEnvVar) : \
                                tempEnvVar.GetType() == IEnvVar::UChar ? ((CAPL_UINT8)tempEnvVar) : \
                                tempEnvVar.GetType() == IEnvVar::Int ? ((CAPL_INT32)tempEnvVar) : \
                                tempEnvVar.GetType() == IEnvVar::UInt ? ((CAPL_UINT32)tempEnvVar) : \
                                tempEnvVar.GetType() == IEnvVar::Int64 ? ((CAPL_INT64)tempEnvVar) : \
                                tempEnvVar.GetType() == IEnvVar::UInt64 ? ((CAPL_UINT64)tempEnvVar) : \
                                tempEnvVar.GetType() == IEnvVar::Short ? ((CAPL_INT16)tempEnvVar) : \
                                tempEnvVar.GetType() == IEnvVar::UShort ? ((CAPL_UINT16)tempEnvVar) : \
                                                            -1 )

#endif

}; //namespace capl

