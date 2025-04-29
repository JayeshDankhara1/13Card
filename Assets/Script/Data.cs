using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static int Score {
        get {
            return PlayerPrefs.GetInt("Score", 0);
        }
        set {
            PlayerPrefs.SetInt("Score", value);
            PlayerPrefs.Save();
        }
    }
    public static int Highscore
    {
        get
        {
            return PlayerPrefs.GetInt("Highscore", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Highscore", value);
            PlayerPrefs.Save();
        }
    }



}
