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
        Task<IReadOnlyList<Post>> GetAll();
        Task<IReadOnlyList<Post>> GetAll(ISpecification<Post> spec);
        Task<Post> Create(Post post);
        Task<Post> Update(Post post);
        Task Delete(Post post);
    }
}
