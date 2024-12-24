using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData 
{
    #region Sound 
    public static float Sound { 
        get
        {
           return PlayerPrefs.GetFloat("Sound", 0.0f);
        }
        set
        {
            PlayerPrefs.SetFloat("Sound", value);
            PlayerPrefs.Save();
        }
    }
    public static int MuteSound
    {
        get
        {
            return PlayerPrefs.GetInt("MuteSound", 0);
        }
        set
        {
            PlayerPrefs.SetInt("MuteSound", value);
            PlayerPrefs.Save();

        }
    }
    #endregion

    #region Music
    public static float Music {
        get
        {
            return PlayerPrefs.GetFloat("Music", 0.0f);
           
        }
        set
        {
            PlayerPrefs.SetFloat("Music", value);
            PlayerPrefs.Save();
        }
    }

    public static int MuteMusic {
        get
        {
            return PlayerPrefs.GetInt("MuteMusic", 0);
        }
        set
        {
            PlayerPrefs.SetInt("MuteMusic", value);
            PlayerPrefs.Save();

        }
    }
    #endregion

    #region Score
    public static int Score
    {
        get
        {
            return PlayerPrefs.GetInt("Score", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Score", value);
            PlayerPrefs.Save();

        }
    }

    public static int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt("HighScore", 0);
        }
        set
        {
            PlayerPrefs.SetInt("HighScore", value);
            PlayerPrefs.Save();

        }
    }
    #endregion

    #region Touch
    public static float Touch
    {
        get
        {
            return PlayerPrefs.GetFloat("Touch", 0.0f);

        }
        set
        {
            PlayerPrefs.SetFloat("Touch", value);
            PlayerPrefs.Save();
        }
    }

    public static int MuteTouch
    {
        get
        {
            return PlayerPrefs.GetInt("MuteTouch", 0);
        }
        set
        {
            PlayerPrefs.SetInt("MuteTouch", value);
            PlayerPrefs.Save();

        }
    }

    #endregion

    public static int Player_Touch
    {
        get
        {
            return PlayerPrefs.GetInt("Player_Touch", 0 ) ;

        }
        set
        {
            PlayerPrefs.SetInt("Player_Touch", value);
            PlayerPrefs.Save();
        }
    }


}
