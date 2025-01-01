using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayButtonManager : MonoBehaviour
{
    public GamePlayManager Ref_GamePlayManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
    }
}
