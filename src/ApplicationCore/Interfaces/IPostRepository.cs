using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IPostRepository
    {
        Task<IReadOnlyList<Post>> GetAll();
        Task<Post> GetById(int id);
        Task<IReadOnlyList<Post>> GetPostsByAuthor(int authorId);
        Task<Post> Add(Post post);
        Task<Post> Update(Post post);
        Task Delete(Post post);
    }
}
