using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Models
{
    class Sale : BaseModel
    {
        [Required]
        [Range(0, double.MaxValue)] // This means it cannot be a negative value
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual SalesPerson Person { get; set; } // virtual allows Entity Framework to add extra code and track when that property has been changed

        [Required]
        public int PersonId { get; set; }

        //These two above properties attache the sales person to the sale
        //In the database it will create one column for the person ID
        //When we pull the sale info from the database we can optionally pull the Sales Person info as well
        // We named the Sales Person "Person" and Person ID "Person Id" so that Entity Framework can match
        // the Person property with the Person ID property and know that PersonId property is the property for [that]Person

        // If the Person for the Sale was optional (allows null) we would do the following
        // public int? PersonId { get; set; } 

        public virtual SalesRegion Region { get; set; }

        [Required]
        public int RegionId { get; set; }

    }
}
