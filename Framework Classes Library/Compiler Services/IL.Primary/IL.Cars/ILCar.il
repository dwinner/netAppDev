// Добавление ссылок на mscorlib.dll и System.Windows.Forms.dll
.assembly extern mscorlib
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}

.assembly extern System.Windows.Forms
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}

// Определение однофайловой сборки
.assembly IL.Cars
{
	.hash algorithm 0x00008005
	.ver 1:0:0:0
}

.module IL.Cars.dll

// Реализация типа IlCar
.namespace Il.Cars
{
	.class public auto ansi beforefieldinit IlCar
		extends [mscorlib]System.Object
	{
		// Определение полей данных в IlCar
		.field public string petName
		.field public int32 currSpeed

		// Определение специального конструктора, позволяющего
		// вызывающему коду присваивать полям данных определенные значения
		.method public hidebysig specialname rtspecialname instance void .ctor(int32 c, string p) cil managed
		{
			.maxstack 8

			// Загрузка первого аргумента в стек и вызов конструктора базового класса
			ldarg.0	// Объект this
			call instance void [mscorlib]System.Object::.ctor()

			// Загрузки первого и второго аргументов в стек
			ldarg.0	// Объект this
			ldarg.1	// Аргумент int32

			// Сохранение самого верхнего элемента из стека (int32) в поле currSpeed
			stfld int32 Il.Cars.IlCar::currSpeed

			// Загрузка аргумента string и его сохранение в поле petName
			ldarg.0	// Объект this
			ldarg.2	// Объект string
			stfld string Il.Cars.IlCar::petName

			ret
		}
	}

	.class public auto ansi beforefieldinit IlCarInfo
		extends [mscorlib]System.Object
	{
		.method public hidebysig static void Display(class Il.Cars.IlCar c) cil managed
		{
			.maxstack 8

			// Необходима какая-нибудь локальная строковая переменная
			.locals init ([0] string caption)

			// Загрузка строки и входного параметра IlCar в стек
			ldstr "{0}'s speed is:"
			//ldstr "Speed {0} is: "
			ldarg.0

			// Помещение в стек значения поля petName из IlCar и вызов статического метода String.Format()
			ldfld string Il.Cars.IlCar::petName
			call string [mscorlib]System.String::Format(string, object)
			stloc.0

			// Загрузка значения поля currSpeed и получение его строкового представления (обратите внимание на вызов ToString())
			ldarg.0
			ldflda int32 Il.Cars.IlCar::currSpeed
			call instance string [mscorlib]System.Int32::ToString()
			ldloc.0

			// Вызов метода MessageBox.Show() с загруженными значениями
			call valuetype [System.Windows.Forms]System.Windows.Forms.DialogResult [System.Windows.Forms]System.Windows.Forms.MessageBox::Show(string, string)

			pop
			ret
		}
	}
}