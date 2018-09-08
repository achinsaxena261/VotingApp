using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VistaDataAccess;
using VistaHackathon.Models;

namespace VistaHackathon.Controllers
{
	public class VistaAPIController : Controller
	{
		private Model1 da = new Model1();
		[HttpGet]
		public String Index()
		{
			return "tests";
		}

		[HttpGet]
		public JsonResult GetTeams()
		{
			var result = da.Teams.Select(o => new TeamVM()
			{
				TeamId = o.Id,
				TeamName = o.TeamName
			}).ToList();
			
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public string Contact()
		{
			ViewBag.Message = "Your contact page.";

			return "Bests";
		}
	}
}