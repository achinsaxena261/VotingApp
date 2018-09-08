namespace VistaDataAccess
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class Model1 : DbContext
	{
		public Model1()
			: base("name=connectionStr")
		{
		}

		public virtual DbSet<Participant> Participants { get; set; }
		public virtual DbSet<ParticipantsVote> ParticipantsVotes { get; set; }
		public virtual DbSet<Team> Teams { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Participant>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Participant>()
				.Property(e => e.MobileNo)
				.IsUnicode(false);

			modelBuilder.Entity<Participant>()
				.HasMany(e => e.ParticipantsVotes)
				.WithRequired(e => e.Participant)
				.HasForeignKey(e => e.ParticipantsId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Team>()
				.Property(e => e.TeamName)
				.IsUnicode(false);

			modelBuilder.Entity<Team>()
				.Property(e => e.Idea)
				.IsUnicode(false);


			modelBuilder.Entity<Team>()
				.HasMany(e => e.Participants)
				.WithRequired(e => e.Team)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Team>()
				.HasMany(e => e.ParticipantsVotes)
				.WithRequired(e => e.Team)
				.HasForeignKey(e => e.TeamIdVoted)
				.WillCascadeOnDelete(false);
		}
	}
}
