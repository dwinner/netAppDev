using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static _06_BooksSample.ColumnNames;

namespace _06_BooksSample
{
   public class AuthorConfiguration : IEntityTypeConfiguration<Author>
   {
      public void Configure(EntityTypeBuilder<Author> builder)
      {
         builder.Property(a => a.FirstName).HasField("_firstName").HasMaxLength(50);
         builder.Property(a => a.LastName).HasField("_lastName").HasMaxLength(50);
         builder.Property<int>(AuthorId).HasField("_authorId").IsRequired();
         builder.HasKey(AuthorId);
      }
   }
}