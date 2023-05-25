using KittATM.Models;

namespace KittATM.Repositories;

public interface IUserRepository
{
    User? GetUserByCardNumber(string cardNumber);
    User? UpdateAccount(User newUser);
    bool VerifyUser(string cardNumber, string pin);
}