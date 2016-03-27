using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpreter
{
   public class ContactList
   {
      public IList<IContact> Contacts
      {
         get { return new List<IContact>(_contacts); }
         set { _contacts = value; }
      }
      private IList<IContact> _contacts = new List<IContact>();

      public IContact[] ContactsArray { get { return _contacts.ToArray(); } }

      public IList<IContact> ContactsMatchingExpression(IExpression expression, IContext context, object key)
      {
         IList<IContact> contacts=new List<IContact>();
         foreach (var contact in _contacts)
         {
            context.AddVariable(key, contact);
            expression.Interpret(context);
            object interpretResult = context.Get(expression);
            if (interpretResult != null && interpretResult.Equals(true))
            {
               contacts.Add(contact);
            }
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

      public bool Remove(IContact contact)
      {
         return _contacts.Remove(contact);
      }

      public override string ToString()
      {
         return _contacts.Aggregate(string.Empty, (current, contact) => current + (contact + Environment.NewLine));
      }
   }
}