using ServerMonitor.Context;
using ServerMonitor.Domain;
using ServerMonitor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ServerMonitor.Controllers
{
    public class HomeController : Controller
    {
        public Frame snapshot;
        public ActionResult Index()
        {
            Refresh();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void SetSnapshot()
        {
            Watchdog monitor = Watchdog.GetInstance();
            Frame rawSnapshot = monitor.ToFrame();
            snapshot = Format.RoundPercents(rawSnapshot, 1);
        }
        [OutputCache(Location = OutputCacheLocation.Client, Duration = 1)]
        public ActionResult Refresh()
        {
            SetSnapshot();   
            return PartialView("MainParamsPartial", snapshot);
        }

        [HttpGet]
        public ActionResult GetCurrentMetrics()
        {
            SetSnapshot();
            return Json(snapshot, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetPeriodMetrics(DateTime? startTime, DateTime? endTime)
        {
            Debug.WriteLine(startTime.ToString(), endTime.ToString());
            List<Frame> frames = await DbManager.GetInstance().GetFramesByPeriod(startTime, endTime);
            return PartialView("FramesListPartial", frames);
        }

    }
}