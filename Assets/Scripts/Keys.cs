using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Keys : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public bool isWhite;
    public Sprite whiteUP;
    public Sprite whiteDOWN;
    public Sprite blackUP;
    public Sprite blackDOWN;
    Canvas Gui;
    private float StageWidth, StageHeight;
    bool mustplay = false;
    AudioSource source;
    Stage st;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        source = GetComponent<AudioSource>();
        st = FindObjectOfType<Stage>();

        source.clip = st.Tones[transform.name];
    }


    public void ChangeClip(bool rotartOn)
    {
        if (rotartOn == false)
        {
            source.clip = st.Tones[transform.name];
        }
        else
        {
            source.clip = st.TonesRotary[transform.name];
        }
    }

    void Update()
    {
        if (mustplay == true)
        {
            if (!source.isPlaying)
            {
                PlayKey();
            }
        }
        else
        {
            if (source.isPlaying)
            {
                StopKey();
            }
        }
    }




    public void OnPointerDown(PointerEventData eventData)
    {
        if (!source.isPlaying)
        {
            PlayKey();
        }
        mustplay = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!source.isPlaying)
        {
            PlayKey();
        }
        mustplay = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mustplay = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mustplay = false;
    }


    void PlayKey()
    {
        source.volume = 1;
        source.Play();
        if (isWhite == true)
        {
            transform.GetComponent<SpriteRenderer>().sprite = whiteDOWN;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().sprite = blackDOWN;
        }
    }

    void StopKey()
    {
        source.Stop();
        if (isWhite == true)
        {
            transform.GetComponent<SpriteRenderer>().sprite = whiteUP;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().sprite = blackUP;
        }
    }

    IEnumerator voDo()
    {
        float Vol=1;
        do
        {
            source.volume-=0.1f;
            Vol = source.volume;
            if (Vol <= 0.1) {
                source.Stop();
            }
            yield return null;
        } while (Vol > 0);
    }
}
