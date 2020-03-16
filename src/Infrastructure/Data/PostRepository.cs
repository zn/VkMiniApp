using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    class PostRepository : IPostRepository
    {
        private readonly ApplicationContext context;
        public PostRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Post> Add(Post post)
        {
            EntityEntry<Post> entry = context.Add(post);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task Delete(Post post)
        {
            post.IsDeleted = true;
            context.Update(post);
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Post>> GetAll()
        {
            return (await context.Posts.ToListAsync()).AsReadOnly();
        }

        public async Task<Post> GetById(int id)
        {
            return await context.Posts.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Post>> GetPostsByAuthor(int authorId)
        {
            return (await context.Posts.Where(p => p.AuthorVkId == authorId)
                                        .ToListAsync())
                                        .AsReadOnly();
        }

        public async Task<Post> Update(Post post)
        {
            post.IsEdited = true;
            context.Entry(post).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return post;
        }
    }
}
