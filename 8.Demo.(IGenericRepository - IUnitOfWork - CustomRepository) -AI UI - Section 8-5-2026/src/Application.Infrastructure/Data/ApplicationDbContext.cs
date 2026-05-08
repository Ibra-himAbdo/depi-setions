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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entity in ChangeTracker.Entries<BaseEntity>())
        {
            if (entity.State is EntityState.Modified)
            {
                entity.Entity.UdaptedAt = DateTime.UtcNow;
            }
        }

        foreach (var entity in ChangeTracker.Entries<BaseSettingEntity>())
        {
            if (entity.State is EntityState.Added or EntityState.Modified)
            {
                entity.Entity.NormalizedName = entity.Entity.Name?.ToUpperInvariant();
                entity.Entity.NormalizedNameSecondLanguage = entity.Entity.NameSecondLanguage?.ToUpperInvariant();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}