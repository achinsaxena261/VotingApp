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
	}
}