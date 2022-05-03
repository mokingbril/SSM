using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SSM.Models.Models.Mapping
{
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            // Primary Key
            this.HasKey(t => t.StuNo);

            // Properties
            this.Property(t => t.StuNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LoginPwd)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.Address)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Students");
            this.Property(t => t.StuNo).HasColumnName("StuNo");
            this.Property(t => t.LoginPwd).HasColumnName("LoginPwd");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.DId).HasColumnName("DId");
            this.Property(t => t.GId).HasColumnName("GId");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Birthday).HasColumnName("Birthday");

            // Relationships
            this.HasRequired(t => t.Department)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.DId);
            this.HasRequired(t => t.Grade)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.GId);

        }
    }
}
