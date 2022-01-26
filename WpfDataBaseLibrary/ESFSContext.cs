using System.Data.Entity;

namespace WpfDataBaseLibrary
{
    public class EsfsContext : DbContext
    {
        public EsfsContext()
            : base("name=ESFSContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<Theory> Theories { get; set; } = null!;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers).WithOptional(e => e.Question!)
                .HasForeignKey(e => e.QuestionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.Test)
                .HasForeignKey(e => e.TestId).WillCascadeOnDelete(true);

            modelBuilder.Entity<Theory>()
                .Property(e => e.TextTheory)
                .IsUnicode(false);
        }
    }
}