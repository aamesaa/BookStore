using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerceApp.Helper
{
    public class MsgBox
    {
        public static string GetMsg(string jenis, string caption,  string ket)
        {
            return string.Format(@"<div class='alert alert-{0}'>
                <a href = '#' class='close' data-dismiss='alert' aria-label='close'>&times;</a> <strong>{1}</strong>{2} </div>",jenis,caption, ket);
        }




    }
}