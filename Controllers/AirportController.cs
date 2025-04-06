using System.Collections;
using System.Collections.Generic;
using AMRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace AMRS.Controllers
{
    public class AirportController : Controller
    {

    
      static string connstr = "Server=localhost;User ID=admin;Password=admin123!;Port=3306;Database=fastair";
      AMRSDBContext dbcontext = new AMRSDBContext(connstr);

     /// <summary>
     ///  Read Airport
     /// </summary>
     /// <returns></returns>
     [HttpGet]
      public IActionResult Index(string strSearch)
        {    
             AMRSDBContext context = HttpContext.RequestServices.GetService(typeof(AMRS.Models.AMRSDBContext)) as AMRSDBContext;
             
             if (strSearch != null && strSearch != "")
             {
                IList lst = dbcontext.SearchAirport(strSearch);           
                return View(lst);  
             }
              else
              {
                  IList lst = dbcontext.GetAllAirports();           
                return View(lst);  
              }             
        }

        /// <summary>
        /// Create new Airport
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Create()
        {
             return View();
        }

        /// <summary>
        /// Add New Airport
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind("portName,portCountry,portCity,portZip")] AirportsModel airportsModel)
        {
            if (ModelState.IsValid)
            {
                  int rowsAffected = dbcontext.AddAirport(airportsModel);
                  if (rowsAffected != 1) 
                  {
                      ViewData["result"] = rowsAffected;   
                      return RedirectToAction("Create");
                      //return View();                     
                  }    
                    ViewData["result"] = rowsAffected;              
            }
                    //ViewBag.Message = "Airport Inserted successfully";
                
                  return RedirectToAction("Index"); 
                 //return View();
        }

        [HttpGet]
        public ActionResult Update(string portid)
        {
             AirportsModel objAirport  = dbcontext.GetById_Airport(int.Parse(portid));           
             return View(objAirport);             
        }

        [HttpPost]
        public ActionResult Update([Bind("portId,portName,portCountry,portCity,portZip")] AirportsModel airportsModel)
        {
         
           if (ModelState.IsValid)
            {
                  int rowsAffected = dbcontext.UpdateAirport(airportsModel);
                  if (rowsAffected != 1) 
                  {
                      ViewData["result"] = rowsAffected;   
                      //return RedirectToAction("Create");
                      return RedirectToAction("Index");                     
                  }    
                    ViewData["result"] = rowsAffected;              
            }
                    
                 return RedirectToAction("Index"); 
        }

        /// <summary>
        /// Delete airport 
        /// </summary>
        /// <param name="portid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(string portid)
        {

            int retId  = dbcontext.DeleteAirport(int.Parse(portid));           
             
            ViewData["result"] = retId;  

            return RedirectToAction("Index"); 
        }


        [HttpGet]
        public ActionResult Search(string portName)
        {

          AMRSDBContext context = HttpContext.RequestServices.GetService(typeof(AMRS.Models.AMRSDBContext)) as AMRSDBContext;                      
          IList lst = dbcontext.SearchAirport(portName);           
          return View(lst); 

        }

    }
}