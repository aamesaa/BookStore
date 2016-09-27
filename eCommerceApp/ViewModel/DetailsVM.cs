using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ViewModel
{
    public class DetailsVM
    {
        [Key]
        public int RecordID { get; set; }

        [StringLength(50)]
        public string CartID { get; set; }

        public int Quantity { get; set; }

        public int BookID { get; set; }


        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

     
        public int AuthorID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }


        [StringLength(50)]
        public string LastName { get; set; }


        public int CategoryID { get; set; }
        [StringLength(50)]
        public string CategoryName { get; set; }


        [StringLength(10)]
        public string ISBN { get; set; }

        [StringLength(50)]
        public string CoverImage { get; set; }



        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Publisher { get; set; }
    }
}