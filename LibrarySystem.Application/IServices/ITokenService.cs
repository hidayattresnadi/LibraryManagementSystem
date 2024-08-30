using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public interface ITokenService
    {
        Task SaveRefreshToken(string username, string token);
        Task<string> RetrieveUsernameByRefreshToken(string refreshToken);
        Task<bool> RevokeRefreshToken(string refreshToken);
    }
}
