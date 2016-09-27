namespace eCommerceApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

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

        public virtual Author Author { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
