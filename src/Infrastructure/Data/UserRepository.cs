using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;
        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.VkontakteId == id);
            if(user == null)
            {
                throw new NotFoundException(id);
            }
            await context.Entry(user).Collection(u => u.Posts).LoadAsync();
            return user;
        }

        public async Task<UserUpdateResult> Update(User user)
        {
            var existingUser = await context.Users.SingleOrDefaultAsync(u => u.VkontakteId == user.VkontakteId);
            UserUpdateResult result;
            if(existingUser == null)
            {
                context.Add(user);
                result = UserUpdateResult.Created;
            }
            else
            {
                context.Entry(existingUser).State = EntityState.Detached;
                context.Entry(user).State = EntityState.Modified;
                result = UserUpdateResult.Updated;
            }
            await context.SaveChangesAsync();
            return result;
        }
    }
}
