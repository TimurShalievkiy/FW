using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour
{
    public FillWordCreator creator;
    public GameObject CellGrid;
    public static List<List<int>> cellNumbers;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SetGameGread", 1.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetGameGread()
    {
        creator.ResetFillWord();
        FillTheCellsWithLetters();
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
        foreach (var x in cellNumbers)
        {
            string str = DictionaryController.GetordByTheNumberOfLetters(x.Count);
            int index = 0;
            foreach (var y in x)
            {
                CellGrid.transform.GetChild(y).transform.GetChild(0).GetComponent<Text>().text = str[index].ToString().ToUpper();
                index++;
            }

        }
    }
}
