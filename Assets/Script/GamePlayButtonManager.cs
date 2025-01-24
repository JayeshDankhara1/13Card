using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayButtonManager : MonoBehaviour
{
    public GamePlayManager Ref_GamePlayManager;

    #region function
    public void ButtonControl(string Btn_Name)
    {

           switch(Btn_Name)
           {
            case "Switch":
                SwitchButton_Click();
                break;
            case "Submit":
                GameOver();
                break;
            case "Restart":
                GameStart();
                break;

           }
    }

    public void SwitchButton_Click()
    {
        Ref_GamePlayManager.Ref_GamePlayUiManager.SwitchList();
        Ref_GamePlayManager.Ref_GamePlayUiManager.DeHighliteAllCard();
        Ref_GamePlayManager.ShowResult();

    }

    public void GameOver()
    {
        Ref_GamePlayManager.GameOver();
        if (Ref_GamePlayManager.Check_Score())
        {
            Ref_GamePlayManager.Ref_GamePlayUiManager.GameOver("You Are Win !",true);
        }
        else
        {
            Ref_GamePlayManager.Ref_GamePlayUiManager.GameOver("You Are Loss !");
        }
    }

    public void GameStart()
    {
        Ref_GamePlayManager.Ref_GoogleAds.ShowRewardedAd();
        Ref_GamePlayManager.GameSatrt();
    }
    #endregion
}
