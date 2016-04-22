using System;
using System.Linq;
using System.Transactions;

namespace Gallery.DataLevel.Orm.Extensions
{
   /// <summary>
   /// Операции с БД Account через OR/M
   /// </summary>
   public static class AccountDbUtils
   {
      /// <summary>
      /// Вставка вошедшего пользователя в таблицу Account для галереи изображений, если его еще не существовало
      /// </summary>
      /// <param name="loggedInUser">Имя, указанное при логине</param>
      /// <exception cref="InvalidOperationException">Если таких аккаунтов больше одного</exception>
      /// <remarks>
      ///   Маловероятны, но возможны исключения при конфликтах СУБД, вызванных обновлением (типа OptimisticConcurrencyException)
      /// </remarks>      
      public static void InsertIfNotExists(string loggedInUser)
      {
         using (var galleryEntities = new ImageGalleryEntities())
         {
            using (var transactionScope = new TransactionScope())
            {
               Account userAccount = (from account in galleryEntities.Accounts
                                      where account.UserName == loggedInUser
                                      select account).SingleOrDefault();

               if (userAccount == null)
               {
                  var newAccount = new Account { UserName = loggedInUser };
                  galleryEntities.Accounts.Add(newAccount);
                  galleryEntities.SaveChanges();
               }
               
               transactionScope.Complete();
            }
         }
      }
   }
}