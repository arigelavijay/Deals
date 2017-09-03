using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace users.ViewModels.Signin
{
    public class loginVm
    {
        [Required(ErrorMessage = "Please enter username")]        
        public string userName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string password { get; set; }
    }
}