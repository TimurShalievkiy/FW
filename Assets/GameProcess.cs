using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour
{
    public FillWordCreator creator;
    public GameObject CellGrid;
    public static List<List<int>> cellNumbers;
    List<string> usedWords;
    int counter = 0;
    // Use this for initialization
    void Start()
    {
        usedWords = new List<string>();
        //PlayerPrefs.DeleteAll();
       // InvokeRepeating("SetGameGread", 1.0f, 0.7f);
       
    }
     
    // Update is called once per frame
    void Update()
    {

    }
    public void SetGameGread() 
    {
        creator.ResetFillWord();
        FillTheCellsWithLetters();

        counter++;
        if (counter == 10) 
        {
            string str = PlayerPrefs.GetString("Animals");
            string[] str2 = str.Split(' ');
            string str3 = "";
            for (int i = 0; i < str2.Length - 1; i += 2)
            {
                str3 += str2[i] + " " + str2[i + 1] + "\n";
            }
            Debug.Log(str3);
            counter = 0;
        }
    }
    public static void ShowCellNumbers()
    {
        string str = "";
        foreach (var x in cellNumbers)
        {
            foreach (var y in x)
            {
                str += y.ToString() + " ";
            }
            str += "\n";
        }
        Debug.Log(str);
    }

    void FillTheCellsWithLetters()
    {
        usedWords = new List<string>();
        usedWords.Clear();
        foreach (var x in cellNumbers)
        {
   
            string str =  DictionaryController.GetordByTheNumberOfLetters(x.Count, usedWords);
            if (str == null)
            {
                SetGameGread();
                return;
            }
            usedWords.Add(str);
             
            int index = 0;
            foreach (var y in x)
            {
                CellGrid.transform.GetChild(y).transform.GetChild(0).GetComponent<Text>().text = str[index].ToString().ToUpper();
                index++;
            }

        }
    }
}
