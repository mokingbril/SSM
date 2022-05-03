using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SSM.Models.Models.Mapping
{
    public class AdminMap : EntityTypeConfiguration<Admin>
    {
        public AdminMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.LoginName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LoginPwd)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Admins");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LoginName).HasColumnName("LoginName");
            this.Property(t => t.LoginPwd).HasColumnName("LoginPwd");

            // Relationships

        }
    }
}
