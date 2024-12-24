 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.IK;

public class SplashScreenButtonManager : MonoBehaviour
{
 /*   public SplashScreenUiManagar Ref_SplashScreenUiManagar;
    public Animation Ref_Animation;

    public void Start()
    {
        Ref_SplashScreenUiManagar.SettingPopUP_Close();
        Ref_Animation = Animation.instance;
    }
    public void ButtonManager(string BtnName)
    {
        if (Ref_Animation.IsClick) return;

        Ref_SplashScreenUiManagar.Play_TouchSound();

        switch (BtnName)
        {
            case "Play":
                //  Invoke(nameof(Play_Click), 0.3f);
                Play_Click();
                break;
            case "Open_SettingPopUp":
                SettingPopUp_Click();
                break;
            case "Close_SettingPopUp":
                SettingPopUp_Close();
                break;
            case "Sound_Click":
                Sound_Click();
                break;
            case "Music_Click":
                Music_Click();
                break;
            case "Sound_Slidar":
                SoundSlidar_Click();
                break;
            case "Music_Slidar":
                MusicSlidar_Click();
                break;
            case "Singal_Touch":
                SingalTouch_Click();
                break;
            case "Multi_Touch":
                MultiTouch_Click();
                break;
        }
    }

    public void Play_Click()
    {
                     Ref_SplashScreenUiManagar.PlayBtnCLick();
        
    }
    public void Sound_Click()
    {
        if (StaticData.MuteSound == 0)
        {
            Ref_SplashScreenUiManagar.Sound_Off();
           
        }
        else
        {
            Ref_SplashScreenUiManagar.Sound_on();
            
        }
    }

    public void Music_Click()
    {
        if (StaticData.MuteMusic == 0)
        {
            Ref_SplashScreenUiManagar.Music_off();
       
        }
        else
        {
            Ref_SplashScreenUiManagar.Music_on();
            
        }
    }

    public void SoundSlidar_Click()
    {
        Ref_SplashScreenUiManagar.Sound_Slider_Click();
    }

    public void MusicSlidar_Click()
    {
        Ref_SplashScreenUiManagar.Music_Slider_Click();
    }


   
    public void SettingPopUp_Click()
    {

        Ref_SplashScreenUiManagar.SettingPopUp_Open();
    }

    public void SettingPopUp_Close()
    {
        Ref_SplashScreenUiManagar.SettingPopUP_Close();
       
    }

    public void SingalTouch_Click()
    {
        Ref_SplashScreenUiManagar.Touch_On();
    }

    public void MultiTouch_Click()
    {
        Ref_SplashScreenUiManagar.Touch_Off();
    }

 

 */
}
