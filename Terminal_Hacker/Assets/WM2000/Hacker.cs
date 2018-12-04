using System;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    //Configuration data
    enum Screen { MainMenu, Password, Win }
    Dictionary<string, List<string>> PasswordsByLevel
    {
        get
        {
            return new Dictionary<string, List<string>>()
            {
                {"1", Level1Passwords }, 
                {"2", Level2Passwords },
                {"3", Level3Passwords },
                {"4", Level4Passwords }
            };
        }
    }
    List<string> Level1Passwords
    {
        get
        {
            return new List<string> { "Legos", "Snack", "Game", "Puppy", "Sports" };
        }
    }
    List<string> Level2Passwords
    {
        get
        {
            return new List<string> { "Business", "Vacation", "Chardonnay", "Children", "Gentrification" };
        }
    }
    List<string> Level3Passwords
    {
        get
        {
            return new List<string>() { "Rapacious", "Adulation","Consternation","Querulous","Beatific" };
        }
    }
    List<string> Level4Passwords
    {
        get
        {
            return new List<string>() { "Wooky", "Chuba", "Bukee", "Goola", "PooDoo"};
        }
    }
    string Greeting
    {
        get
        {
            return "Welcome, analyst.";
        }
    }

    //Game state
    Screen currentScreen;
    enum Level
    {
        Child = 1,
        Adult = 2,
        Dog = 3,
        Alien = 4
    }
    int currentLevel;
    string password;

    //For testing random selection functions
    //void Update()
    //{
    //    int index1 = Random.Range(0, PasswordsByLevel["1"].Count);
    //    int index2 = Random.Range(0, PasswordsByLevel["2"].Count);
    //    print(index1);
    //    print(index2);
    //}

	// Use this for initialization

	void Start () {
        ShowMainMenu();
    }
	
    void OnUserInput(string input)
    {
        if (input == "menu") // we can always go directly to main menu
        {
            ShowMainMenu();
        }
        else if(currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            RunMatchPassword(input);
        }
    }

    //Print main menu
    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("*Alpha build");
        Terminal.WriteLine(Greeting);
        Terminal.WriteLine("Read their mind:");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1: Child");
        Terminal.WriteLine("Press 2: Adult");
        Terminal.WriteLine("Press 3: Dog");
        Terminal.WriteLine("Press 4: Alien");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter selection:");
    }

    //Select a level
    void RunMainMenu(string input)
    {
        if (PasswordsByLevel.ContainsKey(input))
        {
            currentLevel = int.Parse(input);
            password = PasswordsByLevel[input][UnityEngine.Random.Range(0, PasswordsByLevel[input].Count)];
            AskForPassword();
        }
        else
        {
            string message = "";
            message += "You must select valid difficulty";
            Terminal.WriteLine(message);
        }
    }

    //Print password screen
    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        Terminal.WriteLine("You chose level " + currentLevel);
        Terminal.WriteLine("Enter your password: ");
        Terminal.WriteLine("Hint: "+ password.Anagram());

    }

    //Try to match the password
    void RunMatchPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
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
        switch (currentLevel)
        {
            case 1:
                Level1Reward();
                break;
            case 2:
                Level2Reward();
                break;
            case 3:
                Level3Reward();
                break;
            case 4:
                Level4Reward();
                break;
        }    
    }

    

    void TemporaryWinText()
    {
        Terminal.WriteLine("You win. Type menu for next challenge");
    }

    void Level1Reward()
    {
        AsciiBrain();
        TemporaryWinText();
    }

    void Level2Reward()
    {
        AsciiBrain();
        TemporaryWinText();
    }

    void Level3Reward()
    {
        AsciiBrain();
        TemporaryWinText();
    }

    void Level4Reward()
    {
        AsciiBrain();
        TemporaryWinText();
    }

    void AsciiBrain()
    {
        Terminal.WriteLine(@"
        _---~~(~~-_.
    _{            )   )
  ,   ) -~~-     ( ,-'   )_
 (  `-,_..`., )     -- '_,)
( ` _)  (  -~(     -_ `,  }
(_-  _  ~_-~~~~`,     ,' )
`~ -^(    __;-,   ((()))
        ~~~~ {_ -_ (())
              `\  } 
        ");
    }

    
}
