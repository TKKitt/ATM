using KittATM.Models;
using KittATM.Repositories;
using KittATM.Migrations;

using Microsoft.Extensions.DependencyInjection;

public class Program
{
    static void Main(string[] args)
    {
        var collection = new ServiceCollection();
        collection
        .AddScoped<IUserRepository, UserRepository>()
        .AddDbContext<DataDbContext>();
        bool continueProgram = false;

        var service = collection.BuildServiceProvider();
        var _userRepository = service.GetService<IUserRepository>();

        Console.WriteLine("Enter your bank card");

        var cardNumber = Console.ReadLine();

        Console.WriteLine("Enter your PIN");
        var pin = Console.ReadLine();

        var currentUser = new User();
        var verified = _userRepository.VerifyUser(cardNumber, pin);

        if(verified == true)
        {
            Console.WriteLine("Logged in");
            currentUser = _userRepository.GetUserByCardNumber(cardNumber);
            continueProgram = true;
        }
        if(verified == false) 
        {
            Console.WriteLine("Credentials Invalid. Shutting Down...");
            continueProgram = false;
        }

        while(continueProgram)  
        {
            printOptions();
            int choice = int.Parse(Console.ReadLine()); 

            switch(choice) {
                case 1: 
                    showBalance();

                    break;
                case 2:
                    withdraw();

                    break;
                case 3: 
                    deposit();

                    break;
                default: 
                    Console.WriteLine("Exiting Program...");
                    continueProgram = false;
                    break;
            }
        }
        
        void printOptions() {
            Console.WriteLine("Choose a transaction (1 - 4)");
            Console.WriteLine("1. Show Balance");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Exit");
        }

        void showBalance() {
            Console.WriteLine("Current Balance: " + currentUser.Balance);
        }

        void withdraw() {
            Console.WriteLine("Enter withdrawal amount");
            var withdrawalAmount = double.Parse(Console.ReadLine());

            if(withdrawalAmount > currentUser.Balance)
            {
                Console.WriteLine("Insufficient Funds");

            } else {
                var newBalance = currentUser.Balance - withdrawalAmount;
                currentUser.Balance = newBalance;
                _userRepository.UpdateAccount(currentUser);

                Console.WriteLine("Your updated balance is: " + currentUser.Balance);
            }
        }

        double deposit() {
            Console.WriteLine("Enter deposit amount");
            var depositAmount = double.Parse(Console.ReadLine());

            var newBalance = currentUser.Balance + depositAmount;
            currentUser.Balance = newBalance;
            _userRepository.UpdateAccount(currentUser);

            Console.WriteLine("Your updated balance is: " + currentUser.Balance);

            return currentUser.Balance;
        }
    }
}