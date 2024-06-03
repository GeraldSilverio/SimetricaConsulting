namespace TaskManagement.Core.Application.Interfaces.Repositories
{
    public interface IDapperRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync(string idUser);
    }
}
