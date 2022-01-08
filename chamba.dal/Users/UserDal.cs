using chambapp.storage.Models;
using System.Linq;
using System.Threading.Tasks;

namespace chambapp.dal.Users
{
    public class UserDal : IUserDal
    {
        public async Task<User> CreateAsync(User candidate)
        {
            using (var context = new chamba_storageContext())
            {
                context.Entry<User>(candidate).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await context.SaveChangesAsync();
            }
            return candidate;
        }

        public User Get(int id)
        {
            User result;
            using (var context = new chamba_storageContext())
            {
                //_listInterview = context.Interviews.Include(t => t.IdcompanyNavigation)
                //    .Include(t => t.IdrecruiterNavigation).ToList();
                result = context.Users.AsQueryable().FirstOrDefault<User>(x => x.Id == id);

            }
            return result;
        }

        public async Task<User> SetAsync(User user)
        {
            using (var context = new chamba_storageContext())
            {
                context.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();
            }
            return user;
        }
    }
}
