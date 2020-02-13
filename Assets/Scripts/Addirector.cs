using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addirector : MonoBehaviour
{
    admob ads;
    
    void Start()
    {
        ads = FindObjectOfType<admob>();
    }

    public void PrepareAds()
    {
        ads.RequestInterstitial();
    }

    public void PrepareBanner()
    {
        ads.RequestBanner();
    }

    public void ShowAds()
    {
        ads.ShowInterstitial();
    }

    public void AdFinished()
    {
    }
}
