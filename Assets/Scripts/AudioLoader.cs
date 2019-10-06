using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AudioLoader : MonoBehaviour
{

    private AudioSource sAudio;

    public Transform songListItem;
    public Transform songListContainer;

    public GameObject debugCanvas;
    // Start is called before the first frame update
    void Start()
    {
        sAudio = transform.GetComponent<AudioSource>();
        PopulateMenu();
    }

    private void PopulateMenu()
    {
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/SongData.txt");
        foreach (string line in lines)
        {
            string[] data = line.Split('%');
            Transform listItem = Instantiate(songListItem, songListContainer);
            listItem.GetChild(0).GetComponent<Text>().text = data[0];
            listItem.GetChild(1).GetComponent<Text>().text = data[1];
        }
    }

    public void LoadTrack(string trackName)
    {
        string [] lines = File.ReadAllLines(Application.streamingAssetsPath + "/SongData.txt");
        foreach(string line in lines)
        {
            string[] data = line.Split('%');
            if (data[0] == trackName)
            {
                StartCoroutine(LoadAudio(trackName, data[1]));
            }
        }
    }

    private IEnumerator LoadAudio(string track, string bpm)
    {
        Debug.Log("loading track: " + track + ", bpm=" + bpm);
        string path = Application.streamingAssetsPath + "/Tracks/" + track + ".wav";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                sAudio.clip = clip;
                StartCoroutine(StartSong(clip.length, float.Parse(bpm)));
            }
        }
    }

    private IEnumerator StartSong(float duration, float bpm)
    {
        sAudio.Play();
        debugCanvas.SetActive(true);
        GameObject.Find("Cube").GetComponent<Oscillation>().StartOscillation(bpm);
        yield return new WaitForSeconds(duration);
        Debug.Log("Song over!");
    }
}
