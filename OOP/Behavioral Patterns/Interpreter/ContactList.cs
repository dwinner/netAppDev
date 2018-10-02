using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter
{
   public class ContactList
   {
      private readonly IList<IContact> _contacts = new List<IContact>();

      public IList<IContact> ContactsMatchingExpression(
         IExpression expression, IContext context, object key)
      {
         IList<IContact> contacts = new List<IContact>();
         foreach (var contact in _contacts)
         {
            context.AddVariable(key, contact);
            expression.Interpret(context);
            var interpretResult = context.Get(expression);
            if (interpretResult != null && interpretResult.Equals(true))
               contacts.Add(contact);
         }

         return contacts;
      }

      public bool Add(IContact contact)
      {
         if (!_contacts.Contains(contact))
         {
            _contacts.Add(contact);
            return true;
         }

         return false;
      }

      public override string ToString()
      {
         return _contacts.Aggregate(
            string.Empty,
            (current, contact) => current + (contact + Environment.NewLine));
      }
   }
}