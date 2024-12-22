using Application.Interface;
using Application.Interface.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWorks;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
     this._dbContext = dbContext;
    }
    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
   

    IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_dbContext);
    IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_dbContext);
    
    public async  Task<int> SaveAsync()=> await _dbContext.SaveChangesAsync();
    public int Save()=> _dbContext.SaveChanges();
    
}