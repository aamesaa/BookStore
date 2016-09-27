using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApp.ViewModel
{
    public class BookVM
    {
        public int BookID { get; set; }

        [Required]
        [DisplayName("Author")]
        public int AuthorID { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Publication Date")]
        public DateTime? PublicationDate { get; set; }

        [StringLength(10)]
        public string ISBN { get; set; }

        [StringLength(50)]
        public string CoverImage { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [DisplayName("First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(50)]
        public string LastName { get; set; }


    }
}