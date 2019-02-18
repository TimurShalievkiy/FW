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
    public TipController tipController;
 
    public static int currentWordColorId; 

    static int colorNum = 0;

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
            if(cells[i].GetComponent<Cell>().isTipCell)
                cells[i].GetComponent<Image>().color = CollorManager.tipColor;
            else
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
                cells[i].GetComponent<Image>().color = SetColor();

                if (tipController.CheckWordIsCurret(cellsList))
                    tipController.ResetTip();
            }
            colorNum++;

            if (СheckForСompletion())
            {
                tipController.ResetTip();

                DictionaryController.SavePasedDictionary();
                ThemsController.SetValuesOfProgresInThemes();
                ResetCellsValue();
                this.gameObject.transform.GetComponent<GameProcess>().SetGameGread();
            }
            currentWordColorId++;
        }
        else if (tipController.wordIsDone && tipController.CheckWordIsCurret(cellsList))
        {

            for (int i = 0; i < tipController.word.Count; i++)
            {
                CellGrid.transform.GetChild(tipController.word[i]).transform.GetComponent<Cell>().used = true;
                //cells[i].transform.GetComponent<Cell>().used = true;
                CellGrid.transform.GetChild(tipController.word[i]).transform.GetComponent<Image>().color = SetColor();
                //cells[i].GetComponent<Image>().color = SetColor();
            }
            if (tipController.CheckWordIsCurret(cellsList))
                tipController.ResetTip();
            colorNum++;

            if (СheckForСompletion())
            {
                tipController.ResetTip();

                DictionaryController.SavePasedDictionary();
                ThemsController.SetValuesOfProgresInThemes();
                ResetCellsValue();
                this.gameObject.transform.GetComponent<GameProcess>().SetGameGread();
            }
            currentWordColorId++;


        }
        //обработка правильного слова при неправильных ячейках
        else if (gameProcess.usedWords.Find(x => x.ToLower() == t.text.ToLower()) != null)
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

        Color c = CollorManager.colors[colorNum];
        if (c != null)
            return c;

                return Color.white;
    }
  
    public void ResetCellsValue()
    {
        for (int i = 0; i < CellGrid.transform.childCount; i++)
        {
            CellGrid.transform.GetChild(i).transform.GetComponent<Cell>().used = false;
            CellGrid.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        colorNum = 0;
    }

    public List<int> GetNextWordForTip()       
    {
        List<int> listWordIndex = new List<int>();
        for (int i = 0; i < CellGrid.transform.childCount; i++)
        {

            if (!CellGrid.transform.GetChild(i).transform.GetComponent<Cell>().used)
            {
                
                for (int j = 0; j < GameProcess.cellNumbers.Count; j++)
                    if (GameProcess.cellNumbers[j].Exists(x => x == i))
                    {
                        //Debug.Log(j + " ");
                        if (!listWordIndex.Exists(x => x == j))
                        {
                            listWordIndex.Add(j);
                        }
                    }
            }
        }
            int index = listWordIndex[Random.Range(0, listWordIndex.Count)];
            return GameProcess.cellNumbers[index];
    }

        

}
