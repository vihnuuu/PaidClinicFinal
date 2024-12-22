using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(); // отримати всі записи
        Task<T> GetByIdAsync(int id); // отримати запис за айді
        Task AddAsync(T entity); // додати новий запис
        Task UpdateAsync(T entity); // оновити існуючий запис
        Task DeleteAsync(int id); // видалити запис за айді
    }
}
