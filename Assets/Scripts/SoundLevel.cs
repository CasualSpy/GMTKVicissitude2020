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
    Sprite[] spriteArray;

    public Sprite[] sprites = new Sprite[4];

    // Start is called before the first frame update
    void Start()
    {
        soundLevel = 0f;
        annoyance = 0f;

        spriteArray = new Sprite[4];

        spriteArray[0] = Resources.Load("window1") as Sprite;
        spriteArray[1] = Resources.Load("window2") as Sprite;
        spriteArray[2] = Resources.Load("window3") as Sprite;
        spriteArray[3] = Resources.Load("window4") as Sprite;


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
        //Debug.Log($"annoyance: {annoyance}");
        annoyance += soundLevel * Time.fixedDeltaTime * annoyanceSpeed;
        if (annoyance <= 2f)
        {
            windowRenderer.sprite = null;
        }
        else if (annoyance >= 10f)
        {
            cm.AddComplaint(new ComplaintManager.Complaint() { plaintiff = this, reason = ComplaintManager.Reasons.Noise });
            annoyance = 0f;
        }
        else if (annoyance >= 8f)
        {
            windowRenderer.sprite = sprites[3];
        }
        else if (annoyance >= 6f)
        {
            windowRenderer.sprite = sprites[2];
        }
        else if (annoyance >= 4f)
        {
            windowRenderer.sprite = sprites[1];
        }
        else if (annoyance >= 2f)
        {
            windowRenderer.sprite = sprites[0];
        }




    }

    public void Complain(ComplaintManager.Reasons reason)
    {
        throw new System.NotImplementedException();
    }
}
