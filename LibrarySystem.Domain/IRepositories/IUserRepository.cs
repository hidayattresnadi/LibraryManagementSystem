using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Update(User foundUser, User user);
        User AddNote(User foundUser, string note);
    }
}