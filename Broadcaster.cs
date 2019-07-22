using UnityEngine;
using System.Collections.Generic;


public static class Broadcaster
{
    private static GameObject prefabName;
    private static GameControl gameController;
    private static AudioSource soundHolder;
    private static GameObject leavedAt;


    private static string sub_month;
    private static string sub_quartal;
    private static string sub_year;
    public static string sub_week;
    private static string who;
    private static string buttonValue;
    private static bool isFreeLevel;
    private static bool freePassed;
    private static bool isFromGame;
    private static Admob admob;
    private static string main_mode_from;

    public static string From
    {
        get
        {
            return main_mode_from;
        }
        set
        {
            main_mode_from = value;
        }
    }

    public static Dictionary<string, int> PuzzlesPiecesToWin = new Dictionary<string, int>()
    {
        {"Dog",7 },
        {"Rabbit",8},
        {"Cat",8},
        {"Hengehog",7},
        {"Cow",8},
        {"Donkey",8},
        {"Eagle",8},
        {"Sloth",7},
        {"Whale",8},
        {"Fish",8},
        {"Panda",8},
        {"Parrot",8},
        {"Tiger",8},
        {"Beer",8},
        {"Cheetah",8},
        {"Jiraffe",8},
        {"Penguin",8},
        {"Leo",8},
        {"Lemur",8},
        {"Pig",8},
        {"Elephant",8},
        {"Fox",8},
        {"Owl",8},
        {"Squirrel",8},
        {"Hamster",8},
        {"Bat",8},
        {"Racoon",8},
        {"Tortoise",8},
        {"Monkey",8},
        {"Chameleon",8},
        {"Luigi",13},
        {"Guido",16},
        {"Monty",12},
        {"Sally",13},
        {"Sheriff",13},
        {"Dino1",11},
        {"Dino2",18},
        {"Dino3",14},
        {"Dino4",12},
        {"Dino5",13},
    };
    public static Dictionary<string, string> PuzzlesWho = new Dictionary<string, string>()
    {
        {"Dog","Animal"},
        {"Rabbit","Animal"},
        {"Cat","Animal"},
        {"Hengehog","Animal"},
        {"Cow","Animal"},
        {"Donkey","Animal"},
        {"Eagle","Animal"},
        {"Sloth","Animal"},
        {"Whale","Animal"},
        {"Fish","Animal"},
        {"Panda","Animal"},
        {"Parrot","Animal"},
        {"Tiger","Animal"},
        {"Beer","Animal"},
        {"Cheetah","Animal"},
        {"Jiraffe","Animal"},
        {"Penguin","Animal"},
        {"Leo","Animal"},
        {"Lemur","Animal"},
        {"Pig","Animal"},
        {"Elephant","Animal"},
        {"Fox","Animal"},
        {"Owl","Animal"},
        {"Squirrel","Animal"},
        {"Hamster","Animal"},
        {"Bat","Animal"},
        {"Racoon","Animal"},
        {"Tortoise","Animal"},
        {"Monkey","Animal"},
        {"Chameleon","Animal"},
        {"Luigi","Car"},
        {"Guido","Car"},
        {"Monty","Car"},
        {"Sally","Car"},
        {"Sheriff","Car"},
        {"Dino1","Dino"},
        {"Dino2","Dino"},
        {"Dino3","Dino"},
        {"Dino4","Dino"},
        {"Dino5","Dino"},
    };
    public static Dictionary<string, int> PuzzlesIsFree = new Dictionary<string, int>()
    {
        {"Dog",1},
        {"Rabbit",1},
        {"Cat",1},
        {"Hengehog",1},
        {"Cow",1},
        {"Donkey",1},
        {"Eagle",1},
        {"Sloth",1},
        {"Whale",1},
        {"Fish",1},
        {"Panda",1},
        {"Parrot",1},
        {"Tiger",0},
        {"Beer",0},
        {"Cheetah",0},
        {"Jiraffe",0},
        {"Penguin",0},
        {"Leo",0},
        {"Lemur",0},
        {"Pig",0},
        {"Elephant",0},
        {"Fox",0},
        {"Owl",0},
        {"Squirrel",0},
        {"Hamster",0},
        {"Bat",0},
        {"Racoon",0},
        {"Tortoise",0},
        {"Monkey",0},
        {"Chameleon",0},
        {"Luigi",1},
        {"Guido",1},
        {"Monty",1},
        {"Sally",1},
        {"Sheriff",1},
        {"Dino1",1},
        {"Dino2",1},
        {"Dino3",1},
        {"Dino4",1},
        {"Dino5",1},
    };
    public static string[] pref_num = new string[]
        {
            "Dog",
            "Rabbit",
            "Cat",

            "Hengehog",
            "Cow",
            "Donkey",

            "Eagle",
            "Sloth",
            "Whale",


            "Fish",
            "Panda",
            "Parrot",

            "Tiger",
            "Beer",
            "Cheetah",

             "Jiraffe",
             "Penguin",
             "Leo",

             "Lemur",
             "Pig",
             "Elephant",

            "Fox",
            "Owl",
            "Squirrel",

            "Hamster",
            "Bat",
            "Racoon",

            "Tortoise",
            "Monkey",
            "Chameleon"
             };
    public static string[] pref_num_cars = new string[]
        {
            "Luigi",
            "Guido",
            "Sheriff",
            "Sally",
            "Monty"
             };
    public static string[] pref_num_dinos = new string[]
        {
            "Dino1",
            "Dino2",
            "Dino3",
            "Dino4",
            "Dino5"
             };

    public static Admob AdHolder
    {
        get
        {
            return admob;
        }
        set
        {
            admob = value;
        }
    }
    public static GameObject Prefab
    {
        get
        {
            return prefabName;
        }
        set
        {
            prefabName = value;
        }
    }

    


    public static GameObject lastPlayed
    {
        get
        {
            return leavedAt;
        }
        set
        {
            leavedAt = value;
        }
    }
    public static AudioSource sounds
    {
        get
        {
            return soundHolder;
        }
        set
        {
            soundHolder = value;
        }
    }
    public static GameControl Game
    {
        get
        {
            return gameController;
        }
        set
        {
            gameController = value;
        }
    }



    public static string Month
    {
        get
        {
            return sub_month;
        }
        set
        {
            sub_month = value;
        }
    }
    public static string ParentalButton
    {
        get
        {
            return buttonValue;
        }
        set
        {
            buttonValue = value;
        }
    }
    public static string Week
    {
        get
        {
            return sub_week;
        }
        set
        {
            sub_week = value;
        }
    }
    public static string Year
    {
        get
        {
            return sub_year;
        }
        set
        {
            sub_year = value;
        }
    }
    public static string Quartal
    {
        get
        {
            return sub_quartal;
        }
        set
        {
            sub_quartal = value;
        }
    }
    public static string Who
    {
        get
        {
            return who;
        }
        set
        {
            who = value;
        }
    }
    public static bool isFreeLvl
    {
        get
        {
            return ((who == "Dog")
                || (who == "Rabbit")
                || (who == "Cat")
                || (who == "Hengehog")
                || (who == "Cow")
                || (who == "Donkey")
                || (who == "Eagle")
                || (who == "Sloth")
                || (who == "Whale")
                || (who == "Fish")
                || (who == "Panda")
                || (who == "Luigi")
                || (who == "Guido")
                || (who == "Sally")
                || (who == "Monty")
                || (who == "Sheriff")
                || (who == "Dino1")
                || (who == "Dino2")
                || (who == "Dino3")
                || (who == "Dino4")
                || (who == "Dino5")
                || (who == "Parrot"));
        }
        set
        {
            isFreeLevel = value;
        }
    }
    public static bool isEPFromGame
    {
        get
        {
            return isFromGame;
        }
        set
        {
            isFromGame = value;
        }
    }
    public static bool freeLevelsPassed
    {
        get
        {
            return ((PlayerPrefs.GetInt("Dog") == 1) &&
            (PlayerPrefs.GetInt("Rabbit") == 1) &&
            (PlayerPrefs.GetInt("Cat") == 1) &&
            (PlayerPrefs.GetInt("Hengehog") == 1) &&
            (PlayerPrefs.GetInt("Cow") == 1) &&
            (PlayerPrefs.GetInt("Donkey") == 1) &&
            (PlayerPrefs.GetInt("Eagle") == 1) &&
            (PlayerPrefs.GetInt("Sloth") == 1) &&
            (PlayerPrefs.GetInt("Whale") == 1) &&
            (PlayerPrefs.GetInt("Fish") == 1) &&
            (PlayerPrefs.GetInt("Panda") == 1) &&
            (PlayerPrefs.GetInt("Parrot") == 1) &&
            (PlayerPrefs.GetInt("Dino1") == 1) &&
            (PlayerPrefs.GetInt("Dino2") == 1) &&
            (PlayerPrefs.GetInt("Dino3") == 1) &&
            (PlayerPrefs.GetInt("Dino4") == 1) &&
            (PlayerPrefs.GetInt("Dino5") == 1) &&
            (PlayerPrefs.GetInt("Sally") == 1) &&
            (PlayerPrefs.GetInt("Monty") == 1) &&
            (PlayerPrefs.GetInt("Sheriff") == 1) &&
            (PlayerPrefs.GetInt("Luigi") == 1) &&
            (PlayerPrefs.GetInt("Guido") == 1));
        }
        set
        {
            freePassed = value;
        }
    }
}