using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REST.DataAccess.Models;

namespace REST.DataAccess.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<DbPerson>
    {
        public void Configure(EntityTypeBuilder<DbPerson> builder)
        {
            builder.ToTable("tbl_persons").HasKey(person => person.Id);
            builder.Property(person => person.Id).HasColumnName("cln_id");
            builder.Property(person => person.Name).HasColumnName("cln_name");
            builder.Property(person => person.Surname).HasColumnName("cln_surname");
            builder.Property(person => person.BirthDate).HasColumnName("cln_birth_date");
        }
    }
}