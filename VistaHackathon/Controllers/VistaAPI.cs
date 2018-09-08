using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VistaDataAccess;
using VistaHackathon.Models;

namespace VistaHackathon.Controllers
{
	public class VistaAPIController : Controller
	{
		const int SuperUser = 18;
		private Model1 da = new Model1();

		[HttpGet]
		public JsonResult GetTeams()
		{
			var result = da.Teams.Select(o => new TeamVM()
			{
				TeamId = o.Id,
				TeamName = o.TeamName,
				TeamIdea = o.Idea
			}).ToList();
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ValidateMember(int teamId, string phone)
		{
			FeedbackPage result = new FeedbackPage() { SuccessStatus = Status.InValid.ToString() };

			var participant = da.Participants.Where(o => o.TeamId == teamId && o.MobileNo == phone).FirstOrDefault();
			
			if (participant == null)
			{
				result = new FeedbackPage()
				{
					SuccessStatus = Status.InValid.ToString()
				};
			}
			else
			{
				result = new FeedbackPage()
				{
					SuccessStatus = Status.Valid.ToString(),
					MemberId = participant.Id,
					MemberName = participant.Name,
					MemberTeamName = participant.Team.TeamName,
					TeamsListForFeedback = da.Teams.Where(o => participant.TeamId != o.Id).Select(k => new TeamVM()
					{
						TeamId = k.Id,
						TeamName = k.TeamName,
						TeamIdea = k.Idea,
						CompanyName = k.CompanyName,
						Votes = k.ParticipantsVotes.Count
					}).ToList()
				};

				if (teamId == SuperUser)
				{
					result.SuccessStatus = Status.ValidSuperUser.ToString();
				}
				else
				{
					if (participant.ParticipantsVotes.Count > 0)
					{
						result.SuccessStatus = Status.AlreadySubmitted.ToString();
					}
				}
			}

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public bool UpdateVote(int memberId, int teamId)
		{
			try
			{
				var participantVote = da.ParticipantsVotes.Where(o => o.ParticipantsId == memberId).FirstOrDefault();

				if (participantVote == null)
				{
					var vote = new ParticipantsVote()
					{
						ParticipantsId = participantVote.ParticipantsId,
						TeamIdVoted = teamId
					};
					da.ParticipantsVotes.Add(vote);
				}
				else
				{
					participantVote.TeamIdVoted = teamId;
				}

				da.SaveChanges();

				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		private List<TeamVM> GetVotes(int teamId)
		{
			List<TeamVM> result = null;
			if (teamId == SuperUser)
			{
				result = da.Teams.Select(k => new TeamVM()
				{
					TeamId = k.Id,
					TeamName = k.TeamName,
					TeamIdea = k.Idea,
					CompanyName = k.CompanyName,
					Votes = k.ParticipantsVotes.Count
				}).ToList();
			}
			else
			{
				return result;
			}
			return result;
		}


		[HttpPost]
		public ActionResult ExportToExcel(int teamId = 0)
		{
			var obj = GetVotes(teamId);

			StringBuilder str = new StringBuilder();

			str.Append("<table border=`" + "1px" + "`b>");

			str.Append("<tr>");

			str.Append("<td><b><font face=Arial Narrow size=3>Company Name</font></b></td>");

			str.Append("<td><b><font face=Arial Narrow size=3>Team Name</font></b></td>");

			str.Append("<td><b><font face=Arial Narrow size=3>Team Idea</font></b></td>");

			str.Append("<td><b><font face=Arial Narrow size=3>Votes</font></b></td>");

			str.Append("</tr>");

			foreach (var val in obj)
			{
				str.Append("<tr>");
				str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.CompanyName.ToString() + "</font></td>");
				str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.TeamName.ToString() + "</font></td>");
				str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.TeamIdea.ToString() + "</font></td>");
				str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Votes.ToString() + "</font></td>");
				str.Append("</tr>");
			}

			str.Append("</table>");

			HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Information" + DateTime.Now.Year.ToString() + ".xls");

			this.Response.ContentType = "application/vnd.ms-excel";

			byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());

			return File(temp, "application/vnd.ms-excel");
		}
	}
}