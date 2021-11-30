using BdkAutomation.Sharp7.ReadWritePLC;
using Newtonsoft.Json;
using Sharp7;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace RealTimeJquery.Controllers
{
    public class PLCController : Controller
    {
        // GET: PLC
        PlcRW readWrite = new PlcRW();
        S7Client client = new S7Client();
        DataBlock db = new DataBlock();
        static string ip;
        Timer tmr = new Timer();
        public ActionResult Index()
        {
            readWrite.ConnecToPlc("192.168.1.10", 0, 0, client);
            return View();
        }



        public JsonResult GetData()
        {
            ip = client.PLCIpAddress;
            var listPlc = readWrite.plcItems(@"C:\BDK\Veritas-TR\Data\1080_hap1.json", client, 7, 528);
            return Json(new { Success = true, Data = listPlc.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetValue()
        {
            lock (this)
            {
                readWrite.DisconnecToPlc(client);
                readWrite.ConnecToPlc("192.168.1.10", 0, 0, client);
                var listPlc = readWrite.plcItems(@"C:\BDK\Veritas-TR\Data\1080_hap1.json", client, 7, 528).ToList();
                return Json(new
                {
                    Success = true,
                    Data = listPlc[17].ItemValue
                }, JsonRequestBehavior.AllowGet); 
            }
            ///*client.Connected.ToString() + " " *//*+*/ /*Math.Round(Convert.ToDouble(*/listPlc[17].ItemValue/*), 3).ToString() */
        }
    }
}