using LibrarySystem.Application.DTO;
using LibrarySystem.Application.DTO.UserDTO;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Services
{
    public interface IUserService
    {
        Task<UserDTO> AddUser(UserDTO user);
        Task<User> GetUserById(int id);
        Task<UserDTO> GetUserDetail(int id);
        Task<User> UpdateUser(UserDTO user, int id);
        Task<bool> DeleteUser(int id);
        Task<User> AddNote(string note, int id);
    }
}