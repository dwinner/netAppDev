using System;
using AutolotLibrary;

namespace AdoNetTransaction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Simple Transaction Example *****\n");
            
            bool throwEx = true;
            string userAnswer = string.Empty;

            Console.Write("Do you want to throw an exception (Y or N): ");
            userAnswer = Console.ReadLine();
            if (userAnswer != null && userAnswer.ToLower() == "n")
            {
                throwEx = false;
            }

            InventoryDal dal = new InventoryDal();
            dal.OpenConnection(@"Data Source=Hi-Tech-PC;Initial Catalog=AutoLot;Integrated Security=True;Pooling=False");

            dal.ProcessCreditRisk(333, throwEx);
            Console.WriteLine("Check CreditRisk table for results");
            Console.ReadLine();
        }
    }
}
