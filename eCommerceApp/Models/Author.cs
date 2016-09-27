namespace eCommerceApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Author
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorID { get; set; }

        [Required(ErrorMessage ="First Name Required")]
        [DisplayName("First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
