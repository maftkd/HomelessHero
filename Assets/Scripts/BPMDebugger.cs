using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPMDebugger : MonoBehaviour
{

    public Oscillation oscilator;
    private Text debugText;
    private float tTime;

    private void OnEnable()
    {
        tTime = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        debugText = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tTime += Time.deltaTime;
        float estBPM = (oscilator.beatCount/ tTime)*60f;
        debugText.text = estBPM.ToString();
    }
}
