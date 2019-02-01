/**
 * Возможность преобразования и ковариантность
 */


namespace _03_ArrayCovariance
{
   class Program
   {
      static void Main(string[] args)
      {
         Dog[] dogs = new Dog[3];
         Cat[] cats = new Cat[3];
         Animal[] animals = dogs;
         Animal[] moreAnimals = cats;
      }
   }

   class Animal { }
   class Dog : Animal { }
   class Cat : Animal { }
}
