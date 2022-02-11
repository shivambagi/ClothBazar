using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public interface ISharedInsuranceService
    {
        List<Gender> GetGenders();
        List<Frequency> GetFrequencies();
        List<InsuranceProduct> GetInsuranceProducts(string type);
        List<int> GetPPT(int productid);

        List<int> GetTerm(int productid,int ppt);
    }
    public class SharedInsuranceService : ISharedInsuranceService
    {
        CBContext context;
        public SharedInsuranceService()
        {
            context = new CBContext();
        }

        public List<Frequency> GetFrequencies()
        {
            return context.Frequencies.ToList();
        }

        public List<Gender> GetGenders()
        {
            return context.Genders.ToList();
        }

        public List<InsuranceProduct> GetInsuranceProducts(string type)
        {
            return context.InsuranceProducts.Where(x=>x.ProductType.ToLower() == type.ToLower()).ToList();
        }

        public List<int> GetPPT(int productid)
        {            
            if(productid == 1) //SIGNATURE
            {
                return new List<int> { 5, 7, 10 };
            }
            else if (productid == 2) //LTC
            {
                return new List<int> { 10, 12, 15 };
            }
            else if (productid == 3) //EASYRETIREMENT
            {
                return new List<int> { 10, 12, 15 };
            }
            else
            {
                return new List<int> {1};
            }
            
        }

        public List<int> GetTerm(int productid,int ppt)
        {
            if (productid == 1) //SIGNATURE
            {
                if(ppt == 5)
                {
                    return new List<int> { 5, 6, 7, 8, 9, 10 };
                }
                else if(ppt == 7)
                {
                    return new List<int> { 7, 8, 9, 10, 11, 12 };
                }
                else if(ppt == 10)
                {
                    return new List<int> { 10, 11, 12, 13, 14, 15 };
                }
                else
                {
                    return new List<int> { 1 };
                }                
            }
            else if (productid == 2) //LTC
            {
                return new List<int> { 10, 11, 12, 13, 14, 15 };
            }
            else if (productid == 3) //EASYRETIREMENT
            {
                return new List<int> { 15, 16, 17, 18, 19, 20 };
            }
            else
            {
                return new List<int> { 1 };
            }
        }
    }

    //public class SharedAccess
    //{
    //    private ISharedInsuranceService _iSharedInsuranceService;

    //    public SharedAccess(ISharedInsuranceService iSharedInsuranceService)
    //    {
    //        _iSharedInsuranceService = iSharedInsuranceService;
    //    }

    //    public List<Frequency> GetFrequenciesAcc()
    //    {
    //        return _iSharedInsuranceService.GetFrequencies();
    //    }
    //}
}
