using Microsoft.EntityFrameworkCore;

namespace Way2bill.Models
{
    public class Way2BillDbContext : DbContext
    {
        //Dependency Injection
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Way2BillDbContext(DbContextOptions<Way2BillDbContext>
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            options) : base(options)
        {

        }

        public DbSet<BranchMaster> BranchMasters { get; set; }

        public DbSet<CompanyMaster> CompanyMasters { get; set; }
        public DbSet<ComplainMaster> ComplainMasters { get; set; }
        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<FeedbackMaster> FeedbackMasters { get; set; }

        public DbSet<ProductMainCategoryMaster> ProudctMainCategoryMasters { get; set; }
        public DbSet<ProductSubCategoryMaster> ProductSubCategoryMasters { get; set; }
        public DbSet<QuestionMaster> QuestionMasters { get; set; }

        public DbSet<CartMaster> CartMasters { get; set; }

        public DbSet<InvoiceMaster> InvoiceMasters { get; set; }

        public DbSet<InvoiceDetails> InvoiceDetailsMasters { get; set; }


    }
}
