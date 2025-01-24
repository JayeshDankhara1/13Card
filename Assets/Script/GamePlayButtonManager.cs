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

           }
    }

    public void SwitchButton_Click()
    {
        Ref_GamePlayManager.Ref_GamePlayUiManager.SwitchList();
        Ref_GamePlayManager.Ref_GamePlayUiManager.DeHighliteAllCard();
        Ref_GamePlayManager.ShowResult();

    }
    #endregion
}
