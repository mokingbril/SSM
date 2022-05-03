using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SSM.Models.Models.Mapping
{
    public class SubjectMap : EntityTypeConfiguration<Subject>
    {
        public SubjectMap()
        {
            // Primary Key
            this.HasKey(t => t.SubId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Subjects");
            this.Property(t => t.SubId).HasColumnName("SubId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Period).HasColumnName("Period");
            this.Property(t => t.Score).HasColumnName("Score");
        }
    }
}
