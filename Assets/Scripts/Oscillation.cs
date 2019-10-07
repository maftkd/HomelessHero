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
    public AnimationCurve beatShape, noteShape;
    [HideInInspector]
    public int beatCount;
    private float beatPeriod;

    public float thresholdPercentage;
    public float perfectPercentage;
    private float threshold;
    private float perfectThreshold;

    private float timer;

    private MeshRenderer rendr;

    public Transform note, noteFollow;

    private bool noteKnocked = false;

    // Start is called before the first frame update
    void Start()
    {
        beatPeriod = 60f / bpm;
        osci = OsciRoutine();
        rendr = transform.GetComponent<MeshRenderer>();
        //StartCoroutine(osci);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if(timer<threshold || beatPeriod-timer < threshold)
            {
                //success
                if(timer<perfectThreshold || beatPeriod-timer < perfectThreshold)
                {
                    StartCoroutine(VisualFeedback(Color.green));
                }
                else
                {
                    StartCoroutine(VisualFeedback(Color.yellow));
                }

                //push down note follow
                StartCoroutine(KnockNote());
            }
            else
            {
                //fail
                StartCoroutine(VisualFeedback(Color.red));
            }
        }
    }

    public void StartOscillation(float bpm)
    {
        this.bpm = bpm;
        beatPeriod = 60f / bpm;
        threshold = beatPeriod * thresholdPercentage;
        perfectThreshold = beatPeriod * perfectPercentage;
        StartCoroutine(osci);
    }

    private IEnumerator OsciRoutine()
    {
        timer = 0;
        while (!gameOver)
        {
            float curTime = timer / beatPeriod;
            transform.localPosition = Vector3.up * beatShape.Evaluate(curTime);
            note.localPosition = Vector3.right* 12 * noteShape.Evaluate(curTime);
            if (!noteKnocked)
                noteFollow.localPosition = Vector3.right * 12 * (noteShape.Evaluate(curTime) - 1f);
            else
                noteFollow.localPosition = Vector3.up * 4f * (noteShape.Evaluate(curTime) - 1f);
            timer += Time.deltaTime;
            if(timer > beatPeriod)
            {
                timer -= beatPeriod;
                beatCount++;
            }
            yield return null;
        }
    }

    private IEnumerator VisualFeedback(Color c)
    {
        Material mat = rendr.material;
        mat.color = c;
        rendr.material = mat;

        yield return new WaitForSeconds(threshold);

        mat.color = Color.white;

    }

    private IEnumerator KnockNote()
    {
        noteKnocked = true;
        float knockTimer = 0;
        while(knockTimer < beatPeriod)
        {
            noteKnocked = true;
            knockTimer += Time.deltaTime;
            yield return null;
        }
        noteKnocked = false;
    }
}
