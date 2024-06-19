using BuilderFacets;
using static System.Console;

var personBuilder = new PersonBuilder();
Person person = personBuilder
   .Lives
   .At("123 London Road")
   .In("London")
   .WithPostcode("SW12BC")
   .Works
   .At("Fabrikam")
   .As("Engineer")
   .Earning(123000);

WriteLine(person);