using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Entities.Base;

namespace SimpleFormsService.Persistence;

public class SimpleFormsServiceDbContext : DbContext
{
    private readonly string _currentUser;

    #region Constructors

    #region Applications

    public SimpleFormsServiceDbContext(DbContextOptions<SimpleFormsServiceDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _currentUser = getCurrentUserName(httpContextAccessor);
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
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        setJournallingFieldsWithinJsonData();
        
        return await base.SaveChangesAsync(true, cancellationToken);
    }

    public override int SaveChanges()
    {
        setJournallingFieldsWithinJsonData();
        
        return base.SaveChanges();
    }

    #region Private Helpers
    
    private string getCurrentUserName(IHttpContextAccessor httpContextAccessor)
    {
        var nameIdentifierClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        
        var nameClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);

        return nameIdentifierClaim != null ? nameIdentifierClaim.Value : nameClaim != null ? nameClaim.Value : string.Empty;
    }

    private static void applyIndividualEntityTypeConfigurations(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    private void setJournallingFieldsWithinJsonData() // create and update user, create and update date
    {
        #region example code

        foreach (var changedEntity in ChangeTracker.Entries())
        {
            // // the following garbage code handle direct descendents of changedEntity ---------------------------------
            //
            // // this is only here to provide an example and needs to be significantly refactored
            //
            // var jsonEntityBaseObjectDictionary = new Dictionary<string, IJsonEntityBase>();
            //
            // // get json entity base children
            // foreach (var property in changedEntity.Entity.GetType().GetProperties())
            //     if (property.PropertyType.GetInterfaces().Contains(typeof(IJsonEntityBase)))
            //         jsonEntityBaseObjectDictionary.Add(property.Name, property.GetValue(changedEntity.Entity, null) as IJsonEntityBase);
            //
            // // for each json entity base child
            // foreach (var jsonEntityBaseEntry in jsonEntityBaseObjectDictionary)
            // {
            //     if (jsonEntityBaseEntry.Value != null)
            //     {
            //         switch (changedEntity.State)
            //         {
            //             case EntityState.Added:
            //                 jsonEntityBaseEntry.Value.CreateUser = _currentUser;
            //                 jsonEntityBaseEntry.Value.UpdateUser = _currentUser;
            //                 break;
            //             case EntityState.Modified:
            //                 jsonEntityBaseEntry.Value.UpdateUser = _currentUser;
            //                 break;
            //         }
            //     }
            // }
            //
            // // end handle direct descendents of changed entity ------------------------------------------------------

            // the following code below provides further insight into what could be done
            //
            // public static void NullifyIds(this IEntityBase input)
            // {
            //     foreach (var property in input.GetType().GetProperties())
            //     {
            //         if (property.Name == nameof(IEntityBase.Id))
            //             property.SetValue(input, default(uint));
            //
            //         if (property.Name == nameof(IEntityBase.Id))
            //             property.SetValue(input, Guid.NewGuid());
            //
            //         if (property.PropertyType.GetInterfaces().Contains(typeof(IEntityBase))) // child objects 
            //             if (property.GetValue(input, null) != null)
            //                 ((IEntityBase)property.GetValue(input, null)).NullifyIds();
            //
            //         if (property.PropertyType.GetInterfaces().Contains(typeof(System.Collections.IEnumerable)) &&
            //             property.PropertyType.GetGenericArguments().Any()) // child collections
            //             if (property.GetValue(input, null) is System.Collections.IEnumerable enumerable)
            //                 foreach (var item in enumerable)
            //                     ((IEntityBase)item).NullifyIds();
            //     }
            // }
        }

        #endregion
    }

    


    #endregion

}

