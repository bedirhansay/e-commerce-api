using Application.Interface.Repositories;
using core.Common;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class,IEntityBase,new()

{
    private readonly DbContext _context;
    public WriteRepository(DbContext dbContext)
    {
        this._context = dbContext;
    }
    private DbSet<T> Table {get => _context.Set<T>();}
    
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task AddRangeAsync(IList<T> entities)
    {
        await Table.AddRangeAsync(entities);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        await Task.Run(()=>Table.Update(entity));
        return entity;
    }

    public async Task HardDeleteAsync(T entity)
    {
        await Task.Run(() => Table.Remove(entity));
    }

    public async Task SoftDeleteAsync(T entity)
    {
        await Task.Run(()=>Table.Update(entity));
    }
}