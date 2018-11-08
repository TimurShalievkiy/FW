using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public  class DictionaryController : MonoBehaviour {

   // public GameObject Animals;

    public  enum Topic { Random = 0, Animals = 1 }

   static List<Word> words;
    static int max = 0;
   static int min = 0;
    static Topic currentTopic = Topic.Animals;


    // Use this for initialization
    void Start () {
        words = FillTheWordsOnTheCurrentTopic();
        //max = 
        //min = 
    }

    public static int GetMin()
    {
        return words.Min(x => x.numberOfLetters); ;
    }

    public static int GetMax()
    {
        return words.Max(x => x.numberOfLetters); ;
    }

    public static int GetMaxWithRestriction(int res)
    {
        //int max = -1;
        //for (int i = 0; i < words.Length; i++)
        //{
        //    if (words[i].Length > max && words[i].Length <= res)
        //        max = words[i].Length;
        //}
        //return max;
        return 0;
    }

     static List<Word> FillTheWordsOnTheCurrentTopic()
    {
        switch (currentTopic)
        {
            case Topic.Random:
                break;
            case Topic.Animals:
                Animals a = new Animals();
                return a.words;
        }
        return null;
    }







}
