using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectMain : MonoBehaviour
{
    public bool isHighlighted;
    SpriteRenderer[] srs;
    List<Vector3> scales;

    // Start is called before the first frame update
    void Start()
    {
        srs = GetComponentsInChildren<SpriteRenderer>();
        scales = srs.ToArray().Select(x => x.transform.localScale).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighted)
        {

            for (int i = 0; i < srs.Length; i++)
            {
                Vector3 scale = scales[i];
                srs[i].transform.localScale = new Vector3(scale.x * 1.2f, scale.y * 1.2f, scale.z * 1.2f);
            }
            isHighlighted = false;
        }
        else
        {
            for (int i = 0; i < srs.Length; i++)
            {
                Vector3 scale = scales[i];
                srs[i].transform.localScale = scale;
            }
        }

    }
}
