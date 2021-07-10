using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdScript : MonoBehaviour
{
    // public GameManagerObject ManagerObject;
    public bool BlockAds;
    public static bool BannerOn;
    private void Awake()
    {
        RequestInterstitialAds();
    }
    void Start()
    {
        if (!BlockAds)
        {
            // ManagerObject = GameObject.Find("GameManagerObject").GetComponent<GameManagerObject>();

            if (BannerOn == false)
            {
                //print("tru");
                showBannerAd();
                BannerOn = true;


            }
        }
  
    }

    private void showBannerAd()
    {
   //string adID = "ca-app-pub-3940256099942544/6300978111";
     string adID = "ca-app-pub-2519339969103025/3936026493";
     
     /*
        //***For Testing in the Device***
        AdRequest request = new AdRequest.Builder()
       .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
       .Build();
 */

        //***For Production When Submit App***
       AdRequest request = new AdRequest.Builder().Build();

        BannerView bannerAd = new BannerView(adID, AdSize.SmartBanner, AdPosition.Bottom);
        bannerAd.LoadAd(request);
    }
    
    public void showInterstitialAd()
    {
        if (!BlockAds)
        {

            //Show Ad
            if (interstitial.IsLoaded())
            {
                interstitial.Show();

                //Stop Sound
                //

                //            Debug.Log("SHOW AD XXX");
                RequestInterstitialAds();
            }
        }
    }

    InterstitialAd interstitial;
    private void RequestInterstitialAds()
    {
      //   string adID = "ca-app-pub-3940256099942544/1033173712";
     string adID = "ca-app-pub-2519339969103025/4399060929";
#if UNITY_ANDROID
        string adUnitId = adID;
#elif UNITY_IOS
        string adUnitId = adID;
#else
        string adUnitId = adID;
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

  /*
        //***Test***
        AdRequest request = new AdRequest.Builder()
       .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
       .Build();
    */

        //***Production***
       AdRequest request = new AdRequest.Builder().Build();

        //Register Ad Close Event
        interstitial.OnAdClosed += Interstitial_OnAdClosed;

        // Load the interstitial with the request.
        interstitial.LoadAd(request);

       // Debug.Log("AD LOADED XXX");


    }

    //Ad Close Event
    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        //Resume Play Sound

    }

}



