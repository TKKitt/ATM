using KittATM.Models;
using KittATM.Migrations;

namespace KittATM.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataDbContext _context;

    public UserRepository(DataDbContext context)
    {
        _context = context;
    }

    public User? GetUserByCardNumber(string cardNumber)
    {
        return _context.User.SingleOrDefault(u => u.CardNumber == cardNumber);   
    }

    public User? UpdateAccount(User newUser)
    {
        var originalUser = _context.User.Find(newUser.Id);
        if(originalUser != null) {
            originalUser.CardNumber = newUser.CardNumber;
            originalUser.PIN = newUser.PIN;
            originalUser.Balance = newUser.Balance;
            _context.SaveChanges();
        }
        return originalUser;
    }

    public bool VerifyUser(string cardNumber, string pin)
    {
        var user = _context.User.SingleOrDefault(x => x.CardNumber == cardNumber);
        if(user != null && user.PIN == pin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}