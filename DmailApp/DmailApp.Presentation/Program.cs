using DmailApp.Data.Entities.Models;
using DmailApp.Domain.Factories;
using DmailApp.Domain.Repositories;
using DmailApp.Presentation.Enums;
using DmailApp.Presentation.Extensions;
using DmailApp.Presentation.Factories;
using System.Text;

int choice = -1;
while (choice != (int)ChoiceEnum.Exit)
{
	Console.WriteLine($"{(int)ChoiceEnum.Login}. Login\n{(int)ChoiceEnum.Registration}. Registration\n{(int)ChoiceEnum.Exit}. Exit");
    var isValidInput = int.TryParse(Console.ReadLine(), out choice);
	if (!isValidInput)
	{
		Console.WriteLine("Please type in number");
		continue;
	}
	switch (choice)
	{
		case (int)ChoiceEnum.Login:
			Login();
			break;
		case (int)ChoiceEnum.Registration:
			Register();
			break;
		case (int)ChoiceEnum.Exit:
			Console.WriteLine("Exit");
			break;
		default:
			Console.WriteLine("Please select valid option");
			break;
	}
}

void Login()
{
	string adress = "";
	string password = "";
	var userRepo = RepositoryFactory.Create<UserRepository>();
	var exit = "";
	DateTime loginTime = new DateTime();

	while (!userRepo.CheckIfAdressPasswordCombinationExists(adress, password) && exit != "y")
	{

		if ((DateTime.Now - loginTime).TotalSeconds > 30)
		{
            Console.WriteLine("\nEmail adress:");
            adress = Console.ReadLine();
            Console.WriteLine("\nPassword");
            password = Console.ReadLine();
			loginTime= DateTime.Now;
            if (!userRepo.CheckIfAdressPasswordCombinationExists(adress, password))
            {
                Console.WriteLine("Incorrect adress-password combination");
                Console.WriteLine("Exit? <y>");
                exit = Console.ReadLine();
				Console.WriteLine("30 second timeout");
				Thread.Sleep(30000);
            }
        }
        		        
    }
	if (exit != "y")
	{
        var mainMenuActions = MainMenuFactory.CreateActions(adress);
        mainMenuActions.PrintActionsAndOpen();
    }
}

void Register()
{
	
    var userRepo = RepositoryFactory.Create<UserRepository>();
    Console.WriteLine("\nEmail adress");
	var adress = Console.ReadLine();

	if (!adress.Contains("@"))
	{
		Console.WriteLine("Incorrect input (no <@>)");
		return;
	}
	var adressSplit = adress.Split('@', 2);

	if (!adressSplit[1].Contains("."))
	{
		Console.WriteLine("Incorrect input (no <.>)");
		return;
	}

	var adressAfterAtSplit = adressSplit[1].ToString().Split(".",2);

	if (adressSplit[0].Count() < 1 || adressAfterAtSplit[0].Count() < 2 || adressAfterAtSplit[1].Count() < 3)
	{
		Console.WriteLine("Incorrect input (Dmail adress format: [string min 1 char]@[string min 2 char].[string min 3 char])");
		return;
	}

	if (!userRepo.CheckIfAdressIsUnique(adress))
    {
        Console.WriteLine("Email adress taken");
        return;
    }

    Console.WriteLine("\nPassword");
	var password = Console.ReadLine();
	Console.WriteLine("\nRepeat password");
	var repeatedPassword = Console.ReadLine();
	if (password != repeatedPassword)
	{
		Console.WriteLine("Did not correctly repeat password");
		return;
	}

	var captcha = GenerateCaptcha();
	Console.WriteLine($"Repeat captcha: {captcha}");
	var repeatedCaptcha = Console.ReadLine();
	if (captcha != repeatedCaptcha)
	{
		Console.WriteLine("Did not correctly repeat captcha");
		return;
	}
	var newUser = new User(adress, password)
	{
		UserId = userRepo.GetFirstFreeId()
	};
	userRepo.Add(newUser);
}

string GenerateCaptcha()
{
    Random rnd = new Random();
	int randomNumber;
    StringBuilder captcha = new StringBuilder("");
    string letters = "ABCDEFGHIJKLMNOPRSTUVZXYWabcdefgijklmnoprstuvzxyw";
	string digits = "0123456789";
	for (int i = 0; i < 3; i++)
	{
		randomNumber = rnd.Next(letters.Length - 1);
		captcha.Append(letters[randomNumber]);
		randomNumber = rnd.Next(digits.Length - 1);
		captcha.Append(digits[randomNumber]);
	}
	return captcha.ToString();
}