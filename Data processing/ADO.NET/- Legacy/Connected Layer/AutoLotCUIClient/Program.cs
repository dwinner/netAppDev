using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using AutolotLibrary;

namespace AutoLotCUIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** The AutoLot Console UI *****\n");

            // Берем строку подключения из конфигурации.
            string cnStr =
              ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;
            bool userDone = false;

            InventoryDal inventoryDal = new InventoryDal();
            inventoryDal.OpenConnection(cnStr);

            #region Get user input            
            try
            {
                ShowInstructions();
                do
                {
                    Console.Write("\nPlease enter your command: ");
                    string userCommand = Console.ReadLine();
                    Console.WriteLine();
                   if (userCommand != null)
                      switch (userCommand.ToUpper())
                      {
                         case "I":
                            InsertNewCar(inventoryDal);
                            break;

                         case "U":
                            UpdateCarPetName(inventoryDal);
                            break;

                         case "D":
                            DeleteCar(inventoryDal);
                            break;

                         case "L":
                            // ListInventory(invDAL);
                            ListInventoryViaList(inventoryDal);
                            break;

                         case "S":
                            ShowInstructions();
                            break;

                         case "P":
                            LookUpPetName(inventoryDal);
                            break;

                         case "Q":
                            userDone = true;
                            break;

                         default:
                            Console.WriteLine("Bad data!  Try again");
                            break;
                      }
                }
                while (!userDone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                inventoryDal.CloseConnection();
            }
            #endregion
        }

        #region Show instructions
        private static void ShowInstructions()
        {
            Console.WriteLine("I: Inserts a new car.");
            Console.WriteLine("U: Updates an existing car.");
            Console.WriteLine("D: Deletes an existing car.");
            Console.WriteLine("L: Lists current inventory.");
            Console.WriteLine("S: Shows these instructions.");
            Console.WriteLine("P: Looks up pet name.");
            Console.WriteLine("Q: Quits program.");
        }

        #endregion

        #region List inventory
        private static void ListInventory(InventoryDal invDal)
        {            
            DataTable dt = invDal.GetAllInventoryAsDataTable();
            DisplayTable(dt);
        }

        private static void ListInventoryViaList(InventoryDal invDAL)
        {            
            List<NewCar> record = invDAL.GetAllInventoryAsList();

            foreach (NewCar c in record)
            {
                Console.WriteLine("CarID: {0}, Make: {1}, Color: {2}, PetName: {3}",
                    c.CarId, c.Make, c.Color, c.PetName);
            }
        }

        private static void DisplayTable(DataTable dt)
        {            
            for (int currentColumn = 0; currentColumn < dt.Columns.Count; currentColumn++)
            {
                Console.Write(dt.Columns[currentColumn].ColumnName + "\t");
            }
            Console.WriteLine("\n----------------------------------");
            
            for (int currentRow = 0; currentRow < dt.Rows.Count; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < dt.Columns.Count; currentColumn++)
                {
                    Console.Write(dt.Rows[currentRow][currentColumn] + "\t");
                }
                Console.WriteLine();
            }
        }

        #endregion

        #region Delete car
        private static void DeleteCar(InventoryDal invDal)
        {            
            Console.Write("Enter ID of Car to delete: ");
            int id = int.Parse(Console.ReadLine());
            
            try
            {
                invDal.DeleteCar(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Insert car
        private static void InsertNewCar(InventoryDal invDal)
        {            
            int newCarID;
            string newCarColor, newCarMake, newCarPetName;

            Console.Write("Enter Car ID: ");
            newCarID = int.Parse(Console.ReadLine());
            Console.Write("Enter Car Color: ");
            newCarColor = Console.ReadLine();
            Console.Write("Enter Car Make: ");
            newCarMake = Console.ReadLine();
            Console.Write("Enter Pet Name: ");
            newCarPetName = Console.ReadLine();
            
            NewCar c = new NewCar
            {
                CarId = newCarID,
                Color = newCarColor,
                Make = newCarMake,
                PetName = newCarPetName
            };
            invDal.InsertAuto(c);
        }
        #endregion

        #region Update pet name
        private static void UpdateCarPetName(InventoryDal invDal)
        {            
            int carID;
            string newCarPetName;

            Console.Write("Enter Car ID: ");
            carID = int.Parse(Console.ReadLine());
            Console.Write("Enter New Pet Name: ");
            newCarPetName = Console.ReadLine();
            
            invDal.UpdateCarPetName(carID, newCarPetName);
        }
        #endregion

        #region Look up name
        private static void LookUpPetName(InventoryDal invDal)
        {            
            Console.Write("Enter ID of Car to look up: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Petname of {0} is {1}.",
              id, invDal.LookUpPetName(id).TrimEnd());
        }
        #endregion
    }
}
