using ClothBazar.Services;
using ClothBazar.Web.CustomsFilter;
using ClothBazar.Web.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class InsuranceController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InsuranceController));

        ISharedInsuranceService _iSharedInsuranceService;
        IULIPValidationService _iULIPValidationService;
        public InsuranceController(ISharedInsuranceService iSharedInsuranceService, IULIPValidationService iULIPValidationService)
        {
            _iSharedInsuranceService = iSharedInsuranceService;
            _iULIPValidationService = iULIPValidationService;
        }
        // GET: Insurance
        public ActionResult Index()
        {
            return View();
        }

        [CustomException]
        public ActionResult ULIP(string name,int? productid,int? ppt,DateTime? dob,bool? smoke,int? genderid)
        {
            ULIPViewModel model = new ULIPViewModel();

            productid = productid == null ? 1 : productid;
            ppt = productid == null ? 5 : ppt == null ? 5 : ppt;
            genderid = genderid == null ? 1 : genderid;
            smoke = smoke == null ? false : true;
            dob = dob == null ? new DateTime(1980, 01, 01) : dob;

            model.Name = name;
            model.ProductID = productid.Value;
            model.PPT = ppt.Value;
            model.DOB = dob.Value;
            model.Smoke = smoke.Value;
            model.GenderID = genderid.Value; 

            model.PremiumPaymentTerm = _iSharedInsuranceService.GetPPT(productid.Value);
            model.PolicyTerm = _iSharedInsuranceService.GetTerm(productid.Value,ppt.Value);
            model.Gender = _iSharedInsuranceService.GetGenders();
            model.Frequency = _iSharedInsuranceService.GetFrequencies();
            model.InsuranceProduct = _iSharedInsuranceService.GetInsuranceProducts("ULIP");
            
            return PartialView(model);
        }
        public ActionResult TERM()
        {
            TermViewModel model = new TermViewModel();
            
            model.Gender = _iSharedInsuranceService.GetGenders();
            model.Frequency = _iSharedInsuranceService.GetFrequencies();
            model.InsuranceProduct = _iSharedInsuranceService.GetInsuranceProducts("TERM");

            return PartialView(model);
        }
        public ActionResult TRADITIONAL()
        {
            TraditionalViewModel model = new TraditionalViewModel();

            model.Gender = _iSharedInsuranceService.GetGenders();
            model.Frequency = _iSharedInsuranceService.GetFrequencies();
            model.InsuranceProduct = _iSharedInsuranceService.GetInsuranceProducts("TRADITIONAL");

            return PartialView(model);
        }

        public ActionResult Result(ResultULIPViewModel model)
        {
            Log.Info("Request - " + GetModeldataInJson(model));

            if (ModelState.IsValid)
            {
                string error = _iULIPValidationService.ULIPValidation(model);
                if(!string.IsNullOrEmpty(error))
                {
                    JsonResult result = new JsonResult();
                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                    result.Data = new { Success = false, errormessage = error };
                    Log.Warn("Response - " + "Validation Error - " + error + Environment.NewLine);
                    return result;
                }
                else
                {
                    Log.Info("Response - " + GetModeldataInJson(model) + Environment.NewLine);
                    return PartialView(model);
                }                
            }
            else
            {
                Log.Error(Json(model));
                return PartialView(model);
            }
                
        }

        [ChildActionOnly]
        public string GetModeldataInJson(object model)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return json;
        }
    }
}