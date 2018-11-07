using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryController : MonoBehaviour {

    public  enum Сhapter {Random = 0, Animals = 1 }

    static string[] words = { "паук","заяц","крыса","ехидна","Савинья","лисица","олень",
        "барсук","буйвол","бурундук","волк","гепард","лев","слон","вол",
        "собака","кошка","мышь","жираф "};

    static Сhapter currentChapter = Сhapter.Random;
    // Use this for initialization
    void Start () {
		
	}

    public static int GetMin()
    {
        int min = 100;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length < min)
                min = words[i].Length;
        }
        //Debug.Log("Min = " + min);
        return min;
    }

    public static int GetMax()
    {
        int max = -1;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > max)
                max = words[i].Length;
        }
        //Debug.Log("Max = " + max);
        return max;
    }

    public static int GetMaxWithRestriction(int res)
    {
        int max = -1;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > max && words[i].Length <= res)
                max = words[i].Length;
        }
        return max;
    }



}
