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
	Console.WriteLine("\nEmail adress:");
	string adress = Console.ReadLine();
	Console.WriteLine("\nPassword");
	string password = Console.ReadLine();
	var userRepo = RepositoryFactory.Create<UserRepository>();
	while (!userRepo.CheckIfAdressPasswordCombinationExists(adress, password))
	{
		Console.WriteLine("Incorrect adress-password combination");
        Console.WriteLine("\nEmail adress:");
        adress = Console.ReadLine();
        Console.WriteLine("\nPassword");
        password = Console.ReadLine();

    }
	//ovde smo se dobro loginali i cila logika triba krenit od vamo
	var mainMenuActions = MainMenuFactory.CreateActions(adress);
	mainMenuActions.PrintActionsAndOpen();
}

void Register()
{
	
    var userRepo = RepositoryFactory.Create<UserRepository>();

    Console.WriteLine("\nEmail adress");
	var adress = Console.ReadLine();

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