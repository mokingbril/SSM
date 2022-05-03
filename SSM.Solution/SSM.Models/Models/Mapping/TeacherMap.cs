using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SSM.Models.Models.Mapping
{
    public class TeacherMap : EntityTypeConfiguration<Teacher>
    {
        public TeacherMap()
        {
            // Primary Key
            this.HasKey(t => t.TeaNo);

            // Properties
            this.Property(t => t.TeaNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LoginPwd)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Teachers");
            this.Property(t => t.TeaNo).HasColumnName("TeaNo");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LoginPwd).HasColumnName("LoginPwd");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.JId).HasColumnName("JId");

            // Relationships
            this.HasRequired(t => t.Job)
                .WithMany(t => t.Teachers)
                .HasForeignKey(d => d.JId);
                
                

        }
    }
}
