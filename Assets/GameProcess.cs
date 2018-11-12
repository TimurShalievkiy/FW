﻿using System.Collections;
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

    public void SetGameGread()   
    {
        
        creator.ResetFillWord();
       // FillTheCellsWithLetters(); 

        Debug.Log(FillWordCreator.countOfResets);
        FillWordCreator.countOfResets = 0;

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
        string str = "";
        for (int i = 0; i < cellNumbers.Count; i++)
        {
            str = DictionaryController.GetordByTheNumberOfLetters(cellNumbers[i].Count, usedWords);
            if (str == null)
            {
                SetGameGread();
                return;
            }
            usedWords.Add(str);

            int index = 0;
            for (int j = 0; j < cellNumbers[i].Count; j++)
            {
 
                CellGrid.transform.GetChild(cellNumbers[i][j]).transform.GetChild(0).GetComponent<Text>().text = str[index].ToString().ToUpper();
                index++;
            }
        }

        //=========================================
        //foreach (var x in cellNumbers)
        //{
   
        //    str =  DictionaryController.GetordByTheNumberOfLetters(x.Count, usedWords);
        //    if (str == null)
        //    {
        //        SetGameGread();
        //        return;
        //    }
        //    usedWords.Add(str);
             
        //    int index = 0;
        //    foreach (var y in x)
        //    {
        //        CellGrid.transform.GetChild(y).transform.GetChild(0).GetComponent<Text>().text = str[index].ToString().ToUpper();
        //        index++;
        //    }

        //}
    }
}
