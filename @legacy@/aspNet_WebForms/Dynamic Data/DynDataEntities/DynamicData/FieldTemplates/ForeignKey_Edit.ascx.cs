﻿using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynDataEntities
{
   public partial class ForeignKey_EditField : System.Web.DynamicData.FieldTemplateUserControl
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (DropDownList1.Items.Count == 0)
         {
            if (Mode == DataBoundControlMode.Insert || !Column.IsRequired)
            {
               DropDownList1.Items.Add(new ListItem("[Не задано]", ""));
            }
            PopulateListControl(DropDownList1);
         }

         SetUpValidator(RequiredFieldValidator1);
         SetUpValidator(DynamicValidator1);
      }

      protected override void OnDataBinding(EventArgs e)
      {
         base.OnDataBinding(e);

         string selectedValueString = GetSelectedValueString();
         ListItem item = DropDownList1.Items.FindByValue(selectedValueString);
         if (item != null)
         {
            DropDownList1.SelectedValue = selectedValueString;
         }

      }

      protected override void ExtractValues(IOrderedDictionary dictionary)
      {
         // Если это пустая строка, измените ее на null
         string value = DropDownList1.SelectedValue;
         if (String.IsNullOrEmpty(value))
         {
            value = null;
         }

         ExtractForeignKey(dictionary, value);
      }

      public override Control DataControl
      {
         get
         {
            return DropDownList1;
         }
      }

   }
}
