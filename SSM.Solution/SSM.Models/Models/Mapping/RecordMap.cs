using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SSM.Models.Models.Mapping
{
    public class RecordMap : EntityTypeConfiguration<Record>
    {
        public RecordMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.StuNo)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(t => t.ExamTime)
               .IsRequired();
            this.Property(t => t.Tip)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Records");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StuNo).HasColumnName("StuNo");
            this.Property(t => t.SubId).HasColumnName("SubId");
            this.Property(t => t.Score).HasColumnName("Score");
            this.Property(t => t.ExamTime).HasColumnName("ExamTime");
            this.Property(t => t.Tip).HasColumnName("Tip");
            this.Property(t => t.TrueScore).HasColumnName("TrueScore");

            // Relationships
            this.HasRequired(t => t.Student)
                .WithMany(t => t.Records)
                .HasForeignKey(d => d.StuNo);
            this.HasRequired(t => t.Subject)
                .WithMany(t => t.Records)
                .HasForeignKey(d => d.SubId);

        }
    }
}
