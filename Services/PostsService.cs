using HanamiAPI.Dtos;
using HanamiAPI.Models;
using HanamiAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HanamiAPI.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postRepository;

        public PostsService(IPostsRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Posts> AddPostAsync(CreatePostsDto createPostDto)
        {
            var post = new Posts
            {
                Title = createPostDto.Title,
                Content = createPostDto.Content,
                CreatedAt = DateTime.UtcNow
            };

            return await _postRepository.AddPostAsync(post);
        }

        public async Task<IEnumerable<Posts>> GetPostsAsync()
        {
            return await _postRepository.GetPostsAsync();
        }

        public async Task<Posts?> GetPostByIdAsync(int id)
        {
            return await _postRepository.GetPostByIdAsync(id);
        }

        public async Task UpdatePostAsync(int id, UpdatePostsDto updatePostDto)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post != null)
            {
                post.Title = updatePostDto.Title;
                post.Content = updatePostDto.Content;
                await _postRepository.UpdatePostAsync(post);
            }
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.DeletePostAsync(id);
        }
    }
}
