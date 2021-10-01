#ifndef CAPLPLUGINAPI_H__INCLUDED
#define CAPLPLUGINAPI_H__INCLUDED

#include "Defines.h"
#include <string>

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

    CAPL_FUNC(void, SetDebugInfo)(CAPL_HANDLE pThread, const char *sCAPLFileName,
        const char *sCAPLFunction, const CAPL_UINT32& uiCAPLRow);

    CAPL_FUNC(void, GetDebugInfo)(CAPL_HANDLE pThread, char *psCAPLFileName,
        char *psCAPLFunction, CAPL_UINT32 *puiCAPLRow);
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

    // Interface für Botschaften.
    class IMessage
    {
    public:
        enum Direction { Rx, Tx, TxRequest }; // Übertragungsrichtung.

        CAPL_METHOD(void, Send)() = 0;
        CAPL_METHOD(IMessage*, GetMessage)(const char* sName) = 0;
        CAPL_METHOD(ISignal*, GetSignal)(CAPL_UINT32 uiIndex, tSignalNameArr(pSigNameArr)) = 0;

        /*!
		Message identifier 
		\return Byte
		\note CAN Message Selectors . "ID" field
		*/
		CAPL_METHOD(DWORD&, ID)() = 0;
        
	    /*!
		Transmission channel or channel through which the frame has been received. Value range: 1..32 
		\return Byte
		\note CAN Message Selectors. "CAN" field
		*/
		CAPL_METHOD(BYTE&, CAN)() = 0;
        
		/*!
		The data field length of a message is coded with the DLC (Data Length Code). Value range: 0..15(Data field length: CAN messages: 0..8, CAN FD messages: 0..64, J1939 parameter groups: 0..1785)
		\return Byte
		\note CAN Message Selectors. "DLC" field
		*/
		CAPL_METHOD(BYTE&, DLC)() = 0;

        /*!
		Direction of transmission, event classification; possible values: Rx, Tx, TXREQUEST
		\return Direction
		\note CAN Message Selectors. "DIR" field
		*/
		CAPL_METHOD(Direction&, DIR)() = 0;
        
		/*!
		Remote Transmission Request; possible values: 0 (no RTR), 1 (RTR)
		\return Bool
		\note CAN Message Selectors. "RTR" field
		*/
		CAPL_METHOD(bool&, RTR)() = 0;
        
		/*!
		Remote Transmission Request; possible values: 0 (no RTR), 1 (RTR)
		\return Bool
		\note CAN Message Selectors. "TYPE" field
		*/
		CAPL_METHOD(CAPL_INT32, TYPE)() const = 0;
        
		/*!
        Transmission channel or channel through which the frame has been received. Value range: 1..32
		\return Word
		\note CAN Message Selectors. "MsgFlags" field
		*/
		CAPL_METHOD(CAPL_INT32, MsgFlags)() const = 0;
        
		/*!
		Point in time, units: 10 microseconds. This selector is not available when executing CAPL code directly on hardware interfaces. Therefore you must use Time_ns
		\return QWORD
		\note CAN Message Selectors. "TIME" field
		*/
		CAPL_METHOD(QWORD&, TIME)() = 0;
        
		/*!
		Point in time, units: nanoseconds
		\return QWORD
		\note CAN Message Selectors. "TIME_NS" field
		*/
		CAPL_METHOD(QWORD&, Time_NS)() = 0;
        
		/*!
		Point in time, units: nanoseconds
		\return Bool
		\note CAN Message Selectors. "SIMULATED" field
		*/
		CAPL_METHOD(bool&, SIMULATED)() = 0;
		
		
		/*!
		Message data byte (unsigned 8 bit); possible values for xx sets the datafield byte for the beginning of the value.*: 0…63
		\return Byte
		\note CAN Message Selectors. "Byte(x)" field
		*/
        CAPL_METHOD(BYTE&, Byte)(BYTE byteIndex) = 0;
        
		/*!
		Message data word (unsigned 16 bit); possible values for xx sets the datafield byte for the beginning of the value.*: 0…62. The index is byte-oriented; for example, word(1) references to the data beginning at byte 1 and consists of byte 1…2 (16 bit)
		\return Word
		\note CAN Message Selectors. "Word(x)" field
		*/
		CAPL_METHOD(WORD&, Word)(BYTE byteIndex) = 0;
        
		/*!
		Message data word (unsigned 32 bit); possible values for xx sets the datafield byte for the beginning of the value.*: 0…60. The index is byte-oriented; for example, dword(1) references to the data beginning at byte 1 and consists of byte 1…4 (32 bit)
		\return Dword
		\note CAN Message Selectors. "DWord(x)" field
		*/
		CAPL_METHOD(DWORD&, Dword)(BYTE byteIndex) = 0;
        
		/*!
		Message data word (unsigned 64 bit); possible values for xx sets the datafield byte for the beginning of the value.*: 0…56
		\return Qword
		\note CAN Message Selectors. "QWORDord(x)" field
		*/
		CAPL_METHOD(QWORD&, Qword)(BYTE byteIndex = 0) = 0;
        
		/*!
        Message data byte (signed 8 bit); possible values for xx sets the datafield byte for the beginning of the value.*: 0…63
		\return Char
		\note CAN Message Selectors. "Char(x)" field
		*/
		CAPL_METHOD(char&, Char)(BYTE byteIndex) = 0;
		CAPL_METHOD(short&, Short)(BYTE byteIndex) = 0;
        
		/*!
		Message data word (signed 32 bit); possible values for xx sets the datafield byte for the beginning of the value.*: 0…60. The index is byte-oriented; for example, long(1) references to the data beginning at byte 1 and consists of byte 1…4 (32 bit)
		\return Long
		\note CAN Message Selectors. "Char(x)" field
		*/
		CAPL_METHOD(CAPL_INT32&, Long)(Long byteIndex) = 0;
        
		/*!
		Message data word (signed 32 bit); possible values for xx sets the datafield byte for the beginning of the value.*: 0…60. The index is byte-oriented; for example, long(1) references to the data beginning at byte 1 and consists of byte 1…4 (32 bit)
		\return Long
		\note CAN Message Selectors. "Char(x)" field
		*/
		CAPL_METHOD(void, CopyData)(IMessage& msg) = 0;
        CAPL_METHOD(BYTE*, GetData)() = 0;        

        ATTRIBUTE_HOLDER;

		/*!
		Bit Rate Switch. Only for CAN FD messages. 0: Use arbitration bit rate for data segment; 1: use data bit rate for data segment
		\return Bool
		\note CAN Message Selectors. "BRS" field
		*/
        CAPL_METHOD(bool&, BRS)() = 0;
        
		CAPL_METHOD(bool&, EDL)() = 0;
        
		/*!
		FD Format Indicator: 0 = Classic CAN message; 1 = CAN FD message
		\return Bool
		\note CAN Message Selectors. "FDF" field
		*/
		CAPL_METHOD(bool&, FDF)() = 0;
		
		
		/*!
		Sets the values of the signals in the parameter to the start values defined in the database
		\param msg Objects where the signals shall be set
		\return 0: function was successful
		\return 1: message / frame / PDU / paramGroup wasn't found in the database
		\return 2: at least one signal start value didn't fit into the signal in the message
		\note setSignalStartValues(message msg)
		*/
        CAPL_METHOD(CAPL_INT32, SetSignalStartValues)() = 0;

        //PDU specific properties
		
		/*!
		Returns the bus type to which the channel belongs
		\param object
		\remarks The following bus types are defined:
			1|cCAN 
			2|cJ1939 
			4|cTTP 
			5|cLIN 
			6|cMOST
		\return The bus type
		\note object.BusType
		*/	
        CAPL_METHOD(CAPL_UINT32, BusType)() const = 0;
        CAPL_METHOD(const CAPL_INT8*, Name)() const = 0;
        CAPL_METHOD(CAPL_UINT32, MaxDLC)() const = 0;
        CAPL_METHOD(CAPL_INT8, UpdateBit)() const = 0;
        CAPL_METHOD(CAPL_UINT8*, Payload)() = 0;
        CAPL_METHOD(CAPL_UINT8, ValidationFlags)() const = 0;
        CAPL_METHOD(bool, IsContained)() const = 0;
        CAPL_METHOD(CAPL_UINT8, AutosarPDUType)() const = 0;
        CAPL_METHOD(CAPL_UINT8, IsSecured)() const = 0;
        CAPL_METHOD(CAPL_UINT32, BitCount)() const = 0;
    };

    // Interface für LinWakeupFrame.
    class ILinWakeupFrame
    {
    public:
	
		/*!
        Time stamp synchronized with the global time base on the PC (CAN hardware or PC system clock). Unit: 10µs
		\return Qword
		\note linWakeupFrame Selectors. "Time" field
		*/
        CAPL_METHOD(QWORD&, TIME)() = 0;
        
		/*!
		Time stamp synchronized with the global time base on the PC (CAN hardware or PC system clock). Unit: nanoseconds
		\return Qword
		\note linWakeupFrame Selectors. "Time_NS" field
		*/
		CAPL_METHOD(QWORD&, Time_NS)() = 0;
        
		/*!
		Time stamp generated by the LIN hardware. This time stamp is unsynchronized with the global time base on the PC and thus still original. It can be used to compare the time of two LIN hardware generated events. Unit: 10µs
		\return Dword
		\note linWakeupFrame Selectors. "MsgOrigTime" field
		*/
		CAPL_METHOD(DWORD&, MsgOrigTime)() = 0;
        
		/*!
		Status of MsgOrigTime: Bit 0 set - Time stamp is valid; Bit 0 not set - Time stamp is invalid; Bit 1 set - Time stamp was generated by software; Bit 1 not set - Time stamp was generated by hardware; Bit 4 - Has a bus-specific meaning; not currently in use for LIN
 		\return Byte
		\note linWakeupFrame Selectors. "MsgTimeStampStatus" field
		*/
		CAPL_METHOD(BYTE&, MsgTimeStampStatus)() = 0;
        
		/*!
		Channel through which the event was received. Value range: 1..32
		\return Word
		\note linWakeupFrame Selectors. "MsgChannel" field
		*/
		CAPL_METHOD(WORD&, MsgChannel)() = 0;
        
        /*!
		Signal actually received. Because of different baud rates of transmitter and receiver in Sleep state, this signal can take on the legal values 0x00, 0x80 (signal actually transmitted) and 0xC0
		\return Byte 
		\note linWakeupFrame Selectors. "lin_Signal" field
		*/
		CAPL_METHOD(BYTE&, lin_Signal)() = 0;
        
		/*!
		indicates whether the wake-up signal was received by an external device (selector set) or transmitted by the LIN hardware itself (selector not set)
		\return Byte 
		\note linWakeupFrame Selectors. "lin_External" field
		*/
		CAPL_METHOD(BYTE&, lin_External)() = 0;
        
		/*!
		Length of the wake-up signal in ns
		\return Qword 
		\note linWakeupFrame Selectors. "length_ns" field
		*/
		CAPL_METHOD(QWORD&, length_ns)() = 0;
        
		/*!
		Start of wake-up signal time stamp in ns
		\return Qword 
		\note linWakeupFrame Selectors. "SOF" field
		*/
		CAPL_METHOD(QWORD&, SOF)() = 0;
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
        CAPL_METHOD(void, SetHexValue))(QWORD val) const = 0;
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
        
		/*!
		Sets a timer
		\param Timer variable
		\param msTimer variable
		\note void msTimer::set(long); 
		*/
		CAPL_METHOD(void, Set)(DWORD u32Timer, DWORD u32Nano = 0) = 0;
		
		/*!
		Sets a cyclical timer
		\param firstDuration Time in milliseconds until the timer runs out for the first time
		\param period Time in milliseconds in which the timer is restarted in case of expiration
		\note void msTimer::setCyclic(long firstDuration, long period);
		*/
        CAPL_METHOD(void, SetCyclic)(CAPL_INT32 firstDuration, CAPL_INT32 period = -1) = 0;
        
		/*!
		Stops an active timer
		\note void msTimer::cancel(); 
		*/
		CAPL_METHOD(void, Kill)() = 0;
        CAPL_METHOD(bool, UpdateTimer)() = 0;
		
		/*!
		Returns a value indicating how much more time will elapse before an on timer event procedure is called. For form 1, the time value is returned in seconds; for form 2, the time value is returned in milliseconds.  If the timer is not active, -1 is returned. This is also the case in the on timer event procedure itself
		\return Time to go until the timer elapses and the event procedure is called
		\note long msTimer::timeToElapse(); 
		*/
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
	
		/*!
		Reinitializes the object for the given service or primitive. Diagnostics request will be initialized with the default request parameters of the service, while diagnostic responses will be initialized with the default parameters of the first or specified primitive of the service. If the service is not defined, or the primitive is not defined at the given service, nothing happens and an error code is returned
		\param object Diagnostics object to re-initialize
		\param serviceQualifier Qualifier of the service that should be used for reinterpretation
		\param Qualifier of the service primitive that should be used for reinterpretation
		\return 0: No error, OK
		\return <0: Error code
		\note diagReponse::Initialize( );
		*/
        CAPL_METHOD(CAPL_INT32, Initialize)() = 0;
		
		/*!
		Reinitializes the object for the given service or primitive. Diagnostics request will be initialized with the default request parameters of the service, while diagnostic responses will be initialized with the default parameters of the first or specified primitive of the service. If the service is not defined, or the primitive is not defined at the given service, nothing happens and an error code is returned
		\param serviceQualifier Qualifier of the service that should be used for reinterpretation
		\return 0: No error, OK
		\return <0: Error code
		\note diagReponse::Initialize( char serviceQualifier[]);
		*/
        CAPL_METHOD(CAPL_INT32, Initialize)(const char serviceQualifier[]) = 0;

		/*!
		Sets the numeric parameter to the specified value
		\param parameterName Parameter qualifier (NOT the language-dependent name of the parameter!)
		\param newValue Numeric value to which the parameter should be set
		\return Error code 
		\note diagResponse::SetParameter (char parameterName[], double newValue)
		*/
        CAPL_METHOD(CAPL_INT32, SetParameter)(char parameterName[], double newValue) = 0;
        
		/*!
		Sets the numeric parameter to the specified value
		\param parameterName Parameter qualifier (NOT the language-dependent name of the parameter!)
		\param newValue Numeric value to which the parameter should be set
		\param mode Access mode
		\return Error code
		\note diagResponse::SetParameter (long mode, char parameterName[], double newValue)
		*/
		CAPL_METHOD(CAPL_INT32, SetParameter)(CAPL_INT32 mode, char parameterName[], double newValue) = 0;
        
		/*!
		Sets a parameter to the symbolically-specified value. This is possible for all parameters, also numeric ones
		\param parameterName Parameter qualifier
		\param newValue Symbolic value
		\return Error code 
		\note diagResponse::SetParameter (char parameterName[], char newValue[])
		*/
		CAPL_METHOD(CAPL_INT32, SetParameter)(char parameterName[], char newValue[]) = 0;
			
		/*!
		Returns the value of the numeric parameter, either in an output field or as return value (without the possibility of checking the correct function)
		\param parameterName
		\param Output Parameter qualifier
		\return Error code or value of the parameter or 0.0 if this could not be acquired
		\note long diagResponse::GetParameter (char parameterName[], double output[1])
		*/
        CAPL_METHOD(CAPL_INT32, GetParameter)(char parameterName[], double output[1]) const = 0;
        
		/*!
		Returns the value of the numeric parameter, either in an output field or as return value (without the possibility of checking the correct function)
		\param parameterName Parameter qualifier
		\return Error code or value of the parameter or 0.0 if this could not be acquired
		\note long diagResponse::GetParameter (char parameterName[])
		*/
		CAPL_METHOD(double, GetParameter)(char parameterName[]) const = 0;
        
		/*!
		Returns the value of the numeric parameter, either in an output field or as return value (without the possibility of checking the correct function)
		\param parameterName Parameter qualifier
		\param Output Parameter qualifier
		\param mode Access mode
		\return Error code or value of the parameter or 0.0 if this could not be acquired
		\note long diagResponse::GetParameter (long mode, char parameterName[], double output[1])
		*/
		CAPL_METHOD(CAPL_INT32, GetParameter)(CAPL_INT32 mode, char parameterName[], double output[1]) const = 0;
        
		/*!
		Returns the value of the numeric parameter, either in an output field or as return value (without the possibility of checking the correct function)
		\param parameterName Parameter qualifier
		\param mode Access mode
		\return Error code or value of the parameter or 0.0 if this could not be acquired
		\note long diagResponse::GetParameter (long mode, char parameterName[])
		*/
		CAPL_METHOD(double, GetParameter)(CAPL_INT32 mode, char parameterName[]) const = 0;
		
		/*!
		Returns the symbolic value of the parameter. This functions for all parameters. The values received can be used for setting the parameter
		\param parameterName Parameter qualifier
		\param buffer Output field
		\param buffersize Size of the buffer
		\return Number of chars written to buffer or error code
		\note diagResponse::GetParameter (char parameterName[], char buffer[], dword buffersize)
		*/
        CAPL_METHOD(CAPL_INT32, GetParameter)(char parameterName[], char buffer[], CAPL_UINT32 buffersize) const = 0;
		
		/*!
		Reads/sets the raw data of the complete service primitive (all data that is transmitted via the transport protocol). When setting the data the length of the primitive is not changed
		\param buffer Input/output buffer
		\param buffersize Buffer size
		\return Number of bytes copied into the buffer or error code
		\note diagResponse::SetPrimitiveData (byte* buffer, DWORD buffersize)
		*/
        CAPL_METHOD(CAPL_INT32, SetPrimitiveData)(CAPL_UINT8 buffer[], CAPL_UINT32 buffersize) = 0;
        
		/*!
		Reads/sets the raw data of the complete service primitive (all data that is transmitted via the transport protocol). When setting the data the length of the primitive is not changed
		\param buffer Input/output buffer
		\param buffersize Buffer size
		\return Number of bytes copied into the buffer or error code
		\note diagResponse::GetPrimitiveData (byte* buffer, DWORD buffersize)
		*/
		CAPL_METHOD(CAPL_INT32, GetPrimitiveData)(CAPL_UINT8 buffer[], CAPL_UINT32 buffersize) const = 0;
	
		/*!
		Adapt size of a diagnostic object to match specified parameter iterations, or set size of bus message to given number of byte
		\param byteCount Length of data to send
		\return Error code 
		\note diagResponse::Resize (dword byteCount)
		*/
		CAPL_METHOD(CAPL_INT32, Resize)(CAPL_UINT32 byteCount) = 0;

		/*!
		Returns the byte length of the object
		\return >0: Number of bytes
		\return <0: Error code
		\note diagRequest::GetPrimitiveSize();
		*/
		CAPL_METHOD(CAPL_INT32, GetPrimitiveSize)() const = 0;
		
        CAPL_METHOD(void, CopyData)(IDiagBase& msg) = 0;
		/*!
		Sets or specifies the value of a (complex) parameter directly via uncoded data bytes
		\param parameterName Parameter qualifier
		\param buffer Input/output buffer
		\param buffersize Buffer size
		\return 0 if bytes were copied, otherwise <0 for an error code
		\note long diagResponse::GetParameterRaw (char parameterName[], byte* buffer, DWORD buffersize)
		*/
        CAPL_METHOD(CAPL_INT32, GetParameterRaw)(const char parameter[], BYTE buffer[], CAPL_UINT32 buffersize) const = 0;
        
		/*!
		Sets or specifies the value of a (complex) parameter directly via uncoded data bytes
		\param parameterName Parameter qualifier
		\param buffer Input/output buffer
		\param buffersize Buffer size
		\return 0 if bytes were copied, otherwise 
		\return <0 for an error code
		\note long diagResponse::SetParameterRaw (char parameterName[], byte* buffer, DWORD buffersize)
		*/
		CAPL_METHOD(CAPL_INT32, SetParameterRaw)(const char parameter[], BYTE buffer[], CAPL_UINT32 buffersize) const = 0;
    };

    // Interface für DiagResponse.
    class IDiagResponse : public IDiagBase
    {
    public:
	
		/*!
		Reinitializes the object for the given service or primitive
		\param serviceQualifier Diagnostics object to re-initialize
		\param primitiveQualifier Qualifier of the service primitive that should be used for reinterpretation
		\return 0: No error, OK
		\return <0: Error code
		\note diagReponse::Initialize( char serviceQualifier[], char primitiveQualifier[]);
		*/
        CAPL_METHOD(CAPL_INT32, Initialize)(const char serviceQualifier[], const char primitiveQualifier[]) = 0;
       
	   
		/*!
		Sends the response object back to the tester. Can only be called in the ECU simulation
		\return Error code
		\note diagResponse::SendResponse ()
		*/
		CAPL_METHOD(CAPL_INT32, SendResponse)() = 0;
       
		/*!
		Returns value <> 0 if the object is a negative response to a request
		\return 0 or <>0
		\note diagResponse::IsNegativeResponse ()
		*/
		CAPL_METHOD(CAPL_INT32, IsNegativeResponse)() const = 0;
        
		/*!
		Returns the code of the specified response or last received response (for the specified request)
		\return -1: The response was positive, i.e. there is no error code
		\return 0: No response has been received yet
		\return >0: Error code of the negative response
		\return Error code
		\note long diagResponse::GetResponseCode(); 
		*/
		CAPL_METHOD(CAPL_INT32, GetResponseCode)() const = 0;
        
		/*!
		Returns the code of the specified response or last received response (for the specified request)
		\return -1: The response was positive, i.e. there is no error code
		\return 0: No response has been received yet
		\return >0: Error code of the negative response
		\return Error code
		\note long diagRequest::GetLastResponseCode() 
		*/
		CAPL_METHOD(CAPL_INT32, GetLastResponse)() const = 0;
        
		/*!
		Sends the response object back to the tester. Can only be called in the ECU simulation
		\return Error code 
		\note diagResponse::SendPositiveResponse()
		*/
		CAPL_METHOD(CAPL_INT32, IsPositiveResponse)() const = 0;
    };

    // Interface für DiagRequest.
    class IDiagRequest : public IDiagBase
    {
    public:
		
		/*!
		Sends the request object to the ECU
		\return Error code 
		\note diagRequest::SendRequest()
		*/
        CAPL_METHOD(CAPL_INT32, SendRequest)() = 0;
         
		 
		/*!
		Waits for the arrival of the response to the given request
		\param timeout [ms] Maximum wait time
		\return <0: An internal error occurred, e.g. a protocol error or a faulty configuration of the diagnostic layer
		\return 0: The timeout was reached, i.e. the event of interest did not occur within the specified time
		\return	1: The event occurred
		\note long TestWaitForDiagResponse (dword timeout);
		*/ 
		CAPL_METHOD(CAPL_INT32, WaitForDiagResponse)(CAPL_UINT32 timeout) const = 0;
        
		/*!
		Returns the code of the specified response or last received response (for the specified request)
		\return -1: The response was positive, i.e. there is no error code
		\return 0: No response has been received yet
		\return >0: Error code of the negative response
		\return Error code
		\note long diagRequest::GetLastResponseCode();
		*/
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
        virtual CAPL_CALL operator BYTE() = 0;
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
        CAPL_METHOD(BYTE&, Byte)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(WORD&, Word)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(DWORD&, Dword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(QWORD&, Qword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(char&, Char)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(short&, Short)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(CAPL_INT32&, Long)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(CAPL_INT64&, LongLong)(CAPL_INT32 byteIndex) = 0;

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
	
		/*!
        4-bit internet protocol version number = 4
		\return Byte
		\note IPv4 protocol Header structure. "Version" field
		*/
        CAPL_METHOD(IttProtocolByteField&, version)() = 0;
        
		/*!
		Internet header length This is the length of the IP header as a multiple of 4 bytes. It is necessary due to possible options
		\return Byte
		\note IPv4 protocol Header structure. "Ihl" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, ihl)() = 0;
        
		/*!
        The Differentiated Services Code Point value (DSCP) sets the priority of the packet. The first three bits specify the Class Selector, the next three bits specify the Drop-Precedence
		\return Byte
		\note IPv4 protocol Header structure. "Dscp" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, dscp)() = 0;
        
		/*!
		If there is an overload possible the router can signal this with the Explicit Congestion Notification (ECN). 00: The sending host does not support ECN.  01 or 10: The sending host supports ECN.  11: A router is busy
		\return Byte
		\note IPv4 protocol Header structure. "Ecn" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, ecn)() = 0;
        
		/*!
        Total length of a single IP datagram in bytes (IP header plus payload)
		\return Word
		\note IPv4 protocol Header structure. "Lenght" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, length)() = 0;
        
		/*!
		Identification of a datagram. If a datagram is fragmented each fragment has the same identification number
		\return Word 
		\note IPv4 protocol Header structure. "Identification" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, identification)() = 0;
        
		/*!
		Various control flags: bit 0: reserved, must be zero; bit 1: don't fragment this datagram (DF); bit 2: more fragments flag (MF)
		\return Byte 
		\note IPv4 protocol Header structure. "Flags" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, flags)() = 0;
        
		/*!
		The offset identifies the position of a fragment in the original IP packet. Due fragments always have a length of a multiple of 8 byte, 13 bit are adequate 
		\return Word 
		\note IPv4 protocol Header structure. "Offset" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, offset)() = 0;
        
		/*!
		This value is decremented of the packet passed the router. If the value is set to 0 the packet is discarded
		\return Byte 
		\note IPv4 protocol Header structure. "Ttl" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, ttl)() = 0;
        
		/*!
		Indication of the transported protocol: 1: ICMPv4; 2: IGMP; 6: TCP; 17: UDP
		\return Byte 
		\note IPv4 protocol Header structure. "Protocol" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, protocol)() = 0;
        
		/*!
		Checksum of the header. Some header fields can change (e.g. ttl), so the checksum is calculated and verified at each point that the Internet header is processed
		\return Word 
		\note IPv4 protocol Header structure. "Checksum" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, checksum)() = 0;
        
		/*!
		source IPv4 address. The first byte is the Source Network. The following three bytes are the Source Local Address
		\return DWord 
		\note IPv4 protocol Header structure. "Source" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolDWordField&, source)() = 0;
        
		/*!
		destination IPv4 address. The first byte is the Destination Network. The following three bytes are the Destination Local Address
		\return DWord 
		\note IPv4 protocol Header structure. "Destination" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolDWordField&, destination)() = 0;
    };
    // Interface für IPv6 in IEthernetPacket.
    class IIPv6 : public IEthernetPacketProtocol
    {
    public:
		
		/*!
		4-bit Internet Protocol version number = 6
		\return Byte
		\note IPv6 protocol Header structure. "Version" field
		*/
        CAPL_METHOD(IEthernetPacketProtocolByteField&, version)() = 0;
		
		/*!
		This eight bit field is available for use by originating nodes and/or forwarding routers to identify and distinguish between different classes or priorities of IPv6 packets
		\return Byte
		\note IPv6 protocol Header structure. "Class" field
		*/
        CAPL_METHOD(IEthernetPacketProtocolByteField&, Class)() = 0;
       
		/*!
		This field in the IPv6 header may be used by a source to label sequences of packets for which it requests special handling by the IPv6 routers, such as non-default quality of service or "real-time" service
		\return DWord
		\note IPv6 protocol Header structure. "Flow" field
		*/	
        CAPL_METHOD(IEthernetPacketProtocolDWordField&, flow)() = 0;
        
		/*!
        Length of the IPv6 payload (without header) and the extension headers
		\return Word
		\note IPv6 protocol Header structure. "Length" field
		*/ 
		CAPL_METHOD(IEthernetPacketProtocolWordField&, length)() = 0;
        
		/*!
        This field identifies the type of the next header, this can also be an extension header. The same values as in the IPv4 protocol field are used
		\return Byte
		\note IPv6 protocol Header structure. "Next" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, next)() = 0;
        
		/*!
        This fields sets the maximum number of steps through routers that a packet is allowed to cover. It is decremented by one on passing a router (hops). Packets with a hop limit of 0 are discarded
		\return Byte
		\note IPv6 protocol Header structure. "HopLimit" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, hopLimit)() = 0;
        
		/*!
        128-bit address of the sender of the packet
		\return QWord
		\note IPv6 protocol Header structure. "Source" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolQWordField&, source)() = 0;
        
		/*!
		128-bit address of the receiver of the packet. The receiver is possibly not the final receiver, if a routing header is present
		\return QWord
		\note IPv6 protocol Header structure. "Destination" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolQWordField&, destination)() = 0;
    };
    // Interface für ICMIPv6 in IEthernetPacket.
    class IICMPv6 : public IEthernetPacketProtocol
    {
    public:
		
		/*!
		ICMP type
		\return Byte
		\note ICMPv6 protocol Header structure. "Type" field
		*/
        CAPL_METHOD(IEthernetPacketProtocolByteField&, type)() = 0;
        
		/*!
		Depends on the message type, and it is used to create an additional level of message granularity
		\return Byte
		\note ICMPv6 protocol Header structure. "Code" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, code)() = 0;
        
		/*!
		Depends on the message type, and it is used to create an additional level of message granularity
		\return Word
		\note ICMPv6 protocol Header structure. "Checksum" field
		*/
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
	
		/*!
		Default value that should be placed in the Hop Count field of the IP header for outgoing IP packets
		\return Byte
		\note NDP protocol Header structure. "CurHopLimit" field
		*/
        CAPL_METHOD(IEthernetPacketProtocolByteField&, curHopLimit)() = 0;
        
		/*!
		When set, it indicates that addresses are available via DHCPv6
		\return Byte
		\note NDP protocol Header structure. "ManagedAddrConfig" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, managedAddrConfig)() = 0;
        
		
		/*!
		When set, it indicates that other configuration information is available via DHCPv6
		\return Byte
		\note NDP protocol Header structure. "OtherConfig" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, otherConfig)() = 0;
        
		/*!
		When set, it indicates that the router sending this router Advertisement is also functioning as a mobile IPv6 home agent on this link
		\return Byte
		\note NDP protocol Header structure. "MobileHomeAgent" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, mobileHomeAgent)() = 0;
        
		/*!
		Indicates whether to prefer this router over other default routers(01 - high; 00 - medium; 11 - low; 10 - reserved)
		\return Byte
		\note NDP protocol Header structure. "DefaultRouterPrefs" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, defaultRouterPrefs)() = 0;
        
		/*!
		\return Byte
		\note NDP protocol Header structure. "neighborDiscoveryProxy" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, neighborDiscoveryProxy)() = 0;
        
		/*!
		Lifetime associated with the default router in units of seconds
		\return Word
		\note NDP protocol Header structure. "RouterLifeTime" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, routerLifeTime)() = 0;
        
		/*!
		Time in milliseconds that a node assumes a neighbor is reachable after having received a reachability confirmation
		\return DWord
		\note NDP protocol Header structure. "ReachableTime" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolDWordField&, reachableTime)() = 0;
        
		/*!
		Time in milliseconds between retransmitted Neighbor Solicitation messages
		\return DWord
		\note NDP protocol Header structure. "RetransTimer" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolDWordField&, retransTime)() = 0;
    };
    // Interface für NeighborSolicitation NDP in IEthernetPacket.
    class INDPNeighborSolicitation : public IEthernetPacketProtocol
    {
    public:
	
		/*!
		Time in milliseconds between retransmitted Neighbor Solicitation messages
		\return QWord
		\note NDP protocol Header structure. "Target Address" field
		*/
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, target)() = 0;
    };
    // Interface für NeighborAdvertisement NDP in IEthernetPacket.
    class INDPNeighborAdvertisement : public IEthernetPacketProtocol
    {
    public:
		
		/*!
		When set, it indicates that the sender is a router
		\return Byte
		\note NDP protocol Header structure. "RouterFlag" field
		*/
        CAPL_METHOD(IEthernetPacketProtocolByteField&, routerFlag)() = 0;
        
		/*!
		When set, it indicates that the advertisement was sent in response to a Neighbor Solicitation
		\return Byte
		\note NDP protocol Header structure. "SolicitedFlag" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, solicitedFlag)() = 0;
        
		/*!
		When set, it indicates that the advertisement should override an existing cache entry and update the cached link-layer address
		\return Byte
		\note NDP protocol Header structure. "OverrideFlag" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, overrideFlag)() = 0;
        
		/*!
		For a solicited advertisement: target address field in the Neighbor Solicitation message. For an unsolicited advertisement: address whose link-layer address has changed
		\return QWord
		\note NDP protocol Header structure. "Target Address" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolQWordField&, target)() = 0;
    };
    // Interface für Redirect NDP in IEthernetPacket.
    class INDPRedirect : public IEthernetPacketProtocol
    {
    public:
	
		/*!
		IP address that is a better first hop to use for the ICMP destination address
		\return QWord
		\note NDP protocol Header structure. "Target Address" field
		*/
        CAPL_METHOD(IEthernetPacketProtocolQWordField&, target)() = 0;
        
		/*!
		IP address of the destination that is redirected to the target
		\return QWord
		\note NDP protocol Header structure. "Destination Address" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolQWordField&, destination)() = 0;
    };
    // Interface für NDP in IEthernetPacket.
    class INDP
    {
    public:
	
		/*!
		A host can request the router to send a Router Advertisement message
		\note NDP protocol Header structure. "routerSolicitation" field
		*/
        CAPL_METHOD(INDPRouterSolicitation&, routerSolicitation)() = 0;
        
		/*!
		With this message the router announce themselves in the network. The message is sent from the router cyclically or is requested with a Router Solicitation message
		\note NDP protocol Header structure. "routerAdvertisement" field
		*/
		CAPL_METHOD(INDPRouterAdvertisement&, routerAdvertisement)() = 0;
		 
		/*!
		A node can request the network address of another node with this message, or check if a node is still available. It can be checked too if an address is already assigned
		\note NDP protocol Header structure. "neighborSolicitation" field
		*/
		CAPL_METHOD(INDPNeighborSolicitation&, neighborSolicitation)() = 0;
        	 
		/*!
		This message is the answer to a Neighbor Solicitation message. A node can send this message too to announce a network address change
		\note NDP protocol Header structure. "neighborAdvertisement" field
		*/
		CAPL_METHOD(INDPNeighborAdvertisement&, neighborAdvertisement)() = 0;
        
		/*!
		This message is used by the router to show a host a better way to a destination node
		\note NDP protocol Header structure. "redirect" field
		*/
		CAPL_METHOD(INDPRedirect&, redirect)() = 0;
        
		/*!
		IP address of the destination that is redirected to the target
		\return QWord
		\note NDP protocol Header structure. "target" field
		*/
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
	
		/*!
		Source port number
		\return Word
		\note TCP protocol Header structure. "Source" field
		*/
        CAPL_METHOD(IEthernetPacketProtocolWordField&, source)() = 0;
        
		
		/*!
		Destination port number
		\return Word
		\note TCP protocol Header structure. "Destination" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, destination)() = 0;
        
		/*!
		Destination port number
		\return DWord
		\note TCP protocol Header structure. "Sequence" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolDWordField&, sequence)() = 0;
        
		/*!
		Destination port number
		\return DWord
		\note TCP protocol Header structure. "AckNumber" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolDWordField&, ackNumber)() = 0;
        
		/*!
		Number of 32 bit words in the TCP Header. This indicates where the data begins. The TCP header including options is an integral number of 32 bits long
		\return Byte
		\note TCP protocol Header structure. "Offset" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, offset)() = 0;
        
		/*!
		8 bits (from lsb to msb): 
			FIN -	more data from sender
			SYN - Synchronize sequence numbers
			RST - Reset the connection
			PSH - Push function
			ACK - Acknowledgment field significant
			URG - Urgent Pointer field significant
			ECE - ECN-Echo
			CWR - Congestion Window Reduced
		\return Byte
		\note TCP protocol Header structure. "Flags" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolByteField&, flags)() = 0;
        
		/*!
		The number of data bytes beginning with the one indicated in the field which the sender of this segment is willing to accept
		\return Word
		\note TCP protocol Header structure. "Window" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, window)() = 0;
        
		/*!
		The 16 bit one's complement of the one's complement sum of all 16 bit words in the header and text. If a segment contains an odd number of header and text bytes to be checksummed, the last byte is padded on the right with zeros to form a 16 bit word for checksum purposes. The pad is not transmitted as part of the segment. While computing the checksum, the checksum field itself is replaced with zeros
		\return Word
		\note TCP protocol Header structure. "Checksum" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, checksum)() = 0;
        
		
		/*!
		16 bit offset from the sequence number indicating the last urgent data byte
		\return Word
		\note TCP protocol Header structure. "Pointer" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, pointer)() = 0;
    };
    // Interface für UDP in IEthernetPacket.
    class IUDP : public IEthernetPacketProtocol
    {
    public:
	
		/*!
		Port number of the source (optional), 0: unknown port
		\return Word
		\note UDP protocol Header structure. "Source" field
		*/
        CAPL_METHOD(IEthernetPacketProtocolWordField&, source)() = 0;
        
		/*!
		Port number of the destination
		\return Word
		\note UDP protocol Header structure. "Destination" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, destination)() = 0;
        
		/*!
		Length of this user datagram (in bytes) including this header and the data. This means the minimum value of the length is eight
		\return Word
		\note UDP protocol Header structure. "Length" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, length)() = 0;
        
		/*!
		Optional checksum, composed of information of the IP header, the UDP header and the data
		\return Word
		\note UDP protocol Header structure. "Checksum" field
		*/
		CAPL_METHOD(IEthernetPacketProtocolWordField&, checksum)() = 0;
    };
    // Interface für Ethernet Botschaften.
    class IEthernetPacket
    {
    public:

        CAPL_METHOD(void, Send)() = 0;

		
        CAPL_METHOD(QWORD, Time_NS)() const = 0;
        CAPL_METHOD(BYTE, DIR)() const = 0;
        CAPL_METHOD(WORD&, MsgChannel)() = 0;
		
        CAPL_METHOD(WORD&, Length)() = 0;
		
        CAPL_METHOD(DWORD, FCS)() const = 0;
        CAPL_METHOD(CAPL_INT64, FrameLen)() const = 0;
		CAPL_METHOD(CAPL_INT64, SOF)() const = 0;
		
		/*!
		Determines the type of the environment variable
		\remarks The Environment object is only available in CANoe. The following types are defined:
			0: Integer 
			1: Float 
			2: String 
			3: Data 
		\param 
		\return 
		\note 
		*/
        CAPL_METHOD(IProperty<WORD>&, Type)() = 0;
        CAPL_METHOD(CAPL_INT32&, TypeInternal)() = 0;
		
		/*!
		Returns a Files object for each source file of the exporter or offline source
		\remarks On the first access to the source files of the exporter the Files object contains the source file as defined in the Export dialog
		\param object
		\return A Files object
		\note object.Sources
		*/
        CAPL_METHOD(QWORD&, Source)() = 0;
		
		/*!
		Returns a Files object for the destination files of an exporter
		\remarks On the first access to the destination files of an exporter the Files object contains the destination file as it is defined in the according Export dialog
		\param object
		\return The Files object
		\note object.Destinations 
		*/
        CAPL_METHOD(QWORD&, Destination)() = 0;

        CAPL_METHOD(BYTE&, Byte)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(WORD&, Word)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(DWORD&, Dword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(QWORD&, Qword)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(char&, Char)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(short&, Short)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(CAPL_INT32&, Long)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(CAPL_INT64&, LongLong)(CAPL_INT32 byteIndex) = 0;
        CAPL_METHOD(void, CopyData)(IEthernetPacket& msg) = 0;
        CAPL_METHOD(void, StartGenerator)(DWORD transmissionRate, DWORD numberOfFrames, DWORD counterByteOffset) = 0;
        
		/*!
		This function stops the Ethernet packet generator
		\return 0 or error code
		\note long EthStopPacketGenerator( );
		*/
		CAPL_METHOD(void, StopGenerator)() = 0;

		/*!
		Removes all elements from the field
		\note void map[int64]::clear();
		*/
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

		//IpGetAddressAsNumber//
        CAPL_METHOD(IIPv4&, IPv4)() = 0;
		//IpGetAddressAsArray//
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
		
		//on ethernetStatus //
		//ethSetLinkStatus//
		//ethGetLinkStatus//
		
		/*!
		Configures the channel of the Vector hardware to establish or destablish a link
		\param channel Ethernet channel number. Value range: 1..32
		\param status 0 = link down; 1 = link up
		\return 0: Success
		\return 1: Invalid channel
		\return 2: Read configuration failed
		\return 3: Link down failed
		\return 4: Link up failed
		\return 5: Restore bypass failed
		\return 6: Invalid link state
		\note long EthSetLinkStatus( long channel, long status );
		*/
        CAPL_METHOD(LONG, Status)() const = 0;
        CAPL_METHOD(DWORD, Bitrate)() const = 0;
        CAPL_METHOD(WORD&, MsgChannel)() = 0;
        CAPL_METHOD(WORD&, HwChannel)() = 0;
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

    class DebugInfo
    {
    private:
        DebugInfo() { };
        DebugInfo(const DebugInfo&) { };

        CAPL_HANDLE m_pThread;

        // Speichert die zuvor gesetzten Debug-Informationen im Konstruktor
        // und stellt sie im Destruktor wieder her.
        char m_sPrevCAPLFileName[MAX_PATH];
        char m_sPrevCAPLFunction[MAX_PATH];
        CAPL_UINT32 m_uiPrevCAPLRow;

    public:
        DebugInfo(CAPL_HANDLE thread, const char *sCAPLFileName,
            const char *sCAPLFunction,
            const CAPL_UINT32& uiCAPLRow)
            : m_pThread(thread)
            , m_uiPrevCAPLRow(0)
        {
            ZeroMemory(m_sPrevCAPLFileName, sizeof(char) * MAX_PATH);
            ZeroMemory(m_sPrevCAPLFunction, sizeof(char) * MAX_PATH);

            capl::GetDebugInfo(m_pThread, m_sPrevCAPLFileName, m_sPrevCAPLFunction, &m_uiPrevCAPLRow);
            capl::SetDebugInfo(m_pThread, sCAPLFileName, sCAPLFunction, uiCAPLRow);
        }

        ~DebugInfo()
        {
            capl::SetDebugInfo(m_pThread, m_sPrevCAPLFileName, m_sPrevCAPLFunction, m_uiPrevCAPLRow);
        }
    };

    class TestInfo
    {
    private:
        TestInfo() { };
        TestInfo(const TestInfo&) { };

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

    // Basis-Interface für ein CAPL-Event.
    template<class T>
    class IEvent
    {
    public:
        IEvent() : m_hEvent(NULL) {}
        CAPL_METHOD(void, OnEvent)(T) = 0;
    protected:
        CAPL_HANDLE m_hEvent;
    };

    // Templatespezialisierung damit der GCC nicht meckert, da dieser den Typ
    // "T=void" in der Parameterliste von OnEvent akzeptiert.
    template<>
    class IEvent<void>
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
        MessageEvent(DWORD u32FileIndex, const char* sFilter);
        virtual ~MessageEvent();
    };

    // Signaländerungs-Event.
    class SignalEvent : public IEvent<ISignalVal&>
    {
    public:
        SignalEvent(const char* sIds);
        SignalEvent(ISignalVal& sig);
        virtual ~SignalEvent();
    };

    // Signalupdate-Event.
    class SignalUpdateEvent : public IEvent<ISignalVal&>
    {
    public:
        SignalUpdateEvent(const char* sIds);
        SignalUpdateEvent(ISignalVal& sig);
        virtual ~SignalUpdateEvent();
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
			
		/*!
		Sets a timer
		\param Timer variable
		\param msTimer variable
		\note void msTimer::set(long); 
		*/
        CAPL_METHOD(void, Set)(DWORD u32Timer);
		
		/*!
		Sets a cyclical timer
		\param firstDuration Time in milliseconds until the timer runs out for the first time
		\param period Time in milliseconds in which the timer is restarted in case of expiration
		\note void msTimer::setCyclic(long firstDuration, long period);
		*/
        CAPL_METHOD(void, SetCyclic)(CAPL_INT32 firstDuration, CAPL_INT32 period = -1);
        
		/*!
		Stops an active timer
		\note void msTimer::cancel();
		*/
		CAPL_METHOD(void, Kill)();
		
		/*!
		Returns a value indicating how much more time will elapse before an on timer event procedure is called. For form 1, the time value is returned in seconds; for form 2, the time value is returned in milliseconds.  If the timer is not active, -1 is returned. This is also the case in the on timer event procedure itself
		\param timer variable
		\param mstimer variable
		\return Time to go until the timer elapses and the event procedure is called
		\note long msTimer::timeToElapse(); 
		*/
        CAPL_METHOD(DWORD, GetTime)() const;
    };

    // Umgebungsvariablen-Event.

    class IEnvVarEvent : public IEvent<IEnvVar&>
    {
    public:
        CAPL_METHOD(void, SetEnvVar)(IEnvVar& envvar) = 0;
    };

    // PreStart-Event.
    class PreStartEvent : public IEvent<IPreStart&>
    {
    public:
        PreStartEvent();
        virtual ~PreStartEvent();
    };

    // Start-Event.
    class StartEvent : public IEvent<IStart&>
    {
    public:
        StartEvent();
        virtual ~StartEvent();
    };

    // PreStart-Event.
    class PreStopEvent : public IEvent<IPreStop&>
    {
    public:
        PreStopEvent();
        virtual ~PreStopEvent();
    };

    // Stop-Event.
    class StopEvent : public IEvent<IStop&>
    {
    public:
        StopEvent();
        virtual ~StopEvent();
    };

    // Key-Event.
    class KeyEvent : public IEvent<IKey&>
    {
    public:
        KeyEvent(DWORD u32FileIndex, WORD u16Key, DWORD u32Modifier);
        KeyEvent(DWORD u32FileIndex, CAPL_INT32 caplKey);
        virtual ~KeyEvent();
    };

    // ErrorPassive-Event.
    class BusEvent : public IEvent<ICanError&>
    {
    public:
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
    };

    class LinWakeupFrameEvent : public IEvent<ILinWakeupFrame&>
    {
    public:
        LinWakeupFrameEvent();
        virtual ~LinWakeupFrameEvent();
    };

    // Diagnose-Event.
    class DiagEvent : public IEvent<IDiag&>
    {
    public:
        DiagEvent();
        virtual ~DiagEvent();
    };

    class DiagRequestEvent : public IEvent<IDiagRequest&>
    {
    public:
        DiagRequestEvent(const char* sIds);
        virtual ~DiagRequestEvent();
    };

    class DiagResponseEvent : public IEvent<IDiagResponse&>
    {
    public:
        DiagResponseEvent(const char* sIds);
        virtual ~DiagResponseEvent();
    };

    // Ethernet-Event.
    class EthernetPacketEvent : public IEvent<IEthernetPacket&>
    {
    public:
        EthernetPacketEvent(BYTE u8Channel);
        virtual ~EthernetPacketEvent();
    };

    class EthernetStatusEvent : public IEvent<IEthernetStatus&>
    {
    public:
        EthernetStatusEvent(BYTE u8Channel);
        virtual ~EthernetStatusEvent();
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
            operator ITimer&()
            {
                return GetTimer();
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

	
	/*!
    Sets a cyclical timer
	\param firstDuration Time in milliseconds until the timer runs out for the first time
	\param period Time in milliseconds in which the timer is restarted in case of expiration
	\note void msTimer::setCyclic(long firstDuration, long period);
	*/
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
	
	/*!
    Stops an active timer
	\param Timer or msTimer variable 
	\note void cancelTimer(msTimer t);
	*/
    CAPL_FUNC(void, cancelTimer)(capl::ITimer&);

	/*!
    The function converts the string s into a double number. Normally, the base is decimal and must have the following format: [Blank space] [Sign] [Digits] [.Digits] [ {d | D | e | E}[Sign]Digits]
	\param Input string to be converted
	\return Double number, the converted string
	\note double atodbl(char s[]);
	*/
    CAPL_FUNC(double, atodbl)(const char*);
	
	/*!
    This function converts the string s to a LONG number. The number base is decimal. Starting with string 0x, base 16 is used. Leading blanks are not read
	\param String to be converted
	\return long integer
	\note long atol(char s[]);
	*/
    CAPL_FUNC(CAPL_INT32, atol)(const char*);
	
	/*!
    The number val is converted to a string s. In this case, base indicates a number base between 2 and 36. s must be large enough to accept the converted number!
	\param val Number to be converted
	\param s String, which contains the converted number
	\param base Number base
	\note void ltoa(long val, char s[], long base);
	*/
    CAPL_FUNC(void, ltoa)(CAPL_INT32, char*, CAPL_INT32);

	/*!
    Rounds x to the nearest integral number. The rounding method used is symmetric arithmetic rounding
	\param x Number to be rounded
	\return Nearest integral number
	\note long _round(double x);
	*/
    CAPL_FUNC(CAPL_INT32, _round)(double);
	
	/*!
    Rounds x to the nearest integral number. The rounding method used is symmetric arithmetic rounding
	\param x Number to be rounded
	\return Nearest integral number
	\note int64 _round64(double x);
	*/
    CAPL_FUNC(CAPL_INT64, _round64)(double);

	/*!
    Calculates the floor of a value, i.e. the largest integer smaller or equal to the value
	\param x Value of which the floor shall be calculated
	\return Floor of x
	\note float _floor(float x);
	*/
    CAPL_FUNC(double, _floor)(double x);
	
	/*!
    Calculates the ceiling of a value, i.e. the smallest integer larger or equal to the value
	\param x Value of which the ceiling shall be calculated
	\return Ceiling of x
	\note float _ceil(float x);
	*/
    CAPL_FUNC(double, _ceil)(double x);

	/*!
    Calculates a random number between 0 and x-1
	\param Determines the interval
	\return Random number between 0 and x-1
	\note dword random(dword x);
	*/
    CAPL_FUNC(DWORD, random)(DWORD x);

    //CAPL_FUNC(double, _pow)(double, double); Wird auf pow abgebildet, welches durch math.h implementiert wird!
    //CAPL_FUNC(double, abs)(double); Wird durch math.h implementiert!
    //CAPL_FUNC(CAPL_INT32, abs)(CAPL_INT32); Wird durch math.h implementiert!
    //CAPL_FUNC(double, cos)(double); Wird durch math.h implementiert!
    //CAPL_FUNC(double, exp)(double); Wird durch math.h implementiert!
    //CAPL_FUNC(double, sin)(double); Wird durch math.h implementiert!
    //CAPL_FUNC(double, sqrt)(double); Wird durch math.h implementiert!

    CAPL_FUNC(void, loadPanel)(const char*); // Gibt es in CAPL nicht, nur zum Testen implementiert!
	
	/*!
    Opens a panel.
	\param panelName Panel name, restricted to 128 characters
	\note If several panels with the same name exist in the CANoe configuration this command has an effect on all these panels
	*/
    CAPL_FUNC(void, openPanel)(const char*);
	
	/*!
    Closes a panel
	\param panelName Panel name, restricted to 128 characters
	\note If several panels with the same name exist in the CANoe configuration this command has an effect on all these panels
	*/
    CAPL_FUNC(void, closePanel)(const char*);
	
	/*!
    Sets the background color of panel elements
	\param panel Panel name, restricted to 128 characters
	\param control Name of the control, restricted to 128 characters
	\param color Color value (e.g. calculated by MakeRGB)
	\note void SetControlBackColor(char[] panel, char[] control, long color);
	*/
    CAPL_FUNC(void, SetControlBackColor)(const char* sPanel, const char* sControl, CAPL_INT32 color);
	
	/*!
    Sets the foreground color of panel elements
	\param panel Panel name, restricted to 128 characters
	\param control Name of the control, restricted to 128 characters
	\param color Color value (e.g. calculated by MakeRGB)
	\note void SetControlForeColor(char[] panel, char[] control, long color);
	*/
    CAPL_FUNC(void, SetControlForeColor)(const char* sPanel, const char* sControl, CAPL_INT32 color);
	
	/*!
    Sets the background and text color of panel elements
	\param panel Panel name, restricted to 128 characters
	\param control Name of the control, restricted to 128 characters
	\param backcolor Color value (e.g. calculated by MakeRGB)
	\param textcolor Color value (e.g. calculated by MakeRGB) 
	\note void SetControlColors(char[] panel, char[] control, long backcolor, long textcolor);
	*/
    CAPL_FUNC(void, SetControlColors)(const char* sPanel, const char* sControl, CAPL_INT32 backcolor, CAPL_INT32 textcolor);
	
	/*!
    Selective activation and deactivation of the following elements:
		control elements 
		control and display elements 
	\param panel Name of the panel, restricted to 128 characters
	\param control Name of the control, restricted to 128 characters
	\param enable
		0 turn off (disable) 
		1 turn on (enable)
	\note void enableControl(char panel[], char control[], long enable);
	*/
    CAPL_FUNC(void, enableControl)(const char* sPanel, const char* sControl, CAPL_INT32 enable);
	
	/*!
    Sets the visibility of panel elements.
	\param control Name of the control, restricted to 128 characters
	\param panel Panel name, restricted to 128 characters
	\param visible
		Sets if the panel element should be visible or not
		visible = 1 => Panel element is visible
		visible = 0 => Panel element is not visible
	\note void SetControlVisibility(char[] panel, char[] control, long visible);
	*/
    CAPL_FUNC(void, SetControlVisibility)(const char* sPanel, const char* sControl, CAPL_INT32 visible);
	
	/*!
    Sets the time of the Panel Designer Clock Control
	\param panel Panel name, restricted to 128 characters
	\param control Name of the control, restricted to 128 characters
	\param hours Defines the hours of the time to be displayed in the clock control
	\param minutes Defines the minutes of the time to be displayed in the clock control
	\param seconds Defines the seconds of the time to be displayed in the clock control 
	\note void SetClockControlTime(char[] panel, char[] control, int hours, int minutes, int seconds);
	*/
    CAPL_FUNC(void, SetClockControlTimeByValues)(const char* sPanel, const char* sControl, CAPL_INT32 hours, CAPL_INT32 minutes, CAPL_INT32 seconds);
	
	/*!
    Sets the time of the Panel Designer Clock Control
	\param panel Panel name, restricted to 128 characters
	\param control Name of the control, restricted to 128 characters
	\param time Defines the time in seconds to be displayed in the clock control. The corresponding hours, minutes and seconds are calculated during runtime
	\note void SetClockControlTime(char[] panel, char[] control, int time);
	*/
    CAPL_FUNC(void, SetClockControlTime)(const char* sPanel, const char* sControl, CAPL_INT32 time);
	
	/*!
    Starts the Clock Control designed as stop watch in the Panel Designer (setting Mode = StopWatch)
	\param panel Panel name, restricted to 128 characters 
	\param control Name of the control, restricted to 128 characters
	\note void ClockControlStart(char[] panel, char[] control);
	*/
    CAPL_FUNC(void, ClockControlStart)(const char* sPanel, const char* sControl);
	
	/*!
    Stops the Clock Control designed as stop watch with the Panel Designer (setting Mode = StopWatch)
	\param panel Panel name, restricted to 128 characters 
	\param control Name of the control, restricted to 128 characters
	\note void ClockControlStop(char[] panel, char[] control);
	*/
    CAPL_FUNC(void, ClockControlStop)(const char* sPanel, const char* sControl);
	
	/*!
    Resets the Clock Control designed as stop watch with the Panel Designer (setting Mode = StopWatch)
	\param panel Panel name, restricted to 128 characters 
	\param control Name of the control, restricted to 128 characters
	\note void ClockControlReset(char[] panel, char[] control);
	*/
    CAPL_FUNC(void, ClockControlReset)(const char* sPanel, const char* sControl);
	
    /*!
    Searches for the variable entry in the section section of the file file. Its contents are interpreted as a list of byte values. The number format is decimal or with the prefix 0x it is hexadecimal. Numbers are separated by spaces, tabs, a comma, semi-colon or slash. The buffer is filled up to a quantity of bufferlen bytes
	\param section Section of file
	\param entry Name of variable
	\param buffer buffer for characters to be read
	\param bufferlen Size of buffer in byte
	\param	filename Size of buffer in byte
	\return Number of characters read
	\note long fileReadArray(char section[], char entry[], char buffer[], long bufferlen, char file[]);
	*/
	CAPL_FUNC(CAPL_INT32, fileReadArray_Internal)(const char*, const char*, char*, short, const char*, const char*, const char*);
    
	/*!
    Analogous to fileReadInt for floats
	\param section Section of file
	\param entry Name of variable
	\param def Value
	\param file Name of file
	\return Number of characters read
	\note float fileReadFloat(char section[], char entry[], float def, char ile);
	*/
	CAPL_FUNC(double, fileReadFloat_Internal)(const char*, const char*, double, const char*, const char*, const char*);
    
	/*!
    Searches for the variable entry in the section section of the file filename. If its value is a number, this number is returned as the functional result. If the file or entry is not found, or if entry does not contain a valid number, the default value def is returned as the functional result
	\param section Section of file
	\param entry Name of variable
	\param def Value
	\param file Name of file
	\return Integer read
	\note long fileReadInt(char section[], char entry[], long def, char file[]);
	*/
	CAPL_FUNC(CAPL_INT32, fileReadInt_Internal)(const char*, const char*, CAPL_INT32, const char*, const char*, const char*);
	
	/*!
    Searches for the variable entry in the section section of the file filename. Its content (value) is written to the buffer buffer. Its length must be passed correctly to bufferlen. If the file or entry is not found, the default value def is copied to buffer
	\param section Section of file
	\param entry Name of variable
	\param def Value
	\param filename Name of file
	\param buffer Buffer for characters to be read
	\param bufferlen Size of buffer in byte
	\return Number of bytes read
	\note long fileReadString(char section[], char entry[], char def[], char buffer[], long bufferlen, char filename[]);
	*/
    CAPL_FUNC(CAPL_INT32, fileReadString_Internal)(const char*, const char*, const char*, char*, CAPL_INT32, const char*, const char*, const char*);
    
	/*!
	Analogous to fileWriteInt, but writes a float variable to the file instead of a text
	\param section Section of file
	\param entry Name of variable
	\param def Value
	\param file Name of file
	\return 0 if an error has occurred else 1
	\note long fileWriteFloat(char section[], char entry[], float def, char file[]);
	*/
	CAPL_FUNC(CAPL_INT32, fileWriteFloat_Internal)(const char*, const char*, double, const char*, const char*, const char*);
    
	/*!
    Analogous to fileWriteString, but writes a long variable to the file instead of a text
	\param section Section of file
	\param entry Name of variable
	\param def Value
	\param file Name of file
	\return 0 if an error has occurred else 1
	\note long fileWriteInt(char section[], char entry[], long def, char file[]);
	*/
	CAPL_FUNC(CAPL_INT32, fileWriteInt_Internal)(const char*, const char*, CAPL_INT32, const char*, const char*, const char*);
    
	/*!
    Opens the file filename, finds the section section and writes the variable entry with the value value. If entry already exists, the old value is overwritten. The functional result is the number of characters written, or 0 for an error
	\param section Section of file
	\param entry Name of variable
	\param value
	\param filename Name of file 
	\return Number of written characters of 0 if an error has occurred
	\note long fileWriteString(char section[], char entry[], char value[], char filename[]);
	*/
	CAPL_FUNC(CAPL_INT32, fileWriteString_Internal)(const char*, const char*, const char*, const char*, const char*, const char*);

	/*!
    Returns details to the current date and time in an array of type long
	\param An array of type long with at least 9 entries. The entries of the array will be filled with the following information:
		1 Seconds (0 - 60)
		2 Minutes (0 - 60)
		3 Hours (0 - 24)
		4 Day of month (1 - 31)
		5 Month (0 - 11)
		6 Year (starting with 1900)
		7 Day of week (0 - 7)
		8 Day of Year (0 - 365)
		9 Flag for daylight saving time (0 - 1, 1 = daylight saving time)
	\note void getLocalTime(long time[]);
	*/
    CAPL_FUNC(void, getLocalTime)(CAPL_INT32*);
	
	/*!
    Copies a printed representation of the current date and time into the supplied character buffer
	\param timeBuffer The buffer the string will be written in.  This buffer must be at least 26 characters long.
	\note void getLocalTimeString(char timeBuffer[]);
	*/
    CAPL_FUNC(void, getLocalTimeString)(char*);
	
	/*!
    Searches the file under section section for the variable entry. Entry is interpreted as a list of numerical values, separated by comma, tab, space, semicolon or slash. A 0x prefix indicates hex values
	\param section Section as a string
	\param entry Name as a string
	\param filename File path as a string
	\param buff Buffer for the read-in numerical values
	\param buffsize Size of buff: Maximum number of read in numerical values (max. 1279 characters)
	\return Number of numerical values read in
	\note long getProfileArray(char section[], char entry[],char buff[], long buffsize, char filename[]);
	*/
    CAPL_FUNC(CAPL_INT32, getProfileArray_Internal)(const char*, const char*, char*, CAPL_INT32, const char*, const char*, const char*);
    
	/*!
	Searches the file filename under section section for the variable entry. If its value is a number, this number is returned as the functional result. If the file or entry is not found, or if entry does not contain a valid number, the default value def is returned as the functional result
	\param section Section as a string
	\param entry Name as a string
	\param filename File path as a string
	\param def Default value in case of error as a float
	\return Number of numerical values read in 
	\note float getProfileFloat(char section[], char entry[], long def, char filename[])
	*/
	CAPL_FUNC(double, getProfileFloat_Internal)(const char*, const char*, double, const char*, const char*, const char*);
    
	/*!
    Searches the file filename under section section for the variable entry. If its value is a number, this number is returned as the functional result. If the file or entry is not found, or if entry does not contain a valid number, the default value def is returned as the functional result
	\param section Section as a string
	\param entry Name as a string
	\param filename File path as a string
	\param def Default value in case of error as an integer
	\return Integer that was read in
	\note long getProfileInt(char section[], char entry[], long def, char filename[])
	*/
	CAPL_FUNC(CAPL_INT32, getProfileInt_Internal)(const char*, const char*, CAPL_INT32, const char*, const char*, const char*);
		
	/*!
    Searches the file filename under section section for the variable entry. Its contents (value) are written to the buffer buff. Its length must be passed correctly in buffsize. If the file or entry is not found, the default value def is copied to buffer. If the read string length is longer than the buffer, the string will be cut to the buffer length
	\param section Section as a string
	\param entry Variable name as a string
	\param filename File path as a string
	\param buff Buffer for the read-in numerical as a string
	\param buffsize Size of buff in bytes (max. 1022 characters)
	\return Length of the read string
	\return Length of the default string: Fault Key not found
	\return -1: Fault File not found
	\note long getProfileString(char section[], char entry[], char def[], char buff[], long buffsize, char filename[])
	*/
    CAPL_FUNC(CAPL_INT32, getProfileString_Internal)(const char*, const char*, const char*, char*, CAPL_INT32, const char*, const char*, const char*);

	/*!
    Returns the number of Error Frames on channel x since start of measurement
	\return Number of Error Frames on channel x since start of measurement
	\note long ErrorFrameCount ()
	*/
    CAPL_FUNC(long, ErrorFrameCount)(BYTE channel);

	/*!
    Gets the value of a signal
	\param aSignal The signal to be polled
	\note This function replaces getSignalByTxNode
	*/
    CAPL_FUNC(double, getSignal)(ISignal& var);
	
	/*!
    Sets the transmitted signal aSignal to the accompanying value
	\param aSignal Signal to be set
	\param aValue Physical value to be accepted
	\note This function replaces SetSignalByTxNode
	*/
    CAPL_FUNC(void, setSignal)(ISignal& var, double value);
	
	
    CAPL_FUNC(double, getSignalByStringId)(const char* sId);
    CAPL_FUNC(void, setSignalByStringId)(const char* sId, double value);

	/*!
    Retrieves the current raw value of a signal
	\param dbSignal The signal
	\return Form 1: the current raw value of the signal
	\return Form 2: 0 on success, -1 on error
	\note Int64 GetRawSignal(dbSignal aSignal);
	*/
    CAPL_FUNC(CAPL_INT64, getRawSignal)(ISignal& var);
	
	/*!
    Retrieves the current raw value of a signal
	\param dbSignal The signal
	\param data Receives the data bytes of the signal
	\param dataLength Length of the data buffer
	\return Form 1: the current raw value of the signal
	\return Form 2: 0 on success, -1 on error 
	\note long GetRawSignal(dbSignal aSignal, byte data[], dword dataLength); 
	*/
    CAPL_FUNC(CAPL_INT32, getRawSignalArray)(ISignal& var, CAPL_UINT8* data, CAPL_INT32 dataLength);

	/*!
    Sets the raw value of a signal
	\param dbSignal The signal
	\return Form 2: 0 on success, -1 on error
	\note void SetRawSignal(dbSignal aSignal, int64 value);
	*/
    CAPL_FUNC(void, setRawSignal)(ISignal& var, CAPL_INT64 value);
	
	/*!
    Sets the raw value of a signal
	\param dbSignal The signal
	\param data The data bytes of the signal
	\param dataLength Length of the data
	\return Form 2: 0 on success, -1 on error
	\note void SetRawSignal(dbSignal aSignal, byte data[], dword dataLength); 
	*/
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


	/*!
    Sets the value of a variable of the double type
	\param namespace Name of the name space
	\param value New value of the variable
	\param SysVarName Name of the fully qualified name of the system variable, including all name spaces, separated by "::". The name must be preceded by "sysVar::"
	\return 0: no error, function successful
			1: name space was not found or second try to define the same name space
			2: variable was not found or second try to define the same variable
			3: no writing right for the name space available
			4: the variable has no suitable type for the function
	\note long sysSetVariableFloat(char namespace[], char variable[], float value);
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableFloatByString)(const char* sNamespace, const char* sName, double val);
	
	/*!
    Sets the value of a variable of the double type
	\param variable Name of the variable
	\param value New value of the variable
	\return 0: no error, function successful
			1: name space was not found or second try to define the same name space
			2: variable was not found or second try to define the same variable
			3: no writing right for the name space available
			4: the variable has no suitable type for the function
	\note long sysSetVariableFloat(SysVarName, float value);
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableFloatByObj)(IEnvVar& var, double val);
	
	/*!
    Sets the value of a variable of a 32 bit integer type
	\param namespace Name of the name space
	\param SysVarName Name of the fully qualified name of the system variable, including all name spaces, separated by "::". The name must be preceded by "sysVar::"
	\param value New value of the variable
	\return 0: no error, function successful
			1: name space was not found or second try to define the same name space
			2: variable was not found or second try to define the same variable
			3: no writing right for the name space available
			4: the variable has no suitable type for the function
	\note long sysSetVariableInt(char namespace[], char variable[], long value); 
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableIntByString)(const char* sNamespace, const char* sName, CAPL_INT32 val);
	
	/*!
    Sets the value of a variable of a 32 bit integer type
	\param variable Name of the variable
	\param value New value of the variable
	\return 0: no error, function successful
			1: name space was not found or second try to define the same name space
			2: variable was not found or second try to define the same variable
			3: no writing right for the name space available
			4: the variable has no suitable type for the function 
	\note long sysSetVariableInt(SysVarName, long value);
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableIntByObj)(IEnvVar& var, CAPL_INT32 val);
	
	/*!
    Sets the value of a variable of a 64 bit integer type
	\param namespace Name of the name space
	\param SysVarName Name of the fully qualified name of the system variable, including all name spaces, separated by "::". The name must be preceded by "sysVar::"
	\param value New value of the variable
	\return 0: no error, function successful
			1: name space was not found
			2: variable was not found
			3: the variable has no suitable type for the function
	\note long sysSetVariableLongLong(char namespace[], char variable[], int64 value); 
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableLongLongByString)(const char* sNamespace, const char* sName, long long val);
	
	/*!
    Sets the value of a variable of a 64 bit integer type
	\param variable Name of the variable
	\param value New value of the variable
	\return 0: no error, function successful
			1: name space was not found
			2: variable was not found
			3: the variable has no suitable type for the function 
	\note long sysSetVariableLongLong(SysVarName, int64 value);
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableLongLongByObj)(IEnvVar& var, long long val);
	
	/*!
    Sets the value of a variable of the float[] type
	\param namespace Name of the name space
	\param SysVarName Name of the fully qualified name of the system variable, including all name spaces, separated by "::". The name must be preceded by "sysVar::"
	\param values New values of the variable
	\param arraySize Number of values in the array
	\return 0: no error, function successful
			1: name space was not found or second try to define the same name space
			2: variable was not found or second try to define the same variable
			3: no writing right for the name space available
			4: the variable has no suitable type for the function
	\note long sysSetVariableFloatArray(char namespace[], char variable[], float values[], long arraySize);
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableFloatArrayByString)(const char* sNamespace, const char* sName, const double* val, CAPL_INT32 size);
	
	/*!
    Sets the value of a variable of the float[] type.
	\param variable Name of the variable
	\param values New values of the variable
	\param arraySize Number of values in the array
	\return 0: no error, function successful
			1: name space was not found or second try to define the same name space
			2: variable was not found or second try to define the same variable
			3: no writing right for the name space available
			4: the variable has no suitable type for the function 
	\note long sysSetVariableFloatArray(SysVarName,float values[], long arraySize); 
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableFloatArrayByObj)(IEnvVar& var, const double* val, CAPL_INT32 size);
	
	/*!
    Sets the value of a variable of the int[] type
	\param namespace Name of the name space
	\param SysVarName Name of the fully qualified name of the system variable, including all name spaces, separated by "::". The name must be preceded by "sysVar::"
	\param values Values of the variable
	\param arraySize Number of values in the array
	\return 0: no error, function successful
			1: name space was not found or second try to define the same name space
			2: variable was not found or second try to define the same variable
			3: no writing right for the name space available
			4: the variable has no suitable type for the function 
	\note long sysSetVariableLongArray(char namespace[], char variable[], long values[], long arraySize); 
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableLongArrayByString)(const char* sNamespace, const char* sName, const CAPL_INT32* val, CAPL_INT32 size);
	
	/*!
    Sets the value of a variable of the int[] type
	\param variable Name of the variable
	\param values Values of the variable
	\param arraySize Number of values in the array
	\return 0: no error, function successful
			1: name space was not found or second try to define the same name space
			2: variable was not found or second try to define the same variable
			3: no writing right for the name space available
			4: the variable has no suitable type for the function  
	\note long sysSetVariableLongArray(SysVarName, long values[], long arraySize); 
	*/
    CAPL_FUNC(CAPL_INT32, SysSetVariableLongArrayByObj)(IEnvVar& var, const CAPL_INT32* val, CAPL_INT32 size);

	/*!
    Returns the length of the array of the system variable
	\param namespace Name of the name space
	\param SysVarName Name of the fully qualified name of the system variable, including all name spaces, separated by "::". The name must be preceded by "sysVar::"
	\return For system variables of type data, returns the current size (in bytes) of the value.
			For system variables of type long or float array, returns the number of elements in the array.
			For system variables of type string, returns the length of the string, excluding the terminating 0 character. 
			For system variables of type long or float, returns 1.
	\note dword sysGetVariableArrayLength(char namespace[], char variable[]); 
	*/
    CAPL_FUNC(CAPL_INT32, SysGetVariableArrayLengthByString)(const char* sNamespace, const char* sName);
	
	/*!
    Returns the length of the array of the system variable
	\param variable Name of the variable
	\return For system variables of type data, returns the current size (in bytes) of the value.
			For system variables of type long or float array, returns the number of elements in the array.
			For system variables of type string, returns the length of the string, excluding the terminating 0 character. 
			For system variables of type long or float, returns 1. 
	\note dword sysGetVariableArrayLength(SysVarName); 
	*/
    CAPL_FUNC(CAPL_INT32, SysGetVariableArrayLengthByObj)(IEnvVar& var);

    template<class T>
    class IAccess
    {
    public:
        CAPL_METHOD(T, operator=)(const T& value) = 0;
        virtual CAPL_CALL operator T()const = 0;
        CAPL_METHOD(T,  operator++)() = 0;
        CAPL_METHOD(T, operator++)(int) = 0;
        CAPL_METHOD(T, operator--)() = 0;
        CAPL_METHOD(T, operator--)(int) = 0;
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


	/*!
    Checks parameter for extended identifier (29 bit) or standard identifier (11 Bit)
	\param id Id part of a message
	\param variable	
	\return 1 if check was successful, else 0
	\note long isExtId(dword id);
	*/
    CAPL_FUNC(CAPL_INT32, isExtIdById)(DWORD dwMsgId);
		
	/*!
    Checks parameter for extended identifier (29 bit) or standard identifier (11 Bit)
	\param id Id part of a message 
	\return 1 if check was successful, else 0
	\note long isStdId(message m);
	*/
	CAPL_FUNC(CAPL_INT32, isExtIdByObj)(capl::IMessage& m);
    
	/*!
    Checks parameter for extended identifier (29 bit) or standard identifier (11 Bit)
	\param id Id part of a message 
	\param variable
	\return 1 if check was successful, else 0
	\note long isStdId(dword id);
	*/
	CAPL_FUNC(CAPL_INT32, isStdIdById)(DWORD dwMsgId);
	CAPL_FUNC(CAPL_INT32, isStdIdByObj)(capl::IMessage& m);

	/*!
    Calculates the color value from the three primary color components
	\param Red Red color component (0 - 255)
	\param Green Green color component (0 - 255)
	\param Blue Blue color component (0 - 255)
	\return Color value (type: long)
	\note long MakeRGB(long Red, long Green, long Blue);
	*/
    CAPL_FUNC(CAPL_INT32, MakeRGB)(CAPL_INT32, CAPL_INT32, CAPL_INT32);
    
	CAPL_FUNC(DWORD, mkExtId)(DWORD);
    
	/*!
    Outputs a message 
	\param msg Variable of type message
	\note void output(message msg); 
	*/
	CAPL_FUNC(void, outputInternal)(capl::IMessage&);
    
	/*!
    Outputs a Ethernet packet from the program block
	\param packet Variable of type ethernetPacket
	\note void output(ethernetPacket packet);
	*/
	CAPL_FUNC(void, outputEthernetPacket)(capl::IEthernetPacket&);

   	/*!
    Sets another baud rate. The values do not become active until the next call of the function resetCan
	\param CAN channel 0: All controllers; 1 - 32: channel 1 - 32
	\param BTR0 Value of Bit Timing Register 0
	\param BTR1 Value of Bit Timing Register 1
	\return Always 1
	\note long setBtr(long channel, byte btr0, byte btr1);
	*/
	CAPL_FUNC(CAPL_INT32, setBtr)(CAPL_INT32, BYTE, BYTE);
    
	/*!
    long snprintf(char dest[], long len, char format[], ...);
	\param dest Character buffer to print to
	\param len Maximum number of characters printed to buffer including terminating '\0'. Must be at most the size of the buffer
	\param format Formatted string printed to buffer
	\return The number of characters written
	\note long snprintf(char dest[], long len, char format[], ...);
	*/
	CAPL_FUNC(CAPL_INT32, snprintf)(char*, CAPL_INT32, const char*, ...);
    
	/*!
    Programmed interrupt of the ongoing measurement.In offline mode this function interrupts but does not end the measurement. In offline mode the measurement can only be ended with <ESC>
	\note void stop();
	*/
	CAPL_FUNC(void, stop)();

    // CAPL_FUNC(INT32, strlen)(char*); Wird durch String.h verwendet
    // CAPL_FUNC(void, strncat)(char*, char*, INT32); Wird durch String.h verwendet
    // CAPL_FUNC(INT32, strncmp)(char*, char*, INT32); Wird durch String.h verwendet
    
	/*!
    This function copies src to dest. len indicates the size of the string that shall be copied. The function ensures that there is a terminating '\0'. Thus, a maximum of len-1 characters are copied
	\param dest Destination buffer
	\param src Source string
	\param len Size of the string to be copied. Must not be larger than the size of dest. If it is larger than the size of src, characters are copied until a terminating '\0' is encountered. A maximum of len-1 characters are copied
	\note void strncpy(char dest[], char src[], long len);
	*/
	CAPL_FUNC(void, strncpy)(char*, const char*, INT32);
    
	/*!
    Searches in s1 for s2
	\param s1 First string
	\param s2 Second string
	\return First position of s2 in s1, or -1 if s2 is not found in s1
	\note long strstr(char s1[], char s2[]);
	*/
	CAPL_FUNC(CAPL_INT32, strstr)(const char*, const char*);
    
	/*!
    Searches in s1 for s2
	\param s1 First string
	\param offset Offset in s1 at which the search shall be started
	\param s2 Second string
	\return First position of s2 in s1, or -1 if s2 is not found in s1
	\note long strstr_off(char s1[], long offset, char s2[]);
	*/
	CAPL_FUNC(CAPL_INT32, strstr_off)(const char*, CAPL_INT32, const char*);

    /*!
    This function compares s1 with s2 for a maximum of len characters. If they are identical the functional result is 0. If s1 is less than s2 the result is -1, else +1
	\param s1 First string
	]param s2 Second string
	\param len Maximum number of characters to compare	
	\return -1 if s1 is less than s2
	\return 1 if s2 is less than s1
	\return	0 if the strings are equal
	\note long strncmp(char s1[], char s2[], long len);
	*/
	CAPL_FUNC(CAPL_INT32, strncmp)(const char*, const char*, CAPL_INT32, CAPL_INT32);
    
	/*!
    This function starts the comparison in s2 at the specified offset
	\param s1 First string
	]param s2 Second string
	\param len Maximum number of characters to compare	
	\return -1 if s1 is less than s2
	\return 1 if s2 is less than s1
	\return	0 if the strings are equal
	\note long strncmp(char s1[], char s2[], long s2offset, long len)
	*/
	CAPL_FUNC(CAPL_INT32, strncmp_off)(const char*, CAPL_INT32, const char*, CAPL_INT32, CAPL_INT32);
    
	/*!
    This function copies src to dest. max indicates the size of dest. The function ensures that there is a terminating '\0'. Thus, a maximum of max-1-destOffset characters are copied. Characters are overwritten in dest starting at destOffset
	\param dest Destination buffer
	\param destOffset Offset in destination buffer
	\param src Source string
	\param max Is used to determine the maximum number of copied characters. Must not be larger than the size of dest. A maximum of max-1-destOffset characters are copied.If src is shorter than that, characters are only copied until a terminating '\0' is encountered
	\note void strncpy_off(char dest[], long destOffset, char src[], long max);
	*/
	CAPL_FUNC(void, strncpy_off)(char*, CAPL_INT32, const char*, CAPL_INT32);
    
	/*!
    This function copies a substring of src to dest. max indicates the size of dest. The function ensures that there is a terminating '\0'. Thus, a maximum of max-1 characters are copied
	\param dest Destination buffer
	\param src Source string
	\param srcStart Start index in src of substring
	\param len Length of the substring, or -1 to copy the string until the end
	\param	max Size of dest
	\note void substr_cpy(char dest[], char src[], long srcStart, long len, long max);
	*/
	CAPL_FUNC(void, substr_cpy)(char*, const char*, CAPL_INT32, CAPL_INT32, CAPL_INT32);
    
	/*!
	This function copies a substring of src to dest. max indicates the the size of dest
	\param dest Destination buffer
	\param destOffset Offset in destination buffer 
	\param src Source string
	\param srcStart Start index in src of substring
	\param len Length of the substring, or -1 to copy the string until the end
	\param	max Size of dest
	\note void substr_cpy_off(char dest[], long destOffset, char src[], long srcStart, long len, long max)
	*/
	CAPL_FUNC(void, substr_cpy_off)(char*, CAPL_INT32, const char*, CAPL_INT32, CAPL_INT32, CAPL_INT32);

    CAPL_FUNC(void, mbstrncpy_off_internal)(char* dest, long destSize, const char* src, long srcSize);
    template<class D, class S>
    void mbstrncpy_off(D& dest, long destOffset, S& src, long len)
    {
        long destSize = (long)sizeof(dest) - destOffset;
        if (destSize < 0)
            return;
        src[sizeof(src) - 1] = 0;
        mbstrncpy_off_internal(dest + destOffset, destSize, src, len);
    }
    template<class D, class S>
    void mbstrncpy(D& dest, S& src, long len)
    {
        mbstrncpy_off(dest, 0, src, len);
    }

    /*!
    Returns the value of a variable of the double type
	\param namespace Name of the name space 
	\param variable Name of the variable
	\return Value of the variable or 0 in case of error 
	\note double sysGetVariableFloat(char namespace[], char variable[]); 
	*/
	CAPL_FUNC(double, SysGetVariableFloat)(const char*, const char*);
    
	/*!
    Returns the value of a variable of a 32 bit integer typev
	\param namespace Name of the name space
	\param variable Name of the variable
	\return Value of the variable or 0 in case of error
	\note long sysGetVariableInt(char namespace[], char variable[]); 
	*/
	CAPL_FUNC(CAPL_INT32, SysGetVariableInt)(const char*, const char*);
	CAPL_FUNC(CAPL_INT32, SysGetVariableInt)(const char*, const char*);
    
	/*!
    Returns the value of a message identifier independent of its type. Identifier as long value
	\param Id
	\return Identifier as long value
	\note long valOfId(dword id);
	*/
	CAPL_FUNC(CAPL_INT32, valOfId)(DWORD);
    
	/*!
    Returns the value of a message identifier independent of its type. Identifier as long value
	\param message
	\return Identifier as long value
	long valOfId(message m);
	*/
	CAPL_FUNC(CAPL_INT32, msgvalOfId)(capl::IMessage&);

	/*!
    Opens the file filename, searches the section section and writes the variable entry with the value value. If entry already exists the old value is overwritten
	\param section Section of the file as a string
	\param entry Variable name as a string
	\param value Value as a float
	\param filename File path as a string
	\return The functional result is 0 in case of an error
	\note long writeProfileFloat(char section[], char entry[], float value, char filename[])
	*/
    CAPL_FUNC(CAPL_INT32, writeProfileFloat_Internal)(const char*, const char*, double, const char*, const char*, const char*);
    
	/*!
    Opens the file filename, searches the section section and writes the variable entry with the value value. If entry already exists the old value is overwritten
	\param section Section of the file as a string
	\param entry Variable name as a string
	\param value Value as a float
	\param filename File path as a string
	\return The functional result is 0 in case of an error
	\note long writeProfileInt(char section[], char entry[], long value, char filename[]);
	*/
	CAPL_FUNC(CAPL_INT32, writeProfileInt_Internal)(const char*, const char*, CAPL_INT32, const char*, const char*, const char*);
    
	/*!
    Opens the file filename, searches the section section and writes the variable entry with the value value. If entry already exists the old value is overwritten. The functional result is the number of characters written or 0 in case of an error
	\param section Section of the file as a string
	\param entry Variable name as a string
	\param value Value as a float
	\param filename File path as a string
	\note long writeProfileString(char section[], char entry[], char value[], char filename[]);
	*/
	CAPL_FUNC(CAPL_INT32, writeProfileString_Internal)(const char*, const char*, const char*, const char*, const char*, const char*);

    // Bislang nicht unterstützte CAPL-Funktionen
	
	/*!
    Tone output. In the Windows Version the parameter duration has no effect
	\param freq Integer for tone pitch. In the Windows version, the parameters freq defines the tone output. Different sounds are defined in the section [SOUND] in the file WIN.ini:
		freq 0x0000 (SystemDefault)
		freq 0x0010 (SystemHand)
		freq 0x0020 (SystemQuestion)
		freq 0x0030 (SystemExclamation)
		freq 0x0040 (SystemAsterisk)
		freq 0xFFFF Standard Beep
	\param duration Integer for tone length
	\note void beep(int freq, int duration);
	*/
    CAPL_FUNC(void, beep)(short, short);
	
	/*!
    Returns the current busload of the specified channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2 
	\return Current busload of the specified channel in percent
	\note long canGetBusLoad (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetBusLoad)(CAPL_INT32);
	
	/*!
    Returns the current chip state of the specified CAN controller
	\param channel CAN channel
	\return Chip state of the specified CAN controller. See the following table for a description of the return values
		0 Value not available
		1 Simulated
		3 Error Active
		4 Warning Level
		5 Error Passive
		6 Bus Off
	\note long canGetChipState (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetChipState)(CAPL_INT32);
	
	/*!
    Returns the number of Error Frames on the specified channel since start of measurement
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2  
	\return Number of Error Frames on the specified channel since start of measurement
	\note long canGetErrorCount (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetErrorCount)(CAPL_INT32);
	
	/*!
    Returns the current rate of CAN error messages of the specified channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2 
	\return Current Rate of CAN error messages on the specified channel in messages per second
	\note long canGetErrorRate(long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetErrorRate)(CAPL_INT32);
	
	/*!
    Returns the number of extended CAN frames on the specified channel since start of measurement
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2  
	\return Number of extended CAN frames on the specified channel since start of measurement
	\note long canGetExtData (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetExtData)(CAPL_INT32);
	
	/*!
    Returns the current rate of extended CAN frames on the specified channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2  
	\return Current rate of extended CAN frames on the specified channel in messages per second
	\note long canGetExtDataRate(long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetExtDataRate)(CAPL_INT32);
	
	/*!
    Returns the number of extended remote CAN messages on the specified channel since start of measurement
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2 
	\return Number of extended remote CAN messages on the specified channel since start of measurement
	\note long canGetExtRemote (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetExtRemote)(CAPL_INT32);
	
	/*!
    Returns the current rate of extended remote CAN messages on the specified channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2
	\return Current rate of extended remote CAN messages on the specified channel in frames per second
	\note long canGetExtRemoteRate (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetExtRemoteRate)(CAPL_INT32);
	
	/*!
    Returns the number of CAN overload frames on the specified channel since start of measurement
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2 
	\return Number of CAN overload frames on the specified channel since start of measurement
	\note long canGetOverloadCount (long channel) 
	*/
    CAPL_FUNC(CAPL_INT32, canGetOverloadCount)(CAPL_INT32);
	
	/*!
    Returns the current rate of CAN overload frames on the specified channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2 
	\return Current rate of CAN overload frames on the specified channel in messages per second
	\note long canGetOverloadRate (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetOverloadRate)(CAPL_INT32);
	
	/*!
    Returns the peakload of the specified channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2 
	\return Peakload of the specified channel in percent
	\note long canGetPeakLoad (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetPeakLoad)(CAPL_INT32);
	
	/*!
    Returns the current RX error count in the receiver of the specified CAN channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2 
	\return Current error count in the receiver of the specified CAN channel
	\note long canGetRxErrorCount (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetRxErrorCount)(CAPL_INT32);
	
	/*!
    Returns the number of standard CAN frames on the specified channel since start of measurement
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2
	\return Number of standard CAN frames on the specified channel since start of measurement
	\note long canGetStdData (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetStdData)(CAPL_INT32);
	
	/*!
    Returns the current rate of standard CAN frames on the specified channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2 
	\return Current rate of standard CAN frames on the specified channel in messages per second
	\note long canGetStdDataRate (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetStdDataRate)(CAPL_INT32);
	
	/*!
    Returns the number of standard remote CAN frames on the specified channel since start of measurement
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2  
	\return Number of standard remote CAN frames on the specified channel since start of measurement
	\note long canGetStdRemote (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetStdRemote)(CAPL_INT32);
	
	/*!
    Returns the current rate of standard remote CAN frames of the specified channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2  
	\return Current rate of standard remote CAN frames of the specified channel in messages per second
	\note long canGetStdRemoteRate (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetStdRemoteRate)(CAPL_INT32);
	
	/*!
    Returns the current number of TX errors in the CAN receiver of the specified channel
	\param channel CAN channel
		Vector API driver Values 1 ... 32
		Softing API driver Values 1, 2
	\return Current number of errors in the CAN receiver of the specified channel
	\note long canGetTxErrorCount (long channel)
	*/
    CAPL_FUNC(CAPL_INT32, canGetTxErrorCount)(CAPL_INT32);
	
	/*!
    Via an acceptance filter the CAN controllers control which received messages are sent through to CANoe
	\param CAN channel
	\param Acceptance code for ID filtering
	\param Acceptance mask for ID filtering
	\return 0: ok
	\return !=0: error
	\note long canSetChannelAcc(long channel, dword code, dword mask);
	*/
    CAPL_FUNC(CAPL_INT32, canSetChannelAcc)(CAPL_INT32, DWORD, DWORD);
	
	/*!
    Activates/deactivates the TXRQ and Tx of the CAN controller. This function does nothing with the Ack bit
	\param CAN channel gtx
		0 tx off
		1 tx on
	\param gtxreq
		0 gtxreq off
		1 gtxreq on
	\return 0: ok
	\return !=0: error 
	\note long canSetChannelMode(long channel, long gtx, long gtxreq);
	*/
    CAPL_FUNC(CAPL_INT32, canSetChannelMode)(CAPL_INT32, CAPL_INT32, CAPL_INT32);
	
	/*!
    Defines the response of the CAN controller to the bus traffic and sets the ACK bit 
	\param CAN channel
	\param silent
		0 silent
		1 normal
	\return 0: ok
	\return !=0: error  
	\note long canSetChannelOutput(long channel, long silent);
	*/
    CAPL_FUNC(CAPL_INT32, canSetChannelOutput)(CAPL_INT32, CAPL_INT32);

	/*!
    This function closes the specified file
	\param The integer contains the handle to the file
	\return If an error occurs the return value is 0, else 1
	\note long fileClose (dword fileHandle);
	*/
    CAPL_FUNC(CAPL_INT32, fileClose)(DWORD);
	
	/*!
    The function reads characters from the specified file in binary format
	\param buff Buffer
	\param buffsize Maximum of buffsize characters
	\param fileHandle Handle to the file
	\return The function returns the number of characters read
	\note long fileGetBinaryBlock (byte buff[], long buffsize, dword fileHandle)
	*/
    CAPL_FUNC(CAPL_INT32, fileGetBinaryBlock)(BYTE*, CAPL_INT32, DWORD);
	
	/*!
    The function reads a string from the specified file into the buffer buff. Characters are read until the end of line is reached or the number of read characters is equal to buffsize -1. The end of line is marked either 
		by a single line feed or 
		a carriage return and a line feed (DOS file) 
	\param buff Buffer for the read-out string
	\param buffsize Length of the string
	\param fileHandle Handle to the file
	\return If an error occurs, the return value is 0, else 1
	\note long fileGetString (char buff[], long buffsize, dword fileHandle);
	*/
    CAPL_FUNC(CAPL_INT32, fileGetString)(char*, CAPL_INT32, DWORD);
	
	/*!
    The function reads a string from the specified file. Characters are read until the end of line is reached or the number of read characters is equal to buffsize -1. The end of line is marked either 
		by a single line feed or 
		a carriage return and a line feed (DOS file)
	\param buff Buffer for the read-out string
	\param buffsize Length of the string
	\param fileHandle Handle to the file
	\return If an error occurs, the return value is 0, else 1
	\note long fileGetStringSZ(char buff[], long buffsize, dword fileHandle);
	*/
    CAPL_FUNC(CAPL_INT32, fileGetStringSZ)(char*, CAPL_INT32, DWORD);
	
	/*!
    This function writes a string in the specified file
	\param buff Buffer for the read-out string
	\param buffsize Length of the string
	\param fileHandle Handle to the file
	\return If an error occurs, the return value is 0, else 1
	\note long filePutString (char buff[], long buffsize, dword fileHandle);
	*/
    CAPL_FUNC(CAPL_INT32, filePutString)(const char*, CAPL_INT32, DWORD);
	
	/*!
    This function resets the position pointer to the beginning of the file
	\param The integer indicates the file handle
	\return If an error occurs, the return value is 0, else 1
	\note long fileRewind (dword fileHandle);
	*/
    CAPL_FUNC(CAPL_INT32, fileRewind)(DWORD);
	
	/*!
    This function writes buffsize bytes in the specified file
	\param buff Buffer, with the binary data to be written
	\param buffersize Number of bytes to be written
	\param fileHandle Handle to the file
	\return The function returns the number of bytes written
	\note long fileWriteBinaryBlock (byte buff[], long buffsize, dword fileHandle);
	*/
    CAPL_FUNC(CAPL_INT32, fileWriteBinaryBlock)(const BYTE*, CAPL_INT32, DWORD);
    CAPL_FUNC(CAPL_INT32, getCanOeAbsFilePath)(const char*, const char*, char*, CAPL_INT32);

	/*!
    Determines the type of CAN platform being used. Is needed e.g. to program the BTR / OCR values
	\return 17: for Vector drivers
	\return For other manufacturer other values will be returned
	\note long getCardType();
	*/
    CAPL_FUNC(CAPL_INT32, getCardType)();
	
	/*!
    Determines the card type of CAN channel. Is needed e.g. to program the BTR / OCR values
	\param CAN Channel number
	\return Type of board as one of the following values:
		-1 Unknown or Invalid hardware type
		3 DEMO - Demo driver
		25 Vector PCMCIA CANcardXL
		27 Vector USB CANcaseXL
		28 Vector CANcaseXLLog (USB + memory)
		29 Vector CANboardXL PCI
		30 Vector CPCI CANboardXL Compact
		31 Vector CANboardXL PCI express
		33 Vector VN7600
		34 Vector ExpressCard CANcardXLe
		36 Vector VN3300
		37 Vector VN3600
		38 Vector VN2610
		40 Vector VN8950
		41 Remote (IP) Device
		43 Vector VN8910
		46 Vector VN6104
		47 Vector VN8970
		48 Vector VN2640
		49 Vector VN1610
		50 Vector VN1611
		51 Vector VN1630
		52 Vector VN1640
		53 Vector VN5610
		54 Vector VN7570
		55 Vector IP Server
		61 Vector VN7572
		62 Vector VN8972
		63 Vector VN7610
		75 Vector VX1131
	\note int getCardTypeEx(int can);
	*/
    CAPL_FUNC(short, getCardTypeEx)(short);
	
	/*!
    Determines the type of CAN controller used
	\param CAN channel
		0 both controller
		1 Channel 1
		2 Channel 2
	\return Type of controller with the following values:
		5 NEC 72005 
		200 Philipps PCA82C200
		526 Intel 82526
		527 Intel 82527
		1000,1001 Philipps SJA1000
	Other types may occur. DEMO versions return the result 0 or simulate one of the existing types. If an attempt is made to access a nonexistent channel (e.g. Channel 2 for CPC/PP) or if the driver used does not support this function, the functional result is 0
	\note long getChipType(long channel);
	*/
    CAPL_FUNC(CAPL_INT32, getChipType)(CAPL_INT32);
    
	/*!
    Determines the constant deviation when Drift is set
	\return Drift in parts per thousand
	\note int getDrift();
	*/
	CAPL_FUNC(short, getDrift)();
   
    /*!
    Determines the Event Sorting state
	\param message
	\return 0: Invalid state
	\return 1: sorted
	\return	2: unsorted
	\note int GetEventSortingStatus(message msg);
	*/
	CAPL_FUNC(short, GetEventSortingStatus)(capl::IMessage&);
   
    /*!
    Finds out the name of the first assigned database
	\param buffer Buffer in which the database name is written
	\param size Size of the buffer in Buyte
	\return If successful unequal 0, otherwise 0
	\note dword GetFirstCANdbName( char buffer[], dword size);
	*/
    CAPL_FUNC(DWORD, GetFirstCANdbName)(char*, DWORD);
   
    /*!
    Determines the upper limit for the allowable deviation when Jitter is set
	\return Upper deviation in parts per thousand
	\note int getJitterMax();
	*/
    CAPL_FUNC(short, getJitterMax)();
	
	/*!
    Determines the lower limit for the allowable deviation when Jitter is set
	\return Lower deviation in parts per thousand
	\note int getJitterMin();
	*/
    CAPL_FUNC(short, getJitterMin)();
	
	/*!
    Gets the value of a message attribute from the database
	\param canMessage Message variable
	\param attributeName Attribute name
	\return Value of the attribute (or default value) from the database 
	\note long GetMessageAttrInt(message canMessage, char attributeName[]); 
	*/
    CAPL_FUNC(CAPL_INT32, GetMessageAttrInt)(capl::IMessage&, const char*);
    
	/*!
    Finds out the message name
	\param id Id of the message for which the message name should be found
	\return If successful unequal 0, otherwise 0
	\note dword getMessageName( dword id, dword context, char buffer[], dword size);
	*/
	CAPL_FUNC(DWORD, GetMessageName)(DWORD, DWORD, char*, DWORD);
    
	/*!
    Finds out the names of the other assigned databases with pos
	\param pos Position number of the database to be found
	\param buffer Buffer in which the database name is written
	\param size Size of the buffer in Byte
	\return If successful unequal 0, otherwise 0
	\note dword getNextCANdbName( dword pos, char buffer[], dword size); 
	*/
	CAPL_FUNC(DWORD, GetNextCANdbName)(DWORD, char*, DWORD);
    
	/*!
    Determines the value of the start delay configured for this network node in the Simulation Setup
	\return Start delay in ms. If no start delay was set the function returns the value zero
	\note int getStartdelay();
	*/
	CAPL_FUNC(short, getStartdelay)();
    
	/*!
    The function stops both the debugged program as well as the simulation. The debugger window opens and the halt() command is displayed. The function thus creates a breakpoint while simultaneously interrupting the measurement in simulated mode (also for test modules). The simulation can be continued with <F9>. The halt instruction is ignored in Real mode
	\note void halt();
	*/
	CAPL_FUNC(void, halt)();
    
	/*!
    This function causes an update of the variables displayed on the Inspect side of the Write window
	\note void inspect();
	*/
	CAPL_FUNC(void, inspect)();
    
	/*!
     This function is used to get the information if CANoe is in simulated mode
	\return 1: True, CANoe is in simulated mode
	\return 0: False, CANoe is in real mode
	\note long isSimulated();
	*/
	CAPL_FUNC(CAPL_INT32, isSimulated)();
    
	/*!
	This function is used to test whether an acquisition range has already been started
	\return The function returns 1 if an evaluation is already running. Otherwise it returns 0
	\note int isStatisticAcquisitionRunning();
	*/
	CAPL_FUNC(short, isStatisticAcquisitionRunning)();
    
	/*!
    Return value indicates whether a specific timer is active. This is the case between the call to the setTimer function and the call to the on timer event procedure
	\param timer variable
	\param mstimer variable
	\return 1, if the timer is active; otherwise 0
	\return 0 is also returned within the on timer event procedure
	\note int isTimerActive(timer t);
		  int isTimerActive(mstimer t);
	*/
	CAPL_FUNC(short, isTimerActive)(const capl::ITimer&);
    
	/*!
    This function returns the key code of a currently pressed key. If no key is being pressed it returns 0.. For example, pressing of a key can be queried in a timer function. The reaction can also be to letting go of a key
	\return Key code of pressed key
	\note dword keypressed();
	*/
	CAPL_FUNC(DWORD, keypressed)();
    
	/*!
    Returns the time stamp in nanoseconds
	\param message CAN message
	\param linmessage LIN frame
	\param mostMessage, mostAmsMessage, mostRawMessage MOST message
	\return Time stamp of the message in ns
	\note float MessageTimeNS(message msg);
		  float MessageTimeNS(linmessage msg);
          float MessageTimeNS(mostMessage msg);
          float MessageTimeNS(mostAmsMessage msg);
          float MessageTimeNS(mostRawMessage msg);
	*/
	CAPL_FUNC(double, MessageTimeNS)(capl::IMessage&);
    
	/*!
    The msgBeep function plays back a sound predefined by the Windows system. It replaces the previous beep function
	\param soundType Integer for the predefined sound. Specifically these are:
	    0: MB_ICONASTERISK SystemAsterisk 
	 	1: MB_ICONEXCLAMATION SystemExclamation 
	 	2: MB_ICONHAND SystemHand 
	 	3: MB_ICONQUESTION SystemQuestion 
		4: MB_OK SystemDefault 
		5: Standard beep using the PC speaker (default)
	\note void msgBeep (long soundType); 
	*/
	CAPL_FUNC(void, msgBeep)(CAPL_INT32);

    /*!
    This function opens the file named filename for the read access. 
	If mode=0 the file is opened in ASCII mode; 
	if mode=1 the file is opened in binary mode.
	\param filename File name
	\param mode
	\return The return value is the file handle that must be used for read operations. If an error occurs, the return value is 0
	\note dword openFileRead (char filename[], dword mode);
	*/
	CAPL_FUNC(DWORD, openFileRead_Internal)(const char*, DWORD, const char*, const char*);
    
	/*!
    This function opens the file named filename for the write access
	\param filename File name 
	\param mode
	\return The return value is the file handle that must be used for write operations If an error occurs, the return value is 0
	\note dword openFileWrite (char filename[], dword mode);
	*/
	CAPL_FUNC(DWORD, openFileWrite_Internal)(const char*, DWORD, const char*, const char*);

    
	/*!
    Deletes the content of the Panel Designer CAPL Output View control. The panel is accessed by its individual panel name that is entered in the Panel Designer
	\param panel Panel name, restricted to 128 characters. "" – references all panels
	\param control Name of the CAPL Output View control, restricted to 128 characters. "" – references all CAPL Output View controls on the panel
	\note void DeleteControlContent(char[] panel, char[] control);
	*/
	CAPL_FUNC(void, DeleteControlContent)(const char*, const char*);
	
	/*!
    Assigns the value val to the Panel Designer Multi Display Control or the CAPL Output View with the name control. The Multi Display Control/ CAPL Output View is located on the panel with the title panel
	\param panel Name of the panel, restricted to 128 characters
	\param control Name of the control, restricted to 128 characters
	\param val Value to be displayed 	
	\note putValueToControl
	*/
    CAPL_FUNC(void, putValueToControlString)(const char*, const char*, const char*);
    
	CAPL_FUNC(void, putValueToControlInt)(const char*, const char*, CAPL_INT32);
   	CAPL_FUNC(void, putValueToControlIntParagraph)(const char*, const char*, CAPL_INT32, CAPL_INT32);
    CAPL_FUNC(void, putValueToControlIntHex)(const char*, const char*, CAPL_INT32, CAPL_INT32, CAPL_INT32);
   	CAPL_FUNC(void, putValueToControlDouble)(const char*, const char*, double);
    CAPL_FUNC(void, putValueToControlDoubleParagraph)(const char*, const char*, double, CAPL_INT32);
   	CAPL_FUNC(void, putValueToControlObj)(const char*, const char*, capl::IMessage&);
    CAPL_FUNC(void, putValueToControlObjParagraph)(const char*, const char*, capl::IMessage&, CAPL_INT32);
    CAPL_FUNC(void, putValueToControlObjHex)(const char*, const char*, capl::IMessage&, CAPL_INT32, CAPL_INT32);
   
	/*!
    Replaces the image of the Panel Designer Picture Box control during runtime. The panel is accessed by its individual panel name that is entered in the Panel Designer
	\param panel Panel name, restricted to 128 characters. "" – references all opened panels
	\param control Name of the control, restricted to 128 characters. "" – references all controls on the panel
	\param iamgefile Path and name of the image file
	\note setPictureBoxImage(panel, control, imagefile);
	*/
	CAPL_FUNC(void, setPictureBoxImage)(const char*, const char*, const char*);

	/*!
    Resets the CAN controller. Can be used to reset the CAN controller after a BUSOFF or to activate configuration changes. Since execution of the function takes some time and the CAN controller is disconnected from the bus briefly, messages can be lost when this is performed
	\note void resetCan();
	*/
    CAPL_FUNC(void, resetCan)();
    
	/*!
    Resets the CAN controller for one specific CAN channel. Can be used to reset the CAN controller after a BUSOFF or to activate configuration changes. Since execution of the function takes a certain amount of time and the CAN controller is disconnected from the bus for a brief period messages may be lost
	\param channel CAN channel
	\note void ResetCanEx(long channel);
	*/
	CAPL_FUNC(void, ResetCanEx)(CAPL_INT32);
    
	/*!
    Triggers a run error. Outputs the error number to the Write Window indicating the error number and the passed number, and then terminates the measurement
	\param value Numbers that are represented in CANoe as a references for the user. The values under 1000 are reserved for internal purposes. The second parameter is reserved for future expansions
	\note void runError(long err, long);
	*/
	CAPL_FUNC(void, runError)(CAPL_INT32, CAPL_INT32);
    
	/*!
    The function determines the baud rate for the given channel. Result of the function is written into the Write Window
	\param channel Channel number. (1,..,32)
    \param messageID ID of the message that the scanner will send to detect the baud rate. The DLC of the message is always 8
	\param firstBaudrate/lastBaudrate Baud rate range to scan
	\param timeout Period of time [ms] the scanner waits when the message is sent
	\return Returns 0 if the scan function was successfully started. Otherwise the return value is non-zero
	\note long ScanBaudrateActive( dword channel, dword messageID, double firstBaudrate, double lastBaudrate, dword timeout);
	*/
	CAPL_FUNC(CAPL_INT32, ScanBaudrateActive)(DWORD, DWORD, double, double, DWORD);
   
	/*!
    Baud rate scanner checks different baud rates and tries to receive a message on the channel. Function starts the scan and detects the baud rate on the given channel. Result of the function is written into the Write Window
	\param channel Channel number. (1,..,32)
    \param messageID ID of the message that the scanner will send to detect the baud rate. The DLC of the message is always 8
	\param firstBaudrate/lastBaudrate Baud rate range to scan
	\param timeout Period of time [ms] the scanner waits when the message is sent
	\param bAcknowledge Acknowledge mode on (1)/off (0). If a wrong baud rate is present, CANoe cannot receive messages and sends an Error Frame, which can be put on the bus using the parameter bAcknowledge. The parameter serves for the fact that CANoe - as a passive receiver - can participate indirectly in the network communication by sending an Error Frame. The parameter does not change the acknowledge settings of the Hardware Configuration dialog. The parameter has an effect only during runtime of the function.
	\return Returns 0 if the scan function was successfully started. Otherwise the return value is non-zero
	\note long ScanBaudratePassive( dword channel, dword messageID, double firstBaudrate, double lastBaudrate, dword timeout, dword bAcknowledge);
	*/
    CAPL_FUNC(CAPL_INT32, ScanBaudratePassive)(DWORD, DWORD, double, double, DWORD, DWORD);
    
	/*!
    Various CANcabs modes may be set. Replaces the setPortBits functions
	\param ntype Unused, must be set to 0
	\param nchannel CAN channel
	\param nmode Is used for the control of the board lines via a bit pattern. Mode, valid values:
				 0: NORMAL
				 1: SLEEP
				 2: HIVOLTAGE
				 3: HISPEED
				 4: DUAL_WIRE
				 5: SINGLE_WIRE_LOW
				 6: SINGLE_WIRE_HIGH
				 7: is reserved 
	\param CANcab Eva Two output lines can be set
	\param nflags Is used for the control of the board lines via a bit pattern
	\return 0: ok
	\return !=0: Error
	\note long setCanCabsMode(long ntype,long nchannel,long nmode,long nflags);
	*/
	CAPL_FUNC(CAPL_INT32, setCanCabsMode)(CAPL_INT32, CAPL_INT32, CAPL_INT32, CAPL_INT32);


	/*!
    A constant deviation can be set for the timers of a network node with this function. Inputs for the two values may lie between –10000 and 10000 (corresponds to –100.00% to 100.00%). If the value does not lie within this range, a message is output in the Write Window
	\param drift Integer for the constant deviation 
	\note void setDrift(int drift);
	*/
    CAPL_FUNC(void, setDrift)(short);
    
	/*!
    The Jitter interval for the timers of a network node can be set with this function. The two values may lie between –10000 and 10000 (corresponds to –100.00% to 100.00%). If one of the two values does not lie within this range, a message is output in the Write Window
	\param min Integer for the lower interval limit
	\param max Integer for the upper interval limit
	\note setJitter(int min, int max);
	*/
	CAPL_FUNC(void, setJitter)(short, short);
    
	/*!
    Sets the name of the logging file. If a valid extension is given it also changes the file type
	\param fileName 
	\param strLoggingBlockName Name of the Logging Block
	\note setLogFileName(char fileName[]);
	*/
	CAPL_FUNC(void, setLogFileName)(const char*);
    
	/*!
    Sets the Output Control Register. The values do not become active until the next call of the function resetCan()
	\param CAN channel  0 - all channels; > 0 - only the given channel
 	\param OCR Value of the Output Control Register
	\return 1: success
	\return 0: error
	\note setOcr(long channel, byte ocr);
	*/
	CAPL_FUNC(void, setOcr)(CAPL_INT32, BYTE);
    
	/*!
    Sets the posttrigger of the logging. The posttrigger set with this function is valid until the end of the measurement or until the next call of this function
	\param value New posttrigger value in milliseconds. If a value of -1 is supplied, the prosttrigger will be set to infinity
	\return 1, if the posttrigger is set to the given value, 0 else
	\note setPostTrigger(long preTriggerTime);
	*/
	CAPL_FUNC(void, setPostTrigger)(CAPL_INT32);
    
	/*!
    
	\param 
	\return 
	\note 
	*/
	CAPL_FUNC(void, setPreTrigger)(CAPL_INT32);
    
	/*!
    Sets the pretrigger of the logging. The pretrigger set with this function is valid until the end of the measurement or until the next call of this function
	\param value New pretrigger value in milliseconds
	\return 1, if the pretrigger is set to the given value, 0 else
	\note setPreTrigger(long preTriggerTime);
	*/
	CAPL_FUNC(void, setStartdelay)(short);
    
	/*!
    This function sets the priority level for the writeDbgLevel CAPL function. The output priority must be set for every network node
	\param priority Priority of current CAPL node for output to the Write Window. Rays for priority: 0 to 15
				0 - Only write output with a priority of 0 are shown in the Write Window.
				 
				5 - Write output with a priority ranging from 0 to 5 are shown.
				 
				15 - All outputs are shown
	\note void setWriteDbgLevel (unsigned int priority);
	*/
	CAPL_FUNC(void, setWriteDbgLevel)(WORD);

	/*!
    Sets a property of a Panel Editor ActiveX control
	\param panel Panel name, restricted to 128 characters. "" – references all opened panels
	\param control Name of the panel control, restricted to 128 characters. "" – references all elements on the panel
	\param property Name of the property
	\param value Value to be set (long, float or string value)
	\note void SetControlProperty(char[] panel, char[] control, char[] property, char[] value);
	*/
    CAPL_FUNC(void, SetControlPropertyString)(const char*, const char*, const char*, const char*);
    
	CAPL_FUNC(void, SetControlPropertyInt)(const char*, const char*, const char*, CAPL_INT32);
 	CAPL_FUNC(void, SetControlPropertyDouble)(const char*, const char*, const char*, double);
	CAPL_FUNC(void, setTimer2P)(ITimer& timer, CAPL_INT32 time);
    CAPL_FUNC(void, setTimer3P)(ITimer& timer, CAPL_INT32 time, CAPL_INT32 time2);
    
	/*!
    Sets a cyclical timer
	\param t The timer to be set
	\param period Time in milliseconds in which the timer is restarted in case of expiration
	\note void setTimerCyclic(msTimer t, long period);
	*/
	CAPL_FUNC(void, setTimerCyclic2P)(ITimer& timer, CAPL_INT32 period);
    
	/*!
    \param t The timer to be set
	\param period Time in milliseconds in which the timer is restarted in case of expiration
	\param firstDuration Time in milliseconds until the timer runs out for the first time  
	\note void setTimerCyclic(msTimer t, long firstDuration, long period);
	*/
	CAPL_FUNC(void, setTimerCyclic3P)(ITimer& timer, CAPL_INT32 firstDuration, CAPL_INT32 period);
    
	/*!
    Starts all Logging Blocks immediately bypassing all logging trigger settings
	\param strLoggingBlockName Name of the Logging Block
	\param preTriggerTime Pre-trigger time interval in ms
	\note void startLogging();
	*/
	CAPL_FUNC(void, startLogging)();
	
	/*!
    Starts all Logging Blocks immediately bypassing all logging trigger settings
	\param strLoggingBlockName Name of the Logging Block
	\note void startLogging(char strLoggingBlockName[]);
	*/
	CAPL_FUNC(void, startLogging1P)(const char*);
    
	/*!
    Starts all Logging Blocks immediately bypassing all logging trigger settings
	\param strLoggingBlockName Name of the Logging Block
	\param preTriggerTime Pre-trigger time interval in ms
	\note void startLogging(char strLoggingBlockName[], long preTriggerTime);
	*/
	CAPL_FUNC(void, startLogging2P)(const char*, CAPL_INT32);
	
	/*!
    Stops all Logging Blocks immediately bypassing all logging trigger settings
	\param strLoggingBlockName Name of the Logging Block
	\param postTriggerTime Post-trigger time interval in ms
	\note void stopLogging();
	*/
    CAPL_FUNC(void, stopLogging)();
	
	/*!
    Stops all Logging Blocks immediately bypassing all logging trigger settings
	\param strLoggingBlockName Name of the Logging Block
	\note void stopLogging(char strLoggingBlockName[]);
	*/
    CAPL_FUNC(void, stopLogging1P)(const char*);
    
	/*!
    Stops all Logging Blocks immediately bypassing all logging trigger settings
	\param strLoggingBlockName Name of the Logging Block
	\param postTriggerTime Post-trigger time interval in ms
	\note void stopLogging(char strLoggingBlockName[], long postTriggerTime);
	*/
	CAPL_FUNC(void, stopLogging2P)(const char*, CAPL_INT32);

	/*!
    Starts playing the macro with the fileName file name
	\param fileName Macro file
	\return The returned handle is required to stop the macro playback
	\note dword StartMacroFile(char fileName[]);
	*/
    CAPL_FUNC(DWORD, StartMacroFile)(const char*);
	
	/*!
    Starts playing the replay file with the name fileName. This function replays events of the following types:
		CAN messages 
		CAN Error Frames 
		CAN overload frames (not sent, only visible in Measurement Setup) 
		J1708 messages 
		Ethernet packets 
		WLAN packets 
		AFDX packets 
		Diagnostic requests 
		GPS events 
		Signal values 
		System variable updates 
		Environment variable updates 
	\param fileName Replay file
	\return The returned handle is required to stop the replay file
	\note dword StartReplayFile(char fileName[]);
	*/
    CAPL_FUNC(DWORD, StartReplayFile)(const char*);
	
	/*!
    A new acquisition range is started with this function. If an acquisition range has already been started, the function has no effect since it cannot influence the currently active range
	\note void startStatisticAcquisition()
	*/
    CAPL_FUNC(void, startStatisticAcquisition)();

	/*!
    Stops the macro from playing with the handle handle
	\param handle Handle of the started macro
	\note void StopMacroFile(dword handle);
	*/
    CAPL_FUNC(void, StopMacroFile)(DWORD);
	
	/*!
    Stops the replay file from playing with the handle handle
	\param handle Handle of the started replay file
	\note void StopReplayFile(dword handle);
	*/
    CAPL_FUNC(void, StopReplayFile)(DWORD);
	
	/*!
    A started acquisition range is stopped with this function. If no acquisition range has been started yet, this function has no effect
	\note void stopStatisticAcquisition()
	*/
    CAPL_FUNC(void, stopStatisticAcquisition)();
	
	/*!
    Exits the system (CANoe) from within a CAPL program
	\note void sysExit();
	*/
    CAPL_FUNC(void, sysExit)();
	
	/*!
    The application window of CANoe will be minimized or restored. The first call of the function minimizes the window, afterwards the window will be restored to normal size and minimized alternaltly
	\note void sysMinimize();
	*/
    CAPL_FUNC(void, sysMinimize)();
	
	/*!
    Time difference between messages or between a message and the current time in ms (msg2 - msg1 or now - msg1). Starting with CANalyzer 2.xx this difference can be calculated directly (Units of 10 microseconds)
	\param Variable of type message
	\param Variable of type now
	\return Time difference in ms
	\note long timeDiff(message m1, NOW);
	*/
    CAPL_FUNC(CAPL_INT32, timeDiff)(capl::IMessage&, capl::IMessage&);
	
	/*!
    Supplies the current simulation time (maximum time: .2^32 * 10 microseconds = 11 hours, 55 minutes, 49 seconds, 672 milliseconds, 96 microseconds)
	The simulation time can be correlated with the hardware results of the interface cards (e.g. CANcardXL).
	The resolution of this time is dependent upon the hardware used (usually a millisecond or better).
		Depending on the hardware configuration, the simulation time
		will be the same as the message time calculated by the interface cards		
		(e.g., system with two CAN channels connected to one CANcardXL) 
	or
		the message times will have a higher accuracy 
	\return Simulation time in 10 microsecondsdword timeNow();
	\note dword timeNow();
	*/
    CAPL_FUNC(DWORD, timeNow)();
	
	/*!
    Supplies the current simulation time.
	The simulation time can be correlated with the hardware results of the interface cards (e.g. CANcardXL).
	The resolution of this time is dependent upon the hardware used (usually a millisecond or better).
	Depending on the hardware configuration, the simulation time 
		will be the same as the message time calculated by the interface cards
		(e.g., system with two CAN channels connected to one CANcardXL) 
	or
		the message times will have a higher accuracy 
	\return Simulation time in 10 microseconds
	\note float timeNowFloat();
	*/
    CAPL_FUNC(double, timeNowFloat)();
	
	/*!
    Supplies the current simulation time.
	The simulation time can be correlated with the hardware results of the interface cards (e.g. CANcardXL).
	The resolution of this time is dependent upon the hardware used (usually a millisecond or better).
	Depending on the hardware configuration, the simulation time 
		will be the same as the message time calculated by the interface cards
		(e.g., system with two CAN channels connected to one CANcardXL) 
	or
		the message times will have a higher accuracy 
	\return Simulation time in nanoseconds
	\note float TimeNowNS();
	*/
    CAPL_FUNC(double, timeNowNS)();
	
	/*!
    Supplies the current simulation time.
	The simulation time can be correlated with the hardware results of the interface cards (e.g. CANcardXL).
	The resolution of this time is dependent upon the hardware used (usually a millisecond or better).
	Depending on the hardware configuration, the simulation time 
		will be the same as the message time calculated by the interface cards
		(e.g., system with two CAN channels connected to one CANcardXL) 
	or
		the message times will have a higher accuracy
	\return Simulation time in nanoseconds
	\note int64 timeNowInt64();
	*/
    CAPL_FUNC(CAPL_INT64, timeNowInt64)();
	
	/*!
    Returns a value indicating how much more time will elapse before an on timer event procedure is called
	\param timer or mstimer variable
	\return Time to go until the timer elapses and the event procedure is called
	\note long timeToElapse(timer t)
	*/
    CAPL_FUNC(CAPL_INT32, timeToElapse)(const capl::ITimer&);
    CAPL_FUNC(void, traceSetEventColors)(capl::IMessage&);
	
	/*!
    Sends a trigger event to all CANoe Logging or Trigger Blocks
	For a Logging Block, the trigger event starts and stops logging, depending on
		the trigger mode (single or toggle trigger) 
		the trigger conditions for toggle on and toggle off 
	\note void trigger();
	*/
    CAPL_FUNC(void, trigger)();
	
	/*!
    Sends a trigger event to a CANoe Logging or Trigger Block specified by name.
	For a Logging Block, the trigger event starts and stops logging, depending on
		the trigger mode (single or toggle trigger) 
		the trigger conditions for toggle on and toggle off 
	\param name Name of the Logging or Trigger Block
	\note void triggerEx(char name[]);
	*/
    CAPL_FUNC(void, triggerEx)(const char*);

	/*!
    Generates a new page in the Write Window with the specified name. The page is automatically deleted the next time a measurement starts
	\param name Name of the page to be generated
	\return Sink identifier that is valid for output to the new page
	\note dword writeCreate(char name[])
	*/
    CAPL_FUNC(DWORD, writeCreate)(const char*);
	
	/*!
    Removes the specified page from the Write Window. Only pages that have been created with the aid of the writeCreate function can be removed
	\param sink Target identifier for the page to be removed
	\note void writeDestroy(dword sink)
	*/
    CAPL_FUNC(void, writeDestroy)(DWORD);
	
	/*!
    Clears the contents of the specified page in the Write Window
	\param sink Target identifier for the page to be deleted
	\note void writeClear(dword sink)
	*/
    CAPL_FUNC(void, writeClear)(DWORD);
	
	/*!
    Outputs a text message to the Write Window. Write is based on the C function printf
	\param Format string, variables or expressions. Legal format expressions:
		"%ld","%d" decimal display
		"%lx","%x" hexadecimal display
		"%lX","%X" hexadecimal display (upper case)
		"%lu","%u" unsigned display
		"%lo","%o" octal display
		"%s" display a string
		"%g","%f" floating point displa e.g. %5.3f means, 5 digits in total (decimal point inclusive) and 3 digits after the decimal point. 5 is the minimum of digits in this case.
		"%c" display a character
		"%%" display %-character
		"%I64d","%lld" decimal display of a 64 bit value
		"%I64x","%llx hexadecimal display of a 64 bit value
		"%I64X","%llX" hexadecimal display of a 64 bit value (upper case)
		"%I64u","%llu" unsigned display of a 64 bit value
		"%I64o","%llo" octal display of a 64 bit value
	\note void write(char format[], ...);
	*/
    CAPL_FUNC(void, write)(const char*, ...);
	
	/*!
    Writes the text into the last line of the specified CANoe window, into a page of the Write Window or into a logging file without previously creating a new line
	\param sink Sink identifier of the page to which the output will take place. Values:
		-3 Trace Window
		-2 Output to the logging file (only in ASC format and if the CAPL node is inserted in the Measurement Setup in front of the Logging Block)
		-1 Reserved
		0 Output to the System page of the Write Window
		1 Output to the CAPL page of the Write Window
		4 Output to the Test page of the Write Window
	\param severity Constant for the type of message. Values:
		0 Success
		1 Information
		2 Warning
		3 Error
	\param format Formatting character sequence
	\note void writeEx(long sink, dword severity, char format[], ...)
	*/
    CAPL_FUNC(void, writeEx)(DWORD, DWORD, const char*, ...);
	
	/*!
    Writes the text into a new line of the specified window, into a page of the CANoe Write Window or into a logging file
	\param sink Sink identifier of the page to which the output will take place. Values:
		-3 Trace Window
		-2 Output to the logging file (only in ASC format and if the CAPL node is inserted in the Measurement Setup in front of the Logging Block)
		-1 Reserved
		0 Output to the System page of the Write Window
		1 Output to the CAPL page of the Write Window
		4 Output to the Test page of the Write Window
	\param severity Constant for the type of message. Values:
		0 Success
		1 Information
		2 Warning
		3 Error
	\param format Formatting character sequence
	\note void writeLineEx(long sink, dword severity, char format[], ...)
	*/
    CAPL_FUNC(void, writeLineEx)(DWORD, DWORD, const char*, ...);

	/*!
    Sets the color for text background of the specified page in the Write Window
	\param sink The target identifier of the page on which the color settings should have an effect
	\param red Specifies the intensity of the red color
	\param green Specifies the intensity of the green color
	\param blue Specifies the intensity of the blue color
	\note void writeTextBkgColor(dword sink,dword red, dword green, dword blue)
	*/
    CAPL_FUNC(void, writeTextBkgColor)(DWORD, DWORD, DWORD, DWORD);
	
	/*!
    Sets the color for text of the specified page in the Write Window
	\param sink The target identifier of the page on which the color settings should have an effect
	\param red Specifies the intensity of the red color
	\param green Specifies the intensity of the green color
	\param blue Specifies the intensity of the blue color
	\note void writeTextColor(dword sink,dword red, dword green, dword blue)
	*/
    CAPL_FUNC(void, writeTextColor)(DWORD, DWORD, DWORD, DWORD);
	
	/*!
    Writes an output string to an ASCII logging file. Write is based on the C function printf
	\param Format string, variables or expressions. Legal format expressions:
		"%ld","%d" decimal display
		"%lx","%x" hexadecimal display
		"%lX","%X" hexadecimal display (upper case)
		"%lu","%u" unsigned display
		"%lo","%o" octal display
		"%s" display a string 
		"%g","%lf" floating point display
		"%c" display a character
		"%%" display %-character
		"%I64d" decimal display of a 64 bit value
		"%I64x" hexadecimal display of a 64 bit value
		"%I64X" hexadecimal display of a 64 bit value (upper case)
		"%I64u" unsigned display of a 64 bit value 
	\note void writeToLog(char format[], ...);
	*/
    CAPL_FUNC(void, writeToLog)(const char*);
	
	/*!
    Writes an output string to an ASCII logging file. Write is based on the C function printf
	\param "%ld","%d" decimal display
		"%lx","%x" hexadecimal display
		"%lX","%X" hexadecimal display (upper case)
		"%lu","%u" unsigned display
		"%lo","%o" octal display
		"%s" display a string 
		"%g","%lf" floating point display
		"%c" display a character
		"%%" display %-character
		"%I64d" decimal display of a 64 bit value
		"%I64x" hexadecimal display of a 64 bit value
		"%I64X" hexadecimal display of a 64 bit value (upper case)
		"%I64u" unsigned display of a 64 bit value 
	\note void writeToLogEx(char format[], ...);
	*/
    CAPL_FUNC(void, writeToLogEx)(const char*);
	
	/*!
    Outputs a message to the Write Window with the specified priority. This function can be used for debugging to vary the output to the Write Window. This function is especially useful if nodelayer-DLL’s are used
	\param priority Output priority from 0 to 15
	\param format Format string, variables or expressions. legal format expressions:
		"%ld", "%d" decimal display
		"%lx", "%x" hexadecimal display
		"%lX", "%X" hexadecimal display (upper case)
		"%lu", "%u" unsigned display
		"%lo", "%o" octal display
		"%s" display a string
		"%g", "%lf" floating point display
		"%c" display a character
		"%%" display %-character 
	\note long writeDbgLevel(unsigned int priority, char format1[], char format2[], ...);
	*/
    CAPL_FUNC(CAPL_INT32, writeDbgLevel)(CAPL_UINT32 priority, const char* format, ...);
    CAPL_FUNC(bool, validatePlugin)();
    // Swapt Bytes Intel/Motorola.
	
	/*!
    Swaps bytes of parameters. CAPL arithmetics follows the "little-endian-format" (Intel). The swap-functions serve for swapping bytes for the transition to and from the "big-endian-format" (Motorola)
	\param Value whose bytes are to be swapped
	\return Value with swapped bytes
	\note word swapWord(word x);
	*/
    CAPL_FUNC(WORD, swapWord)(WORD u16In);
	
	/*!
    Swaps bytes of parameters. CAPL arithmetics follows the "little-endian-format" (Intel). The swap-functions serve for swapping bytes for the transition to and from the "big-endian-format" (Motorola)
	\param Value whose bytes are to be swapped
	\return Value with swapped bytes
	\note int swapInt(int x);
	*/
    CAPL_FUNC(SHORT, swapInt)(SHORT i16In);
	
	/*!
    Swaps bytes of parameters. CAPL arithmetics follows the "little-endian-format" (Intel). The swap-functions serve for swapping bytes for the transition to and from the "big-endian-format" (Motorola)
	\param Value whose bytes are to be swapped
	\return Value with swapped bytes
	\note dword swapDWord(dword x);
	*/
    CAPL_FUNC(DWORD, swapDWord)(DWORD u32In);
	
	/*!
    Swaps bytes of parameters. CAPL arithmetics follows the "little-endian-format" (Intel). The swap-functions serve for swapping bytes for the transition to and from the "big-endian-format" (Motorola)
	\param Value whose bytes are to be swapped
	\return Value with swapped bytes
	\note qword swapQWord(qword x);
	*/
    CAPL_FUNC(CAPL_UINT64, swapQWord)(CAPL_UINT64 u64In);
	
	/*!
    Swaps bytes of parameters. CAPL arithmetics follows the "little-endian-format" (Intel). The swap-functions serve for swapping bytes for the transition to and from the "big-endian-format" (Motorola)
	\param Value whose bytes are to be swapped
	\return Value with swapped bytes
	\note long swapLong(long x);
	*/
    CAPL_FUNC(CAPL_INT32, swapLong)(CAPL_INT32 i32In);
	
	/*!
    Swaps bytes of parameters. CAPL arithmetics follows the "little-endian-format" (Intel). The swap-functions serve for swapping bytes for the transition to and from the "big-endian-format" (Motorola)
	\param Value whose bytes are to be swapped
	\return Value with swapped bytes
	\note int64 swapInt64(int64 x);
	*/
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

	/*!
    This command is used to send Wakeup frames. Wakeup frames can only be sent while the LIN hardware is in Sleep mode. If no parameters are given, the default values of the parameters are used
	\param ttobrk This parameter specifies the time difference between the transmissions of two consecutive Wakeup frames, i.e. the time between end of one wake-up frame and start of the next one
		Units of this parameter as well as default value depend on the hardware settings (see Hardware Configuration: LIN)
		Value range (for units expected in bit times): 20 .. 50000
		Value range (for units expected in ms): 1 .. 65536 
	\param count Sets the number of Wakeup frame retransmissions
		Value range: 1…255
		Default value depends on the hardware settings: see Hardware Configuration: LIN
	\param length This parameter sets the length of the wake-up frame to be sent in microseconds. The resolution is 50 µs
		This parameter is only used by LIN2.x slave nodes
		Value range: 250...5000 µs
		Default value depends on the hardware settings: see Hardware Configuration: LIN
	\return On success, a value unequal to zero, otherwise zero
	\note long linSendWakeup()
	*/
    CAPL_FUNC(CAPL_INT32, linSendWakeupInternal)(const char* sBusStringIds, CAPL_INT32 ttobrk, CAPL_INT32 count, CAPL_UINT32 length);

	/*!
    This function switches from the current schedule table to another one
	\param tableIndex Index of the schedule table to be changed to. Value range: 0..N-1, where N is a total number of  defined schedule tables
	\param slotIndex Index of slot to be started within the new schedule table. Default value: 0. Value range: 0..Y-1, where Y is a total number of slots in the new schedule table
	\param onSlotIndex Index of last slot in the current schedule table to be sent before changing to the new schedule table. Default value: -2 - makes change immediately
		Value range: -2..X-1, where X is a total number of slots in the current schedule table
		Value: -1 - makes change on reaching the end of current schedule table
		Value: -2 - makes change immediately
	\return Index of the current schedule table or -1 if no active schedule table exists and on failure
	\note long linChangeSchedTable(dword tableIndex)
	*/
    CAPL_FUNC(CAPL_INT32, linChangeSchedTableInternal)(const char* sBusStringIds, CAPL_UINT32 tableIndex, CAPL_UINT32 slotIndex, CAPL_UINT32 onSlotIndex);
    CAPL_FUNC(CAPL_INT32, linSendSleepModFrmInternal)(const char* sBusStringIds, CAPL_INT32 silent, CAPL_INT32 restartScheduler, CAPL_INT32 wakeupIdentifier);
     
	/*!
    With this function it is possible to calculate protected ID for the corresponding LIN frame identifier (i.e. the frame identifier with parity bits)
	\param frameID LIN frame identifier whose protected ID will be calculated. Value range: 0 .. 63
	\return Returns the calculated protected identifier or -1 on failure
	\note dword linGetProtectedID (long frameID)
	*/
    CAPL_FUNC(CAPL_UINT32, linGetProtectedID)(CAPL_INT32 frameID);

	/*!
    This function is used to get the information whether the CAPL code is executed on the local PC or by a runtime kernel running on a remote device (VN8900/CANoe RT Server)
	\return 1 : True (execution on remote runtime kernel)
	\return 0 : False (execution on local PC)
	\note long IsRunningOnRemoteKernel()
	*/
    CAPL_FUNC(CAPL_INT32, IsRunningOnRemoteKernel)();

	/*!
    Converts the string s to an unsigned 32bit integer. The number base is hexadecimal if the string starts with ‘0x’, octal if it starts with ‘0’, decimal otherwise. Whitespace (spaces or tabs) at the start of the string is ignored
	\param s String to be converted
	\param result Contains the converted value after the call. The value is 0 if the string can’t be converted to a number. It is the largest possible positive/negative number in case of overflow
	\return -2: If s is empty or startIndex is larger than strlen(s) 
	\return -1: An overflow occurs
	\return Otherwise, returns the index of the first character after the number
	\note int strtoul(char s[], dword& result);
	*/
    CAPL_FUNC(CAPL_INT32, strtoul)(const char* s, CAPL_UINT32& result);
    
	/*!
    Converts the string s to an unsigned 32bit integer. The number base is hexadecimal if the string starts with ‘0x’, octal if it starts with ‘0’, decimal otherwise. Whitespace (spaces or tabs) at the start of the string is ignored
	\param s String to be converted
	\param result Contains the converted value after the call. The value is 0 if the string can’t be converted to a number. It is the largest possible positive/negative number in case of overflow
	\param startIndex Position in s where the conversion shall begin 
	\return -2: If s is empty or startIndex is larger than strlen(s) 
	\return -1: An overflow occurs
	\return Otherwise, returns the index of the first character after the number
	\note int strtoul(char s[], dword startIndex, dword& result);
	*/
	CAPL_FUNC(CAPL_INT32, strtoul_index)(const char* s, CAPL_UINT32 startIndex, CAPL_UINT32& result);
 
	/*!
    Converts the string s to an unsigned 64bit integer. The number base is hexadecimal if the string starts with ‘0x’, octal if it starts with ‘0’ , decimal otherwise. Whitespace (spaces or tabs) at the start of the string is ignored
	\param s String to be converted
	\param result Contains the converted value after the call. The value is 0 if the string can’t be converted to a number. It is the largest possible positive/negative number in case of overflow
	\return -2: If s is empty or startIndex is larger than strlen(s) 
	\return -1: An overflow occurs
	\return Otherwise, returns the index of the first character after the number
	\note int strtoull(char s[], qword& result);
	*/
    CAPL_FUNC(CAPL_INT32, strtoull)(const char* s, unsigned __int64& result);
   
	/*!
    Converts the string s to an unsigned 64bit integer. The number base is hexadecimal if the string starts with ‘0x’, octal if it starts with ‘0’ , decimal otherwise. Whitespace (spaces or tabs) at the start of the string is ignored
	\param s String to be converted
	\param result Contains the converted value after the call. The value is 0 if the string can’t be converted to a number. It is the largest possible positive/negative number in case of overflow
	\param startIndex Position in s where the conversion shall begin
	\return -2: If s is empty or startIndex is larger than strlen(s) 
	\return -1: An overflow occurs
	\return Otherwise, returns the index of the first character after the number
	\note int strtoull(char s[], dword startIndex, qword& result);
	*/
    CAPL_FUNC(CAPL_INT32, strtoull_index)(const char* s, CAPL_UINT32 startIndex, unsigned __int64& result);
    
	/*!
    Searches for a regular expression pattern in a string
	\param s String to be searched
	\param pattern Regular expression which is searched. For the regular expression, the same syntax is used as in the Perl programming language
	\return The position in s where the pattern was found, or -1 if it wasn’t found
	\note long strstr_regex(char s[], char pattern[])
	*/
	CAPL_FUNC(CAPL_INT32, strstr_regex)(const char* s, const char* pattern);
    
	/*!
    Searches for a regular expression pattern in a string
	\param s String to be searched
	\param offset Offset in s at which the search shall be started
	\param pattern Regular expression which is searched. For the regular expression, the same syntax is used as in the Perl programming language
	\return The position in s where the pattern was found, or -1 if it wasn’t found
	\note long strstr_regex_off(char s[], long offset, char pattern[])
	*/
	CAPL_FUNC(CAPL_INT32, strstr_regex_off)(const char* s, long offset, const char* pattern);
    
	/*!
    Replaces all occurrences of a text in a string with another string
	\param s String to be modified 
	\param searched Text which shall be replaced
	\param replacement Text which replaces the original characters
	\return 1 if successful, 0 if the resulting string would be too long for the buffer s
	\note long str_replace(char s[], char searched[], char replacement[]) 
	*/
	CAPL_FUNC(CAPL_INT32, str_replace)(char* s, const char* searched, const char* replacement);
    
	/*!
	Replaces a part of a string with another string
	\param s String to be modified 
	\param startoffset Offset at which to start replacing characters
	\param replacement Text which replaces the original characters
	\param length Maximum number of characters to replace
	\return 1 if successful, 0 if the resulting string would be too long for the buffer s
	\note long str_replace(char s[], long startoffset, char replacement[], long length) 
	*/
	CAPL_FUNC(CAPL_INT32, str_replace_off)(char* s, long startoffset, const char* replacement, long length);

    /*!
    Returns the file name of the currently loaded configuration. It includes neither the file extension nor any path information, for example CANSystemDemo
	\param buffer Space for the returned name
	\param bufferLength Length of the buffer
	\return -1 Buffer too small
	\return >= 0 Operation successful. The return value specifies the length of the configuration name
	\note long getConfigurationName(char buffer[], dword bufferLength); 
	*/
	CAPL_FUNC(CAPL_INT32, getConfigurationName)(char buffer[], CAPL_UINT32 bufferLength);

    /*!
    Converts a time stamp to separate parts 
	\param timestamp Time stamp in nanoseconds 
	\param days Receives the days of the time stamp
	\param hours Receives the hours the time stamp (between 0 and 23)
	\param minutes Receives the minutes of the time stamp (between 0 and 59)
	\param seconds Receives the seconds of the time stamp (between 0 and 59)
	\param milliseconds Receives the milliseconds of the time stamp (between 0 and 999)
	\param microseconds Receives the microseconds of the time stamp (between 0 and 999)
	\note void convertTimestamp(dword timestamp, dword& days, byte& hours, byte& minutes, byte& seconds, word& milliSeconds, word& microSeconds); 
	*/
	CAPL_FUNC(CAPL_INT32, convertTimestamp)(CAPL_UINT32 timestamp, CAPL_UINT32& days, CAPL_UINT8& hours, CAPL_UINT8& minutes, CAPL_UINT8& seconds, CAPL_UINT16& milliSeconds, CAPL_UINT16& microSeconds);
    
	/*!
    Converts a time stamp to separate parts 
	\param timestamp Time stamp in 10 microseconds
	\param days Receives the days of the time stamp
	\param hours Receives the hours the time stamp (between 0 and 23)
	\param minutes Receives the minutes of the time stamp (between 0 and 59)
	\param seconds Receives the seconds of the time stamp (between 0 and 59)
	\param milliseconds Receives the milliseconds of the time stamp (between 0 and 999)
	\param nanoseconds Receives the nanoseconds of the time stamp (between 0 and 999)
	\param microseconds Receives the microseconds of the time stamp (between 0 and 999)
	\note void convertTimestampNS(qword timestamp, dword& days, byte& hours, byte& minutes, byte& seconds, word& milliSeconds, word& microSeconds, word& nanoSeconds);
	*/
	CAPL_FUNC(CAPL_INT32, convertTimestampNS)(CAPL_UINT64 timestamp, CAPL_UINT32& days, CAPL_UINT8& hours, CAPL_UINT8& minutes, CAPL_UINT8& seconds, CAPL_UINT16& milliSeconds, CAPL_UINT16& microSeconds, CAPL_UINT16& nanoSeconds);

	/*!
    Checks whether a string completely matches a regular expression pattern
	\param s String to be checked
	\param pattern Regular expression against which the string is matched. For the regular expression, the same syntax is used as in the Perl programming language
	\return 1 if the string matches the pattern, 0 if it doesn’t match the pattern
	\note long str_match_regex(char s[], char pattern[])
	*/
    CAPL_FUNC(CAPL_INT32, str_match_regex)(char s[], char pattern[]);
	
	CAPL_FUNC(void, _gcvtInternal)(double val, CAPL_INT32 digits, char* s, CAPL_INT32 length);
    template<class T>
    void _gcvt(double val, CAPL_INT32 digits, T& s)
    {
        _gcvtInternal(val, digits, s, sizeof(s));
    }

    /*!
    Transforms a character or string to upper case. Only characters a-z and A-Z are supported
	\param c Character to be transformed
	\return Character to be transformed
	\note char toUpper(char c);
	*/
	CAPL_FUNC(CAPL_INT8, toUpperChar)(CAPL_INT8 c);
    
	/*!
    Transforms a character or string to upper case. Only characters a-z and A-Z are supported
	\param source String to be transformed
	\param dest Destination buffer for the transformed string
	\param bufferSize Size of the destination buffer
	\note void toUpper(char dest[], char source[], dword bufferSize);
	*/
	CAPL_FUNC(CAPL_UINT32, toUpperInternal)(CAPL_INT8 dest[], CAPL_INT8 source[], CAPL_UINT32 bufferSize, CAPL_UINT32 sourceSize);
    template<class T>
    CAPL_UINT32 toUpper(CAPL_INT8 dest[], T source, CAPL_UINT32 bufferSize)
    {
        return toUpperInternal(dest, source, bufferSize, sizeof(source));
    }

    /*!
    These functions interpret the actual bytes of a value as if the value was of another data type
	\param x The number which shall be interpreted
	\return The interpreted value
	\note dword interpretAsDword(float x);
	*/
	CAPL_FUNC(CAPL_UINT32, interpretAsDword)(float x);
    
	/*!
    These functions interpret the actual bytes of a value as if the value was of another data type
	\param x The number which shall be interpreted
	\return The interpreted value
	\note float interpretAsFloat(dword x);
	*/
	CAPL_FUNC(float, interpretAsFloat)(CAPL_UINT32 x);
    
	/*!
    These functions interpret the actual bytes of a value as if the value was of another data type
	\param x The number which shall be interpreted
	\return The interpreted value
	\note qword interpretAsQword(double x);
	*/
	CAPL_FUNC(CAPL_UINT64, interpretAsQword)(double x);
    
	/*!
    These functions interpret the actual bytes of a value as if the value was of another data type
	\param x The number which shall be interpreted
	\return The interpreted value
	\note double interpretAsDouble(qword x);
	*/
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
    
	/*!
    Returns the state of the Replay Block with the name pName
	\param pName Name of the Replay Block
	\return 0: Replay Block is stopped (state: stopped)
	\return 1: Execution of the Replay file was started (state: running)
	\return 2: Execution of the Replay file was stopped (state: suspended)
	\return (dword)-1: when the Replay Block does not exist
	\note dword ReplayState (char pName[])
	*/
	CAPL_FUNC(DWORD, ReplayState)(const char* pName);

    // Starts the Replay block with the name pName.
    // Parameter:
    //    pName, Name of the Replay block
    // Return:
    //    1: If successful
    //    0: If the Replay block does not exist or cannot be restarted
    
	/*!
    Starts the Replay Block with the name pName
	\param pName Name of the Replay Block
	\return 1: If successful
	\return 0: If the Replay Block does not exist or cannot be restarted
	\note dword ReplayStart (char pName[])
	*/
	CAPL_FUNC(DWORD, ReplayStart)(const char* pName);

    // Stops the Replay block with the name pName.                
    // Parameter:
    //    pName, Name of the Replay block.                                                                                               

    // Return:
    //    1: If successful                                                                                                      

    //    0: If the Replay block does not exist or cannot be restarted
    
	/*!
    Stops the Replay Block with the name pName
	\param pName Name of the Replay Block
	\return 1: If successful
	\return 0: If the Replay Block does not exist or cannot be restarted
	\note dword ReplayStop (char pName[])
	*/
	CAPL_FUNC(DWORD, ReplayStop)(const char* pName);

    // Suspends the Replay block with the name pName.
    // Parameter:
    //    pName, Name of the Replay block
    // Return:
    //    1: If successful
    //    0: If the Replay block does not exist or cannot be restarted
    
	/*!
    Suspends the Replay Block with the name pName
	\param pName Name of the Replay Block
	\return 1: If successful
	\return 0: If the Replay Block does not exist or cannot be restarted
	\note dword ReplaySuspend (char pName[])
	*/
	CAPL_FUNC(DWORD, ReplaySuspend)(const char* pName);

    // Starts the Replay block with the name pName after it is suspended by ReplaySuspend.
    // Parameter:
    //   pName, Name of the Replay block.
    // Return:
    //   1: If successful
    //   0: If the Replay block does not exist or cannot be restarted
    
	/*!
    Starts the Replay Block with the name pName after it is suspended by ReplaySuspend
	\param pName Name of the Replay Block
	\return 1: If successful
	\return 0: If the Replay Block does not exist or cannot be restarted 
	\note dword ReplayResume (char pName[]) 
	*/
	CAPL_FUNC(DWORD, ReplayResume)(const char* pName);

    //////////////////////////////////
    // Interaction Layer (TrsCtrl2) //
    //////////////////////////////////

    // Initialisiert die IL.
    
	/*!
    Initialization of CANoe IL. This function may only be used in on preStart to prevent the IL autostart function
	\return 0: No error
	\return -1: Momentary state of the IL does not permit this query
	\return	-50: Node layer is inactive — possibly deactivated in the node’s configuration dialog
	\note long ILControlInit ()
	*/
	CAPL_FUNC(CAPL_INT32, ILControlInitInternal)(LPCSTR sEcuRef);
    
	/*!
	Cyclical sending starts; setting signals is now possible. Signal changes that occurred while the interaction layer was switched off (by ILControlStop or ILControlSimulationOff) are not considered on its activation
	\return 0: No error
	\return -1: Momentary state of the IL does not permit this query
	\return	-50: Node layer is inactive — possibly deactivated in the node’s configuration dialog
	\note long ILControlStart ()
	*/
	CAPL_FUNC(CAPL_INT32, ILControlStartInternal)(LPCSTR sEcuRef);
    // Signaländerungen werden verworfen
    
	/*!
	Sending is completely stopped. In this state the interaction layer is inoperative. Neither function calls, nor signal changes are considered during its inactivity and on its activation (by ILControlStart)
	\return 0: No error
	\return -1: Momentary state of the IL does not permit this query
	\return	-50: Node layer is inactive — possibly deactivated in the node’s configuration dialog
	\note long ILControlStop()
	*/
	CAPL_FUNC(CAPL_INT32, ILControlStopInternal)(LPCSTR sEcuRef);
    // Signaländerungen werden gemerkt
    
	/*!
	Cyclical sending is stopped; setting signals is possible. Cyclical sending is continued only after an ILControlResume
	\return 0: No error
	\return -1: Momentary state of the IL does not permit this query
	\return	-50: Node layer is inactive — possibly deactivated in the node’s configuration dialog
	\note long ILControlWait()
	*/
	CAPL_FUNC(CAPL_INT32, ILControlWaitInternal)(LPCSTR sEcuRef);
    // Kl15 on
   
	/*!
    Forwards the appropriate state (active/deactive) of clamp 15 to the NM simulation (if present) 
	\return 0: No error
	\return -1: The IL is not initialized properly. The function is ignored
	\note long ILActivateClamp15();
	*/
    CAPL_FUNC(CAPL_INT32, ILActivateClamp15Internal)(LPCSTR sEcuRef);
    // Kl15 off
   
	/*!
    Forwards the appropriate state (active/deactive) of clamp 15 to the NM simulation (if present) 
	\return 0: No error
	\return -1: The IL is not initialized properly. The function is ignored
	\note long ILDeactivateClamp15();
	*/
    CAPL_FUNC(CAPL_INT32, ILDeactivateClamp15Internal)(LPCSTR sEcuRef);

	/*!
    Start the simulation of the IL. The IL is in the same state as it was before stopping it by the function ILControlSimulationOff
	\return 0: No error
	\return -1: Momentary state of the IL does not permit this query
	\note long ILControlSimulationOn() 
	*/
    CAPL_FUNC(CAPL_INT32, ILControlSimulationOnInternal)(LPCSTR sEcuRef);
    
	/*!
    The simulation of the IL is stopped. After that no other function to control the IL has an effect to the IL. To restart the simulation of the IL use ILControlSimulationOn
	\return 0: No error
	\note long ILControlSimulationOff()
	*/
	CAPL_FUNC(CAPL_INT32, ILControlSimulationOffInternal)(LPCSTR sEcuRef);
    
	/*!
    Sends the transferred message directly to the bus if the network is active. The send model is ignored
	\param msg Message that should be sent
	\return 0: No error
	\return -1: Momentary state of the IL does not permit this query
	\return -50: Nodelayer is inactive — possibly deactivated in the node’s configuration dialog
	\return -100: Signal or message was not found in the database or is not mapped to this node
	\return -104: General error for invalid calls
	\note long ILSetMsgEvent (dbMessage msg) 
	*/
	CAPL_FUNC(CAPL_INT32, ILSetMsgEventInternal)(LPCSTR sMsgRef);
    
	/*!
    Enables the sending of the message
	\param msg Message that should be re-enabled after having disabled it with ILFaultInjectionDisableMsg
	\return 0: No error
	\return -1: Momentary state of the IL does not permit this function
	\return -1: Momentary state of the IL does not permit this function
	\return -100: Signal or message was not found in the database or is not mapped to this node
	\note long ILFaultInjectionEnableMsg (dbMessage msg)
	*/
	CAPL_FUNC(CAPL_INT32, ILFaultInjectionEnableMsg)(LPCSTR sMsgRef);
    
	/*!
    Disables the sending of the message except by calling the function ILSetMsgEvent
	\param msg Message that should be disabled
	\return 0: No error
	\return -1: Momentary state of the IL does not permit this function
	\return -50: Nodelayer is inactive — possibly deactivated in the node’s configuration dialog
	\return -100: Signal or message was not found in the database or is not mapped to this node
	\note long ILFaultInjectionDisableMsg (dbMessage msg)
	*/
	CAPL_FUNC(CAPL_INT32, ILFaultInjectionDisableMsg)(LPCSTR sMsgRef);
    
	CAPL_FUNC(CAPL_INT32, ILSetPDUTimingCyclic)(LPCSTR sMsgRef, CAPL_INT32 timing, CAPL_INT32 offset, CAPL_INT32 period, CAPL_INT32 disturbanceCount, CAPL_INT32 flags);
    CAPL_FUNC(CAPL_INT32, ILResetPDUTimingCyclic)(LPCSTR sMsgRef, CAPL_INT32 timing);
    CAPL_FUNC(CAPL_INT32, ILResetPDUTimingEvent)(LPCSTR sMsgRef, CAPL_INT32 timing);
    CAPL_FUNC(CAPL_INT32, ILSetPDUAsrTXMode)(LPCSTR sMsgRef, CAPL_INT32 mode, CAPL_INT32 disturbanceCount, CAPL_INT32 flags);
    CAPL_FUNC(CAPL_INT32, ILSetPDUTimingEvent)(LPCSTR sMsgRef, CAPL_INT32 timing, CAPL_INT32 enabled,
                                                CAPL_INT32 repetitionCount, CAPL_INT32 repetitionPeriod, 
                                                CAPL_INT32 debounceDelay, CAPL_INT32 disturbanceCount, CAPL_INT32 flags);
    
    CAPL_FUNC(CAPL_INT32, ILSetPDUEvent)(LPCSTR sMsgRef);


    /////////////////////////////
    // Diagnose-Schnittstellen //
    /////////////////////////////

    CAPL_FUNC(CAPL_INT32, diagInitialize)(capl::IDiagBase& request);
	
	/*!
    Reinitializes the object for the given service or primitive. Diagnostics request will be initialized with the default request parameters of the service, while diagnostic responses will be initialized with the default parameters of the first or specified primitive of the service. If the service is not defined, or the primitive is not defined at the given service, nothing happens and an error code is returned
	\param object Diagnostics object to re-initialize
	\param serviceQualifier Qualifier of the service that should be used for reinterpretation
	\return 0: No error, OK
	\return <0: Error code
 	\note long diagInitialize( diagRequest object, char serviceQualifier[]);
	*/
    CAPL_FUNC(CAPL_INT32, diagInitializeQualivier)(capl::IDiagBase& request, char serviceQualifier[]);
    
	/*!
    Reinitializes the object for the given service or primitive. Diagnostics request will be initialized with the default request parameters of the service, while diagnostic responses will be initialized with the default parameters of the first or specified primitive of the service. If the service is not defined, or the primitive is not defined at the given service, nothing happens and an error code is returned
	\param object Diagnostics object to re-initialize
	\param serviceQualifier Qualifier of the service that should be used for reinterpretation
	\param primitiveQualifier Qualifier of the service primitive that should be used for reinterpretation
	\return 0: No error, OK
	\return <0: Error code
	\note long diagInitialize( diagReponse object, char serviceQualifier[], char primitiveQualifier[]);
	*/
	CAPL_FUNC(CAPL_INT32, diagInitializePrimitive)(capl::IDiagResponse& request, char serviceQualifier[], char primitiveQualifier[]);

	/*!
    Sets the numeric parameter to the specified value
	\param parameterName Parameter qualifier (NOT the language-dependent name of the parameter!)
	\param obj Diagnostics object
	\param newValue Numeric value to which the parameter should be set
	\return Error code 
	\note long diagSetParameter (diagResponse obj, char parameterName[], double newValue)
	*/
    CAPL_FUNC(CAPL_INT32, diagSetParameterDouble)(capl::IDiagBase&, char parameterName[], double newValue);
    
	/*!
    Sets the numeric parameter to the specified value
	\param mode Access 
	\param obj Diagnostics object
	\param parameterName Parameter qualifier (NOT the language-dependent name of the parameter!)
	\param newValue Numeric value to which the parameter should be set
	\return Error code
	\note long diagSetParameter (diagResponse obj, long mode, char parameterName[], double newValue)
	*/
	CAPL_FUNC(CAPL_INT32, diagSetParameterDoubleMode)(capl::IDiagBase&, CAPL_INT32 mode, char parameterName[], double newValue);
    
	/*!
    Sets a parameter to the symbolically-specified value. This is possible for all parameters, also numeric ones
	\param parameterName Parameter qualifier
	\param newValue Symbolic value
	\param obj Diagnostics object
	\return Error code
	\note long diagSetParameter (diagResponse obj, char parameterName[], char newValue[])
	*/
	CAPL_FUNC(CAPL_INT32, diagSetParameterString)(capl::IDiagBase&, char parameterName[], char newValue[]);

	/*!
    Returns the value of the numeric parameter, either in an output field or as return value (without the possibility of checking the correct function)
	\param parameterName Parameter qualifier
	\param output Output field 
	\param obj Diagnostics object
	\return Error code or value of the parameter or 0.0 if this could not be acquired
	\note long diagGetParameter (diagResponse obj, char parameterName[], double output[1])
	*/
	CAPL_FUNC(CAPL_INT32, diagGetParameterDoubleParam)(capl::IDiagBase&, char parameterName[], double output[1]);
    
	/*!
    Returns the value of the numeric parameter, either in an output field or as return value (without the possibility of checking the correct function)
	\param parameterName Parameter qualifier
	\return Error code or value of the parameter or 0.0 if this could not be acquired
	\note double diagGetParameter (diagResponse obj, char parameterName[])
	*/
	CAPL_FUNC(double, diagGetParameterDouble)(capl::IDiagBase&, char parameterName[]);
   
	/*! 
    Returns the value of the numeric parameter, either in an output field or as return value (without the possibility of checking the correct function)
	\param parameterName Parameter qualifier
	\param obj Diagnostics object
	\param Output Output field 
	\param mode Access mode
	\return Error code or value of the parameter or 0.0 if this could not be acquired	\note long diagGetParameter (diagResponse obj, long mode, char parameterName[], double output[1])
	\note long diagGetParameter (diagResponse obj, long mode, char parameterName[], double output[1])
	*/
    CAPL_FUNC(CAPL_INT32, diagGetParameterDoubleParamMode)(capl::IDiagBase&, CAPL_INT32 mode, char parameterName[], double output[1]);
    
	/*! 
    Returns the value of the numeric parameter, either in an output field or as return value (without the possibility of checking the correct function)
	\param parameterName Parameter qualifier
	\param obj Diagnostics object 
	\param mode Access mode
	\return Error code or value of the parameter or 0.0 if this could not be acquired	\note long diagGetParameter (diagResponse obj, long mode, char parameterName[], double output[1])
	\note double diagGetParameter (diagResponse obj, long mode, char parameterName[])
	*/	
	CAPL_FUNC(double, diagGetParameterDoubleMode)(capl::IDiagBase&, CAPL_INT32 mode, char parameterName[]);
    
	/*! 
    Returns the value of the numeric parameter, either in an output field or as return value (without the possibility of checking the correct function)
	\param parameterName Parameter qualifier
	\param buffer Output field
	\param buffersize Size of the buffer
	\return Number of chars written to buffer or error code
	\note long diagGetParameter (diagResponse obj, char parameterName[], char buffer[], dword buffersize)
	*/	
	CAPL_FUNC(CAPL_INT32, diagGetParameterString)(capl::IDiagBase&, char parameterName[], char buffer[], CAPL_UINT32 buffersize);

	/*!
    Sends the request object to the ECU
	\param obj Diagnostics object
	\return Error code 
	\note long diagSendRequest (diagRequest obj)
	*/
    CAPL_FUNC(CAPL_INT32, diagSendRequest)(capl::IDiagRequest&);

	/*!
    Sends the response object back to the tester. Can only be called in the ECU simulation
	\param obj Diagnostics object
	\return Error code 
	\note long diagSendResponse (diagResponse obj)
	*/
    CAPL_FUNC(CAPL_INT32, diagSendResponse)(capl::IDiagResponse&);

	/*!
    Reads the raw data of the complete service primitive (all data that is transmitted via the transport protocol). When setting the data the length of the primitive is not changed
	\param objxt Diagnostics object
	\param buffer Input/output buffer
	\param buffersize Buffer size
	\return Number of bytes copied into the buffer or error code
	\note long diagGetPrimitiveData (diagResponse obj, byte* buffer, DWORD buffersize)
	*/
    CAPL_FUNC(CAPL_INT32, diagGetPrimitiveData)(capl::IDiagBase&, BYTE buffer[], CAPL_UINT32 buffersize);
    
	/*!
    Sets the raw data of the complete service primitive (all data that is transmitted via the transport protocol). When setting the data the length of the primitive is not changed
	\param objxt Diagnostics object
	\param buffer Input/output buffer
	\param buffersize Buffer size
	\return Number of bytes copied into the buffer or error code
	\note long diagSetPrimitiveData (diagResponse obj, byte* buffer, DWORD buffersize)
	*/
	CAPL_FUNC(CAPL_INT32, diagSetPrimitiveData)(capl::IDiagBase&, BYTE buffer[], CAPL_UINT32 buffersize);

    
	/*!
    Returns the byte length of the object
	\param request Request
	\param response Response
	\return >0: Number of bytes
	\return <0: Error code 
	\note long DiagGetPrimitiveSize( diagRequest request);
	*/
	CAPL_FUNC(CAPL_INT32, diagGetPrimitiveSize)(capl::IDiagBase&);

	/*!
    Returns value <> 0 if the object is a negative response to a request
	\param obj Diagnostics object
	\return 0 or <>0
	\note long diagIsNegativeResponse (diagResponse obj)
	*/
    CAPL_FUNC(CAPL_INT32, diagIsNegativeResponse)(capl::IDiagResponse&);
    
	/*!
    Returns value <> 0 if the object is a positive response to a request
	\param obj Diagnostics object
	\return 0 or <>0 
	\note long diagIsPositiveResponse (diagResponse obj)
	*/
	CAPL_FUNC(CAPL_INT32, diagIsPositiveResponse)(capl::IDiagResponse&);

   	/*!
    Returns the code of the specified response or last received response (for the specified request)
	\param resp Response 
	\return -1: The response was positive, i.e. there is no error code.
	\return 0: No response has been received yet.
	\return >0: Error code of the negative response.
	\return Error code 
	\note long diagGetResponseCode (diagResponse resp);
	*/
	CAPL_FUNC(CAPL_INT32, diagGetResponseCode)(capl::IDiagResponse&);
    
	/*!
    Returns the code of the specified response or last received response (for the specified request)
	\param req Request 
	\return -1: The response was positive, i.e. there is no error code.
	\return 0: No response has been received yet.
	\return >0: Error code of the negative response.
	\return Error code 
	\note long diagGetLastResponseCode (diagRequest req);
	*/
	CAPL_FUNC(CAPL_INT32, diagGetLastResponseCode)(capl::IDiagRequest&);

    CAPL_FUNC(CAPL_INT32, diagGetLastResponse)(capl::IDiagResponse&);

	/*!
    Sets or specifies the value of a (complex) parameter directly via uncoded data bytes
	\param Sets or specifies the value of a (complex) parameter directly via uncoded data bytes
	\param parameterName Parameter qualifier
	\param buffer Input/output buffer
	\param buffersize Buffer size
	\return 0 if bytes were copied, otherwise <0 for an error code
	\note long diagGetParameterRaw (diagResponse obj, char parameterName[], byte* buffer, DWORD buffersize)
	*/
    CAPL_FUNC(CAPL_INT32, diagGetParameterRaw)(capl::IDiagBase&, char parameter[], BYTE buffer[], CAPL_UINT32 buffersize);
   
	/*!
    Sets or specifies the value of a (complex) parameter directly via uncoded data bytes
	\param Sets or specifies the value of a (complex) parameter directly via uncoded data bytes
	\param parameterName Parameter qualifier
	\param buffer Input/output buffer
	\param buffersize Buffer size
	\return 0 if bytes were copied, otherwise <0 for an error code
	\note long diagSetParameterRaw (diagResponse obj, char parameter[], byte* buffer, DWORD buffersize)
	*/
    CAPL_FUNC(CAPL_INT32, diagSetParameterRaw)(capl::IDiagBase&, char parameter[], BYTE buffer[], CAPL_UINT32 buffersize);

	/*!
    Waits for the arrival of the response to the given request
	\param request Sent request
	\param timeout [ms] Maximum wait time
	\return <0: An internal error occurred, e.g. a protocol error or a faulty configuration of the diagnostic layer
	\return 0: The timeout was reached, i.e. the event of interest did not occur within the specified time
	\return 1: The event occurred
	\note long TestWaitForDiagResponse (diagRequest request, dword timeout);
	*/
    CAPL_FUNC(CAPL_INT32, testWaitForDiagResponse)(capl::IDiagRequest&, CAPL_UINT32 timeout);

    typedef void (OnDiagTiemoutFunction)();
    CAPL_FUNC(CAPL_INT32, diagSetTimeoutHandlerInternal)(OnDiagTiemoutFunction* pCallback, char stackName[]);
   	CAPL_FUNC(CAPL_INT32, diagSetTimeoutHandlerObjInternal)(OnDiagTiemoutFunction* pCallback, capl::IDiagRequest&);

	/*!
    Starts sending autonomous/cyclical Tester Present requests from CANoe to the specified or current ECU
	\param ecuQualifier Qualifier of the ECU as specified in the diagnostic configuration dialog, for which the Tester Present service shall be started
	\return Error code
	\note long diagStartTesterPresent(char ecuQualifier[])
	*/
    CAPL_FUNC(CAPL_INT32, diagStartTesterPresentObj)(char ecuQualifier[]);
   
    CAPL_FUNC(CAPL_INT32, diagTestStack)(char stackvar[], char stackattr[]);

    /////////////////////////////
    //   Test-Schnittstellen   //
    /////////////////////////////


	/*!
    With these functions, test steps can be reported within a test case. TestStep reports a test step without influence on the result. TestStepPass reports a test step that was executed as expected. This is displayed accordingly in the test report
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param Description 
	\note TestStep (dword LevelOfDetail, char Identifier[], char Description[], ...)
	*/
    CAPL_FUNC(CAPL_INT32, TestStepLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 1
	
	
	/*!
    With these functions, test steps can be reported within a test case. TestStep reports a test step without influence on the result. TestStepPass reports a test step that was executed as expected. This is displayed accordingly in the test report
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param handle Handle to the table to be displayed within the step content, created by the function TestInfoTable
	\note TestStep (dword LevelOfDetail, char[] Identifier, long handle)
	*/
    CAPL_FUNC(CAPL_INT32, TestStepHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 2
	
	/*!
    With these functions, test steps can be reported within a test case. TestStep reports a test step without influence on the result. TestStepPass reports a test step that was executed as expected. This is displayed accordingly in the test report
	\param Identifier Test step number
	\param Description 
	\note TestStep (char Identifier[], char Description[], ...)
	*/
    CAPL_FUNC(CAPL_INT32, TestStep)(const char Identifier[], const char Description[], ...); // form 3
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepPass reports a test step that was executed as expected. This is displayed accordingly in the test report
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param Description 
	\note TestStepPass (dword LevelOfDetail, char Identifier[], char Description[], ...) 
	*/
	CAPL_FUNC(CAPL_INT32, TestStepPassLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 4
	
	/*!
    With these functions, test steps can be reported within a test case. TestStepPass reports a test step that was executed as expected. This is displayed accordingly in the test report
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param handle Handle to the table to be displayed within the step content, created by the function TestInfoTable
	\note TestStepPass (dword LevelOfDetail, char[] Identifier, long handle)
	*/
    CAPL_FUNC(CAPL_INT32, TestStepPassHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 5
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepPass reports a test step that was executed as expected. This is displayed accordingly in the test report
	\param Identifier Test step number
	\param Description 
	\note TestStepPass (char Identifier[], char Description[], ...)
	*/
	CAPL_FUNC(CAPL_INT32, TestStepPassIdentifier)(const char Identifier[], const char Description[], ...); // form 6
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepPass reports a test step that was executed as expected. This is displayed accordingly in the test report
	\param Description 
	\note TestStepPass (char Description[])
	*/
	CAPL_FUNC(CAPL_INT32, TestStepPassDescription)(const char Description[]); // form 7
    
	CAPL_FUNC(CAPL_INT32, TestStepPass)(); // form 8
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepFail describes a test step that causes an error. Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to fail
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param Description 
	\note TestStepFail (dword LevelOfDetail, char Identifier[], char Description[], ...)
	*/
	CAPL_FUNC(CAPL_INT32, TestStepFailLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 9
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepFail describes a test step that causes an error. Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to fail
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param handle Handle to the table to be displayed within the step content, created by the function TestInfoTable
	\note TestStepFail (dword LevelOfDetail, char[] Identifier, long handle)
	*/
	CAPL_FUNC(CAPL_INT32, TestStepFailHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // (10)
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepFail describes a test step that causes an error. Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to fail
	\param Identifier Test step number
	\param Description 
	\note TestStepFail (char Identifier[], char Description[], ...)
	*/
	CAPL_FUNC(CAPL_INT32, TestStepFailIdentifier)(const char Identifier[], const char Description[], ...); // form 11
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepFail describes a test step that causes an error. Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to fail
	\param Description 
	\note TestStepFail (char Description[])
	*/
	CAPL_FUNC(CAPL_INT32, TestStepFailDescription)(const char Description[]); // form 12
    
	CAPL_FUNC(CAPL_INT32, TestStepFail)(); // form 13
	
	/*!
    With these functions, test steps can be reported within a test case. TestStepWarning describes a test case that was executed without errors but whose result could contribute to a problem later on. This is represented appropriately in the test report
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param Description 
	\note TestStepWarning (dword LevelOfDetail, char Identifier[], char Description[], ...) 
	*/
    CAPL_FUNC(CAPL_INT32, TestStepWarningLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 14
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepWarning describes a test case that was executed without errors but whose result could contribute to a problem later on. This is represented appropriately in the test report
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param handle Handle to the table to be displayed within the step content, created by the function TestInfoTable
	\note TestStepWarning (dword LevelOfDetail, char[] Identifier, long handle)
	*/
	CAPL_FUNC(CAPL_INT32, TestStepWarningHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 15
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepWarning describes a test case that was executed without errors but whose result could contribute to a problem later on. This is represented appropriately in the test report
	\param Identifier Test step number
	\param Description 
	\note TestStepWarning (char Identifier[], char Description[], ...)
	*/
	CAPL_FUNC(CAPL_INT32, TestStepWarningIdentifier)(const char Identifier[], const char Description[], ...); // form 16
	
	/*!
    With these functions, test steps can be reported within a test case. TestStepWarning describes a test case that was executed without errors but whose result could contribute to a problem later on. This is represented appropriately in the test report
	\param Description 
	\note TestStepWarning (char Description[])
	*/
    CAPL_FUNC(CAPL_INT32, TestStepWarningDescription)(const char Description[]); // form 17
    
	CAPL_FUNC(CAPL_INT32, TestStepWarning)(); // form 18
	
	/*!
    With these functions, test steps can be reported within a test case. TestStepInconclusive describes a test step which can not clearly marked as passed or failed . Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to inconclusive
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param Description 
	\note TestStepInconclusive (dword LevelOfDetail, char Identifier[], char Description[], ...)
	*/
    CAPL_FUNC(CAPL_INT32, TestStepInconclusiveLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 19
    
	
	/*!
    With these functions, test steps can be reported within a test case. TestStepInconclusive describes a test step which can not clearly marked as passed or failed . Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to inconclusive
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param handle Handle to the table to be displayed within the step content, created by the function TestInfoTable
	\note TestStepInconclusive (dword LevelOfDetail, char[] Identifier, long handle)
	*/
	CAPL_FUNC(CAPL_INT32, TestStepInconclusiveHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 20
	
	/*!
    With these functions, test steps can be reported within a test case. TestStepInconclusive describes a test step which can not clearly marked as passed or failed . Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to inconclusive
	\param Identifier Test step number
	\param Discription 
	\note TestStepInconclusive (char Identifier[], char Description[], ...)
	*/
    CAPL_FUNC(CAPL_INT32, TestStepInconclusiveIdentifier)(const char Identifier[], const char Description[], ...); // form 21
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepInconclusive describes a test step which can not clearly marked as passed or failed . Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to inconclusive
	\param Discription 
	\note TestStepInconclusive (char Description[]) 
	*/
	CAPL_FUNC(CAPL_INT32, TestStepInconclusiveDescription)(const char Description[]); // form 22
	
    CAPL_FUNC(CAPL_INT32, TestStepInconclusive)(); // form 23
	
	/*!
    With these functions, test steps can be reported within a test case. TestStepErrorInTestSystem describes a test step that causes an error in test system. Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to error in test system
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param Description 
	\note TestStepErrorInTestSystem (dword LevelOfDetail, char Identifier[], char Description[], ...)
	*/
    CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystemLOD)(CAPL_UINT32 LevelOfDetail, const char Identifier[], const char Description[], ...); // form 24
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepErrorInTestSystem describes a test step that causes an error in test system. Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to error in test system
	\param LevelOfDetail It is possible to identify with a number how important this test step is. In the test report, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance. Without an explicit specification, a LevelOfDetail of 0 is assumed
	\param Identifier Test step number
	\param handle Handle to the table to be displayed within the step content, created by the function TestInfoTable
	\note TestStepErrorInTestSystem (dword LevelOfDetail, char[] Identifier, long handle)
	*/
	CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystemHandle)(CAPL_UINT32 LevelOfDetail, const char Identifier[], long handle); // form 25
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepErrorInTestSystem describes a test step that causes an error in test system. Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to error in test system
	\param Identifier Test step number
	\param Discription 
	\note TestStepErrorInTestSystem (char Identifier[], char Description[], ...) 
	*/
	CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystemIdentifier)(const char Identifier[], const char Description[], ...); // form 26
    
	/*!
    With these functions, test steps can be reported within a test case. TestStepErrorInTestSystem describes a test step that causes an error in test system. Also this is displayed accordingly in the test report. The verdict of the test case is hereby set automatically to error in test system
	\param Discription 
	\note TestStepErrorInTestSystem (char Description[]) 
	*/
	CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystemDescription)(const char Description[]); // form 27
    
	CAPL_FUNC(CAPL_INT32, TestStepErrorInTestSystem)(); // form 28

    /*!
    Waits until the expiration of the specified timeout time
	\param aTimeout Maximum time that should be waited [ms]. (Transmission of 0: no timeout controlling. In this function this results in a hang up of the test module)
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available
	\return 0: Resume due to timeout
	\note long TestWaitForTimeout(dword aTimeout)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForTimeout)(CAPL_UINT32 aTimeout);
	
	/*!
    Creates a popup window that presents the given string to the tester. The tester can acknowledge the window with Yes or No. Wait function has no timeout so it waits for the confirmation of the tester
	\param text This is shown in the popup window. The maximum length of the string is limited (4096 characters)
	\return 0: Timeout occurred
	\return 1: The tester clicked Yes
	\return 2: The tester clicked No
	\return	-1: General error, e.g. due to a call outside of a test sequence
	\return -2: Constraint occurred
	\note long TestWaitForTesterConfirmation(char text[])
	*/
    CAPL_FUNC(CAPL_INT32, TestWaitForTesterConfirmation)(const char text[]);
    
	/*!
     Creates a popup window that presents the given string to the tester. The tester can acknowledge the window with Yes or No. Wait function has a timeout, i.e. the dialog is automatically terminated after the timeout expires
	\param timeout Time in milliseconds after which the dialog is automatically ended
	\param text This is shown in the popup window. The maximum length of the string is limited (4096 characters)
	\return 0: Timeout occurred
	\return 1: The tester clicked Yes
	\return 2: The tester clicked No
	\return	-1: General error, e.g. due to a call outside of a test sequence
	\return -2: Constraint occurred 
	\note long TestWaitForTesterConfirmation(char[] text, unsigned long timeout);
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForTesterConfirmationTimeout)(const char text[], CAPL_UINT32 timeout);
    
	/*!
     Creates a popup window that presents the given string to the tester. The tester can acknowledge the window with Yes or No. Wait function has a timeout and can show a resource, i.e. the dialog can show additional information
	\param timeout Time in milliseconds after which the dialog is automatically ended
	\param text This is shown in the popup window. The maximum length of the string is limited (4096 characters)
	\param heading A heading above the dialog text. The maximum length of the string is limited (256 characters)
	\param resource A URL or file path 
	\param resourceCaption A caption respectively a description for the resource. The maximum length of the string is limited (256 characters)
	\return 0: Timeout occurred
	\return 1: The tester clicked Yes
	\return 2: The tester clicked No
	\return	-1: General error, e.g. due to a call outside of a test sequence
	\return -2: Constraint occurred 
	\note long TestWaitForTesterConfirmation(char[] text, unsigned long timeout, char[] heading, char[] resource, char[] resourceCaption)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForTesterConfirmationHeading)(const char text[], CAPL_UINT32 timeout, const char heading[], const char resource[], const char resourceCaption[]);

  
	/*!
    With these functions, test steps can be logged within a test case. Such a test step is introduced with TestStepBegin, which is then completed with TestStepPass, TestStepFail or TestStepWarning. A test step is noted in the test report only with this conclusion
	\param Importance It is possible to identify with a number how important this test step is, it is possible that only test steps up to a certain importance will be displayed. 0 means "very important", higher numbers indicate lower degrees of importance
	\param Identifier A test step number
	\param Description 
	\note TestStepBegin (dword Importance, char Identifier[], char Description[]) 
	*/
    CAPL_FUNC(CAPL_INT32, TestStepBeginImportance)(CAPL_UINT32 Importance, const char Identifier[], const char Description[]);
    
	/*!
    With these functions, test steps can be logged within a test case. Such a test step is introduced with TestStepBegin, which is then completed with TestStepPass, TestStepFail or TestStepWarning. A test step is noted in the test report only with this conclusion
	\param Identifier A test step number
	\param Description 
	\note TestStepBegin (char Identifier[], char Description[])
	*/
	CAPL_FUNC(CAPL_INT32, TestStepBegin)(const char Identifier[], const char Description[]);

	/*!
    Checks the given value against the value of the signal, the system variable or the environment variable. The resolution of the signal is considered
	\param aSignal Signal to be queried
	\param aCompareValue Value which is compared to the signal value
	\param aTimeout Maximum time that should be waited [ms]
	\return -1: General error
	\return -2: Signal is not valid
	\return 0: Wait state is exited due to a timeout
	\return	1: Wait state is exited due to condition fulfillment
	\note long TestWaitForSignalMatch (Signal aSignal, float aCompareValue, dword aTimeout); 
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForSignalMatchSig)(const ISignalVal& aSignal, float aCompareValue, CAPL_UINT32 aTimeout); // form 1
    
	/*!
    Checks the given value against the value of the signal, the system variable or the environment variable. The resolution of the signal is considered
	\param aEnvVar Environment variable to be queried
	\param aCompareValue Value which is compared to the signal value
	\param aTimeout Maximum time that should be waited [ms]
	\return -1: General error
	\return -2: Signal is not valid
	\return 0: Wait state is exited due to a timeout
	\return	1: Wait state is exited due to condition fulfillment
	\note long TestWaitForSignalMatch (dbEnvVar aEnvVar, float aCompareValue, dword aTimeout); 
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForSignalMatchEnv)(const IEnvVar& aEnvVar, float aCompareValue, CAPL_UINT32 aTimeout); // form 2
    
	CAPL_FUNC(CAPL_INT32, TestWaitForSignalMatchEnvI64)(const IEnvVar& aEnvVar, CAPL_INT64 aCompareValue, CAPL_UINT32 aTimeout); // form 4

    /*!
    Checks the value of the signal, the system or the environment variable against the condition: aLowLimit <= Value <= aHighLimit. If this condition is already met when this function is called, it returns immediately without waiting. The test step is evaluated as either passed or failed depending on the results
	\param aSignal Signal to be queried
	\param aLowLimit Lower limit of the value
	\param aHighLimit Upper limit of the value
	\param aTimeout Maximum time that should be waited [ms]
	\return -1: General error
	\return -2: Type of the system / environment variable is not valid – only float or integer are valid — or signal is not valid or invalid limits of the given range
	\return 0: Wait state is exited due to a timeout
	\return	1: Wait state is exited due to condition fulfilment
	\note long TestWaitForSignalInRange (Signal aSignal, float aLowLimit, float aHighLimit, dword aTimeout); 
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForSignalInRangeSig)(const ISignalVal& aSignal, float aLowLimit, float aHighLimit, CAPL_UINT32 aTimeout); // form 1

	/*!
    Checks the value of the signal, the system or the environment variable against the condition: aLowLimit <= Value <= aHighLimit. If this condition is already met when this function is called, it returns immediately without waiting. The test step is evaluated as either passed or failed depending on the results
	\param aEnvVar Environment variable to be queried
	\param aLowLimit Lower limit of the value
	\param aHighLimit Upper limit of the value
	\param aTimeout Maximum time that should be waited [ms]
	\return -1: General error
	\return -2: Type of the system / environment variable is not valid – only float or integer are valid — or signal is not valid or invalid limits of the given range
	\return 0: Wait state is exited due to a timeout
	\return	1: Wait state is exited due to condition fulfilment 
	\note long TestWaitForSignalInRange (dbEnvVar aEnvVar float aLowLimit, float aHighLimit, dword aTimeout);
	*/
    CAPL_FUNC(CAPL_INT32, TestWaitForSignalInRangeEnv)(const IEnvVar& aEnvVar, float aLowLimit, float aHighLimit, CAPL_UINT32 aTimeout); // form 2

    CAPL_FUNC(CAPL_INT32, TestWaitForSignalInRangeEnvI64)(const IEnvVar& aEnvVar, CAPL_INT64 aLowLimit, CAPL_INT64 aHighLimit, CAPL_UINT32 aTimeout); // form 4

    /*!
    With these functions, information pairs of name and description (e.g. "serial number" and "S012345AB") can be taken up into the report in the areas TestEngineer, TestSetUp, and device (SUT) to be tested. The three areas named must not be created; they are automatically available in the report. In the course of the test execution, any number of information pairs can be written 
	Information pair of name and description
	The format string has the same meaning as with the write function and is described there
	\note TestReportAddEngineerInfo (char name[], char description[], ...)
	*/
	CAPL_FUNC(CAPL_INT32, TestReportAddEngineerInfo)(const char name[], const char description[], ...);
    
	 /*!
    With these functions, information pairs of name and description (e.g. "serial number" and "S012345AB") can be taken up into the report in the areas TestEngineer, TestSetUp, and device (SUT) to be tested. The three areas named must not be created; they are automatically available in the report. In the course of the test execution, any number of information pairs can be written 
	Information pair of name and description
	The format string has the same meaning as with the write function and is described there
	\note TestReportAddSetupInfo (char name[], char description[], ...)
	*/
	CAPL_FUNC(CAPL_INT32, TestReportAddSetupInfo)(const char name[], const char description[], ...);
    
	/*!
    With these functions, information pairs of name and description (e.g. "serial number" and "S012345AB") can be taken up into the report in the areas TestEngineer, TestSetUp, and device (SUT) to be tested. The three areas named must not be created; they are automatically available in the report. In the course of the test execution, any number of information pairs can be written 
	Information pair of name and description
	The format string has the same meaning as with the write function and is described there
	\note TestReportAddSUTInfo (char name[], char description[], ...)
	*/
	CAPL_FUNC(CAPL_INT32, TestReportAddSUTInfo)(const char name[], const char description[], ...);
   
	/*!
	With these functions, information pairs of name and description (e.g. "parameter value V0" and "3.7 V") can be taken up into an additional information area in the report	Information pair of name and description
	The format string has the same meaning as with the write function and is described there
	\note TestReportAddMiscInfo (char name[], char description[], ...)
	*/
    CAPL_FUNC(CAPL_INT32, TestReportAddMiscInfo)(const char name[], const char description[], ...);

    /*!
    The title of the test module is acquired automatically from the name of the test node in the simulation structure. It can also be set explicitly with this function
	\param title Title of the test module
	\note TestModuleTitle (char title[])
	*/
	CAPL_FUNC(CAPL_INT32, TestModuleTitle)(const char title[]);

    /*!
    With this function, a description text for the test module can be written into the report. The function can be called several times in a row, the transmitted texts are then added to one another without additional separation
	\param description Description text for the test module
	\note TestModuleDescription (char description[])
	*/
	CAPL_FUNC(CAPL_INT32, TestModuleDescription)(const char description[]);

    /*!
    Signals the specified event. Consequently resolves a possibly-active wait condition on this event
	\param aText Name of the event to be signaled
	\return 0: Event was signaled successfully
	\return	-1: Event could not be signaled
	\note long TestSupplyTextEvent(char aText[])
	*/
	CAPL_FUNC(CAPL_INT32, TestSupplyTextEvent)(const char aText[]);
    
	/*!
    Waits for the signaling of the specified textual event from the individual test module. A signaling from another test module does not effect this wait instruction
	\param aText Any (meaningful) textual event name
	\param	aTimeout Maximum time that should be waited [ms](Transmission of 0: no timeout controlling)
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available
	\return 0: Resume due to timeout
	\return	1: Resume due to event occurred
	\note long TestWaitForTextEvent(char aText[], dword aTimeout)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForTextEvent)(const char aText[], CAPL_UINT32 aTimeout);

    /*!
    Waits for the description of the specified environment variable (aEnvVar or with the name aEnvVarName). Should the event not occur before the expiration of the time aTimeout, the wait condition is resolved nevertheless
	\param aEnvVar Environment variable that should be awaited
	\param aTimeout Maximum time that should be waited [ms] (Transmission of 0: no timeout controlling)
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available or environment variable with name aEnvVarName cannot be found
	\return 0: Resume due to timeout
	\return 1: Resume due to event occurred
	\note long TestWaitForEnvVar(dbEnvVar aEnvVar, dword aTimeout)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForEnvVar)(const IEnvVar& aEnvVar, DWORD aTimeout);
   
	/*!
     Waits for the description of the specified environment variable (aEnvVar or with the name aEnvVarName). Should the event not occur before the expiration of the time aTimeout, the wait condition is resolved nevertheless
	\param aEnvVarName Name of environment variable that should be awaited
	\param aTimeout Maximum time that should be waited [ms] (Transmission of 0: no timeout controlling)
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available or environment variable with name aEnvVarName cannot be found
	\return 0: Resume due to timeout
	\return 1: Resume due to event occurred
	\note long TestWaitForEnvVar(char aEnvVarName[], dword aTimeout)
	*/
    CAPL_FUNC(CAPL_INT32, TestWaitForEnvVarName)(const char aEnvVarName[], DWORD aTimeout);
    
	/*!
    Waits for the next system variable aSysVar. Should the event not occur before the expiration of the time aTimeout, the wait condition is resolved nevertheless
	\param aSysVar System variable that should be awaited
	\param aTimeout Maximum time that should be waited [ms]
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available or environment variable with name aEnvVarName cannot be found
	\return 0: Resume due to timeout
	\return 1: Resume due to event occurred
	\note long TestWaitForSysVar(sysvar aSysVar, dword aTimeout)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForSysVar)(const IEnvVar& aEnvVar, DWORD aTimeout);
    
	CAPL_FUNC(CAPL_INT32, TestWaitForSysVarName)(const char aEnvVarName[], DWORD aTimeout);
	
	/*!
    Waits for the occurrence of the specified message aMessage. Should the message not occur before the expiration of the time aTimeout, the wait condition is resolved nevertheless
	\param aTimeout Maximum time that should be waited [ms]
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available
	\return 0: Resume due to timeout
	\return 1: Resume due to event occurred
	\note long TestWaitForMessage(dword aTimeout)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForMessage)(const char sRef[], DWORD aTimeout);
    
	/*!
    Waits for the occurrence of the specified message aMessage. Should the message not occur before the expiration of the time aTimeout, the wait condition is resolved nevertheless
	\param aTimeout Maximum time that should be waited [ms]
	\param aMessageId Numeric ID of the message that should be awaited
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available
	\return 0: Resume due to timeout
	\return 1: Resume due to event occurred
	\note long TestWaitForMessage(dword aMessageId, dword aTimeout)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForMessageIdInternal)(const char channelName[], DWORD aMessageId, DWORD aTimeout);
    
	/*!
    Waits for the occurrence of the specified message aMessage. Should the message not occur before the expiration of the time aTimeout, the wait condition is resolved nevertheless
	\param aTimeout Maximum time that should be waited [ms]
	\param aMessage Message that should be awaited
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available
	\return 0: Resume due to timeout
	\return 1: Resume due to event occurred
	\note long TestWaitForMessage(dbMessage aMessage, dword aTimeout)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForMessageAllInternal)(const char channelName[], DWORD aTimeout);

    /*!
    The title of a test case is acquired automatically from the test case name in the CAPL program
	\param title Title of the test case
	\param	identifier Test case number
	\note TestCaseTitle (char identifier[], char title[])
	*/
	CAPL_FUNC(CAPL_INT32, TestCaseTitle)(const char identifier[], const char title[]);
    
	/*!
	With this function, a description test for a test case can be written into the report
	\param discription Description test for the test case
	\note TestCaseDescription (char description[])
	*/
	CAPL_FUNC(CAPL_INT32, TestCaseDescription)(const char desc[]);

    /*!
    With this function within a test case a commentary can be taken over into the report. This comment can relate to a message that can also be output in the report
	\param aComment Commentary to be taken over into the report 
	\note TestCaseComment (char aComment[])
	*/
	CAPL_FUNC(CAPL_INT32, TestCaseComment)(const char aComment[]);
    
	/*!
    With this function within a test case a commentary can be taken over into the report. This comment can relate to a message that can also be output in the report
	\param aMsg CAN-, GMLAN-, LIN-, MOST-, MOST-AMS- or MOST system message to be taken over into the report
	\param aComment Commentary to be taken over into the report 
	\note TestCaseComment (char aComment[], message aMsg)
	*/
	CAPL_FUNC(CAPL_INT32, TestCaseCommentMessage)(const char aComment[], const IMessage& aMsg);
    
	/*!
    With this function within a test case a commentary can be taken over into the report. This comment can relate to a message that can also be output in the report
	\param aComment Commentary to be taken over into the report 
	\param aRawString Here you may enter any ASCII characters. They will be added to the comment in the following way: <Hex value of the given character>(<ASCII display of the given charcter>). In ASCII display special characters will be replaced by '.'
	\note TestCaseComment (char aComment[], char aRawString[])
	*/
	CAPL_FUNC(CAPL_INT32, TestCaseCommentStr)(const char aComment[], const char aRawString[]);

    /*!
    Sets the name and path of a report file
	\param data name Name and path of the report file. Without path specification, the directory of the CANoe configuration file is used. The extension .XML or. .html is added automatically
	\note TestReportFileName (char data name[])
	*/
	CAPL_FUNC(CAPL_INT32, TestReportFileName)(const char name[]);

    /*!
    Completes the current set of "joined events" with the transmitted event. This function does not wait
	\param aMessage Message that should be awaited
	\return -3: Join error
	\return -1: General error, for example, functionality is not available
	\return >0: Number of the joined event
	\note long TestJoinMessageEvent (dbMessage aMessage)
	*/
	CAPL_FUNC(CAPL_INT32, TestJoinMessageEvent)(const char messageStringRef[]);
    
	/*!
    Completes the current set of "joined events" with the transmitted event. This function does not wait
	\param aMessageId Numeric ID of the message for which should be waited
	\return -3: Join error
	\return -1: General error, for example, functionality is not available
	\return >0: Number of the joined event
	\note long TestJoinMessageEvent (dword aMessageId)

	*/
	CAPL_FUNC(CAPL_INT32, TestJoinMessageEventIdInternal)(const char channelName[], DWORD aMessageId);
    
	/*!
    Completes the current set of "joined events" with the transmitted event. This function does not wait
	\param aEnvVar Environment variable that should be awaited
	\return -3: Join error
	\return -1: General error, for example, functionality is not available
	\return	>0: Number of the joined event
	\note long TestJoinEnvVarEvent(dbEnvVar aEnvVar)
	*/
	CAPL_FUNC(CAPL_INT32, TestJoinEnvVarEvent)(const IEnvVar& aEnvVar);
   
    /*!
    Completes the current set of "joined events" with the transmitted event. This function does not wait
	\param aText Textual event that should be awaited
	\return -3: Join error
	\return -1: General error, for example, functionality is not available
	\return	>0: Number of the joined event
	\note long TestJoinTextEvent(char[]aText)
	*/
	CAPL_FUNC(CAPL_INT32, TestJoinTextEvent)(const char aText[]);
    
	/*!
    Waits for the current set of "joined events." Each individual of these events can resolve the wait state
	\param aTimeout Maximum time that should be waited [ms] (Transmission of 0: no timeout controlling)
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available
	\return 0: Resume due to timeout
	\return >0: Resume due to event occurred The return value returns the number of the joined event that triggered the resolution
	\note long TestWaitForAnyJoinedEvent(dword aTimeout)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForAnyJoinedEvent)(DWORD aTimeout);
    
	/*!
    Waits for the current set of "joined events." The wait condition is resolved if all of the joined events were signaled
	\param aTimeout Maximum time that should be waited [ms] (Transmission of 0: no timeout controlling)
	\return -2: Resume due to constraint violation
	\return -1: General error, for example, functionality is not available
	\return 0: Resume due to timeout
	\return >0: Resume due to event occurred The return value returns the number of the joined event that triggered the resolution
	\note long TestWaitForAllJoinedEvents(dword aTimeout)
	*/
	CAPL_FUNC(CAPL_INT32, TestWaitForAllJoinedEvents)(DWORD aTimeout);

    /*!
    A test group is opened with this function. It may only be called in a test module, but not in a test case. All test cases and test groups that are executed before call of the corresponding function TestGroupEnd are part of this test group. If a test group is not closed with TestGroupEnd, then at the end of the test module, a warning is written in the Write Window and the test group is closed automatically
	\param title Title for the test group
	\param discription Description text for the test group
	\note TestGroupBegin (char title[], char description[])
	*/
	CAPL_FUNC(CAPL_INT32, TestGroupBegin)(const char title[], const char description[]);
    
	/*!
    A test group is opened with this function. It may only be called in a test module, but not in a test case. All test cases and test groups that are executed before call of the corresponding function TestGroupEnd are part of this test group. If a test group is not closed with TestGroupEnd, then at the end of the test module, a warning is written in the Write Window and the test group is closed automatically
	\param identifier A test group number
	\param title Title for the test group
	\param discription Description text for the test group	
	\note TestGroupBegin (char ident[], char title[], char description[])
	*/
	CAPL_FUNC(CAPL_INT32, TestGroupBeginIdent)(const char ident[], const char title[], const char description[]);

    /*!
    The function closes a test group opened with TestGroupBegin. If several test groups were opened, then the last-opened and not yet closed test group is closed
	\note TestGroupEnd ()
	*/
	CAPL_FUNC(CAPL_INT32, TestGroupEnd)();

    /*!
    If a message event is the last event that triggers a wait instruction, the message content can be called up with the first function
	\param aMessage Message variable that should be filled in with this function
	\return 0: Data access successful
	\return	-1: Data access could not be executed, the last event was not a message event
	\note long TestGetWaitEventMsgData (message aMessage)
	*/
	CAPL_FUNC(CAPL_INT32, TestGetWaitEventMsgData)(IMessage& aMsg);
    
	/*!
    The function can only be used for "joined events". The number of the "joined event" (return value of "testJoin...") is here being used as an index
	\param aMessage Message variable that should be filled in with this function
	\param index Number of the "joined event" corresponds with the return value of "testJoin..."
	\return 0: Data access successful
	\return	-1: Data access could not be executed, the last event was not a message event 
	\note long TestGetWaitEventMsgData (dword index, message aMessage)
	*/
	CAPL_FUNC(CAPL_INT32, TestGetWaitEventMsgDataIndex)(DWORD index, IMessage& aMsg);

	/*!
    Defers measurement stop
	\param maxDeferTime Indicates the time interval in milliseconds after which completion of pre-stop activities is indicated automatically if it has not yet been done explicitly via CompleteStop
	\note void DeferStop(dword maxDeferTime)
	*/
	CAPL_FUNC(CAPL_INT32, DeferStopInternal)(LPCSTR busContext, DWORD maxDeferTime);
    
	/*!
    Indicates completion of pre-stop actions carried out in a certain node after a measurement stop has been deferred by DeferStop.
	\note void CompleteStop ()
	*/
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
    
	/*!
    Opens a serial port
	\param port A number between 1 and 255 identifying a serial port
	\return 0: error
	\note dword RS232Open( dword port )
	*/
	CAPL_FUNC(DWORD, RS232OpenInternal)(DWORD port, RS232OnSendFunc* pRRS232OnSendFunc, R232ByteCallbackFunc* pByteCallbackFunc, RS232OnErrorFunc* pOnErrorFunc);
    
	/*!
    Opens a serial port
	\param port A number between 1 and 255 identifying a serial port
	\return 0: error
	\note dword RS232Open( dword port )
	*/
	CAPL_FUNC(DWORD, RS232OpenInternalInt)(DWORD port, RS232OnSendFunc* pRRS232OnSendFunc, R232ByteCallbackFuncInt* pByteCallbackFunc, RS232OnErrorFunc* pOnErrorFunc);
    
	/*!
    Sends a block of bytes to a serial port
	\param port A number between 1 and 255 identifying a serial port
	\param buffer An array of bytes of which number will be sent
	\param number Number of bytes to send
	\return 0: error
	\note dword RS232Send( dword port, byte buffer[], dword number )
	*/
	CAPL_FUNC(DWORD, RS232Send)(DWORD port, const BYTE* pBuffer, DWORD number);
    
	/*!
    Sends one bytes to a serial port
	\param port A number between 1 and 255 identifying a serial port
	\param datum Byte to be sent (lowest 8 bits)
	\return 0: error
	\note dword RS232WriteByte( dword port, dword datum )
	*/
	CAPL_FUNC(DWORD, RS232WriteByte)(DWORD port, DWORD datum);
    
	/*!
    Sends a block of bytes to a serial port
	\param port A number between 1 and 255 identifying a serial port
	\param buffer An array of bytes of which number will be sent
	\param number Number of bytes to send
	\return 0: error
	\note dword RS232WriteBlock( dword port, byte buffer[], dword number )
	*/
	CAPL_FUNC(DWORD, RS232WriteBlock)(DWORD port, const BYTE* pBuffer, DWORD number);
    
	/*!
    Closes a serial port
	\param port A number between 1 and 255 identifying a serial port
	\return 0: error The error occurs if the serial port with the given number does not exist on the system
	\return 1: success
	\note dword RS232Close( dword port )
	*/
	CAPL_FUNC(DWORD, RS232Close)(DWORD port);
    
	CAPL_FUNC(DWORD, RS232CloseHandle)(DWORD port);
    
	/*!
    Receive blocks of bytes from a serial port
	\param port A number between 1 and 255 identifying a serial port
	\param buffer An array of bytes
	\param size Maximum number of bytes which can be received
	\return 0: error The error occurs if the serial port with the given number does not exist on the system the port has not been opened
	\note dword RS232Receive( dword port, byte buffer[], dword size )
	*/
	CAPL_FUNC(DWORD, RS232ReceiveInternal)(DWORD port, BYTE* pBuffer, DWORD size, R232ReceivedFunc* pReceiveFunc);
    
	/*!
    Configures a serial port
	\param port A number between 1 and 255 identifying a serial port
	\param baudrate The symbol rate to use for reception and transmission
	\param numberOfDataBits The number of data bits within a transmission frame
	\param numberOfStopBits A code which sets the number of stop bits within a transmission frame
	\param parity A code which identifies the parity mode to use
	\return 0: error The error occurs if the serial port with the given number does not exist on the system and the port has not been opened 
	\note dword RS232Configure( dword port,  dword baudrate, dword numberOfDataBits, dword numberOfStopBits, dword parity );
	*/
	CAPL_FUNC(DWORD, RS232Configure)(DWORD port, DWORD baudrate, DWORD numberOfDataBits, DWORD numberOfStopBits, DWORD parity);
    
	/*!
    Sets signal lines on serial port
	\param port A number between 1 and 255 identifying a serial port
	\param line Specifies signal line of which the state shall be set
	\param state 0 - disabled; 1 - enabled
	\return 0: error The error occurs if the serial port with the given number does not exist on the system and the port has not been opened 
	\note dword RS232SetSignalLine( dword port, dword line, dword state )
	*/
	CAPL_FUNC(DWORD, RS232SetSignalLine)(DWORD port, DWORD line, DWORD state);
    
	/*!
    Sets signal line on a specific serial port
	\param port A number between 1 and 255 identifying a serial port
	\param modemControl Signal lines and levels to bet set on all open ports (opened by CANoe/CANalyzer)
	\return 0: error The error occurs if the serial port with the given number does not exist on the system and the port has not been opened 
	\note dword RS232EscapeCommExt( dword modemControl, dword port )
	*/
	CAPL_FUNC(DWORD, RS232EscapeCommExt)(DWORD modemControl, DWORD port);
    
	/*!
    Sets signal lines on all open serial ports (opened by CANoe/CANalyzer)
	\param modemControl Signal lines and levels to bet set on all open ports (opened by CANoe/CANalyzer)
	\return 0: error The error occurs if the serial port with the given number does not exist on the system and the port has not been opened 
	\note dword RS232EscapeCommFunc( dword modemControl )
	*/
	CAPL_FUNC(DWORD, RS232EscapeCommFunc)(DWORD modemControl);
    
	/*!
    Sets handshake parameters on serial port
	\param port A number between 1 and 255 identifying a serial port
	\param handshake Sets variant of handshake to use
	\param XonLimit Parameter for software flow control. Specifies the minimum number of bytes allowed in the input buffer before the XonChar is sent
	\param XoffLimit Parameter for software flow control. Specifies the maximum number of bytes in the input buffer before the XoffChar is sent. The maximum number of bytes allowed is calculated by subtracting this value from the size, in bytes, of the input buffer
	\param XonChar Parameter for software flow control. It specifies the value of the XON character to start an operation (transmission as well as reception)
	\param XoffChar Parameter for software flow control. It specifies the value of the XOFF character to suspend an operation (transmission as well as reception)
	\param timeout Timeout in milliseconds for all send and receive operations: -1 - infinite; <10 - not allowed
	\return 0: error The error occurs if the serial port with the given number does not exist on the system and the port has not been opened 
	\note dword RS232SetHandshake( dword port, dword handshake, dword XonLimit, dword XoffLimit, dword XonChar, dword XoffChar, dword timeout )
	*/
	CAPL_FUNC(DWORD, RS232SetHandshakeInternal)(DWORD port, DWORD handshake, DWORD XonLimit, DWORD XoffLimit, DWORD XonChar, DWORD XoffChar, DWORD timeout);

    // Benachrichtigt alle Node-Dlls, dass eine Ecu aktiv/inaktiv geschaltet wurde.
    CAPL_FUNC(void, NofityEcuStateChange)(LPCSTR ecuRef, bool boState);

    /*!
    Executes an external command. Does not wait until the command has completed its execution. SysExec must be given an executable; sysExecCmd calls cmd.EXE /K with the first parameter, which opens a command window where the command is executed as if it was entered directly
	\param cmd The command to be executed. Either the full absolute path or a path relative to the current working directory must be given or the command must be in the system path
	\param	params Parameters to the command. A parameter which contains spaces must be enclosed in " "
	\return 1 if the command was successfully started, else 0
	\note long sysExec(char cmd[], char params[]);
	*/
	CAPL_FUNC(CAPL_INT32, sysExec)(const char* cmd, const char* params);
    
	
    /*!
    Executes an external command. Does not wait until the command has completed its execution. SysExec must be given an executable; sysExecCmd calls cmd.EXE /K with the first parameter, which opens a command window where the command is executed as if it was entered directly
	\param cmd The command to be executed. Either the full absolute path or a path relative to the current working directory must be given or the command must be in the system path
	\param	params Parameters to the command. A parameter which contains spaces must be enclosed in " "
	\param directory Working directory for the command. Either an absolute path or a path relative to the current working directory must be given
	\return 1 if the command was successfully started, else 0
	\note long sysExec(char cmd[], char params[], char directory[]);
	*/
	CAPL_FUNC(CAPL_INT32, sysExecDir)(const char* cmd, const char* params, const char* directory);
    
	/*!
    Executes an external command. Does not wait until the command has completed its execution. SysExec must be given an executable; sysExecCmd calls cmd.EXE /K with the first parameter, which opens a command window where the command is executed as if it was entered directly
	\param cmd The command to be executed. Either the full absolute path or a path relative to the current working directory must be given or the command must be in the system path
	\param	params Parameters to the command. A parameter which contains spaces must be enclosed in " "
	\return 1 if the command was successfully started, else 0
	\note long sysExecCmd(char cmd[], char params[]);
	*/
	CAPL_FUNC(CAPL_INT32, sysExecCmd)(const char* cmd, const char* params);
	
    /*!
    Executes an external command. Does not wait until the command has completed its execution. SysExec must be given an executable; sysExecCmd calls cmd.EXE /K with the first parameter, which opens a command window where the command is executed as if it was entered directly
	\param cmd The command to be executed. Either the full absolute path or a path relative to the current working directory must be given or the command must be in the system path
	\param	params Parameters to the command. A parameter which contains spaces must be enclosed in " "
	\param directory Working directory for the command. Either an absolute path or a path relative to the current working directory must be given
	\return 1 if the command was successfully started, else 0
	\note long sysExecCmd(char cmd[], char params[], char directory[]);
	*/
	CAPL_FUNC(CAPL_INT32, sysExecCmdDir)(const char* cmd, const char* params, const char* directory);

    // Ethernet
	
	/*!
    The function converts an address string in colon notation to a 16 byte array with the address bytes in network order
	\param address The numerical IPv4 address to be converted
	\param ipv6Address The array used to store the converted IPv6 address as 16 byte array
	\return 0xFFFFFF: The specified address string was invalid
	\note dword IpGetAddressAsArray( char address[], byte ipv6Address[])
	*/
    CAPL_FUNC(DWORD, IpGetAddressAsArray)(const char address[], BYTE ipv6Address[]); // form 1
	
	/*!
    The function converts an IPv4 address string in dot notation to it's numerical value in network-byte order
	\param addressn The numerical IPv4 address to be converted
	\return 4294967295 (0xFFFFFFF, the equivalent of "255.255.255.255"): The specified address string was invalid
	\note dword IpGetAddressAsNumber( char address[])
	*/
    CAPL_FUNC(DWORD, IpGetAddressAsNumber)(const char address[]); // form 1
	
	/*!
    The function converts a numeric address in host-byte order (little endian) to an address string in dot notation as in "192.168.0.10". For IPv6 the address string has to contain a string in colon notation as in "1234:5678:9ABC:DEF1:2345:6789:ABCD:EF12"
	\param numericAddress The numerical IPv4 address to be converted
	\param address The buffer used to store the converted IPv4 address
	\param count The size of the address buffer
	\return 0: The function completed successfully
	\note long IpGetAddressAsString( dword numericAddress, char address[], dword count)
	*/
    CAPL_FUNC(CAPL_INT32, IpGetAddressAsString)(DWORD numericAddress, char address[], DWORD count); // form 1
	
	/*!
    The function converts a numeric address in host-byte order (little endian) to an address string in dot notation as in "192.168.0.10". For IPv6 the address string has to contain a string in colon notation as in "1234:5678:9ABC:DEF1:2345:6789:ABCD:EF12"
	\param ipv6Address The local IPv6 address in a 16 byte array
	\param address The buffer used to store the converted IPv4 address
	\param count The size of the address buffer
	\return 0: The function completed successfully
	\note long IpGetAddressAsString( byte ipv6Address[], char address[], dword count)
	*/
    CAPL_FUNC(CAPL_INT32, IpGetAddressAsStringIP6)(byte ipv6Address[], char address[], DWORD count); // form 2
	
	/*!
    The function returns the Winsock 2 error code of the last operation that failed
	\return The error code as provided by the Winsock 2 WSAGetLastError function
	\note long IpGetLastError()
	*/
    CAPL_FUNC(CAPL_INT32, IpGetLastError)(); // form 1
	
	/*!
    The function returns the Winsock 2 error code of the last operation that failed on the specified socket
	\param socket The socket handle
	\return WSA_INVALID_PARAMETER (87): The specified socket was invalid
	\note long IpGetLastSocketError( dword socket)
	*/
    CAPL_FUNC(CAPL_INT32, IpGetLastSocketError)(DWORD socket); // form 1
	
	/*!
    The function retrieves the error message of the last operation that failed on the specified socket (see Winsock 2 error code)
	\param socket The socket handle
	\param text The buffer used to store the error message
	\param count The size of the text buffer
	\return 0: The error message was written into the text buffer. In case of an invalid error code, the error message has the format "Unknown error: x" assuming the last error code x for the specified socket
	\return WSA_INVALID_PARAMETER (87): The specified socket was invalid
	\note long IpGetLastSocketErrorAsString( dword socket,  char text[], dword count)
	*/
    CAPL_FUNC(CAPL_INT32, IpGetLastSocketErrorAsString)(DWORD socket, char text[], DWORD count); // form 1
	
	/*!
    The function returns the number of network interfaces for the local computer, not including the loopback interface
	\return The number of network interfaces
	\note dword IpGetAdapterCount()
	*/
    CAPL_FUNC(DWORD, IpGetAdapterCountInternal)(const char sNode[]);
	
	/*!
    The function returns the count of addresses belonging to the given address family which are assigned to the adapter with the given index
	\param ifIndex The 1-based network interface index
	\param addressFamily The address family of the addresses, the address count will be returned
		Possible values are:
		AF_INET (2): IPv4 address family
		AF_INET6 (28): IPv6 address family
	\return The count of addresses of the given address family will be returned 
	\note long IpGetAdapterAddressCount( dword ifIndex, dword addressFamily)
	*/
    CAPL_FUNC(CAPL_INT32, IpGetAdapterAddressCountInternal)(const char sNode[], DWORD ifIndex, DWORD addressFamily);
	
	/*!
    The function retrieves the addresses associated with a network interface. The interface is specified by it's 1-based index in the list of network interfaces, i.e. the first interface has index 1
	\param ifIndex The 1-based network interface index
	\param address The array used to store the numerical IPv4 addresses
	\param count The size of the address array
	\return 0: The function completed successfully.
	\return ERROR_NOT_ENOUGH_MEMORY (8): The address array was insufficient
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\return WSAEADDRNOTAVAIL (10049): No adapter address available
	\note long IpGetAdapterAddress( dword ifIndex, dword address[],dword count)
	*/
    CAPL_FUNC(CAPL_INT32, IpGetAdapterAddressIPv4Internal)(const char sNode[], DWORD ifIndex, DWORD address[], DWORD count);
	
	/*!
    The function retrieves the addresses associated with a network interface. The interface is specified by it's 1-based index in the list of network interfaces, i.e. the first interface has index 1
	\param ifIndex The 1-based network interface index
	\param ipv6Addresses The array used to store the IPv6 addresses as 16 byte arrays
	\param count The size of the address array
	\return 0: The function completed successfully
	\return ERROR_NOT_ENOUGH_MEMORY (8): The address array was insufficient
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\return WSAEADDRNOTAVAIL (10049): No adapter address available
	\note long IpGetAdapterAddress( dword ifIndex, byte ipv6Addresses[][], dword count)
	*/
    CAPL_FUNC(CAPL_INT32, IpGetAdapterAddressIPv6Internal)(const char sNode[], DWORD ifIndex, byte ipv6Addresses[][16], DWORD count);
	
	/*!
    The function adds an address to the network interface with the given index
	\param ifIndex The 1-based network interface index
	\param address The numerical IPv4 address to add to the interface
	\param mask The IPv4 network mask in network byte order
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code
	\note long IpAddAdapterAddress(dword ifIndex, dword address, dword mask)
	*/
    CAPL_FUNC(CAPL_INT32, IpAddAdapterAddressIPv4Internal)(const char sNode[], DWORD ifIndex, DWORD address, DWORD mask);
	
	/*!
    The function adds an address to the network interface with the given index
	\param ifIndex The 1-based network interface index
	\param ipv6Address The IPv6 address in 16 byte array
	\param prefix The IPv6 prefix for the given IPv6 address
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code 
	\note long IpAddAdapterAddress(dword ifIndex, byte ipv6Address[], dword prefix)
	*/
    CAPL_FUNC(CAPL_INT32, IpAddAdapterAddressIPv6Internal)(const char sNode[], DWORD ifIndex, byte ipv6Addresses[], DWORD prefix);
	
	/*!
    The function removes an address from the network interface with the given index.
	\param ifIndex The 1-based network interface index
	\param address The numerical IPv4 address to remove from the interface
	\param mask The IPv4 network mask in network byte order
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code
	\note 
	*/
    CAPL_FUNC(CAPL_INT32, IpRemoveAdapterAddressIPv4Internal)(const char sNode[], DWORD ifIndex, DWORD address, DWORD mask);
	
	/*!
    The function removes an address from the network interface with the given index
	\param ifIndex The 1-based network interface index
	\param ipv6Address The IPv6 address to remove from the interface in a 16 byte array
	\param prefix The IPv6 prefix for the given IPv6 address
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code 
	\note long IpRemoveAdapterAddress (dword ifIndex, byte ipv6Address[], dword prefix)
	*/
    CAPL_FUNC(CAPL_INT32, IpRemoveAdapterAddressIPv6Internal)(const char sNode[], DWORD ifIndex, byte ipv6Addresses[], DWORD prefix);
	
	/*!
    The function retrieves the description of the specified network interface
	\param ifIndex The 1-based network interface index
	\param name The buffer used to store the description
	\param count The size of the name buffer
	\return 0: The function completed successfully
	\return ERROR_NOT_ENOUGH_MEMORY (8): The name buffer was insufficient
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\note long IpGetAdapterDescription( dword ifIndex, char name[],  dword count)
	*/
    CAPL_FUNC(CAPL_INT32, IpGetAdapterDescriptionInternal)(const char sNode[], DWORD ifIndex, char name[], DWORD count);
	
	/*!
    Set the interface for outgoing multicast messages. Without calling this no multicast messages can be sent on the given socket
	\param socket The socket handle
	\param ifIndex The 1-based network interface index
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code
	\note long IpSetMulticastInterface ( dword socket, dword ifIndex )
	*/
    CAPL_FUNC(CAPL_INT32, IpSetMulticastInterfaceInternal)(const char sNode[], DWORD socket, DWORD ifIndex);
	
	/*!
    Joins the multicast group on the given socket, to be able to receive multicast messages
	\param socket The socket handle
	\param ifIndex The 1-based network interface index
	\param address The numerical IPv4 address of the destination
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code
	\note long IpJoinMulticastGroup( dword socket, dword ifIndex, dword address )
	*/
    CAPL_FUNC(CAPL_INT32, IpJoinMulticastGroupIP4Internal)(const char sNode[], DWORD socket, DWORD ifIndex, DWORD address);
	
	/*!
    Joins the multicast group on the given socket,. to be able to receive multicast messages
	\param socket The socket handle
	\param ifIndex The 1-based network interface index
	\param ipv6Address The local IPv6 address in a 16 byte array
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified network interface index was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code
	\note long IpJoinMulticastGroup( dword socket, dword ifIndex, byte ipv6Address[] )
	*/
    CAPL_FUNC(CAPL_INT32, IpJoinMulticastGroupIP6Internal)(const char sNode[], DWORD socket, DWORD ifIndex, byte ipv6Address[]);

	/*!
    This function creates a new Ethernet packet. Other functions can access to the newly created packet with the returned handle
	\param srcMacAddressStruct Source MAC address as struct
	\param dstMacAddressStruct Destination MAC address as struct
	\param srcMacAddress Source MAC address
	\param dstMacAddress Destination MAC address
	\param ethernetType Ethernet type (16 Bit)
	\param protocolDesignator Designator of the protocol, which should be used for initialization
	\param packetTypeDesignator Designator of the packet type
	\param packetToCopy Handle of a packet which was created with EthInitPacket before or handle of a packet which has been received within a callback function (<OnEthPacket>)
	\param rawDataLength Length of rawData in byte
	\param rawData Raw data of an Ethernet packet that is used to initialized the new packet
	\return handle of the created packet or 0
	\note long EthInitPacket( );
	*/
    CAPL_FUNC(CAPL_INT32, EthInitPacket)(); // form 1
	
	/*!
    This function creates a new Ethernet packet. Other functions can access to the newly created packet with the returned handle. Protocol fields that are marked as InitProtocol in the protocol description are initialized
	\param protocolDesignator Designator of the protocol, which should be used for initialization
	\return Handle of the created packet or 0. With EthGetLastError you can check if the function has been processed successfully
	\note long EthInitPacket( char protocolDesignator[] );
	*/
    CAPL_FUNC(CAPL_INT32, EthInitPacketProt)(const char protocolDesignator[]); // form 2
	
	/*!
    This function creates a new Ethernet packet. Other functions can access to the newly created packet with the returned handle. Protocol fields that are marked as InitProtocol in the protocol description are initialized
	\param protocolDesignator Designator of the protocol, which should be used for initialization
	\param packetTypeDesignator Designator of the packet type
	\return Handle of the created packet or 0. With EthGetLastError you can check if the function has been processed successfully
	\note long EthInitPacket( char protocolDesignator[], char packetTypeDesignator[] );
	*/
    CAPL_FUNC(CAPL_INT32, EthInitPacketPacket)(const char protocolDesignator[], const char packetTypeDesignator[]); // form 3
	
    // long EthInitPacket(struct srcMacAddressStruct, struct dstMacAddressStruct, long ethernetType); // form 4
	
	/*!
    This function creates a new Ethernet packet. Other functions can access to the newly created packet with the returned handle. Protocol fields that are marked as InitProtocol in the protocol description are initialized
	\param srcMacAddressStruct Source MAC address as struct
	\param dstMacAddressStruct Destination MAC address as struct
	\param ethernetType Ethernet type (16 Bit)
	\return Handle of the created packet or 0. With EthGetLastError you can check if the function has been processed successfully
	\note long EthInitPacket( byte srcMacAddress[6], byte dstMacAddress[6], long ethernetType );
	*/
    CAPL_FUNC(CAPL_INT32, EthInitPacketArray)(BYTE srcMacAddress[6], BYTE dstMacAddress[6], CAPL_INT32 ethernetType); // form 5
    
	/*!
    This function creates a new Ethernet packet. Other functions can access to the newly created packet with the returned handle. Protocol fields that are marked as InitProtocol in the protocol description are initialized
	\param packetToCopy Handle of a packet which was created with EthInitPacket before or handle of a packet which has been received within a callback function (<OnEthPacket>). The header and the data of this packet are copied to the new created packet
	\return Handle of the created packet or 0 With EthGetLastError you can check if the function has been processed successfully
	\note long EthInitPacket( long packetToCopy ); 
	*/
	CAPL_FUNC(CAPL_INT32, EthInitPacketCopy)(CAPL_INT32 packetToCopy); // form 6
	
	/*!
    This function creates a new Ethernet packet. Other functions can access to the newly created packet with the returned handle. Protocol fields that are marked as InitProtocol in the protocol description are initialized
	\param rawDataLength Length of rawData in byte
    \param rawData Raw data of an Ethernet packet that is used to initialized the new packet
	\return Handle of the created packet or 0 With EthGetLastError you can check if the function has been processed successfully
	\note long EthInitPacket( long rawDataLength, byte rawData[] ); 
	*/
    CAPL_FUNC(CAPL_INT32, EthInitPacketData)(CAPL_INT32 rawDataLength, BYTE rawData[]); // form 7
    
	/*!
    The function initializes the protocol for a packet. If necessary further needed lower protocols are initialized, e.g. IPv4. Already initialized higher protocols are deleted. Protocol fields that are marked as InitProtocol in the protocol overview are initialized
	\param packet Handle of a packet that has been created with EthInitPacket
    \param protocolDesignator Name of the protocol, taken from the protocol overview
	\return 0 or error code
	\note long EthInitProtocol( long packet, char protocolDesignator[] );
	*/
	CAPL_FUNC(CAPL_INT32, EthInitProtocol)(CAPL_INT32 packet, const char protocolDesignator[]); // form 1
    
	/*!
    The function initializes the protocol for a packet. If necessary further needed lower protocols are initialized, e.g. IPv4. Already initialized higher protocols are deleted. Protocol fields that are marked as InitProtocol in the protocol overview are initialized
	\param packet Handle of a packet that has been created with EthInitPacket
    \param protocolDesignator Name of the protocol, taken from the protocol overview
	\param packetTypeDesignator Type of the packet, taken from the protocol overview
	\return 0 or error code
	\note long EthInitProtocol( long packet, char protocolDesignator[], char packetTypeDesignator[] );
	*/
	CAPL_FUNC(CAPL_INT32, EthInitProtocolType)(CAPL_INT32 packet, const char protocolDesignator[], const char packetTypeDesignator[]); // form 2
    
	/*!
    Returns the error code of the last called Eth... function
	\return Error code
	\note long EthGetLastError( void )
	*/
	CAPL_FUNC(CAPL_INT32, EthGetLastError)(void); // form 1
     
	/*!
    Gets the error code description of the last called Eth... function
    \param bufferSize Size of buffer in which the description text is copied
	\param buffer Buffer in which the description text is copied
	\return Number of copied bytes
	\note long EthGetLastErrorText( dword bufferSize, char[] buffer );
	*/

	CAPL_FUNC(CAPL_INT32, EthGetLastErrorText)(DWORD bufferSize, char buffer[]); // form 1
	/*!
    The function adds an additional token at the end of a protocol header or at a specific position (for details see protocol help)
    \param packet Handle of a packet that has been created with EthInitPacket
	\param protocolDesignator Name of the protocol, e.g. "dhcpv4"
	\param tokenDesignator Name of the token, e.g. "option1"
	\return 0 or error code
	\note long EthAddToken( long packet, char protocolDesignator[], char tokenDesignator[] );
	*/
	CAPL_FUNC(CAPL_INT32, EthAddToken)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[]);
    
	/*!
    The function removes a token from a protocol header
    \param packet Handle of a packet that has been created with EthInitPacket
	\param protocolDesignator Name of the protocol, e.g. "dhcpv4"
	\param tokenDesignator Name of the token, e.g. "option1"
	\return 0 or error code
	\note long EthRemoveToken( long packet, char protocolDesignator[], char tokenDesignator[] );
	*/
	CAPL_FUNC(CAPL_INT32, EthRemoveToken)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[]);
   
    /*!
    The function returns the length of a token in bit
    \param packet Handle of a packet that has been created with EthInitPacket
	\param protocolDesignator Name of the protocol, e.g. "ipv4"
	\param tokenDesignator Name of the token, e.g. "source"
	\return Length of the token in bit With EthGetLastError you can check if the function has been processed successfully
	\note long EthGetTokenLengthBit( long packet, char protocolDesignator[], char tokenDesignator[] );
	*/
    CAPL_FUNC(CAPL_INT32, EthGetTokenLengthBit)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[]);
    
	/*!
	The function requests the integer value of a token
    \param packet Handle of a packet that has been created with EthInitPacket
	\param protocolDesignator Name of the protocol, e.g. "ipv4"
	\param tokenDesignator Name of the token, e.g. "source"
	\return integer value of the token or 0 With EthGetLastError you can check if the function has been processed successfully
	\note long EthGetTokenInt( long packet, char protocolDesignator[], char tokenDesignator[] );
	*/
	CAPL_FUNC(CAPL_INT32, EthGetTokenInt)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[]);
    
	/*!
	The function requests the integer value of a token
    \param packet Handle of a packet that has been created with EthInitPacket
	\param protocolDesignator Name of the protocol, e.g. "ipv4"
	\param tokenDesignator Name of the token, e.g. "source"
	\param byteOffset Offset from the beginning of the token in byte
	\param length Length of the integer value, must be 1, 2, 3 or 4 byte
	\param networkByteOrder 0: INTEL (little-endian) 
							1: MOTOROLA (big-endian) 
	\return integer value of the token or 0 With EthGetLastError you can check if the function has been processed successfully
	\note long EthGetTokenInt( long packet, char protocolDesignatorl[], char tokenDesignator[], long byteOffset, long length, long networkByteOrder );
	*/
	CAPL_FUNC(CAPL_INT32, EthGetTokenData)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[], CAPL_INT32 length, void* buffer);
    
	/*!
	The function sets the data or a part of data of a token. It does not resize the token. Use EthResizeToken to change the length
    \param packet Handle of a packet that has been created with EthInitPacket
	\param protocolDesignator Name of the protocol, e.g. "ipv4"
	\param tokenDesignator Name of the token, e.g. "source"
	\param length Number of bytes to be copied
	\param data Data that are copied to the token
	\return 0 or error code
	\note long EthSetTokenData( long packet, char protocolDesignator[], char tokenDesignator[], long length, char data[] );
	*/
	CAPL_FUNC(CAPL_INT32, EthSetTokenData)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[], CAPL_INT32 length, const void* data); // form 1, 2, 3
    
	/*!
	The function sets the data or a part of data of a token. It does not resize the token. Use EthResizeToken to change the length
    \param packet Handle of a packet that has been created with EthInitPacket
	\param protocolDesignator Name of the protocol, e.g. "ipv4"
	\param tokenDesignator Name of the token, e.g. "source"
	\pram byteOffset Offset from the beginning of the token in byte
	\param length Number of bytes to be copied
	\param data Data that are copied to the token
	\return 0 or error code
	\note long EthSetTokenData( long packet, char protocolDesignator[], char tokenDesignator[], long byteOffset, long length, char data[] );
	*/
	CAPL_FUNC(CAPL_INT32, EthSetTokenDataOffset)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[], CAPL_INT32 byteOffset, CAPL_INT32 length, const void* data); // form 4, 5, 6
   
	/*!
	The function sets the integer value of a token
    \param packet Handle of a packet that has been created with EthInitPacket
	\param protocolDesignator Name of the protocol, e.g. "ipv4"
	\param tokenDesignator Name of the token, e.g. "source"
	\param value New integer value
	\return 0 or error code
	\note long EthSetTokenInt( long packet, char protocolDesignatorl[], char tokenDesignator[], long value ); 
	*/
    CAPL_FUNC(CAPL_INT32, EthSetTokenInt)(CAPL_INT32 packet, const char protocolDesignatorl[], const char tokenDesignator[], CAPL_INT32 value); // form 1
    
	/*!
	The function with byte offset sets a part of the token data as integer
    \param packet Handle of a packet that has been created with EthInitPacket
	\param protocolDesignator Name of the protocol, e.g. "ipv4"
	\param tokenDesignator Name of the token, e.g. "source"
	\pram byteOffset Offset from the beginning of the token in byte
	\param length Length of the integer value, must be 1, 2, 3 or 4 byte
	\param networkByteOrder 0: INTEL (little-endian) 
							1: MOTOROLA (big-endian)
	\param value New integer value
	\return 0 or error code
	\note long EthSetTokenInt( long packet, char protocolDesignatorl[], char tokenDesignator[], long byteOffset, long length, long networkByteOrder, long value ); 
	*/
	CAPL_FUNC(CAPL_INT32, EthSetTokenIntOffset)(CAPL_INT32 packet, const char protocolDesignatorl[], const char tokenDesignator[], CAPL_INT32 byteOffset, CAPL_INT32 length, CAPL_INT32 networkByteOrder, CAPL_INT32 value); // form 2
    
	/*!
	The function can only be used with resizable tokens. These are i.g. the "header" and "data" tokens. If a token is not resizable the error code 46-0125 is returned
	\param packet Handle of a packet that has been created with EthInitPacket
    \param  protocolDesignator Name of the protocol, e.g. "ipv4"
	\param tokenDesignator Name of the token, e.g. "source"
	\param newBitLength New length of the token in bits
	\return 0 or error code
	\note long EthResizeToken( long packet, char protocolDesignator[], char tokenDesignator[], long newBitLength ); 
	*/
	CAPL_FUNC(CAPL_INT32, EthResizeToken)(CAPL_INT32 packet, const char protocolDesignator[], const char tokenDesignator[], CAPL_INT32 newBitLength); // form 1
		
	/*!
    Functions assume that the packet’s payload is described in the database. It writes the signal’s value into the packet’s payload
	\param packet Handle of a packet that has been created with EthInitPacket
    \param signal Signal name from database. The signal must be assigned to the node as Tx signal
	\param physValue Physical value
	\return 0 or error code
	\note long EthSetTokenSignalValue( long packet, dbSignal signal, double physValue ); 
	*/
    CAPL_FUNC(CAPL_INT32, EthSetTokenSignalValue)(CAPL_INT32 packet, ISignalVal& signal, double physValue); // form 1
	
	/*!
    Functions assume that the packet’s payload is described in the database. It writes the signal’s value into the packet’s payload. Assumes the passed signal is the first element of an array. The value will be written to the element’s position
	\param packet Handle of a packet that has been created with EthInitPacket
    \param signal Signal name from database. The signal must be assigned to the node as Tx signal
	\param physValue Physical value
	\param index Zero based index of element
	\return 0 or error code
	\note long EthSetTokenSignalValue( long packet, dbSignal signal, double physValue, dword index);
	*/
    CAPL_FUNC(CAPL_INT32, EthSetTokenSignalValueIndex)(CAPL_INT32 packet, ISignalVal& signal, double physValue, DWORD index); // form 2
	
	/*!
    The function completes a packet before sending it with EthOutputPacket. It calculates the fields that are marked with CompleteProtocol in the protocol overview, e.g. checksum, lengths, etc
	\param packet Handle of the packet that has been created with EthInitPacket 
	\return 0 or error code
	\note long EthCompletePacket( long packet );
	*/
    CAPL_FUNC(CAPL_INT32, EthCompletePacket)(CAPL_INT32 packet); // form 1
	
	/*!
    This function sends an Ethernet packet
	\param packet Handle of the packet to send 
	\return 0 or error code
	\note long EthOutputPacket( long packet );
	*/
    CAPL_FUNC(CAPL_INT32, EthOutputPacketInternal)(const char* sChannelRef, CAPL_INT32 packet); // form 1
	
	/*!
    This function sends an Ethernet packet
	\param packet Handle of the packet to send
	\param fcs user-defined frame check sequence 
	\return 0 or error code
	\note long EthOutputPacket(long packet, dword fcs);
	*/
    CAPL_FUNC(CAPL_INT32, EthOutputPacketFcs)(CAPL_INT32 packet, DWORD fcs); // form 2
	
	/*!
    The function deletes a packet created with EthInitPacket. The handle can not be used any longer
    \param packet Handle of the packet to delete
	\return 0 or error code
	\note long EthReleasePacket( long packet );
	*/
    CAPL_FUNC(CAPL_INT32, EthReleasePacket)(CAPL_INT32 packet); // form 1
	
	/*!
    Converts a MAC address string, i.e. "02:00:00:00:00:01", to a qword-number, which can be used with ethernetPacket.source and ethernetPacket.destination.
    \param MAC address as string, i.e. "02:00:00:00:00:01"
	\return The MAC address as qword or 0, if failed
	\note qword EthGetMacAddressAsNumber( char macAddrStr[] );
	*/
    CAPL_FUNC(QWORD, EthGetMacAddressAsNumber)(const char macAddrStr[]); // form 1
	
	/*!
    Converts a MAC address from qword to string. The function is helpful with ethernetPacket.source and ethernetPacket.destination
    \param macAddr MAC address as qword, i.e. from ethernetPacket.source
	\param buffer Buffer where the MAC address string is copied to
	\param bufferLength Length of buffer
 	\return 0: The function completed successfully
	\return 8: The address buffer was insufficient
	\note long EthGetMacAddressAsString( qword macAddr, char buffer[], dword bufferLength );
	*/
    CAPL_FUNC(CAPL_INT32, EthGetMacAddressAsString)(QWORD macAddr, char buffer[], DWORD bufferLength); // form 1

    /*!
	Starts the Ethernet packet generator that sends continuously the configured packets
    \param txPacket The Ethernet packet which was used to start the generator
	\param transmissionRate The rate in frames per second the packet should be transmitted. Range: [1 ... 1000000]
 	\return 0: Success
	\return -1: Output on specified Ethernet channel not allowed
	\return -2: Packet generator not supported in simulation mode
	\return -3: Packet generator is only available with Vector Ethernet network hardware
	\return -4: Configuration of packet generator failed
	\return -5: Starting of packet generator failed
	\note long EthStartPacketGenerator( ethernetPacket txPacket, dword transmissionRate );
	*/
	CAPL_FUNC(CAPL_INT32, EthStartPacketGenerator)(IEthernetPacket& txPacket, DWORD transmissionRate);
	
    /*!
	Starts the Ethernet packet generator that sends continuously the configured packets
    \param txPacket The Ethernet packet which was used to start the generator
	\param transmissionRate The rate in frames per second the packet should be transmitted. Range: [1 ... 1000000]
	\param numberOfFrames The number of Frames that should be transmitted overall. Range: [0x1 ... 0xFFFFFFFE], 0xFFFFFFFF to send until EthStopPacketGenerator
 	\return 0: Success
	\return -1: Output on specified Ethernet channel not allowed
	\return -2: Packet generator not supported in simulation mode
	\return -3: Packet generator is only available with Vector Ethernet network hardware
	\return -4: Configuration of packet generator failed
	\return -5: Starting of packet generator failed
	\note long EthStartPacketGenerator( ethernetPacket txPacket, dword transmissionRate, dword numberOfFrames );
	*/
	CAPL_FUNC(CAPL_INT32, EthStartPacketGeneratorNumberOfFrames)(IEthernetPacket& txPacket, DWORD transmissionRate, DWORD numberOfFrames);
    
	/*!
	Starts the Ethernet packet generator that sends continuously the configured packets
    \param txPacket The Ethernet packet which was used to start the generator
	\param transmissionRate The rate in frames per second the packet should be transmitted. Range: [1 ... 1000000]
	\param numberOfFrames The number of Frames that should be transmitted overall. Range: [0x1 ... 0xFFFFFFFE], 0xFFFFFFFF to send until EthStopPacketGenerator
	\param counterByteOffset The position a 4 byte counter is placed inside the Ethernet-payload on a zero based index. Index 0 is the first byte after the source MAC Address. Take care that the counter has to fit completely into the payload
 	\return 0: Success
	\return -1: Output on specified Ethernet channel not allowed
	\return -2: Packet generator not supported in simulation mode
	\return -3: Packet generator is only available with Vector Ethernet network hardware
	\return -4: Configuration of packet generator failed
	\return -5: Starting of packet generator failed
	\note long EthStartPacketGenerator( ethernetPacket txPacket, dword transmissionRate, dword numberOfFrames, dword counterByteOffset );
	*/
	CAPL_FUNC(CAPL_INT32, EthStartPacketGeneratorCounterByteOffset)(IEthernetPacket& txPacket, DWORD transmissionRate, DWORD numberOfFrames, DWORD counterByteOffset);
	
	/*!
	Stops the Ethernet packet generator, which was started with EthStartPacketGenerator
    \param txPacket The Ethernet packet which was used to start the generator
 	\return 0: Success
	\return -1: Output on specified Ethernet channel not allowed
	\return -2: Packet generator not supported in simulation mode
	\return -3: Packet generator is only available with Vector Ethernet network hardware
	\return -5: Starting of packet generator failed
	\note long EthStopPacketGenerator( ethernetPacket txPacket );
	*/
    CAPL_FUNC(CAPL_INT32, EthStopPacketGenerator)(IEthernetPacket& txPacket);

    /*!
    The function creates a TCP socket for use in connection-based, message-oriented communications. All parameters may be zero. If the port parameter is non-zero the socket is implicitly bound to the given port
	\param address The numerical local IPv4 address to be used with the socket If set to 0, then the socket is address unspecific (address wildcard). It then can be used for all adapters on any address. It can be bound later on with IpBind or TcpConnect
	\param port The port in host-byte order to be used with the socket. If set to 0, then the socket is port unspecific (port wildcard). It then can be used for all ports on given address. It can be bound later on with IpBind or TcpConnect
	\return INVALID_SOCKET (~0): The function failed. Call IpGetLastError to get a more specific error code
	\return	Any other value: A valid socket handle identifying the created socket
	\note dword TcpOpen( dword address, dword port)
	*/
	CAPL_FUNC(DWORD, TcpOpenIP4Internal)(const char sNode[], DWORD address, DWORD port);
    
	/*!
    The function creates a TCP socket for use in connection-based, message-oriented communications. All parameters may be zero. If the port parameter is non-zero the socket is implicitly bound to the given port
	\param ipv6Address The local IPv6 address in a 16 byte array
	\param port The port in host-byte order to be used with the socket. If set to 0, then the socket is port unspecific (port wildcard). It then can be used for all ports on given address. It can be bound later on with IpBind or TcpConnect
	\return INVALID_SOCKET (~0): The function failed. Call IpGetLastError to get a more specific error code
	\return	Any other value: A valid socket handle identifying the created socket
	\note dword TcpOpen( byte ipv6Address[], dword port)
	*/
	CAPL_FUNC(DWORD, TcpOpenIP6Internal)(const char sNode[], const byte ipv6Address[], DWORD port);

    /*!
    The function establishes a connection with the specified location
	\param socket The socket handle
	\param address The numerical IPv4 address of the destination
	\param port The port of the destination in host-byte order
	\return WSA_INVALID_PARAMETER (87): The specified socket was invalid
	\return SOCKET_ERROR (-1): Call IpGetLastSocketError to get a more specific error code. If the error code is different from 10035 this indicates a connection error	
	\note long TcpConnect( dword socket, dword address, dword port)
	*/
	CAPL_FUNC(CAPL_INT32, TcpConnectIP4)(CAPL_UINT32 socket, CAPL_UINT32 address, CAPL_UINT32 port);
    
	/*!
    The function establishes a connection with the specified location
	\param socket The socket handle
	\param ipv6Address The local IPv6 address in a 16 byte array
	\param port The port of the destination in host-byte order 
	\return WSA_INVALID_PARAMETER (87): The specified socket was invalid
	\return SOCKET_ERROR (-1): Call IpGetLastSocketError to get a more specific error code. If the error code is different from 10035 this indicates a connection error	
	\note long TcpConnect( dword socket, byte ipv6Address[], dword port)
	*/
	CAPL_FUNC(CAPL_INT32, TcpConnectIP6)(CAPL_UINT32 socket, CAPL_UINT8 address[], CAPL_UINT32 port);

    /*!
    The function closes the TCP socket. Upon successful completion the passed socket is no longer valid
    \param socket The socket to be closed
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified socket was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastError to get a more specific error code
    \note long TcpClose( dword socket);
    */   
    CAPL_FUNC(CAPL_INT32, TcpClose)(CAPL_UINT32 socket);

    /*!
    The function accepts an incoming connection request on the specified socket resulting in a new socket . If the operation fails, the function will return INVALID_SOCKET (~0)
    \param socket The socket handle of the listen socket that was created with TcpListen
	\return INVALID_SOCKET (~0): The function failed. Call IpGetLastError to get a more specific error code. If this error code is WSA_INVALID_PARAMETER (87), the specified socket was invalid. Otherwise use IpGetLastSocketError to get the reason for the failing
	\return Any other value: A valid socket handle identifying the created socket
    \note dword TcpAccept( dword socket);
    */   
    CAPL_FUNC(CAPL_INT32, TcpAccept)(CAPL_UINT32 socket);
    
    /*!
    The function causes the socket to listen for incoming connection requests, which will be provided in the CAPL callback OnTcpListen, if it is implemented in the same CAPL program. If the operation fails, the function will return SOCKET_ERROR (-1)
    \param socket The socket handle
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified socket was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code
    \note long TcpListen( dword socket);
    */ 
    CAPL_FUNC(CAPL_INT32, TcpListen)(CAPL_UINT32 socket);

    /*!
    The function creates an UDP socket for use in connectionless, datagramm-oriented communications. All parameters may be zero. If  the port parameter is non-zero the socket is implicitly bound to the given port
	\param address The numerical local IPv4 address to be used with the socket. If set to 0, then the socket is address unspecific (address wildcard). It then can be used for all adapters on any address. It can be bound later on with IpBind or UdpSendTo
	\param port The port in host-byte order to be used with the socket. If set to 0, then the socket is port unspecific (port wildcard). It then can be used for all ports on given address. It can be bound later on with IpBind or UdpSendTo
	\return INVALID_SOCKET (~0): The function failed. Call IpGetLastError to get a more specific error code
	\return	Any other value: A valid socket handle identifying the created socket
	\note dword UdpOpen( dword address, dword port)
	*/
	CAPL_FUNC(DWORD, UdpOpenIP4Internal)(const char sNode[], DWORD address, DWORD port); // form 1
    
	/*!
    The function creates an UDP socket for use in connectionless, datagramm-oriented communications. All parameters may be zero. If  the port parameter is non-zero the socket is implicitly bound to the given port
	\param ipv6Address The local IPv6 address in a 16 byte array. Like in the IPv4 address an address wildcard can be retrieved using IpGetAddressAsArray(“::”, ipv6AddrArray)
	\param port The port in host-byte order to be used with the socket. If set to 0, then the socket is port unspecific (port wildcard). It then can be used for all ports on given address. It can be bound later on with IpBind or UdpSendTo	
	\return INVALID_SOCKET (~0): The function failed. Call IpGetLastError to get a more specific error code
	\return	Any other value: A valid socket handle identifying the created socket
	\note dword UdpOpen( byte ipv6Address[], dword port)
	*/
	CAPL_FUNC(DWORD, UdpOpenIP6Internal)(const char sNode[], const byte ipv6Address[], DWORD port); // form 2

    /*!
    The function closes the UDP socket. Upon successful completion the passed socket is no longer valid
    \param socket The socket to be closed
	\return 0: The function completed successfully 
	\return 0: The function completed successfully
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastError to get a more specific error code
    \note long UdpClose( dword socket);
    */   
    CAPL_FUNC(CAPL_INT32, UdpClose)(DWORD socket);

    // Ethernet UDP-Callback-Funktionen.
    typedef void (OnSendFunction)(CAPL_UINT32 socket, CAPL_INT32 result, void* buffer, CAPL_UINT32 size);
    typedef void (OnUdpReceiveFromFunctionIPv4)(CAPL_UINT32 socket, CAPL_INT32 result, CAPL_UINT32 address, CAPL_UINT32 port, void* buffer, CAPL_UINT32 size);
    typedef void (OnUdpReceiveFromFunctionIPv6)(CAPL_UINT32 socket, CAPL_INT32 result, CAPL_UINT8 address[], CAPL_UINT32 port, void* buffer, CAPL_UINT32 size);
    typedef void (OnTcpReceiveFunctionIPv4)(CAPL_UINT32 socket, CAPL_INT32 result, CAPL_UINT32 address, CAPL_UINT32 port, void* buffer, CAPL_UINT32 size);
    typedef void (OnTcpReceiveFunctionIPv6)(CAPL_UINT32 socket, CAPL_INT32 result, CAPL_UINT8 address[], CAPL_UINT32 port, void* buffer, CAPL_UINT32 size);
    typedef void (OnEthernetFunction)(CAPL_INT32 channel, CAPL_INT32 dir, CAPL_INT32 packet);

    /*!
    Use this function to register a CAPL callback function to receive Ethernet packets. The callback has a packet handle as parameter and the functions to access the tokens can be used. The EthGetThis-functions can be used to access the payload of the highest interpretable protocol
    \param onPacketCallback Name of CAPL callback function
	\return 0 or error code
    \note long EthReceivePacket( char *onPacketCallback );
    */
    CAPL_FUNC(CAPL_INT32, EthReceivePacketInternal)(OnEthernetFunction* pCallback);

    /*!
    Use this function to register a CAPL function to receive Ethernet packets. The callback function is called, if a packet with the specified MAC addresses and Ethernet type is received. Use the flags to ignore the Ethernet type or the MAC address, e.g. flag = 7 receives all packets
    \param srcMacAddress Source MAC address
    \param dstMacAddress Destination MAC address
    \param ethernetType Ethernet type (16 Bit)
    \param callback CAPL callback function name
    \note long EthReceiveRawPacket( long flags, byte srcMacAddress[6], byte dstMacAddress[6], long ethernetType, char *callback );
    */
    CAPL_FUNC(CAPL_INT32, EthReceiveRawPacketInternal)(OnEthernetFunction* pCallback, CAPL_INT32 flags, CAPL_UINT8 srcMacAddress[], CAPL_UINT8 dstMacAddress[6], CAPL_INT32 ethernetType);
   
    /*!
    The function gets the returned data. It is only usable in a CAPL callback function that had been registered with EthReceiveRawPacket or EthReceivePacket
    \param offset Byte offset relative to the beginning of a data packet or the payload (see description above)
    \param buffer Buffer in which the requested data are copied
    \param bufferSize Number of requested bytes
	\return Number of copied data bytes
    \note long EthGetThisData( long offset, long bufferSize, byte buffer[] );
    */
    CAPL_FUNC(CAPL_INT32, EthGetThisData)(CAPL_INT32 offset, CAPL_INT32 bufferSize, void* pBuffer);

    /*!
    The function sends data on the specified socket. If the send operation does not complete immediately the operation is performed asynchronously and the function will return SOCKET_ERROR (-1)  
    \param socket The socket handle
    \param buffer The buffer containing the data to be sent
    \param size The size of the data to be sent
    \param data The struct containing the data to be sent
	\return 0: The function completed successfully
	\return WSA_INVALID_PARAMETER (87): The specified socket was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code. If the specific error code is 997 this just indicates asynchronous sending. All other results are sending errors
    \note long TcpSend( dword socket, char buffer[], dword size);
    */
	CAPL_FUNC(CAPL_INT32, TcpSendInternal)(OnSendFunction* pCallback, DWORD socket, const void* buffer, DWORD size);
    
	/*!
    The function sends data to the specified location
	\param socket The socket handle
	\param address The numerical IPv4 address of the destination in network-byte order
	\param port The port of the destination in host-byte order
	\param buffer The buffer containing the data to be sent
	\param size The size of the data to be sent
	\return 0: The function completed successfully
	\return	WSA_INVALID_PARAMETER (87): The specified socket was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code. If the specific error code is 997 this just indicates asynchronous sending. All other results are sending errors
	\note long UdpSendTo( dword socket, dword address, dword port, char buffer[], dword size)
	*/
	CAPL_FUNC(CAPL_INT32, UdpSendToIP4Internal)(OnSendFunction* pCallback, DWORD socket, DWORD address, DWORD port, const void* buffer, DWORD size); // form 1, 3, 4
    
	/*!
    The function sends data to the specified location
	\param socket The socket handle
	\param ipv6Address The local IPv6 address in a 16 byte array
	\param port The port of the destination in host-byte order
	\param buffer The buffer containing the data to be sent
	\param size The size of the data to be sent
	\return 0: The function completed successfully
	\return	WSA_INVALID_PARAMETER (87): The specified socket was invalid
	\return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code. If the specific error code is 997 this just indicates asynchronous sending. All other results are sending errors
	\note long UdpSendTo( dword socket, byte ipv6Address[], dword port, char buffer[], dword size)
	*/
	CAPL_FUNC(CAPL_INT32, UdpSendToIP6Internal)(OnSendFunction* pCallback, DWORD socket, const byte ipv6Address[], DWORD port, const void* buffer, DWORD size); // form 2, 5, 6

    /*!
    The function receives data into the specified buffer. If the receive operation does not complete immediately the operation is performed asynchronously and the function will return SOCKET_ERROR (-1) 
    \param socket The socket handle
    \param buffer The buffer used to store the incoming data
    \param size The size of the data buffer
	\return 0: The function completed successfully
    \return WSA_INVALID_PARAMETER (87): The specified socket was invalid
    \return SOCKET_ERROR (-1): The function failed. Call IpGetLastSocketError to get a more specific error code 	
    \note long UdpReceiveFrom( dword socket, char buffer[], dword size);
    */
    CAPL_FUNC(CAPL_INT32, UdpReceiveFromInternal)(OnUdpReceiveFromFunctionIPv4* pCallbackIPv4, OnUdpReceiveFromFunctionIPv6* pCallbackIPv6, DWORD socket, void* buffer, DWORD size);

    /*!
    The function receives data into the specified buffer
    \param socket The socket handle
    \param buffer The buffer used to store the incoming data
    \param size The size of the data buffer
	\return 0: The function completed successfully
    \return WSA_INVALID_PARAMETER (87): The specified socket was invalid
    \return SOCKET_ERROR (-1): The function failed, if IpGetLastSocketError returns an error code different of 997	
    \note long TcpReceive( dword socket, char buffer[], dword size);
    */   
    CAPL_FUNC(CAPL_INT32, TcpReceiveInternal)(OnTcpReceiveFunctionIPv4* pCallbackIPv4, OnTcpReceiveFunctionIPv6* pCallbackIPv6, DWORD socket, void* buffer, DWORD size);

	/*!
    Retrieves the fully qualified name of the computer
	\param buffer Space for the returned name
	\param bufferSize Length of the buffer
	\return 0 if the function completed successfully, else unequal 0
	\note long GetComputerName(char buffer[], dword bufferSize)
	*/
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

    CAPL_FUNC(CAPL_INT32, elCountInternal)(void* pData, CAPL_INT32 elemSize);

    } //namespace capl

#endif //CAPLPLUGINAPI_H__INCLUDED