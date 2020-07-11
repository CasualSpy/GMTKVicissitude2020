using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using System.Linq;
using Random = UnityEngine.Random;

public class Helpers
{
    #region Guests 

    public static float RandomRebelliousChoice(GuestMain gm)
    {
        return 1 - Mathf.Sqrt(1 - (Random.value + gm.rebelliousness) / 2);
    }

    public static float CalcRebellion()
    {
        return 1 - Mathf.Sqrt(1 - UnityEngine.Random.value);
    }

    public enum SkinColor
    {
        Pale,
        Asian,
        Tan,
        Dark,
        Black
    }

    public enum HairColor
    {
        Black,
        DarkBrown,
        LightBrown,
        Blond,
        Red
    }

    public static Color ShirtColor()
    {
        List<Color> colors = new List<Color>
        {
            Color.green, Color.red, Color.yellow, Color.cyan, Color.blue, Color.magenta
        };

        return colors[UnityEngine.Random.Range(0, colors.Count)];
    }

    public static SkinColor GetSkinColor()
    {
        var values = Enum.GetValues(typeof(SkinColor));
        return (SkinColor)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    public static HairColor GetHairColor(SkinColor skinColor)
    {
        switch (skinColor)
        {
            case SkinColor.Black:
            case SkinColor.Asian:
            case SkinColor.Dark:
                List<HairColor> colors = new List<HairColor> { HairColor.Black, HairColor.DarkBrown, HairColor.LightBrown };
                return colors[UnityEngine.Random.Range(0, colors.Count)];
            case SkinColor.Tan:
                List<HairColor> colorsTan = new List<HairColor> { HairColor.Black, HairColor.DarkBrown, HairColor.LightBrown, HairColor.Blond };
                return colorsTan[UnityEngine.Random.Range(0, colorsTan.Count)];
            case SkinColor.Pale:
                var colorsPale = Enum.GetValues(typeof(HairColor));
                return (HairColor)colorsPale.GetValue(UnityEngine.Random.Range(0, colorsPale.Length));
            default:
                return HairColor.DarkBrown;
        }
    }
    #endregion

    public static Vector2 SpotInsideHouse()
    {
        Vector2 destination;
        bool isObstacle;
        do
        {
            isObstacle = false;
            destination = new Vector2(Random.Range(-5, 2), Random.Range(-3, 2));
            Collider2D[] overlaps = Physics2D.OverlapPointAll(destination);
            foreach (Collider2D overlap in overlaps)
            {
                if (overlap.gameObject.layer == 8)
                {
                    isObstacle = true;
                    break;
                }
            }
        } while (isObstacle);
        return destination;
    }

    public static Vector2 SpotToHangOut()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Zone");

        GameObject chosenZone = objs[Random.Range(0, objs.Length)];
        Collider2D collider = chosenZone.GetComponent<Collider2D>();
        var bounds = collider.bounds;
        var center = bounds.center;

        float x;
        float y;
        int attempt = 0;
        do
        {
            x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
            y = UnityEngine.Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
            attempt++;
        } while (!collider.OverlapPoint(new Vector2(x, y)) || attempt >= 100);
        Debug.Log("Attemps: " + attempt);

        return new Vector3(x, y, 0);
    }
}
