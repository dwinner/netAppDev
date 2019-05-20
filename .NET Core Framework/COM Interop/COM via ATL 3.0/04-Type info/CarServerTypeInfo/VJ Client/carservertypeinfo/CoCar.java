//
// Auto-generated using JActiveX.EXE 5.00.2918
//   ("D:\Program Files\Microsoft Visual Studio\VJ98\jactivex.exe"   /w /xi /X:rkc /l "E:\TEMP\jvc89.tmp" /nologo /d "E:\My Books\ATL 3.0 COM Programming\Labs\Chapter 04\VJ Client" "E:\My Books\ATL 3.0 COM Programming\Labs\Chapter 04\CarServerTypeInfo\Debug\CarServerTypeInfo.tlb")
//
// WARNING: Do not remove the comments that include "@com" directives.
// This source file must be compiled by a @com-aware compiler.
// If you are using the Microsoft Visual J++ compiler, you must use
// version 1.02.3920 or later. Previous versions will not issue an error
// but will not generate COM-enabled class files.
//

package carservertypeinfo;

import com.ms.com.*;
import com.ms.com.IUnknown;
import com.ms.com.Variant;

/** @com.class(classid=82BA20D2-F07D-11D2-B8E0-0020781238D4,DynamicCasts) 
    @com.interface(iid=A533DA32-D372-11D2-B8CF-0020781238D4, thread=AUTO) */
public class CoCar implements IUnknown,com.ms.com.NoAutoScripting,carservertypeinfo.ICreateCar,carservertypeinfo.IStats,carservertypeinfo.IEngine
{
  /** @com.method(vtoffset=0, addFlagsVtable=4)
      @com.parameters([in,type=STRING] petName) */
  public native void SetPetName(String petName);

  /** @com.method(vtoffset=1, addFlagsVtable=4)
      @com.parameters([in,type=I4] maxSp) */
  public native void SetMaxSpeed(int maxSp);


  public static final com.ms.com._Guid iid = new com.ms.com._Guid((int)0xa533da32, (short)0xd372, (short)0x11d2, (byte)0xb8, (byte)0xcf, (byte)0x0, (byte)0x20, (byte)0x78, (byte)0x12, (byte)0x38, (byte)0xd4);
  /** @com.method()
      @hidden */
  public native void DisplayStats();

  /** @com.method()
      @hidden */
  public native String GetPetName();

  /** @com.method()
      @hidden */
  public native void SpeedUp();

  /** @com.method()
      @hidden */
  public native int GetMaxSpeed();

  /** @com.method()
      @hidden */
  public native int GetCurSpeed();


  public static final com.ms.com._Guid clsid = new com.ms.com._Guid((int)0x82ba20d2, (short)0xf07d, (short)0x11d2, (byte)0xb8, (byte)0xe0, (byte)0x0, (byte)0x20, (byte)0x78, (byte)0x12, (byte)0x38, (byte)0xd4);
}
