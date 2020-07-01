using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data
{
    public interface IAuthrRpository
    {
        Task<User> Resgister(User user, string Password);
        Task<User> Login(string userName, string Password);
        Task<bool> UserIsExists(string userName);
    }
}
