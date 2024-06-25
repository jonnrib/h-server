using HanamiAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HanamiAPI.Repositories
{
    public interface IPostsRepository
    {
        Task<Posts> AddPostAsync(Posts post);
        Task<IEnumerable<Posts>> GetPostsAsync();
        Task<Posts?> GetPostByIdAsync(int id);
        Task UpdatePostAsync(Posts post);
        Task DeletePostAsync(int id);
    }
}
