using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeSheets.Models;

namespace TimeSheets.Data.EntityConfiguration
{
    public class InvoiceConfiguration:IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoices");
        }
    }
}