using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class ResultULIPViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool Smoker { get; set; }
        public int Gender { get; set; }
        public List<int> PremiumPaymentTerm { get; set; }
        public List<int> PolicyTerm { get; set; }
        [Required]
        public double SumAssured { get; set; }
        [Required]
        public double ModalPremium { get; set; }
        public double AnnualPremium { get; set; }
        public int Frequency { get; set; }
        public string Goal { get; set; }
        public int InsuranceProduct { get; set; }
    }

        public class ULIPViewModel
    {        
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Smoker { get; set; }
        public List<Gender> Gender { get; set; }
        public List<int> PremiumPaymentTerm { get; set; }
        public List<int> PolicyTerm { get; set; }
        public double SumAssured { get; set; }
        public double ModalPremium { get; set; }
        public double AnnualPremium { get; set; }
        public List<Frequency> Frequency { get; set; }
        public string Goal { get; set; }
        public List<InsuranceProduct> InsuranceProduct { get; set; }



        public int ProductID { get; set; }
        public int PPT { get; set; }
        public DateTime DOB { get; set; }
        public bool Smoke { get; set; }
        public int GenderID { get; set; }
    }

    public class TraditionalViewModel
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Smoker { get; set; }
        public List<Gender> Gender { get; set; }
        public int PremiumPaymentTerm { get; set; }
        public int PolicyTerm { get; set; }
        public double SumAssured { get; set; }
        public double ModalPremium { get; set; }
        public double AnnualPremium { get; set; }
        public List<Frequency> Frequency { get; set; }
        public string PlanOption { get; set; }
        public List<InsuranceProduct> InsuranceProduct { get; set; }
    }

    public class TermViewModel
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Smoker { get; set; }
        public List<Gender> Gender { get; set; }
        public int PremiumPaymentTerm { get; set; }
        public int PolicyTerm { get; set; }
        public double SumAssured { get; set; }
        public double ModalPremium { get; set; }
        public double AnnualPremium { get; set; }
        public List<Frequency> Frequency { get; set; }
        public List<InsuranceProduct> InsuranceProduct { get; set; }
        public double ADBSumAssured { get; set; }
        public double CISumAssured { get; set; }
    }
}