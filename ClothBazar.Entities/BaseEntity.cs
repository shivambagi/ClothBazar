using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        [Required]
        [MinLength(5),MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }

    public class BaseEntityInsurance
    {
        private int _ID { get; set; }
        [Required]
        [MinLength(5), MaxLength(50)]
        private string _Name { get; set; }
        [Required]
        private DateTime _DateOfBirth { get; set; }
        private bool _Smoker { get; set; }
        private Gender _Gender { get; set; }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        public bool Smoker
        {
            get { return _Smoker; }
            set { _Smoker = value; }
        }
        public Gender Gender
        { 
            get { return _Gender; }
            set { _Gender = value; } 
        }
    }

    public class Gender
    {
        public int ID { get; set; }
        public string GenderName { get; set; }
    }

    public class Frequency
    {
        public int ID { get; set; }
        public string FrequencyName { get; set; }
    }

    public class InsuranceProduct
    {
        public int ID { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }

    }
}
