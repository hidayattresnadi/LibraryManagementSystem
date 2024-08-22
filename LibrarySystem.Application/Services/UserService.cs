using LibrarySystem.Application.DTO;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO> AddUser(UserDTO inputUser)
        {
            Guid guid = Guid.NewGuid();
            string libraryCardNumber = $"lib-{guid}";
            DateTime issueDate = DateTime.UtcNow;
            var newUser = new User
            {
                FirstName = inputUser.FirstName,
                LastName = inputUser.LastName,
                Position = inputUser.Position,
                Privilege = inputUser.Privilege,
                LibraryCardNumber = libraryCardNumber,
                LibraryCardExpiringDate = issueDate.AddYears(1)
            };
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveAsync();
            inputUser.UserId=newUser.UserId;
            return inputUser;
        }
        public async Task<User> GetUserById(int id)
        {
            User chosenUser = await _userRepository.GetFirstOrDefaultAsync(foundUser => foundUser.UserId == id);
            if (chosenUser == null){
                throw new NotFoundException("User is not found");
            }
            return chosenUser;
        }
        public async Task<UserDTO> GetUserDetail(int id)
        {
            User chosenUser = await GetUserById(id);
            var userDTODetail = new UserDTO
            {
                UserId = chosenUser.UserId,
                FirstName = chosenUser.FirstName,
                LastName = chosenUser.LastName,
                Position = chosenUser.Position,
                Privilege = chosenUser.Privilege
            };
            return userDTODetail;
        }
        public async Task<User> UpdateUser(UserDTO user, int id)
        {
            var foundUser = await GetUserById(id);
            var mappingUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Position = user.Position,
                Privilege = user.Privilege
            };
            var updatedUser = _userRepository.Update(foundUser,mappingUser);
            await _userRepository.SaveAsync();
            return updatedUser;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var foundBook = await GetUserById(id);
            _userRepository.Remove(foundBook);
            await _userRepository.SaveAsync();
            return true;
        }
        public async Task<User> AddNote(string note, int id)
        {
            var foundUser = await GetUserById(id);
            _userRepository.AddNote(foundUser, note);
            await _userRepository.SaveAsync();
            return foundUser;
        }
    }
}
