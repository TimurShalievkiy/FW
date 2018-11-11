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
        public int lettersNumber;

        public PassedWord(int id, int callNumber, int lettersNumber)
        {
            this.id = id;
            this.callNumber = callNumber;
            this.lettersNumber = lettersNumber;
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
        
        //string str = "";
        //foreach (var item in w)
        //{
        //    str += "len = " + item.numberOfLetters + " call = " + item.callNumber + "\n";
        //}
        //Debug.Log(str);
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

    public static string GetordByTheNumberOfLetters(int num, List<string> usedWords)
    {
        List<Word> buff = new List<Word>();
        int rand = 0;
        int counter = 0;
        do
        {
            buff = words.FindAll(x => x.numberOfLetters == num);
            LoadPasedDictionary();
            int min = 99999;
            rand = 0;

            if (pasedWords.Count > 0)
        {
                for (int j = 0; j < pasedWords.Count; j++)
                {
                    buff.RemoveAll(x => x.id == pasedWords[j].id);                   
                }
                
            
            
            if (buff.Count > 0)
            {               
                rand = Random.Range(0, buff.Count); 
                pasedWords.Add(new PassedWord(buff[rand].id, ++buff[rand].callNumber, buff[rand].numberOfLetters));
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
                        pasedWords[i] = new PassedWord(pasedWords[i].id, ++y, pasedWords[i].lettersNumber);
                        break;
                    }
                }
            }
            
            //SavePasedDictionary();
            //return buff[rand].word;
        }
        else {
            rand = Random.Range(0, buff.Count);
            pasedWords.Add(new PassedWord(buff[rand].id, 1,buff[rand].numberOfLetters));
            //SavePasedDictionary();
            //return buff[rand].word; 
        }
            counter++;
            if (counter > 10)
            {
                //Debug.Log("break"); 
                break;
            }
         
        } while (!usedWords.Exists(x=>x == buff[rand].word));
        if (usedWords.Exists(x => x == buff[rand].word))
        {
            return null;
        }
        SavePasedDictionary();
        return buff[rand].word;

    }
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }


    //static List<Word> GetListWithMinimumOfCalls()
    //{
    //    List<Word> resultList = new List<Word>();

    //    return resultList;
    //}

    static void SavePasedDictionary()
    {
        string str = "";
        if (pasedWords.Count > 0)
        {
            //  Debug.Log("Save");
            foreach (var item in pasedWords)
            {
                str += item.id + " " + item.callNumber + " " + item.lettersNumber + " ";
            }
            PlayerPrefs.SetString(currentTopic.ToString(), str);
            PlayerPrefs.Save();
        }
        else
        {
            foreach (var item in words)
            {
                str += item.id + " " + item.callNumber + " " + item.numberOfLetters + " ";
            }
            PlayerPrefs.SetString(currentTopic.ToString(), str);
            PlayerPrefs.Save();
            //pasedWords = new List<PassedWord>();

        }
    }

   static void LoadPasedDictionary()
    {
        //Debug.Log(currentTopic.ToString());
        if (PlayerPrefs.HasKey(currentTopic.ToString()))
        {
            string str = PlayerPrefs.GetString(currentTopic.ToString());
            //Debug.Log("load str = " + str);
            string[] massSplit = str.Split(' ');
            pasedWords = new List<PassedWord>();
            for (int i = 0; i < massSplit.Length - 2; i += 3)
            {
                pasedWords.Add(new PassedWord(System.Convert.ToInt32(massSplit[i]), System.Convert.ToInt32(massSplit[i + 1]), System.Convert.ToInt32(massSplit[i + 2])));
                // Debug.Log("i = " + massSplit[i] + " i+1 =" + massSplit[i+1]);
            }

        }
        else {
            SavePasedDictionary();
            LoadPasedDictionary();
        }

    }

   public  static void ShowInDebugPassedCell()
    {
        string str = "";
        foreach (var item in pasedWords)
        {
            str += item.id + " " + item.callNumber + "\n";
        }
        Debug.Log(str);
    }
    static void SetWordCallNumber()
    {
        words = words.OrderBy(x => x.id).ToList();
        pasedWords = pasedWords.OrderBy(x => x.id).ToList();

        for (int i = 0; i < words.Count; i++)
        {
            words[i].callNumber = pasedWords[i].callNumber;
        }

    }

    public void ShowCountNumAndCall()
    {
        
        List<int> lint = new List<int>();
        int max = pasedWords.Max(x => x.callNumber);

      
        for (int i = 0; i <= max; i++)
        {
            lint.Add(pasedWords.Count(x => x.callNumber == i));
        }

        string str = "";
        for (int i = 0; i <= max; i++)
        {
            str += i +  " - " + lint[i] + "\n";
        }
        Debug.Log(str);
    }

    public static List<FillWordCreator.MinList> GetMinList()
    {
        List<FillWordCreator.MinList> minList = new List<FillWordCreator.MinList>();

        List<List<PassedWord>> lint = new List<List<PassedWord>>();
        int min = pasedWords.Min(x => x.lettersNumber);
        int max = pasedWords.Max(x => x.lettersNumber);

        for (int i = min; i <= max; i++)
        {
            lint.Add(pasedWords.FindAll(x => x.lettersNumber == i));
        }

        int minCall = pasedWords.Min(x => x.callNumber);
        int maxCall = pasedWords.Max(x => x.callNumber);
        //string str = "";
        for (int i = 0; i < lint.Count; i++)
        {
            //str += "CountLetter = " + lint[i].Count + "\n";
            //for (int j = 0; j < lint[i].Count; j++)
            //{
               
                for (int k = minCall; k <= maxCall; k++)
                {
                    int count = lint[i].FindAll(x => x.callNumber == k).Count();
                    if (count == 0)
                        break;
                minList.Add(new FillWordCreator.MinList(lint[i][0].lettersNumber, k, count));
                  // Debug.Log("lengt = " + lint[i][0].lettersNumber + " where call =  " + k +" = " + count);
                }
            //} 
        }


       
        //Debug.Log(str);
        return minList;
    }



}
