//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EvropskoPrvenstvoUKosarci2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Utakmice
    {
        public int UtakmiceId { get; set; }
        public int ReprezentacijaDomacinId { get; set; }
        public int ReprezentacijaGostId { get; set; }
        public int GrupaId { get; set; }
        public int HalaId { get; set; }
        [Display(Name = "Vreme odigravanja")]
        public System.DateTime VremeOdigravanja { get; set; }
        [Display(Name = "Q1")]
        public Nullable<int> Q1_Domacin { get; set; }
        public Nullable<int> Q1_Gost { get; set; }
        [Display(Name = "Q2")]
        public Nullable<int> Q2_Domacin { get; set; }
        public Nullable<int> Q2_Gost { get; set; }
        [Display(Name = "Q3")]
        public Nullable<int> Q3_Domacin { get; set; }
        public Nullable<int> Q3_Gost { get; set; }
        [Display(Name = "Q4")]
        public Nullable<int> Q4_Domacin { get; set; }
        public Nullable<int> Q4_Gost { get; set; }
        [Display(Name = "Rezultat")]
        public Nullable<int> FinalDomacin { get; set; }
        public Nullable<int> FinalGost { get; set; }
    
        public virtual Grupa Grupa { get; set; }
        public virtual Hala Hala { get; set; }
        public virtual Reprezentacija Reprezentacija { get; set; }
        public virtual Reprezentacija Reprezentacija1 { get; set; }
    }
}