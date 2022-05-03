using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SSM.Models.Models.Mapping
{
    public class GradeMap : EntityTypeConfiguration<Grade>
    {
        public GradeMap()
        {
            // Primary Key
            this.HasKey(t => t.GId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Grades");
            this.Property(t => t.GId).HasColumnName("GId");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
