using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLevel : MonoBehaviour, Plaintiff
{
    private SpriteRenderer windowRenderer;
    public float soundLevel;
    public float annoyance;
    public float annoyanceSpeed;
    ComplaintManager cm;

    // Start is called before the first frame update
    void Start()
    {
        soundLevel = 0f;
        annoyance = 0f;
        windowRenderer = GameObject.Find("Window").GetComponent<SpriteRenderer>();
        cm = GameObject.Find("Master").GetComponent<ComplaintManager>();
    }

    public void Increase()
    {
        soundLevel += soundLevel < 5f ? 1f : 0f;
    }

    public void Lower()
    {
        soundLevel = 0f;
    }

    private void FixedUpdate()
    {
        annoyance += soundLevel * Time.fixedDeltaTime * annoyanceSpeed;
        if (annoyance == 0f)
        {
            windowRenderer.sprite = null;
        }
        else if (annoyance >= 2f)
        {

        }
        else if (annoyance >= 4f)
        {
        }
        else if (annoyance >= 6f)
        {
        }
        else if (annoyance >= 8f)
        {

        }
        else if (annoyance >= 10f)
        {
            cm.AddComplaint(new ComplaintManager.Complaint() { plaintiff = this, reason = ComplaintManager.Reasons.Noise }) ;
            annoyance = 0f;
        }
    }

    public void Complain(ComplaintManager.Reasons reason)
    {
        throw new System.NotImplementedException();
    }
}
