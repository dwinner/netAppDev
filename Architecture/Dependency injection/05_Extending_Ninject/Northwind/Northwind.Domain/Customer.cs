using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Northwind.Domain
{
    public class Customer
    {
        [Required, UniqueCustomerId]
        public string ID { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }
    }


}
