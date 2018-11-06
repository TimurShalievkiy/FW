using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryController : MonoBehaviour {

    public enum Сhapter { }

    static string[] words = { "паук","заяц","крыса","ехидна","Савинья","лисица","олень",
        "барсук","буйвол","бурундук","волк","гепард","лев","слон","вол","як",
        "собака","кошка","мышь","жираф "};
	// Use this for initialization
	void Start () {
		
	}

    public static int GetMin(Сhapter t)
    {
        int min = 100;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length < min)
                min = words[i].Length;
        }
        return min;
    }

    public static int GetMax(Сhapter t)
    {
        int max = -1;
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > max)
                max = words[i].Length;
        }
        return max;
    }



}
