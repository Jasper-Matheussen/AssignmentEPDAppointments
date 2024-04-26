using Chipsoft.Assignments.EPDDomain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Chipsoft.Assignments.EPDApplication.Interfaces;

public interface IEPDDbContext 
{
    IQueryable<T> GetQueryable<T>()
        where T : class, IBaseEntity;
 
    IQueryable<T> GetNoTrackingQueryable<T>()
        where T : class, IBaseEntity;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    EntityEntry Add(object entity);
    
    EntityEntry Update(object entity);

    EntityEntry Remove(object entity);
}