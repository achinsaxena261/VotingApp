namespace VistaDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParticipantsVote")]
    public partial class ParticipantsVote
    {
        public int Id { get; set; }

        public int ParticipantsId { get; set; }

        public int TeamIdVoted { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Participant Participant { get; set; }

        public virtual Team Team { get; set; }
    }
}
