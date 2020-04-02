using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetById(int id);
        Task<IReadOnlyList<Post>> GetPostsForPage(int page);
        Task<IReadOnlyList<Post>> GetPostsByAuthor(int id);
        Task AddAttachments(int id, IEnumerable<string> urls);
        Task<Post> Create(Post post);
        Task<Post> Update(Post post);
        Task Delete(Post post);
    }
}
