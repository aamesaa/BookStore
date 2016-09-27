using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace eCommerceApp.ViewModel
{
    public class ShoppingCartVM
    {
        [Key]
        public int RecordID { get; set; }

        [StringLength(50)]
        public string CartID { get; set; }

        public int Quantity { get; set; }

        public int BookID { get; set; }

        public DateTime DateCreated { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

    }
}