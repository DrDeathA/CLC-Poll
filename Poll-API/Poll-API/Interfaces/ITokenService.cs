using Poll_API.Data.Entities;
using Poll_API.DTOs;

namespace Poll_API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
