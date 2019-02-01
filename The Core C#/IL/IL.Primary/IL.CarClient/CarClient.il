.assembly extern mscorlib
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}

.assembly extern IL.Cars
{
	.ver 1:0:0:0
}

.assembly CarClient
{
	.hash algorithm 0x00008004
	.ver 1:0:0:0
}

.module CarClient.exe

.namespace CarClient
{
	.class private auto ansi beforefieldinit Program
		extends [mscorlib]System.Object
	{
		.method private hidebysig static void Main(string[] args) cil managed
		{
			.entrypoint
			.maxstack 8

			.locals init ([0] class [IL.Cars]Il.Cars.IlCar myCilCar)
			ldc.i4 55
			ldstr "Junior"

			newobj instance void [IL.Cars]Il.Cars.IlCar::.ctor(int32, string)
			stloc.0
			ldloc.0

			call void [Il.Cars]Il.Cars.IlCarInfo::Display(class [Il.Cars]Il.Cars.IlCar)

			ret
		}
	}
}