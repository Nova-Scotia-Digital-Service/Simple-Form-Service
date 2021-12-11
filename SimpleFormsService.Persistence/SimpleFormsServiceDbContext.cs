using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleFormsService.Domain.Entities.Base;
using SimpleFormsService.Domain.Entities.FormSubmission;
using SimpleFormsService.Domain.Entities.FormTemplate;

namespace SimpleFormsService.Persistence;

public class SimpleFormsServiceDbContext : DbContext
{
    private readonly string _currentUser;

    #region Constructors

    #region Applications

    public SimpleFormsServiceDbContext(DbContextOptions<SimpleFormsServiceDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _currentUser = getCurrentUserForJournaling(httpContextAccessor);
    }

    #endregion

    #region Migrations 

    public SimpleFormsServiceDbContext(DbContextOptions<SimpleFormsServiceDbContext> options) : base(options)
    {}

    #endregion

    #endregion

    public DbSet<FormSubmission> FormSubmissions { get; set; }
    public DbSet<FormTemplate> FormTemplates { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        applyIndividualEntityTypeConfigurations(builder);
        configureLifespanEntityBaseDefaults(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        setCreateOrUpdateUser();
        return await base.SaveChangesAsync(true, cancellationToken);
    }

    public override int SaveChanges()
    {
        setCreateOrUpdateUser();
        return base.SaveChanges();
    }

    #region Private Helpers
    
    private string getCurrentUserForJournaling(IHttpContextAccessor httpContextAccessor)
    {
        var nameIdentifierClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        
        var nameClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);

        return nameIdentifierClaim != null ? nameIdentifierClaim.Value : nameClaim != null ? nameClaim.Value : string.Empty;
    }

    private static void applyIndividualEntityTypeConfigurations(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static void configureLifespanEntityBaseDefaults(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.Name is nameof(ILifespanBase.EffectiveDate) or nameof(IEntityBase.CreateDate))
                {
                    property.ValueGenerated = ValueGenerated.OnAddOrUpdate;
                }

                if (property.Name is nameof(IEntityBase.UpdateDate))
                {
                    property.ValueGenerated = ValueGenerated.OnAddOrUpdate;
                }
            }
        }
    }

    public void setCreateOrUpdateUser()
    {
        foreach (var changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is IEntityBase entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.CreateUser = _currentUser;
                        entity.UpdateUser = _currentUser;
                        break;
                    case EntityState.Modified:
                        Entry(entity).Reference(x => x.CreateUser).IsModified = false;
                        entity.UpdateUser = _currentUser;
                        break;
                }
            }
        }
    }

    #endregion
}

