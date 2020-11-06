using System.Threading.Tasks;
using API.Web.Entities;

namespace App.Tracly.Models
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAndPassword(string username, string password);
    }
}