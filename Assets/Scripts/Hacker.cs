using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    private int level;
    private int stage;
    private bool[] completed;
    private string password;
    private bool wrongPassword;
    private bool start;
    private int wrongCount;

    private List<string> level1Password;
    private List<string> level2Password;
    private List<string> level3Password;
    private enum Screen {Credit, MainMenu, Password, Win };
    private Screen currentScreen;
    public AudioSource audioSrc;

    void Start()
    {
        level = 0;
        start = false;
        completed = new bool[3];
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        wrongPassword = false;
        level = 0;
        stage = 1;
        level1Password = new List<string> { "book", "pencil", "club", "exam", "desk",
                                            "essay", "glue", "math", "learn", "lesson",
                                            "paper", "quiz", "test", "school", "bell",
                                            "sport", "class", "locker", "hall", "teach"};
        level2Password = new List<string> { "company", "client", "bonus", "office", "papers",
                                            "party", "workers", "employee", "computer", "meeting",
                                            "branch", "manager", "finance", "interview", "customer",
                                            "phone", "business", "lead", "report", "emails"};
        level3Password = new List<string> { "g0vernment", "area51", "@liens", "hack1ng", "dec0de",
                                            "milit@ry", "defen$e", "m@tr1x", "t3rminal", "robot1c",
                                            "m@ch1n3", "radars", "nucl3ar", "tr@cking", "p@$$w0rd",};

        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        if(completed[0] && completed[1] && completed[2])
        {
            Terminal.WriteLine("Welcome back master hacker!");
        }
        StartUpSequence();  //Run once when started
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("1 - A local school");
        Terminal.WriteLine("2 - A random company");
        Terminal.WriteLine("3 - The Pentagon");
        if (completed[0] && completed[1] && completed[2])
        {
            Terminal.WriteLine("4 - Credit");
        }
        Terminal.WriteLine("Enter your input:");
    }

    private void StartUpSequence()
    {
        if (!start)
        {
            Terminal.WriteLine("Type 'Menu' anytime to return to menu.");
            start = true;
        }
    }

    //Take in and process input-----------------------------------------------------------------
    void OnUserInput(string input)
    {
        
        //Menu should always be available
        if (input == "menu" || input == "Menu" || input == "q")
        {
            currentScreen = Screen.MainMenu;
            ShowMainMenu();
        }
        else if (input == "quit" || input == "exit" || input == "Quit" || input == "Exit")
        {
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
        else if (currentScreen == Screen.Credit)
        {
            if(input == "hacker")
            {
                Application.Quit();
            }
        }
        else if (currentScreen == Screen.Win)
        {
            DisplayResult(input);
        }
    }

    private void DisplayResult(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");

        if (isValidLevelNumber)
        {
            currentScreen = Screen.Credit;
            if (level == 1)
            {
                completed[0] = true;
                audioSrc.Play();
                switch (input)
                {
                    case "1":
                        Terminal.WriteLine("You gave all teachers a raise!");
                        break;
                    case "2":
                        Terminal.WriteLine("You changed all students' grades" + "\n" + "to F!");
                        break;
                    case "3":
                        Terminal.WriteLine("You fixed your transcript to" + "\n" + "all A!");
                        break;
                    default:
                        Debug.LogError("Out of range level remove: " + level);
                        break;
                }
            }
            else if (level == 2)
            {
                completed[1] = true;
                audioSrc.Play();
                switch (input)
                {
                    case "1":
                        Terminal.WriteLine("No work tomorrow except the boss!");
                        break;
                    case "2":
                        Terminal.WriteLine("You promoted all janitors to" + "\n" + "executives!");
                        break;
                    case "3":
                        Terminal.WriteLine("You adopted a pug with company's" + "\n" + "money!");
                        break;
                    default:
                        Debug.LogError("Out of range level remove: " + level);
                        break;
                }
            }
            else
            {
                completed[2] = true;
                audioSrc.Play();
                switch (input)
                {
                    case "1":
                        Terminal.WriteLine("You banned people from making" + "\n" + "cringy tiktok!");
                        break;
                    case "2":
                        Terminal.WriteLine("You became the dictator of" + "\n" + "America!");
                        break;
                    case "3":
                        Terminal.WriteLine("You transfered 10 billions" + "\n" + "dollars to your bank!");
                        break;
                    default:
                        Debug.LogError("Out of range level remove: " + level);
                        break;
                }
            }
        }
        else
        {
            ShowMainMenu();
            Terminal.WriteLine("Kicked out!");
        }
    }

    //-------------------------------------------------------------------------------------------

    private void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");

        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            Terminal.ClearScreen();

            if (level == 1)
            {
                Terminal.WriteLine("Accessing a local school...");
            }
            else if (level == 2)
            {
                Terminal.WriteLine("Accessing a random company...");
            }
            else
            {
                Terminal.WriteLine("Accessing The Pentagon...");
            }

            Terminal.WriteLine("The Firewall scrambled the password!" + "\n");
            AskForPassword(input);
        }
        else if(input == "4" && (completed[0] && completed[1] && completed[2]))
        {
            Credit();
        }
        else
        {
            Terminal.WriteLine("Invalid Input! Try Again!");
        }
    }

    private void Credit()
    {
        currentScreen = Screen.Credit;
        Terminal.ClearScreen();
        Terminal.WriteLine("Credit" + "\n");
        Terminal.WriteLine("By Cat Tran");
        Terminal.WriteLine("Assets by GameDev.tv");
        Terminal.WriteLine("Thanks for Playing!" + "\n");
        Terminal.WriteLine("Clue: " + "hacker".Anagram());
    }

    void AskForPassword(string input)
    {
        currentScreen = Screen.Password;
        if (!wrongPassword)
        {
            SetRandomPassword();
        }
        
        Terminal.WriteLine("Clue: " + password.Anagram());
        Terminal.WriteLine("Please enter the password:");

    }

    private void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Password[(int)Random.Range(0, level1Password.Count)];
                break;
            case 2:
                password = level2Password[(int)Random.Range(0, level2Password.Count)];
                break;
            case 3:
                password = level3Password[(int)Random.Range(0, level3Password.Count)];
                break;
            default:
                Debug.LogError("Out of range level: " + level);
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            wrongPassword = false;
            stage++;
            RemoveAnswered();
            Terminal.ClearScreen();
            Terminal.WriteLine("Correct! Proceed to level " + stage + " security!" + "\n");
            AskForPassword(input);
            if (stage > 5) DisplayWinScreen();
        }
        else
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Type 'Menu' anytime to return to menu.");
            Terminal.WriteLine("Wrong Password! Try Again!" + "\n");
            wrongPassword = true;
            AskForPassword(input);
        }
    }

    void RemoveAnswered()
    {
        switch (level)
        {
            case 1:
                level1Password.Remove(password);
                break;
            case 2:
                level2Password.Remove(password);
                break;
            case 3:
                level3Password.Remove(password);
                break;
            default:
                Debug.LogError("Out of range level remove: " + level);
                break;
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        Terminal.WriteLine("'I'm in!' What will you do next?");
        switch (level)
        {
            case 1:
                Terminal.WriteLine(@"                        __
                       / _)
               _.----./ /
           __/ (  | (  |
          /__.-'|_|--|_|
");
                break;
            case 2:
                Terminal.WriteLine(@"                       __
            .,-;-;-,. /'_\
          _/_/_/_|_\_\) /
        '-<_><_><_><_>=/\
          `/_/====/_/-'\_\
");
                break;
            case 3:
                
                Terminal.WriteLine(@"
                         __|__
                __|__ *---oOo---*
       __|__ *---oOo---*
    *---oOo---*
");
                break;
            default:
                Debug.LogError("Out of range level on win screen: " + level);
                break;
        }
        WinningOption();
    }

    void WinningOption()
    {
        Terminal.WriteLine("1 - Good");
        Terminal.WriteLine("2 - Evil");
        Terminal.WriteLine("3 - Self");
    }
}
