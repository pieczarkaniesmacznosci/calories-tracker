using System.Threading.Tasks;
using API.Entities;

namespace App.Tracly.Models
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAndPassword(string username, string password);
    }
}