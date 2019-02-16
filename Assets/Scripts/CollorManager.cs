using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollorManager : MonoBehaviour
{
    public static List<Color> colors;
    static List<Color> usedColors;
    public static Color tipColor;
    public static Color currentColor;

    static CollorManager()
    {
        Color c;
        //------------цвет подсказки-----------------------
        ColorUtility.TryParseHtmlString("#FFF44F", out c);
        tipColor = c;


        //------------Добавление цветов в список----------
        colors = new List<Color>();
        ColorUtility.TryParseHtmlString("#FFFFFF", out c);
        colors.Add(c);
        ColorUtility.TryParseHtmlString("#6A5ACD", out c);
        colors.Add(c);
        ColorUtility.TryParseHtmlString("#660099", out c);
        colors.Add(c);
        ColorUtility.TryParseHtmlString("#6699CC", out c);
        colors.Add(c);
        ColorUtility.TryParseHtmlString("#C9A0DC", out c);
        colors.Add(c);


        //------------Перемешивание списка ---------------
        for (int i = 1; i < colors.Count; i++)
        {
            Color tmp = colors[i];
            colors.RemoveAt(i);
            colors.Insert(Random.Range(1, colors.Count), tmp);
        }
    }

    public static void MixListColor()
    {
        for (int i = 1; i < colors.Count; i++)
        {
            Color tmp = colors[i];
            colors.RemoveAt(i);
            colors.Insert(Random.Range(1, colors.Count), tmp);
        }
    }


}
