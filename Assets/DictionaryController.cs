using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DictionaryController : MonoBehaviour
{

    // public GameObject Animals;

    public enum Topic { Random = 0, Animals = 1 }

    public struct PassedWord {
        public int id;
        public int callNumber;

        public PassedWord(int id, int callNumber)
        {
            this.id = id;
            this.callNumber = callNumber;
        }
    }
    static List<Word> words;
    static List<PassedWord> pasedWords;

    public static Topic currentTopic = Topic.Animals;


    // Use this for initialization
    void Start()
    {
        pasedWords = new List<PassedWord>();
        words = FillTheWordsOnTheCurrentTopic();
        LoadPasedDictionary();
        Debug.Log( " count = "+ pasedWords.Count);
    }

    public static int GetMin()
    {
        return words.Min(x => x.numberOfLetters); ;
    }

    public static int GetMax()
    {
        return words.Max(x => x.numberOfLetters); ;
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

    public static string GetordByTheNumberOfLetters(int num)
    {
        List<Word> buff = words.FindAll(x => x.numberOfLetters == num);
        LoadPasedDictionary();
        int min = 99999;
        if (pasedWords.Count > 0)
        {
                for (int j = 0; j < pasedWords.Count; j++)
                {
                    buff.RemoveAll(x => x.id == pasedWords[j].id);                   
                }
                
            
            int rand = 0;
            if (buff.Count > 0)
            {               
                rand = Random.Range(0, buff.Count); 
                pasedWords.Add(new PassedWord(buff[rand].id, ++buff[rand].callNumber));
            }
            else
            {
                buff = words.FindAll(x => x.numberOfLetters == num);

                for (int i = 0; i < buff.Count; i++)
                {
                    for (int j = 0; j < pasedWords.Count; j++)
                    {
                        if (buff[i].id == pasedWords[j].id)
                        {
                            buff[i].callNumber = pasedWords[j].callNumber; 
                            if (min > pasedWords[j].callNumber)
                                min = pasedWords[j].callNumber;
                        }
                    }                       
                }
                buff = buff.FindAll(x => x.callNumber == min);                 
                rand = Random.Range(0, buff.Count); 
                for (int i = 0; i < pasedWords.Count; i++)
                {
                    if (pasedWords[i].id == buff[rand].id)
                    {
                        int y = pasedWords[i].callNumber;
                        pasedWords[i] = new PassedWord(pasedWords[i].id, ++y);
                        break;
                    }
                }
            }
            //ShowInDebugPassedCell();
            SavePasedDictionary();
            return buff[rand].word;
        }
        else {
            //Debug.Log("else");
            int x = Random.Range(0, buff.Count);
            pasedWords.Add(new PassedWord(buff[x].id, 1));
            SavePasedDictionary();
            return buff[x].word;
        }

    }
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }


    static List<Word> GetListWithMinimumOfCalls()
    {
        List<Word> resultList = new List<Word>();

        return resultList;
    }

    static void SavePasedDictionary()
    {
        string str = "";
        if (pasedWords.Count > 0)
        {
          //  Debug.Log("Save");
            foreach (var item in pasedWords)
            {
                str += item.id + " " + item.callNumber + " ";
            }
            PlayerPrefs.SetString(currentTopic.ToString(), str);
            PlayerPrefs.Save();
        }
        else
            pasedWords = new List<PassedWord>();
    }

   static void LoadPasedDictionary()
    {
        //Debug.Log(currentTopic.ToString());
        if(PlayerPrefs.HasKey(currentTopic.ToString()))
        {
            string str = PlayerPrefs.GetString(currentTopic.ToString());
            //Debug.Log("load str = " + str);
            string[] massSplit = str.Split(' ');
            pasedWords = new List<PassedWord>();
            for (int i = 0; i < massSplit.Length-1; i+=2)
            {                
                pasedWords.Add(new PassedWord(System.Convert.ToInt32(massSplit[i]), System.Convert.ToInt32(massSplit[i + 1])));
               // Debug.Log("i = " + massSplit[i] + " i+1 =" + massSplit[i+1]);
            }
        
        }
    }

    static void ShowInDebugPassedCell()
    {
        string str = "";
        foreach (var item in pasedWords)
        {
            str += item.id + " " + item.callNumber + "\n";
        }
        Debug.Log(str);
    }



}
