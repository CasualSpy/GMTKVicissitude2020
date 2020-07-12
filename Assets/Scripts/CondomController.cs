using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondomController : MonoBehaviour
{
    private bool hasCondom;

    SpriteRenderer condomRenderer;
    public bool HasCondom
    {
        get { return hasCondom; }
        set 
        {
            hasCondom = value;
            condomRenderer.enabled = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        condomRenderer = GameObject.Find("Condom").GetComponent<SpriteRenderer>();
        HasCondom = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CabanonManager cm = collision.gameObject.GetComponent<CabanonManager>();
        if (collision.transform.name == "PorteCabanon" && hasCondom && !cm.protectionApplied)
        {
            GiveCondom(cm);
        }
    }

    void GiveCondom (CabanonManager cm)
    {
        HasCondom = false;
        cm.protectionApplied = true;
    }
}
