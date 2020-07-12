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
        return 1 - (1 - gm.rebelliousness * Random.value);
    }

    public static float CalcRebellion()
    {
        return Mathf.Log(Random.Range(1, (float)Math.E));
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

    static List<string> ColourValues = new List<string> {
        "#ffa89a", "#c15734", "#ff5f01", "#ffa66b", "#867466", "#dec2a1", "#957206", "#f5c000", "#9db700", "#a4d483", "#00ec2a", "#008f5b", "#00e4bb", "#008587", "#00d9ff", "#96c8ff", "#3a78c3", "#6d80ff", "#918ea2", "#e7afff", "#d554ff", "#ff67c8", "#ffaac8", "#eb1241"
    };

    public static Color GenerateColor()
    {
        if (ColourValues.Count > 0)
        {
            string colorString = ColourValues[Random.Range(0, ColourValues.Count)];
            Color color;
            if (ColorUtility.TryParseHtmlString(colorString, out color))
            {
                ColourValues.Remove(colorString);
                return color;
            }
        }
        return Color.white;
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
        bool isObstacle;
        Vector2 destination;
        do
        {
            isObstacle = false;
            x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
            y = UnityEngine.Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
            destination = new Vector2(x, y);
            Collider2D[] overlaps = Physics2D.OverlapPointAll(destination);
            foreach (Collider2D overlap in overlaps)
            {
                if (overlap.gameObject.layer == 8)
                {
                    isObstacle = true;
                    break;
                }
            }
            attempt++;
        } while (isObstacle && !collider.OverlapPoint(new Vector2(x, y)) || attempt >= 100);

        return new Vector3(x, y, 0);
    }
}
