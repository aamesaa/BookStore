using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ViewModel
{
    public class OrderVM
    {
        public int OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShipDate { get; set; }
        //public int Id { get; set; }

   

        public int? BookID { get; set; }
        public string Title { get; set; }
        public int? Quantity { get; set; }


        public decimal? Price { get; set; }

    }

}