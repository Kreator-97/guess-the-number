// See https://aka.ms/new-console-template for more information

internal class Program
{
    static public int attempts = 1;
    static private int _numberToBeGuessed;
    static private bool _playAgain = true;
    static private int? _betterScore;

    static void Main(string[] args)
    {
        Console.WriteLine("Guess the number game!");
        StartGame();
    }

    static void StartGame()
    {
        _numberToBeGuessed = GenerateRandomNumber();
        Console.WriteLine("Hello! Welcome to the game");
        Console.WriteLine("What's your name?");


        var player = Console.ReadLine();

        if (player is null)
        {
            player = "Player";
        }

        var readyToPlay = ConfirmAction($"\nHi {player}. Are you ready to play?");

        if (!readyToPlay)
        {
            Exit(player);
            return;
        }


        while (_playAgain)
        {
            attempts = 1;
            _numberToBeGuessed = GenerateRandomNumber();
            Play();
            Console.WriteLine($"It took you {attempts} attempts");
            SaveHighScore(attempts);
            ShowHighScore();

            _playAgain = ConfirmAction("\nWould you like to play again? (Y/N)");

            if (!_playAgain)
            {
                Exit(player);
            }
        }
    }

    static bool ConfirmAction(string message)
    {
        Console.WriteLine(message);

        var answer = Console.ReadLine();

        if (answer == null) return false;

        char shortAnswer = answer.ToUpper()[0];

        if (shortAnswer == 'Y') return true;
        if (shortAnswer == 'N') return false;

        return ConfirmAction("Invalid answer, please enter (Y/N)");
    }

    static int GenerateRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 10);
    }

    static void Play()
    {
        try
        {
            Console.WriteLine("\nGuess the number from 1 to 10:");
            int userInput = Convert.ToInt32(Console.ReadLine());

            if (userInput > 10)
            {
                Console.WriteLine("Invalid number: Enter a number between 1 to 10");
                Play();
            }

            if (_numberToBeGuessed == userInput)
            {
                Console.WriteLine("Congratulations! You have guessed the number");
            }

            if (userInput < _numberToBeGuessed)
            {
                Console.WriteLine("Incorrect: The number is higher");
                Play();
                attempts++;
            }

            if (userInput > _numberToBeGuessed)
            {
                Console.WriteLine("Incorrect: The number is lower");
                Play();
                attempts++;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid number. Try again.");
            Play();
            return;
        }
    }

    static void Exit(string name)
    {
        Console.WriteLine($"See you next time! {name}");
    }
    static void ShowHighScore()
    {
        Console.WriteLine($"Your better score is {_betterScore} attempts");
    }

    static void SaveHighScore(int newScore)
    {

        if (_betterScore is null)
        {
            _betterScore = newScore;
        }

        if (newScore < _betterScore)
        {
            _betterScore = newScore;
        }
    }
}


