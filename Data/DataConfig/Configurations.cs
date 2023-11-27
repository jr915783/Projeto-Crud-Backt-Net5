using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DataConfig
{
    public class Configurations : IEntityTypeConfiguration<HeroEntity>
    {
        public void Configure(EntityTypeBuilder<HeroEntity> builder)
        {
           // throw new NotImplementedException();
        }

        public void ConfigureTask(EntityTypeBuilder<HeroEntity> builder)
        {
            builder.ToTable("Hero");
            builder.HasKey(k => k.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
