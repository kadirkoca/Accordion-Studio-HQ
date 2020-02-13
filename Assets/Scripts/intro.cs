using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class intro : MonoBehaviour
{

    public Button share;
    public Button star;
    public Button store;
    public Button play;

    Addirector add;

    void Start()
    {
        share.onClick.AddListener(shareit);
        star.onClick.AddListener(starit);
        store.onClick.AddListener(storeit);
        play.onClick.AddListener(playit);

        add = FindObjectOfType<Addirector>();
        StartCoroutine("Ads");
    }

    IEnumerator Ads()
    {
        yield return new WaitForSeconds(2f);
        add.PrepareAds();
        add.PrepareBanner();
    }

    private void playit()
    {
        StartCoroutine(LoadYourAsyncScene("game"));
        try
        {
            add.ShowAds();
        }
        catch (System.Exception) { }
    }

    private void storeit()
    {
        Application.OpenURL("http://play.google.com/store/apps/dev?id=6635738809391543064");
    }

    private void starit()
    {
        Application.OpenURL("http://play.google.com/store/apps/details?id=com.kazekasdl.accordionstudiohq");
    }

    private void shareit()
    {
        OnShareClicked();
    }


    IEnumerator LoadYourAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    public void OnShareClicked()
    {
#if UNITY_ANDROID
        // Get the required Intent and UnityPlayer classes.
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        // Construct the intent.
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");
        intent.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intent.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "http://play.google.com/store/apps/details?id=com.kazekasdl.accordionstudiohq");
        intent.Call<AndroidJavaObject>("setType", "text/plain");

        // Display the chooser.
        AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intent, "Share");
        currentActivity.Call("startActivity", chooser);
#endif
    }

}
