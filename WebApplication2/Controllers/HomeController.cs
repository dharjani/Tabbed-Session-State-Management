using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using WebApplication2.Models;
using WebApplication2.Filters;
using WebApplication2.App_Start;
using System.Web.Routing;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            userSession = System.Web.HttpContext.Current.Session;
            if (userSession[SessionTableKey] != null)
            {
                table = userSession[SessionTableKey] as Hashtable;
            }
            else
            {
                table = new Hashtable();
            }
            //uniquePageId = userSession.SessionID + "_" + Guid.NewGuid().ToString();
        }

        [HttpGet]
        [AllowAnonymous]
        [SessionFilter]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [SessionFilter]
        public ActionResult NavigateToEmpSummary()
        {
            //GlobalVariables.UniquePageId = Guid.NewGuid().ToString();
            return Json(new { url = "/Home/EmployeeSummary", IsSuccess = true, JsonRequestBehavior.AllowGet });
            //return RedirectToCustomAction("EmployeeSummary", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult EmployeeSummary()
        {
            var EmployeeSummaryModel = new EmployeeSummaryModel();
            EmployeeSummaryModel.EmployeeModels = new List<RegisterModel>();
            if (IsDataExistsInSession("EmployeeModelList"))
            {
                var empObject = GetSession<List<RegisterModel>> ("EmployeeModelList");
                foreach (var emp in empObject)
                {
                    EmployeeSummaryModel.EmployeeModels.Add(emp);
                }

            }
            return View(EmployeeSummaryModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [SessionFilter]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [SessionFilter]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult EmployeeDetails()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult EmployeeAddress()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult EmployeeContact()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult EmployeeDetails(RegisterModel model)
        {
            try
            {
                RegisterModel sessionEmpObject = new RegisterModel();
                if (IsDataExistsInSession("EmployeeModel"))
                {
                    sessionEmpObject = GetSession<RegisterModel>("EmployeeModel");

                }
                else
                    sessionEmpObject = new RegisterModel();

                sessionEmpObject.FirstName = model.FirstName;
                sessionEmpObject.LastName = model.LastName;

                SetSession<RegisterModel>("EmployeeModel", sessionEmpObject);

                return RedirectToCustomAction("EmployeeAddress", "Home");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult EmployeeAddress(RegisterModel model)
        {
            try
            {
                RegisterModel sessionEmpObject = new RegisterModel();
                if (IsDataExistsInSession("EmployeeModel"))
                {
                    sessionEmpObject = GetSession<RegisterModel>("EmployeeModel");

                }
                else
                    sessionEmpObject = new RegisterModel();

                sessionEmpObject.AddressLine1 = model.AddressLine1;
                sessionEmpObject.AddressLine2 = model.AddressLine2;
                sessionEmpObject.PinCode = model.PinCode;
                sessionEmpObject.City = model.City;
                sessionEmpObject.Country = model.Country;
                sessionEmpObject.Landmark = model.Landmark;

                SetSession<RegisterModel>("EmployeeModel", sessionEmpObject);

                return RedirectToCustomAction("EmployeeContact", "Home");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult EmployeeContact(RegisterModel model)
        {
            try
            {
                RegisterModel sessionEmpObject = new RegisterModel();
                if (IsDataExistsInSession("EmployeeModel"))
                {
                    sessionEmpObject = GetSession<RegisterModel>("EmployeeModel");

                }
                else
                    sessionEmpObject = new RegisterModel();

                sessionEmpObject.PrimaryContactNumber = model.PrimaryContactNumber;
                sessionEmpObject.ContactNumber2 = model.ContactNumber2;
                sessionEmpObject.PrimaryEmailId = model.PrimaryEmailId;
                sessionEmpObject.SecondaryEmailId = model.SecondaryEmailId;

                RemoveSession("EmployeeModel");
                List<RegisterModel> empModels = new List<RegisterModel>();
                if (IsDataExistsInSession("EmployeeModelList"))
                {
                    empModels = GetSession<List<RegisterModel>>("EmployeeModelList");

                }
                empModels.Add(sessionEmpObject);
                SetSession<List<RegisterModel>>("EmployeeModelList", empModels);

                return RedirectToCustomAction("EmployeeSummary", "Home");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        #region ISessionContainer Implementation

        private readonly HttpSessionState userSession;
        private const string SessionTableKey = "SessionTableKey";
        private readonly Hashtable table;
        private static List<string> tabExclusionKeys = new List<string>();

        public string GetModifiedKey(string key)
        {
            if (!tabExclusionKeys.Contains(key))
            {
                if (System.Web.HttpContext.Current.Request.Headers.AllKeys.Any(x => x == "X-Custom-UniquePageId"))
                {
                    var uniquePageId = System.Web.HttpContext.Current.Request.Headers.GetValues("X-Custom-UniquePageId").FirstOrDefault();
                    return System.Web.HttpContext.Current.Session.SessionID + "_" + uniquePageId + "_" + key;
                }
            }
            return key;
        }

        public TModel GetSession<TModel>(string key)
        {
            if (!tabExclusionKeys.Contains(key))
            {
                key = GetModifiedKey(key);
            }
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                TModel model = (TModel)System.Web.HttpContext.Current.Session[key];
                return model;
            }

            if (!table.ContainsKey(key))
            {
                return default(TModel);
            }
            return default(TModel);
        }

        public void SetSession<TModel>(string key, TModel dataItem)
        {
            if (!tabExclusionKeys.Contains(key))
            {
                key = GetModifiedKey(key);
            }
            if (!table.ContainsKey(key))
            {
                table.Add(key, typeof(TModel).FullName);
            }

            System.Web.HttpContext.Current.Session[key] = dataItem;
            userSession[SessionTableKey] = table;
        }

        public bool IsDataExistsInSession(string key)
        {
            if (!tabExclusionKeys.Contains(key))
            {
                key = GetModifiedKey(key);
            }
            return table.ContainsKey(key);
        }

        public void RemoveSession(string key)
        {
            if (!tabExclusionKeys.Contains(key))
            {
                key = GetModifiedKey(key);
            }
            if (table.ContainsKey(key))
            {
                table.Remove(key);
                System.Web.HttpContext.Current.Session.Remove(key);
                userSession[SessionTableKey] = table;
            }
        }

        #endregion ISessionContainer Implementation

        #region URL Redirection Helper

        public string GetTabContext()
        {
            string headerPageId = null;
            if (System.Web.HttpContext.Current.Request.Headers.AllKeys.Any(x => x.ToUpper() == ("X-Custom-UniquePageId").ToUpper()))
            {
                headerPageId = System.Web.HttpContext.Current.Request.Headers.GetValues("X-Custom-UniquePageId").FirstOrDefault();
            }
            return headerPageId;
        }

        public RedirectToRouteResult RedirectToCustomAction(string actionName)
        {
            var headerPageId = GetTabContext();
            if (headerPageId != null && headerPageId != string.Empty)
            {
                return RedirectToAction(actionName, new { tabId = headerPageId });
            }
            else
            {
                return RedirectToAction(actionName);
            }
        }

        public RedirectToRouteResult RedirectToCustomAction(string actionName, RouteValueDictionary routeValues)
        {
            var headerPageId = GetTabContext();
            if (headerPageId != null && headerPageId != string.Empty)
            {
                if (routeValues == null)
                    routeValues = new RouteValueDictionary();
                routeValues.Add("tabId", headerPageId);
                return RedirectToAction(actionName, routeValues);
            }
            else
            {
                return RedirectToAction(actionName, routeValues);
            }
        }

        public RedirectToRouteResult RedirectToCustomAction (string actionName, string controllerName)
        {
            var headerPageId = GetTabContext();
            if (headerPageId != null && headerPageId != string.Empty)
            {
                return RedirectToAction(actionName, controllerName, new { tabId = headerPageId });
            }
            else
            {
                return RedirectToAction(actionName, controllerName);
            }
            
        }

        #endregion URL Redirection Helper

    }
}


