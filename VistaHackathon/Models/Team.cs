using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace VistaHackathon.Models
{
	[DataContract]
	public class TeamVM
	{
		[DataMember]
		public int TeamId { get; set; }

		[DataMember]
		public string TeamName { get; set; }

		[DataMember]
		public string TeamIdea { get; set; }

		[DataMember]
		public int Votes { get; set; }

		[DataMember]
		public string CompanyName { get; set; }
	}

	[DataContract]
	public class FeedbackPage
	{
		[DataMember]
		public string SuccessStatus { get; set; }

		[DataMember]
		public int MemberId { get; set; }

		[DataMember]
		public string MemberTeamName { get; set; }

		[DataMember]
		public string MemberName { get; set; }

		[DataMember]
		public List<TeamVM> TeamsListForFeedback { get; set; }
	}

	public enum Status
	{
		Valid = 1,
		InValid,
		AlreadySubmitted,
		ValidSuperUser
	}
}