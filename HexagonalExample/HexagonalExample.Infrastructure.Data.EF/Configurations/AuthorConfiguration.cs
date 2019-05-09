using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HexagonalExample.Infrastructure.Data.EF.Models;

namespace HexagonalExample.Infrastructure.Data.EF.Configurations
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<AuthorEFModel>
    {
        private const int NameMaxLength = 50;
        private const int SurnameMaxLength = 50;

        public void Configure(EntityTypeBuilder<AuthorEFModel> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .Property(x => x.Surname)
                .HasMaxLength(SurnameMaxLength);

            builder
                .HasOne(x => x.Book)
                .WithMany(x => x.Authors)
                .IsRequired();
        }
    }
}
