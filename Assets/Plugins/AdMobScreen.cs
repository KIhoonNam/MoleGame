using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobScreen : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-3940256099942544/6300978111";
    private readonly string test_unitID = "ca-app-pub-3940256099942544/6300978111";

    private readonly string test_deviceID = "";

    private InterstitialAd screenAd;

    private void InitID()
    {
        string id = Debug.isDebugBuild ? test_unitID : unitID;

        screenAd = new InterstitialAd(id);

        AdRequest request = new AdRequest.Builder().Build();

        screenAd.LoadAd(request);
        screenAd.Show();
    }

    IEnumerator ShowscreenAd()
    {
        while(!screenAd.IsLoaded())
        {
            yield return null;
        }

        screenAd.Show();
    }

    void Show()
    {
        StartCoroutine(ShowscreenAd());
    }

    // Start is called before the first frame update
    void Start()
    {
        InitID();
        Invoke("Show", 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
