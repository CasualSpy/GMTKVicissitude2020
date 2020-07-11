using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // Start is called before the first frame update
    public float Rate = 0.5f;
    private float timer;
    void Start()
    {
        timer = Rate;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            GetComponent<AstarPath>().Scan();
            timer = Rate;
        }
    }
}
