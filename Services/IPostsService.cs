using HanamiAPI.Dtos;
using HanamiAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HanamiAPI.Services
{
    public interface IPostsService
    {
        Task<Posts> AddPostAsync(CreatePostsDto createPostDto);
        Task<IEnumerable<Posts>> GetPostsAsync();
        Task<Posts?> GetPostByIdAsync(int id);
        Task UpdatePostAsync(int id, UpdatePostsDto updatePostDto);
        Task DeletePostAsync(int id);
    }
}
