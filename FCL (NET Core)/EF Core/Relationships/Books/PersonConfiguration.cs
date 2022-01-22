using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
   public void Configure(EntityTypeBuilder<Person> builder)
   {
      builder.OwnsOne(p => p.BusinessAddress, closureBuilder =>
      {
         closureBuilder.Property(a => a!.LineOne).HasColumnName("AddressLineOne").HasMaxLength(50);
         closureBuilder.Property(a => a!.LineTwo).HasColumnName("AddressLineTwo").HasMaxLength(50);
         closureBuilder.OwnsOne(a => a!.Location, locationBuilder =>
         {
            locationBuilder.Property(l => l!.City).HasColumnName("BusinessCity").HasMaxLength(30);
            locationBuilder.Property(l => l!.Country).HasColumnName("BusinessCountry").HasMaxLength(30);
         });
      });

      builder.OwnsOne(p => p.PrivateAddress, closureBuilder =>
      {
         closureBuilder.ToTable("PrivateAddresses");
         closureBuilder.Property(a => a!.LineOne).HasMaxLength(50);
         closureBuilder.Property(a => a!.LineTwo).HasMaxLength(50);
         closureBuilder.OwnsOne(a => a!.Location, b =>
         {
            b.Property(a => a!.City).HasColumnName("City").HasMaxLength(30);
            b.Property(a => a!.Country).HasColumnName("Country").HasMaxLength(30);
         });
      });
   }
}