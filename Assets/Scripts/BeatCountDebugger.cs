using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatCountDebugger : MonoBehaviour
{

    public Oscillation oscilator;
    private Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        debugText = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = oscilator.beatCount.ToString() ;
    }
}
