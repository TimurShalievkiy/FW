using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    public Text t;
    public GameObject CellGrid;
    public GameProcess gameProcess;
    public GameObject infoPanel;
    public static List<int> colorsId;
    public static int currentWordColorId; 

    static int colorNum = 1;

    // Use this for initialization
    public static List<GameObject> cells;
    void Start()
    {
        cells = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            PointerExit();
        }
    }

    public void PointerExit()
    {
        string res = "";


        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].GetComponent<Image>().color = Color.white;
            res += cells[i].transform.GetChild(0).GetComponent<Text>().text;
        }

        CheckAnsver(res);

    }
    public void PointerUp()
    {
        PointerExit();

    }
    //проверка введенного слова
    public void CheckAnsver(string str)
    {
        t.text = str;
        bool flag = false;

        List<int> cellsList = new List<int>();

        for (int i = 0; i < cells.Count; i++)
        {
            cellsList.Add(cells[i].transform.GetSiblingIndex());
        }

        if (GameProcess.cellNumbers != null)
        {
         


            for (int i = 0; i < GameProcess.cellNumbers.Count; i++)
            {
                if (cellsList.Count == GameProcess.cellNumbers[i].Count && cellsList[0] == GameProcess.cellNumbers[i][0])
                {
                    for (int j = 0; j < cellsList.Count; j++)
                    {
                        if (cellsList[j] == GameProcess.cellNumbers[i][j])
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }

                }
            }
        }
        //если слово отгадано
        if (flag)
        {
            t.color = Color.green;
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].transform.GetComponent<Cell>().used = true;
                cells[i].GetComponent<Image>().color = SetColor(currentWordColorId);
            }
            colorNum++;
            if (СheckForСompletion())
            {
                DictionaryController.SavePasedDictionary();
                ResetCellsValue();
                this.gameObject.transform.GetComponent<GameProcess>().SetGameGread();
            }
            currentWordColorId++;
        }
        //обработка правильного слова при неправильных ячейках
        else if (gameProcess.usedWords.Find(x => x.ToLower() == t.text.ToLower()) !=null)
        {
            Debug.Log("The same");
            infoPanel.gameObject.SetActive(true);
            Transform g = infoPanel.transform.GetChild(0);
            g.GetComponent<Text>().text = "Попробуйте сложить слово \"" + t.text.ToUpper() + "\" по другому";
            cells.Clear();

        }
        //неправльное слово (возможно вставить поиск не заданных слов)
        else
            t.color = Color.red;



        cells.Clear();

    }
    bool СheckForСompletion()
    {
        bool complete = true;

        for (int i = 0; i < CellGrid.transform.childCount; i++)
        {
            if (!CellGrid.transform.GetChild(i).transform.GetComponent<Cell>().used)
            {
                return false;               
            }
        }
        return complete;
    }


   public static Color SetColor()
    {
        switch (colorNum)
        {
            case 0:
                return Color.white;
            case 1:
                return Color.blue;
            case 2:
                return Color.green;
            case 3:
                return Color.cyan;
            case 4:
                return Color.grey;
            case 5:
                return Color.yellow;
            case 6:
                return Color.red;
            case 7:
                return Color.magenta;
            case 8:
                return new Color(0.3f, 0.8f, 0.2f);
            case 9:
                return new Color(0.5f, 0.9f, 0.5f);
            case 10:
                return new Color(0.2f, 0.4f, 0.8f);
        }
                return Color.white;
    }
    public static Color SetColor(int num)
    {
        
        switch (colorsId[num])
        {
            case 0:
                return Color.white;
            case 1:
                return Color.blue;
            case 2:
                return Color.green;
            case 3:
                return Color.cyan;
            case 4:
                return Color.grey;
            case 5:
                return Color.yellow;
            case 6:
                return Color.red;
            case 7:
                return Color.magenta;
            case 8:
                return new Color(0.3f, 0.8f, 0.2f);
            case 9:
                return new Color(0.5f, 0.9f, 0.5f);
            case 10:
                return new Color(0.2f, 0.4f, 0.8f);
        }
        return Color.white;
    }

    public void ResetCellsValue()
    {
        for (int i = 0; i < CellGrid.transform.childCount; i++)
        {
            CellGrid.transform.GetChild(i).transform.GetComponent<Cell>().used = false;
            CellGrid.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }
    public void SetColorForUsedWords()
    {
        currentWordColorId = 0;
        colorsId = new List<int>();
        List<int> listIndex = new List<int>();
        for (int i = 0; i < gameProcess.usedWords.Count; i++)
        {
            listIndex.Add(i);
        }

        int index;
        int count = listIndex.Count;
        while (count > 0)
        {          
            index = Random.Range(0, listIndex.Count);
            colorsId.Add(listIndex[index]+1);
            listIndex.Remove(listIndex[index]);
            count--;
        }

    }
}
