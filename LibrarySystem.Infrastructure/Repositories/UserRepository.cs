using LibrarySystem.Domain.Models;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Infrastructure.Context;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly MyDbContext _db;
        public UserRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }

        public User Update(User foundUser, User user)
        {
            foundUser.FirstName = user.FirstName;
            foundUser.LastName = user.LastName;
            foundUser.Position = user.Position;
            foundUser.Privilege = user.Privilege;
            return foundUser;
        }
        public User AddNote(User foundUser, string note)
        {
            foundUser.Notes = note;
            return foundUser;
        }
    }
}