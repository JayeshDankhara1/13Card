using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoogleAds : MonoBehaviour
{
    public static GoogleAds instance;

    #region Variables

    public string appId = "ca-app-pub-4109577825364690~1644359047";

    [Space]
    public UnityEvent OnRewardedAdSeen;



     public string bannerld = "ca-app-pub-3940256099942544/6300978111";
     public string interld = "ca-app-pub-3940256099942544/1033173712";
     public  string rewardedld = "ca-app-pub-3940256099942544/5224354917";
     public  string AppOpenID = "ca-app-pub-3940256099942544/9257395921";


    public BannerView bannerView;
    public InterstitialAd interstitialAd;
    public RewardedAd rewardedAd;
    private AppOpenAd appOpenAd;

    #endregion

    #region Unity Functions

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadAllAdAtStart(initStatus);
        });
    }

    #endregion

    #region Load Ad Function

    public void LoadAllAdAtStart(InitializationStatus initstatus)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
        });
            LoadAllAd();
    }

    public void LoadAllAd()
    {
        LoadRewardedAd();
        LoadAppOpenAd();
        LoadInterstitialAd();
    }

    public void LoadAppOpen_WithValidation()
    {
        /*if (//RewardManager.instance != null
            && //RewardManager.instance.CheckIfAnyAppOpenAdTrue())
        {
            LoadAppOpenAd();
        }*/
        if (true)
        {
            LoadAppOpenAd();
        }

    }

    public void LoadInterstitial_WithValidation()
    {
        /*if (//RewardManager.instance != null
            && //RewardManager.instance.CheckIfAnyInterstitialAdTrue())
        {
            LoadInterstitialAd();
        }*/

        if (true)
        {
            LoadInterstitialAd();
        }
    }

    #endregion

    #region Banner Ads

    public void ShowBannerAd()
    {
       // Debug.Log("ShowBannerAd");

        // Create a new banner
        CreateBannerView();

        // listen to banner events
        ListenBannerEvents();

        // load the banner

        if (bannerView == null) CreateBannerView();


        AdRequest adRequest = new();
        adRequest.Keywords.Add("unity-admode-sample");

      //  Debug.Log("Banner Ads Loaded!");

        bannerView.LoadAd(adRequest);
    }

    private void ListenBannerEvents()
    {
        // Raised when an ad is loaded into the banner view.
        bannerView.OnBannerAdLoaded += () =>
        {
            /*Debug.Log("Banner view loaded an ad with response : "
                + bannerView.GetResponseInfo());*/
        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            /*Debug.LogWarning("Banner view failed to load an ad with error : "
                + error);*/
        };
        // Raised when the ad is estimated to have earned money.
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            /*Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));*/
        };
        // Raised when an impression is recorded for an ad.
        bannerView.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        bannerView.OnAdClicked += () =>
        {
            //Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            //Debug.Log("Banner view full screen content closed.");
        };
    }

    private void CreateBannerView()
    {
        if (bannerView != null)
        {
            DestroyBannerAd();
        }

        bannerView = new(bannerld, new AdSize(320, 50), AdPosition.Bottom);
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }

    #endregion

    #region Interstitial Ads

    public void LoadInterstitialAd()
    {
        //Debug.LogWarning("LoadInterstitialAd");
        //AnalyticManager.AnalyticEvent(//AnalyticManager.InterstitialAdLoad);


        // Create a new interstitial
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        AdRequest adRequest = new();

        adRequest.Keywords.Add("unity-admob-sample");


        InterstitialAd.Load(interld, adRequest, (InterstitialAd ad, LoadAdError err) =>
        {
            if (err != null || ad == null)
            {
               // Debug.LogWarning("Interstitial Fail To Load Add!");
                return;
            }
            else
            {
                interstitialAd = ad;
                InterstitialEvents(interstitialAd);
            }


        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
         //   Debug.Log("Show Interstitial Ad");
            //AnalyticManager.AnalyticEvent(//AnalyticManager.InterstitialAdShow);
            //RewardManager.instance.IsInterstitialAdShowing = true;

            interstitialAd.Show();
        }
        else
        {
          //  Debug.LogWarning("Interstitial ad not show");
            //AnalyticManager.AnalyticEvent(//AnalyticManager.InterstitialAdNotLoaded);
            LoadInterstitialAd();
            //RewardManager.instance.InterstitialComp();
        }
    }

    private void InterstitialEvents(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            /*   Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                   adValue.Value,
                   adValue.CurrencyCode));*/
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            //Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            // Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                //AnalyticManager.AnalyticEvent(//AnalyticManager.InterstitialAdEnd);

               // Debug.Log("Interstitial ad full screen content closed.");
                LoadInterstitialAd();
                //RewardManager.instance.InterstitialComp();
            });
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            /*Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);*/
        };

    }

    #endregion

    #region Rewarded Ads

    public bool IsRewardAdLoaded()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            return true;
        }
        else
        {
            LoadRewardedAd();
            return false;
        }
    }

    public bool LoadRewardedAd()
    {
        //AnalyticManager.AnalyticEvent(//AnalyticManager.RewardAdLoad);

        bool result = false;
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        AdRequest adRequest = new();
        adRequest.Keywords.Add("unity-admob-sample");

        RewardedAd.Load(rewardedld, adRequest, (RewardedAd ad, LoadAdError err) =>
        {
            if (err != null || ad == null)
            {
              //  Debug.LogWarning("Reward Fail To Load Add!");
                result = false;
                return;
            }
            else
            {
             //   Debug.Log("RewardedAd loaded! " + Environment.NewLine + ad.GetResponseInfo());
                rewardedAd = ad;

                RewardedAdEvents(rewardedAd);
                result = true;
            }

        });
        return result;
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
          //  Debug.Log("reward Ad Show");
            //AnalyticManager.AnalyticEvent(//AnalyticManager.RewardAdShow);
            //RewardManager.instance.SoundAndMusicPause(true);
            //RewardManager.instance.IsRewardAdShowing = true;


            rewardedAd.Show((Reward reward) =>
            {
              //  Debug.Log("You Got Rewarded!");

                //RewardManager.instance.RewardVideoComp();
            });
        }
        else
        {
          //  Debug.LogWarning("reward ad tatty");
            //AnalyticManager.AnalyticEvent(//AnalyticManager.RewardAdNotLoaded);
            //RewardManager.instance.CallBannerAd();
            //GamePlayManager.instance.IsPlaying = true;

            LoadRewardedAd();
            //RewardManager.instance.Open_NoAD_PopUp();
        }
    }

    public void RewardedAdEvents(RewardedAd ad)
    {

        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            /* Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                 adValue.Value,
                 adValue.CurrencyCode));*/
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            //Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {

            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                //AnalyticManager.AnalyticEvent(//AnalyticManager.RewardAdEnd);
                //RewardManager.instance.CallBannerAd();
                ////GamePlayManager.instance?.HandleCallBackWhenRewardVideoClose();
                LoadRewardedAd();
                //Debug.Log("Rewarded ad full screen content closed.");
            });
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                ////RewardManager.instance.CallBannerAd();
                //////GamePlayManager.instance?.HandleCallBackWhenRewardVideoClose();
                LoadRewardedAd();
            });
            /* Debug.LogError("Rewarded ad failed to open full screen content " +
                            "with error : " + error);*/
        };
    }
    #endregion

    #region AppOpenAd

    public void LoadAppOpenAd()
    {
        ////AnalyticManager.AnalyticEvent(//AnalyticManager.AppOpenAdLoad);
        // Clean up the old ad before loading a new one.
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        //Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(AppOpenID, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    /*Debug.LogWarning("app open ad failed to load an ad " +
                                   "with error : " + error);*/

                    return;
                }
                else
                {
                    /*Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());*/

                    appOpenAd = ad;
                    RegisterEventHandlers(ad);
                }

            });
    }

    public void ShowAppOpenAd()
    {

        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
          //  Debug.Log("Show App Open Ad");
            //AnalyticManager.AnalyticEvent(//AnalyticManager.AppOpenAdShow);
            appOpenAd.Show();
        }
        else
        {
          //  Debug.LogWarning("Show App Open Ad tatty");

            LoadAppOpenAd();
            ////RewardManager.instance.AppOpenComp();
        }
    }

    private void RegisterEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            /*Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));*/
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {

            //Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                //Debug.Log("App open ad full screen content closed.");
                //AnalyticManager.AnalyticEvent(//AnalyticManager.AppOpenAdEnd);
                LoadAppOpenAd();
                //RewardManager.instance.AppOpenComp();
            });
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            /*Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);*/
        };
    }

    #endregion


}

