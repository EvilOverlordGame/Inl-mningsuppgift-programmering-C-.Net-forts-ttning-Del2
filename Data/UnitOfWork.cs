using bageri_api.Interfaces;

namespace bageri_api.Data;

public class UnitOfWork(BageriContext context) : IUnitOfWork
{
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
