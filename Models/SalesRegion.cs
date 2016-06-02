using SalesApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Models
{
    class SalesRegion : BaseModel, IActive 
    {
        [Required]
        public bool Active { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        public string Code { get; set; }

        //Defining that the sales region has multiple sales people within it. 

        // The Below would work for an online application but Window Forms have a data binding problem
        //Microsoft recomends creating an observable list source
        // public virtual ICollection<SalesPerson> People { get; set; }

        public virtual ObservableListSource<SalesPerson> People { get; set; }

        public virtual ObservableListSource<Sale> Sales { get; set; } // One to Many relationship 
        // Sales Region has multiple sales within it, but one sale is a stand alone item, hence one-to-many

        [Required]
        [Range(0, double.MaxValue)] // This means it cannot be a negative value
        public decimal SalesTarget { get; set; }
    }
}
