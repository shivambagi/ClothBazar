using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Insurance : BaseEntityInsurance
    {
        [Required]
        [Display(Name = "Premium Payment Term")]
        public int PPT { get; set; }
        [Required]
        [Display(Name = "Policy Term")]
        public int Term { get; set; }
        [Required]
        [Display(Name = "Sum Assured")]
        public double SumAssured { get; set; }
        [Required]
        [Display(Name = "Modal Premium")]
        public double ModalPremium { get; set; }
        [Display(Name = "Annual Premium")]
        public double AnnualPremium { get; set; }
        [Required]
        [Display(Name = "Premium Payment Frequency")]
        public Frequency Frequency { get; set; }
        public InsuranceProduct InsuranceProduct { get; set; }
    }

    public class ULIP : Insurance
    {
        public string Goal { get; set; }
    }

    public class Traditional : Insurance
    {
        public string PlanOption { get; set; }
    }

    public class Term : Insurance
    {
        public double ADB { get; set; }
        public double CI { get; set; }
    }


}
