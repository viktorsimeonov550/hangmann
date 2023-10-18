using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string WinScreenText = @"
            ┌───────────────────────────┐
            │                           │
            │ WW       WW  **  NN   N   │
            │ WW       WW  ii  NNN  N   │
            │  WW  WW WW   ii  N NN N   │
            │   WWWWWWW    ii  N  NNN   │
            │    WW  W     ii  N   NN   │
            │                           │
            │         Good job!         │
            │   You guessed the word!   │
            └───────────────────────────┘
            ";
            const string LossScreenText = @"
    ┌────────────────────────────────────┐
    │  LLL          OOOO    SSSS   SSSS  │
    │  LLL         OO  OO  SS  SS SS  SS │
    │  LLL        OO    OO SS     SS     │
    │  LLL        OO    OO  SSSS   SSSS  │
    │  LLL        OO    OO     SS     SS │
    │  LLLLLLLLLL  OO  OO  SS  SS SS  SS │
    │   LLLLLLLLL   OOOO    SSSS   SSSS  │
    │                                    |
    │        You were so close.          │
    │ Next time you will guess the word! │
    └────────────────────────────────────┘
";
            string[] wrongGuessesFrames =
            {
            @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"       \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══"

            };
            string[] deathAnimationFrames =
            {
                @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o>  ║   " + '\n' +
    @"     /|   ║   " + '\n' +
    @"      >\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"     <o   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"     /<   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o>  ║   " + '\n' +
    @"     /|   ║   " + '\n' +
    @"      >\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o>  ║   " + '\n' +
    @"     /|   ║   " + '\n' +
    @"      >\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"     <o   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"     /<   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"     <o   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"     /<   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"     <o   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"     /<   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      o   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    ══════╩═══",
    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      /   ║   " + '\n' +
    @"      \   ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    |__   ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    \__   ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"   ____   ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"    __    ║   " + '\n' +
    @"   /══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"    _ '   ║   " + '\n' +
    @"  _/══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      _   ║   " + '\n' +
    @" __/══════╩═══",
    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @" __/══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @" __/══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      _   ║   " + '\n' +
    @" __/══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @" __/══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @" __/══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @"      _   ║   " + '\n' +
    @" __/══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      .   ║   " + '\n' +
    @"          ║   " + '\n' +
    @" __/══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      '   ║   " + '\n' +
    @" __/══════╩═══",
    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"      _   ║   " + '\n' +
    @" __/══════╩═══"

            };
            string[] words = ReadWordsFromFile();
            Console.CursorVisible = false;
            const char Underscore = '_';
            bool invalidInput;


            while (true)
            {
                string word = GetRandomWord(words);
                string wordToGuess = new(Underscore, word.Length);

                List<char> playerUsedLetters = new List<char>();
                int incorrectGuessCount = 0;

                DrawCurrentGameState(false, incorrectGuessCount, wordToGuess, playerUsedLetters);
                Console.ReadLine();

                PlayGame(word, wordToGuess, incorrectGuessCount, playerUsedLetters);

                Console.Write("If you want to play again, press [Enter]. Else, type 'quit':");
                string playerInput = Console.ReadLine();

                if(playerInput == "quit")
                {
                   Console.Clear();
                   Console.WriteLine("Thank you for playing! Hangman was closed.");
                    break;
                }
                Console.Clear();

            }
            string[] ReadWordsFromFile()
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
                const string WordsFileName = "words.txt";
                string path = $@"{projectDirectory}\{WordsFileName}";
                string[] words = File.ReadAllLines(path);
                return words;
            }
            string GetRandomWord(string[] words)
            {
                Random random = new Random();
                string word = words[random.Next(words.Length)];
                return word.ToLower();
            }
            void DrawCurrentGameState(bool invalidInput, int incorrectGuessCount,
               string wordToGuess, List<char> playerUsedLetters)
            {
                string[] wrongGuessesFrames = {
            @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

    @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      |   ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"      |\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"          ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"       \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══",

        @"      ╔═══╗   " + '\n' +
    @"      |   ║   " + '\n' +
    @"      O   ║   " + '\n' +
    @"     /|\  ║   " + '\n' +
    @"     / \  ║   " + '\n' +
    @"     ███  ║   " + '\n' +
    @"    ══════╩═══"

            };
                Console.Clear();
                Console.WriteLine(wrongGuessesFrames[incorrectGuessCount]);
                Console.WriteLine($"Guess: {wordToGuess}");
                Console.WriteLine($"You have to guess {wordToGuess.Length} symbols");
                Console.WriteLine($"The following letters are used: {String.Join(",", playerUsedLetters)}");
                if (invalidInput)
                {
                    Console.WriteLine("You should type only a single charackter!");

                }
                Console.Write("Your symbol: ");
            }
            void PlayGame(string word, string wordToGuess,
               int incorrectGuessCount, List<char> playerUsedLetters)
            {
                while (true)
                {
                    string playerInput = Console.ReadLine().ToLower();
                    if (playerInput.Length != 1)
                    {
                        DrawCurrentGameState(true, incorrectGuessCount, wordToGuess, playerUsedLetters);
                        continue;
                    }

                    char playerLetter = char.Parse(playerInput);
                    playerUsedLetters.Add(playerLetter);

                    bool playerLetterIsContained = CheckIfSymbolIsContained(word, playerLetter);
                    if (playerLetterIsContained)
                    {
                        wordToGuess = AddLetterToGuessWord(word, playerLetter, wordToGuess);

                    }
                    else
                    {
                        incorrectGuessCount++;
                    }

                    DrawCurrentGameState(false, incorrectGuessCount, wordToGuess, playerUsedLetters);

                    bool playerWins = CheckIfPlayerWins(wordToGuess);
                    if (playerWins)
                    {
                        Console.Clear();
                        Console.WriteLine(WinScreenText);
                        Console.WriteLine($"The word you guessed [{word}]");

                        break;
                    }

                    bool playerLoses = CheckIfPlayerLoses(incorrectGuessCount);
                    if (playerLoses)
                    {
                        Console.SetCursorPosition(0, 0);

                        DrawDeathAnimation(deathAnimationFrames);

                        Console.Clear();
                        Console.WriteLine(LossScreenText);
                        Console.WriteLine($"The exact word is [{word}].");

                        break;
                    }


                    bool CheckIfSymbolIsContained(string word, char playerLetter)
                    {
                        if (!word.Contains(playerLetter))
                        {
                            return false;
                        }
                        return true;
                    }
                    string AddLetterToGuessWord(string word, char playerLetter, string WordToGuess)
                    {
                        char[] wordToGuessCharArr = wordToGuess.ToCharArray();

                        for (int i = 0; i < wordToGuess.Length; i++)
                        {
                            if (playerLetter == word[i])
                            {
                                wordToGuessCharArr[i] = playerLetter;
                            }
                        }
                        return new string(wordToGuessCharArr);
                    }
                    bool CheckIfPlayerWins(string wordToGuess)
                    {
                        if (wordToGuess.Contains(Underscore))
                        {
                            return false;
                        }
                        return true;
                    }
                    bool CheckIfPlayerLoses(int incorrectGuessCount)
                    {
                        const int MaxAllowedIncorrectCharackters = 6;
                        if (incorrectGuessCount == MaxAllowedIncorrectCharackters)
                        {
                            return true;
                        }
                        return false;
                    }
                    void DrawDeathAnimation(string[] deathAnimation)
                    {
                        for (int i = 0; i < deathAnimation.Length; i++)
                        {
                            Console.WriteLine(deathAnimation[i]);
                            Thread.Sleep(200);
                            Console.SetCursorPosition(0, 0);

                        }
                    }

                }
            }

        }
         
        

    }
}