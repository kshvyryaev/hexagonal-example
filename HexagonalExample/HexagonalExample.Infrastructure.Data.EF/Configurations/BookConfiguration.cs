using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HexagonalExample.Infrastructure.Data.EF.Models;

namespace HexagonalExample.Infrastructure.Data.EF.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<BookEFModel>
    {
        private const int NameMaxLength = 50;
        private const int DescriptionMaxLength = 150;

        public void Configure(EntityTypeBuilder<BookEFModel> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .Property(x => x.Description)
                .HasMaxLength(DescriptionMaxLength);
        }
    }
}
