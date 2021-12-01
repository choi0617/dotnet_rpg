using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Data
{

    // The interface gets three methods. The first is Register() 
    // with a User and a string as parameter and returning an int - the Id of the user, 
    // second method is Login() with two string parameters and 
    // returning a token as string, and the last method is UserExists() 
    // with the username as string to check if the user already exists.
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, String password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}