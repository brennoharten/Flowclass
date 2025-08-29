using Application.Common.Interfaces;
using BCrypt.Net;

namespace Infrastructure.Security;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
    public bool Verify(string hash, string password) => BCrypt.Net.BCrypt.Verify(password, hash);
}

