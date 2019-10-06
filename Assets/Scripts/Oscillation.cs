using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    [HideInInspector]
    public bool gameOver = false;
    [HideInInspector]
    public float bpm;
    private IEnumerator osci;
    public AnimationCurve beatShape;
    [HideInInspector]
    public int beatCount;
    private float beatPeriod;

    // Start is called before the first frame update
    void Start()
    {
        beatPeriod = 60f / bpm;
        osci = OsciRoutine();
        //StartCoroutine(osci);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartOscillation(float bpm)
    {
        this.bpm = bpm;
        beatPeriod = 60f / bpm;
        StartCoroutine(osci);
    }

    private IEnumerator OsciRoutine()
    {
        float timer = 0;
        while (!gameOver)
        {
            transform.localPosition = Vector3.up * beatShape.Evaluate(timer/beatPeriod);
            timer += Time.deltaTime;
            if(timer > beatPeriod)
            {
                timer -= beatPeriod;
                beatCount++;
            }
            yield return null;
        }
    }
}
