using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    [HideInInspector]
    public bool gameOver = false;
    public float bpm;
    private IEnumerator osci;
    public AnimationCurve beatShape;
    [HideInInspector]
    public int beatCount;

    // Start is called before the first frame update
    void Start()
    {
        osci = OsciRoutine();
        StartCoroutine(osci);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OsciRoutine()
    {
        float timer = 0;
        while (!gameOver)
        {
            transform.localPosition = Vector3.up * beatShape.Evaluate(timer);
            timer += Time.deltaTime;
            if(timer > 1)
            {
                timer -= 1;
                beatCount++;
            }
            yield return null;
        }
    }
}
