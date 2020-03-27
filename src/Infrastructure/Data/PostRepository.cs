using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
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
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext context;
        public PostRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Post> Create(Post post)
        {
            EntityEntry<Post> entry = context.Add(post);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task Delete(Post post)
        {
            var postToDelete = await context.Posts.SingleOrDefaultAsync(p => p.Id == post.Id);
            if(postToDelete == null || postToDelete.IsDeleted)
            {
                throw new NotFoundException(post.Id);
            }

            post.IsDeleted = true;
            context.Update(post);
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Post>> GetAll()
        {
            return (await context.Posts.Where(p => !p.IsDeleted)
                                        .Include(p => p.Author)
                                        .AsNoTracking()
                                        .ToListAsync())
                                        .AsReadOnly();
        }

        public async Task<Post> GetById(int id)
        {
            return await context.Posts.SingleOrDefaultAsync(p => p.Id == id) 
                ?? throw new NotFoundException(id);
        }

        public async Task<IReadOnlyList<Post>> GetPostsByAuthor(int authorId)
        {
            return (await context.Posts.Where(p => p.AuthorVkId == authorId)
                                        .ToListAsync())
                                        .AsReadOnly();
        }

        public async Task<Post> Update(Post post)
        {
            if (await context.Posts.AsNoTracking().SingleOrDefaultAsync(p => p.Id == post.Id) == null)
            {
                throw new NotFoundException(post.Id);
            }
            post.IsEdited = true;
            context.Entry(post).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return post;
        }
    }
}
