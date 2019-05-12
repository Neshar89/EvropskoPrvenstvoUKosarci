using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //d
using System.Linq;
using System.Web;

namespace EvropskoPrvenstvoUKosarci2.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Morate uneti korisničko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Morate uneti lozinku")]
        public string Sifra { get; set; }
    }
}