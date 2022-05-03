using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SSM.Models.Models.Mapping;

namespace SSM.Models
{
    public partial class StuScoreManagerContext : DbContext
    {
        static StuScoreManagerContext()
        {
            Database.SetInitializer<StuScoreManagerContext>(null);
        }

        public StuScoreManagerContext()
            : base("Name=StuScoreManagerContext")
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdminMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new GradeMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new RecordMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new SubjectMap());
            modelBuilder.Configurations.Add(new TeacherMap());
        }
    }
}
