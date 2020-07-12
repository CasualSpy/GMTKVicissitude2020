using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuestBubble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupBubble(string text, Vector3 position)
    {
        GetComponentInChildren<TextMeshPro>().text = text;
        transform.position = position;
    }

    public void SetupBubble(string text)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    private void OnDestroy()
    {
        GameObject.Find("Master").GetComponent<ComplaintManager>().isShowing = false;
    }

}
