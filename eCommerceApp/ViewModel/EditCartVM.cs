using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerceApp.ViewModel
{
    public class EditCartVM
    {
        public int RecordID { get; set; }

        public int Quantity { get; set; }
        public int BookID { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
    }
}