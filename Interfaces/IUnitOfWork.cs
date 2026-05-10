namespace bageri_api.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Complete();
}
