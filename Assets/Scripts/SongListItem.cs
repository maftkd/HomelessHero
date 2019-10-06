using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongListItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadSong()
    {
        GameObject.Find("Audio Source").GetComponent<AudioLoader>().LoadTrack(transform.GetChild(0).GetComponent<Text>().text);
        CanvasGroup menuGroup = transform.parent.parent.parent.parent.GetComponent<CanvasGroup>();
        menuGroup.alpha = 0;
        menuGroup.interactable = false;
    }
}
