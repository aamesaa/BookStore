namespace eCommerceApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShoppingCart
    {
         CommerceModel db = new CommerceModel();
        [Key]
        public int RecordID { get; set; }

        [StringLength(50)]
        public string CartID { get; set; }

        public int Quantity { get; set; }

        public int BookID { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Book Book { get; set; }
       

    }
}
