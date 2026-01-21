using EduKateMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduKateMVC.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher> {


        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(256);
            
        }
    }
}
