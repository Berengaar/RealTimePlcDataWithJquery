using RealTimeJquery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BdkAutomation.Sharp7.ReadWritePLC;
using Sharp7;
namespace RealTimeJquery.Controllers
{
    public class RealTimeTableController : Controller
    {
        // GET: RealTimeTable
        PlcRW readWrite = new PlcRW();
        S7Client client = new S7Client();
        public ActionResult Index()
        {
            ViewBag.P=readWrite.ConnecToPlc("192.168.1.10", 0, 0, client);           
            return View();
        }

        public JsonResult CreateDenemeList()
        {
            Deneme deneme1 = new Deneme()
            {
                Id = 1,
                Name = "AAAA",
                Surname = "aaaa",
                Phone = "0000"
            };
            Deneme deneme2 = new Deneme()
            {
                Id = 2,
                Name = "BBBB",
                Surname = "bbbb",
                Phone = "1111"
            };
            Deneme deneme3 = new Deneme()
            {
                Id = 3,
                Name = "CCCC",
                Surname = "cccc",
                Phone = "2222"
            };

            List<Deneme> denemeList = new List<Deneme>();
            denemeList.Add(deneme1);
            denemeList.Add(deneme2);
            denemeList.Add(deneme3);
            return Json(new { Success = true, Data = denemeList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVariable()
        {
          readWrite.plcItems(@"C:\BDK\Veritas-TR\Data\1080_hap1.json", client, 7, 528).ToList().Count();
      
        Random rand = new Random();
                var x = rand.Next(1, 1000);
                return Json(new { Success = true, Data = x }, JsonRequestBehavior.AllowGet); 
        }
    }
}