namespace Application.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply entity configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductBrand> ProductBrands { get; set; }

    public DbSet<ProductCategory> ProductCategories { get; set; }

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    //{
    //    // Automatically handle audit fields and normalization
    //    HandleAuditFields();
    //    NormalizeCategoryNames();
    //    NormalizeAccountNames();

    //    return await base.SaveChangesAsync(cancellationToken);
    //}

    #region Private Helper Methods

    //private void HandleAuditFields()
    //{
    //    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
    //    {
    //        switch (entry.State)
    //        {
    //            case EntityState.Added:
    //                entry.Entity.CreatedAt = DateTime.UtcNow;
    //                break;

    //            case EntityState.Modified:
    //                entry.Entity.UpdatedAt = DateTime.UtcNow;
    //                break;
    //        }
    //    }
    //}

    //private void NormalizeBaseSettingEntitiesNames()
    //{
    //    foreach (var entry in ChangeTracker.Entries<BaseSettingEntity>())
    //    {
    //        if (entry.State is EntityState.Added or EntityState.Modified)
    //            entry.Entity.NormalizedName =
    //                entry.Entity.Name?.ToUpper();
    //    }
    //}

    #endregion Private Helper Methods
}