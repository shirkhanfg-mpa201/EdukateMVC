using EduKateMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduKateMVC.Configurations
{
    public class CourseCofiguration:IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(512);
            builder.HasOne(x => x.Teacher).WithMany(x => x.Courses).HasForeignKey(x => x.TeacherId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(opt =>
            {
                opt.HasCheckConstraint("CK_Courses_Rating", "[Rating] between 0 and 5");
            });
        }
    }
}
