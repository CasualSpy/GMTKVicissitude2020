using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectMain : MonoBehaviour
{
    public bool isHighlighted;

    Material normalSpriteMaterial;
    Material highlightMaterial;
    List<SpriteRenderer> srs = new List<SpriteRenderer>();
    List<Vector3> scales;

    // Start is called before the first frame update
    void Start()
    {
        normalSpriteMaterial = Resources.Load("OurSpriteMaterial") as Material;
        highlightMaterial = Resources.Load("HighlightMaterial") as Material;
        srs = GetComponentsInChildren<SpriteRenderer>().ToList();

        SpriteRenderer s = GetComponent<SpriteRenderer>();
        if (s != null)
            srs.Add(s);
        scales = srs.ToArray().Select(x => x.transform.localScale).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHighlighted)
        {

            for (int i = 0; i < srs.Count; i++)
            {
                Vector3 scale = scales[i];
                srs[i].transform.localScale = new Vector3(scale.x * 1.2f, scale.y * 1.2f, scale.z * 1.2f);
                srs[i].material = highlightMaterial;
            }
            isHighlighted = false;
        }
        else
        {
            for (int i = 0; i < srs.Count; i++)
            {
                Vector3 scale = scales[i];
                srs[i].transform.localScale = scale;
                srs[i].material = normalSpriteMaterial;
            }
        }

    }
}
