using System.Reflection.Emit;

var dynMeth = new DynamicMethod("Test", null, null, typeof(void));
var gen = dynMeth.GetILGenerator();

// try                               { throw new NotSupportedException(); }
// catch (NotSupportedException ex)  { Console.WriteLine (ex.Message);    }
// finally                           { Console.WriteLine ("Finally");     }

var getMessageProp = typeof(NotSupportedException).GetProperty("Message").GetGetMethod();
var writeLineString = typeof(Console).GetMethod("WriteLine", new[] { typeof(object) });
gen.BeginExceptionBlock();
{
   var ci = typeof(NotSupportedException).GetConstructor(Type.EmptyTypes);
   gen.Emit(OpCodes.Newobj, ci);
   gen.Emit(OpCodes.Throw);
}
gen.BeginCatchBlock(typeof(NotSupportedException));
{
   gen.Emit(OpCodes.Callvirt, getMessageProp);
   gen.Emit(OpCodes.Call, writeLineString);
}
gen.BeginFinallyBlock();
{
   gen.EmitWriteLine("Finally");
}
gen.EndExceptionBlock();

gen.Emit(OpCodes.Ret);

dynMeth.Invoke(null, null); // Hello, world!