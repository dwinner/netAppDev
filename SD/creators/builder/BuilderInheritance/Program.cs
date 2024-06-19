using BuilderInheritance;

var me = Person.New
   .Called("Dmitri")
   .WorksAs("Quant")
   .Born(DateTime.UtcNow)
   .Build();
Console.WriteLine(me);

/*
internal class SomeBuilder : PersonBirthDateBuilder<SomeBuilder>
{
}*/
