using System.Threading.Tasks;

namespace App.Tracly.Models
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAndPassword(string username, string password);
    }
}