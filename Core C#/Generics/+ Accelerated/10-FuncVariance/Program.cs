/**
 * Вариантность функционалов
 */

using System;

namespace _10_FuncVariance
{
   delegate void Task<out T>(Action<T> action);

   class Program
   {
      static void Main()
      {
         Action<Animal> petAnimal = a =>
            {
               Console.Write("Любимое домашнее животное и его реакция: ");
               a.ShowAffection();
            };
         // Note: Правило контравариантности в действии!
         // Поскольку Dog -> Animal и
         // Action<Animal> -> Action<Dog>,
         // то следующее присваивание контравариантно
         Action<Dog> petDog = petAnimal;
         petDog(new Dog());
         Task<Dog> doStaffToADog = BuildTask<Dog>();
         doStaffToADog(petDog);
         // Но задача, принимающая действие над dog,
         // также может принимать действие над animal
         doStaffToADog(petAnimal);
         // Следовательно, вполне логично для Task<Dog>
         // быть неявно преобразуемым в Task<Animal>
         //
         // Note: Ковариантность в действии!
         //
         // Поскольку Dog -> Animal и Task<Dog> -> Task<Animal>,
         // то следующее присваивание ковариантно:
         Task<Animal> doStaffToAnAnimal = doStaffToADog;
         doStaffToAnAnimal(petAnimal);
         doStaffToADog(petAnimal);

         Console.ReadKey();
      }

      static Task<T> BuildTask<T>() where T : new()
      {
         return action => action(new T());
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
