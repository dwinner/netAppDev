/**
 * Разработка свободных грамматик
 */

using System;
using System.Linq;
using static System.Console;

namespace Interpreter
{
   internal static class Program
   {
      private static void Main()
      {
         var candidates = MakeContactList();
         IContext context = new ContextImpl();

         IContact testContact = new ContactImpl();
         var lastNameVariableExpression = new VariableExpression(testContact, "LastName");
         var uConstantExpression = new ConstantExpression("u");
         var containsExpression = new ContainsExpression(lastNameVariableExpression, uConstantExpression);

         var matchingExpression = candidates.ContactsMatchingExpression(containsExpression, context, testContact);
         Array.ForEach(matchingExpression.ToArray(), WriteLine);
         WriteLine();

         var titleVariableExpression = new VariableExpression(testContact, "Title");
         var titleConstantExpression = new ConstantExpression("LT");
         var equalsExpression = new EqualsExpression(titleVariableExpression, titleConstantExpression);

         var expression = candidates.ContactsMatchingExpression(equalsExpression, context, testContact);
         Array.ForEach(expression.ToArray(), WriteLine);
         WriteLine();

         var lastNameExpression = new VariableExpression(testContact, "LastName");
         var lastNameConstant = new ConstantExpression("S");
         var lastNameContains = new ContainsExpression(lastNameExpression, lastNameConstant);

         var andExpression = new AndExpression(equalsExpression, lastNameContains);

         var contactsMatchingExpression = candidates.ContactsMatchingExpression(andExpression, context, testContact);
         WriteLine(contactsMatchingExpression);
      }

      private static ContactList MakeContactList()
      {
         var returnList = new ContactList();
         returnList.Add(new ContactImpl("James", "Kirk", "Captain", "USS Enterprise"));
         returnList.Add(new ContactImpl("Mr.", "Spock", "Science Officer", "USS Enterprise"));
         returnList.Add(new ContactImpl("LT", "Uhura", "LT", "USS Enterprise"));
         returnList.Add(new ContactImpl("LT", "Sulu", "LT", "USS Enterprise"));
         returnList.Add(new ContactImpl("Ensign", "Checkov", "Ensign", "USS Enterprise"));
         returnList.Add(new ContactImpl("Dr.", "McCoy", "Ship's Doctor", "USS Enterprise"));
         returnList.Add(new ContactImpl("Montgomery", "Scott", "LT", "USS Enterprise"));

         return returnList;
      }
   }
}