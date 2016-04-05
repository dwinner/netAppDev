.assembly extern mscorlib
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}

// Наша сборка
.assembly IL.Types
{
	.ver 1:0:0:0
}

// Модуль нашей однофайловой сборки
.module IL.Types.dll

// Наша сборка имеет единственное пространство имен
.namespace IL.Types
{
    // Определение интерфейса
	.class public interface IMyInterface
	{
	}

	// Наследование интерфейса
	.class public interface IMyOtherInterface
		implements IL.Types.IMyInterface
	{
	}

	// Предполагается, что базовым классом является System.Object
	.class public MyBaseClass
	{
		// Определение полей данных
		.field private string stringField	// = "hello!"
		.field private int32 intField		// = int32(42)

		// Определение свойств
		.method public hidebysig specialname instance string get_TheString() cil managed
		{
			// TODO: Добавить код реализации
		}

		.method public hidebysig specialname instance void set_TheString(string 'value') cil managed
		{
			// TODO: Добавить код реализации
		}

		.property instance string TheString()
		{
			.get instance string IL.Types.MyBaseClass::get_TheString()
			.set instance void IL.Types.MyBaseClass::set_TheString(string)
		}

		// Определение конструкторов
		.method public hidebysig specialname rtspecialname instance void .ctor(string aString, int32 anInt) cil managed
		{
			// TODO: Добавить код реализации
		}				
	}

	// Наследование класса
	.class public MyDerivedClass
		extends IL.Types.MyBaseClass
		implements IL.Types.IMyInterface
	{
	}

	// При определении струкруты должен быть указан атрибут sealed
	.class public sealed MyStruct
		extends [mscorlib]System.ValueType
	{
	}

	// Сокращенное объявление структуры
	.class public value MyStruct2
	{
	}

	// Определение перечисления
	.class public sealed MyEnum
		extends [mscorlib]System.Enum
	{
		.field public static literal valuetype IL.Types.MyEnum A = int32(0)
		.field public static literal valuetype IL.Types.MyEnum B = int32(1)
		.field public static literal valuetype IL.Types.MyEnum C = int32(2)
	}

	// Сокращенный вариант определения перечисления
	.class public sealed enum MyEnum2
	{
	}
}