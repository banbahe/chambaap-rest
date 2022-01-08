using chambapp.storage.Models;
using System.Linq;
using System.Threading.Tasks;
namespace chambapp.dal.Users
{
    public interface IUserDal
    {
        Task<User> SetAsync(User user);
        Task<User> CreateAsync(User user);
        User Get(int id);

    }

    
}
