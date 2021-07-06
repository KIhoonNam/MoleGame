using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdBomScript : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-3940256099942544/6300978111";
    private readonly string test_unitID = "ca-app-pub-3940256099942544/6300978111";

    private readonly string test_deviceID = "";
    private InterstitialAd interstitial;
    private BannerView banner;
    public AdSize size = new AdSize(1440,120);
    public AdPosition position;


    private void Start()
    {
        InitID();
        RequestInterstitial();
    }

    private void InitID() 
    {
        string id = Debug.isDebugBuild ? test_unitID : unitID;

        banner = new BannerView(id, AdSize.SmartBanner, position);

        AdRequest request = new AdRequest.Builder().Build() ;

        banner.LoadAd(request);
    }


    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/8691691433";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/8691691433";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }


    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.ToString());
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void ToggleAd(bool active)
    {
        if(active)
        {
            banner.Show();
        }
        else
        {
            banner.Hide();
        }
    }

    public void DestroyAd()
    {
        banner.Destroy();
    }

    public void DestroyRequest()
    {
        interstitial.Destroy();
    }

    public void GameOver()
    {
        if(interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
}
    