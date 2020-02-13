using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    public Transform WhiteKey;
    public Transform BlackKey;
    public Transform StartPos;
    public Canvas gui;
    public Transform KeyHolder;

    private float StageWidth, StageHeight;
    List<string> notes;

    public List<Transform> keys;
    public AudioClip[] Cliplist;
    public AudioClip[] CliplistRotary;
    public Dictionary<string, AudioClip> Tones;
    public Dictionary<string, AudioClip> TonesRotary;

    public Transform top;
    public Transform firstpos,lastpos;

    private void Start()
    {
        RectTransform o = gui.GetComponent<RectTransform>();
        StageWidth = o.rect.width;
        StageHeight = o.rect.height;

        var rt = top.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(StageWidth, rt.sizeDelta.y);


        Application.targetFrameRate = 300;
        Cliplist = Resources.LoadAll<AudioClip>("Samples");
        CliplistRotary = Resources.LoadAll<AudioClip>("SamplesRotary");

        Tones = new Dictionary<string, AudioClip>();

        for (int i = 0; i < Cliplist.Length; i++)
        {
            Tones.Add(Cliplist[i].name, Cliplist[i]);
        }

        TonesRotary = new Dictionary<string, AudioClip>();

        for (int i = 0; i < CliplistRotary.Length; i++)
        {
            TonesRotary.Add(CliplistRotary[i].name, CliplistRotary[i]);
        }

        notes = new List<string>
        {
            "c","c#","d","d#","e","f","f#","g","g#","a","a#","b"
        };
        keys = new List<Transform>();

        float keyWidth = WhiteKey.GetComponent<SpriteRenderer>().bounds.size.x;
        int k = 0;

        int Octav = 1;


        for (int i=0; i<36; i++)
        {
            float x = StartPos.position.x + keyWidth*i;
            Vector2 pos = new Vector2(x,StartPos.position.y);
            if (i == 0)
            {
                firstpos.position = pos;
            }
            if (i == 35)
            {
                lastpos.position = new Vector2(x+keyWidth,StartPos.position.y);
            }
           

            if (k == 1 || k == 3 || k == 6 || k == 8 || k == 10)
            {
                var gb = Instantiate(BlackKey, new Vector2(x - keyWidth/2, StartPos.position.y), BlackKey.rotation);
                gb.name = notes[k] + Octav.ToString();
                k++;
                keys.Add(gb);
                gb.SetParent(KeyHolder, false);
            }

            var gw = Instantiate(WhiteKey, pos, WhiteKey.rotation);

            gw.name = notes[k] + Octav.ToString();
            keys.Add(gw);
            gw.SetParent(KeyHolder, false);

            if (k >= 11)
            {
                k = 0;
                Octav++;
            }
            else
            {
                k++;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadYourAsyncScene("intro"));
        }
    }


    IEnumerator LoadYourAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
