using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using chambapp.storage.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlTypes;
using chambapp.storage.Models;
using Microsoft.EntityFrameworkCore;
namespace chambapp.dal.Users
{
    public interface IUserDal
    {
        Task<User> CreateUserAsync(User candidate);
        User Get(int id);

    }

    public class UserDll : IUserDal
    {
        public async Task<User> CreateUserAsync(User candidate)
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
    }
}
