/**
 * Вариантность делегатов
 */

using System;

namespace _09_DelegateVariance
{
   class Program
   {
      static void Main()
      {
         Action<Animal> petAnimal = a =>
            {
               Console.Write("Любимое домашнее животное и его реакция: ");
               a.ShowAffection();
            };
         /*
          * Правило контравариантности в действии!
          * 
          * Поскольку Dog -> Animal и
          * Action<Animal> -> Action<Dog>,
          * то следующее присваивание контравариантно по определению
          */
         Action<Dog> petDog = petAnimal;
         petDog(new Dog());

         Console.ReadKey();
      }
   }

   class Animal
   {
      public virtual void ShowAffection()
      {
         Console.WriteLine("Реакция неизвестна");
      }      
   }

   class Dog : Animal
   {
      public override void ShowAffection()
      {
         Console.WriteLine("Виляние хвостом...");
      }
   }
}
