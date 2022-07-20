namespace WebApplication1
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebApplication1.Models;

    public interface ICosmosDbService
    {
        Task<IEnumerable<Student>> GetItemsAsync(string query);
        Task<Student> GetItemAsync(string id);
        Task<Student> AddItemAsync(Student student);
        Task UpdateItemAsync(string id, Student student);
        Task DeleteItemAsync(string id);
    }
}