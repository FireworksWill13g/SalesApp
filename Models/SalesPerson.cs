using SalesApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Models
{
    class SalesPerson : BaseModel, IActive  
    {
        [Required]
        public bool Active { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public virtual SalesRegion Region { get; set; }

        [Required]
        public int RegionId { get; set; }

        public virtual ObservableListSource<Sale> Sales { get; set; }
        // Each sales person has multiple sales, again one-to-many relationship

        [Required]
        [Range(0, double.MaxValue)] // This means it cannot be a negative value
        public decimal SalesTarget { get; set; }

    }
}
