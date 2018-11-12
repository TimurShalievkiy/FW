using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FillWordCreator : MonoBehaviour
{

    public GameObject CellGrid; //


    List<List<int>> ListPassedСells; // пройденые пустые ячейки с разделением на зоны
    List<int> deadEndCell; //тупиковые ячейки с которых удобнее всего начинать слова
    List<string> ListWordsForFillword;

    int[,] mass;

    //public int numberLetersInFirstWord = 6;
    int rankOfListPassedCell = 0; // номер пустой зоны


    public int columns = 5;
    public int rows = 6;

    int countOfAddedWord = 1;

    bool bildIsDone = false;

    public static int countOfResets = 0;


    public struct MinList
    {
        public int lengtn;
        public int call;
        public int count;

        public MinList(int lengtn, int call, int count)
        {
            this.lengtn = lengtn;
            this.call = call;
            this.count = count;
        }
    }

    public static List<MinList> minList;
    // Use this for initialization
    void Start()
    {
        minList = DictionaryController.GetMinList();
        //InvokeRepeating("ResetFillWord", 1.0f, 0.2f);
    }


    public void ResetFillWord()
    {
 
        bildIsDone = false;

       // Debug.Log("reset--------------------------------");
        countOfResets++;
        mass = new int[columns, rows];
        ListPassedСells = new List<List<int>>();
        ListPassedСells.Clear();
        deadEndCell = new List<int>();
        GameProcess.cellNumbers = new List<List<int>>();
        GameProcess.cellNumbers.Clear();

        minList = DictionaryController.GetMinList();
        int min = GetMinByMinList();

        rankOfListPassedCell = 0;
        countOfAddedWord = 1;
        SetCellNumbers();

        FillingFirstWord(mass);

        countOfAddedWord++;


        while (!bildIsDone)
        {
            AddNewWord();
        }
        // ShowMassInDebugLog();
        //GameProcess.ShowCellNumbers(); 
    }

    void SetCellNumbers()
    {
        int index = 0;
        for (int i = 0; i < mass.GetLength(0); i++)
        {
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                CellGrid.transform.GetChild(index).GetComponent<Image>().color = Color.white;
                CellGrid.transform.GetChild(index).transform.GetChild(0).GetComponent<Text>().text = index.ToString();
                index++;
            }
        }
    }

    void FillingFirstWord(int[,] mass)
    {
        int startCell = Random.Range(0, mass.GetLength(0) * mass.GetLength(1));

        SetValueByNumber(countOfAddedWord, startCell, ref mass);

        CellGrid.transform.GetChild(startCell).GetComponent<Image>().color = Color.blue;

        int x = startCell;

        ListPassedСells.Clear();
        CheckEmptyCells(mass, GetMinByMinList());

        int numberOfLetters = GetNumberLetersInWord(ListPassedСells[0].Count);

        for (int i = 0; i < numberOfLetters - 1; i++)
        {
            x = GetNextCell(mass, x);
            if (x == -1)
            {
                ResetFillWord();
                break;
            }

            SetValueByNumber(1, x, ref mass);

        }
        SetColors(mass);
    }

    void SetValueByNumber(int value, int number, ref int[,] mass)
    {
        int i = number / mass.GetLength(1);
        int j = number - i * mass.GetLength(1);
        mass[i, j] = value;
        if (GameProcess.cellNumbers.Count == 0)
        {
            GameProcess.cellNumbers.Add(new List<int>());
            GameProcess.cellNumbers[0].Add(GetNumberByPosInArray(i, j));
        }
        else
        {
            if (GetValueByNubber(GameProcess.cellNumbers[GameProcess.cellNumbers.Count - 1][0]) != GetValueByNubber(number))
            {
                GameProcess.cellNumbers.Add(new List<int>());
                GameProcess.cellNumbers[GameProcess.cellNumbers.Count - 1].Add(GetNumberByPosInArray(i, j));
            }
            else
            {
                GameProcess.cellNumbers[GameProcess.cellNumbers.Count - 1].Add(GetNumberByPosInArray(i, j));
            }
        }


        //CellGrid.transform.GetChild(number).GetComponent<Image>().color = Color.blue;
        //CellGrid.transform.GetChild(number).transform.GetChild(0).GetComponent<Text>().
    }

    int GetNextCell(int[,] mass, int numberCurrentCell)
    {
        int i = numberCurrentCell / mass.GetLength(1);
        int j = numberCurrentCell - i * mass.GetLength(1);

        int[] dir = { 0, 0, 0, 0 };

        int index = 0;


        if (i - 1 >= 0)
        {
            int up = GetNumberByPosInArray(i - 1, j);

            //Debug.Log("Current = " + numberCurrentCell + " Up cell = " + up);
            if (GetValueByNubber(up) == 0)
            {
                // Debug.Log("Up cell Free ");
                dir[index] = up;
                index++;
            }
        }

        if (i + 1 < mass.GetLength(0))
        {
            int down = GetNumberByPosInArray(i + 1, j);
            if (GetValueByNubber(down) == 0)
            {
                //Debug.Log("down cell Free ");
                dir[index] = down;
                index++;
            }
        }

        if (j - 1 >= 0)
        {
            //int left = (mass.GetLength(0) * i) + j - 1;
            int left = GetNumberByPosInArray(i, j - 1);
            if (GetValueByNubber(left) == 0)
            {
                //Debug.Log("left cell Free ");
                dir[index] = left;
                index++;
            }
        }

        if (j + 1 < mass.GetLength(1))
        {
            //int right = (mass.GetLength(0) * i) + j + 1;
            int right = GetNumberByPosInArray(i, j + 1);
            if (GetValueByNubber(right) == 0)
            {
                //Debug.Log("right cell Free ");
                dir[index] = right;
                index++;
            }
        }
        if (index == 0)
            return -1;

        int index2 = Random.Range(0, index);
        //Debug.Log("index2 = " + index2 + " index1 = " + index);
        return dir[index2];
    }

    int GetValueByNubber(int number)
    {
        int i = number / mass.GetLength(1);
        int j = number - i * mass.GetLength(1);

        // Debug.Log("number = " + number + " i = " + i + " j = " + j);
        return mass[i, j];
    }

    int GetNumberByPosInArray(int i, int j)
    {
        //Debug.Log("i = " + i + " j = " + j);
        return i * mass.GetLength(1) + j;
    }

    bool CheckEmptyCells(int[,] mass, int min)
    {
        ListPassedСells.Clear();
        rankOfListPassedCell = 0;
        for (int i = 0; i < mass.GetLength(0); i++)
        {
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                if (mass[i, j] == 0)
                {
                    int cellNum = GetNumberByPosInArray(i, j);
                    if (!FindCellInList(cellNum))
                    {
                        ListPassedСells.Add(new List<int>());
                        ListPassedСells[rankOfListPassedCell].Add(cellNum);
                        CheckNearest(cellNum);
                        rankOfListPassedCell++;
                    }
                }
            }
        }

        if (ListPassedСells.Count == 0)
        {
            return true;
        }
        if (CheckMinCountCellInZone(GetMinByMinList()))
        {
            return false;
        }

        return true;
    }

    bool FindCellInList(int number)
    {
        if (ListPassedСells.Count == 0)
            return false;

        for (int i = 0; i < ListPassedСells.Count; i++)
        {
            for (int j = 0; j < ListPassedСells[i].Count; j++)
                if (ListPassedСells[i][j] == number) { return true; }
        }

        return false;
    }

    void CheckNearest(int number)
    {
        int i = number / mass.GetLength(1);
        int j = number - i * mass.GetLength(1);
        //проверка вурхней ячейки на пустоту и запись
        if (i - 1 >= 0)
        {
            //int up = (mass.GetLength(0) * (i - 1)) + j;
            int up = GetNumberByPosInArray(i - 1, j);
            if (GetValueByNubber(up) == 0 && !FindCellInList(up))
            {

                ListPassedСells[rankOfListPassedCell].Add(up);
                CheckNearest(up);
                //Debug.Log("Added in CheckNearest " + up);
            }
        }
        //проверка нижней ячейки на пустоту и запись
        if (i + 1 < mass.GetLength(0))
        {
            //int down = (mass.GetLength(0) * (i + 1)) + j;
            int down = GetNumberByPosInArray(i + 1, j);

            if (GetValueByNubber(down) == 0 && !FindCellInList(down))
            {

                ListPassedСells[rankOfListPassedCell].Add(down);
                CheckNearest(down);
                //Debug.Log("Added in CheckNearest " + down);
            }
        }
        //проверка левой ячейки на пустоту и запись
        if (j - 1 >= 0)
        {
            // int left = (mass.GetLength(0) * i) + j - 1;
            int left = GetNumberByPosInArray(i, j - 1);

            if (GetValueByNubber(left) == 0 && !FindCellInList(left))
            {
                ListPassedСells[rankOfListPassedCell].Add(left);
                CheckNearest(left);
                //Debug.Log("Added in CheckNearest " + left);
            }
        }
        //проверка правой ячейки на пустоту и запись
        if (j + 1 < mass.GetLength(1))
        {
            //int right = (mass.GetLength(0) * i) + j + 1;
            int right = GetNumberByPosInArray(i, j + 1);

            if (GetValueByNubber(right) == 0 && !FindCellInList(right))
            {
                ListPassedСells[rankOfListPassedCell].Add(right);
                CheckNearest(right);
                //Debug.Log("Added in CheckNearest " + right);
            }
        }
    }

    int CountFreeNearestCell(int number)
    {
        int i = number / mass.GetLength(1);
        int j = number - i * mass.GetLength(1);

        int count = 0;

        //проверка вурхней ячейки на пустоту и запись
        if (i - 1 >= 0)
        {
            //int up = (mass.GetLength(0) * (i - 1)) + j;
            int up = GetNumberByPosInArray(i - 1, j);
            if (GetValueByNubber(up) == 0)
            {
                count++;
            }
        }
        //проверка нижней ячейки на пустоту и запись
        if (i + 1 < mass.GetLength(0))
        {
            //int down = (mass.GetLength(0) * (i + 1)) + j;
            int down = GetNumberByPosInArray(i + 1, j);
            if (GetValueByNubber(down) == 0)
            {
                count++;
            }
        }
        //проверка левой ячейки на пустоту и запись
        if (j - 1 >= 0)
        {
            // int left = (mass.GetLength(0) * i) + j - 1;
            int left = GetNumberByPosInArray(i, j - 1);
            if (GetValueByNubber(left) == 0)
            {
                count++;
            }
        }
        //проверка правой ячейки на пустоту и запись
        if (j + 1 < mass.GetLength(1))
        {
            //int right = (mass.GetLength(0) * i) + j + 1;
            int right = GetNumberByPosInArray(i, j + 1);
            if (GetValueByNubber(right) == 0)
            {
                count++;
            }
        }
        return count;
    }

    void CheckTupicalCell(int[,] mass)
    {
        deadEndCell.Clear();
        for (int i = 0; i < ListPassedСells.Count; i++)
        {
            for (int j = 0; j < ListPassedСells[i].Count; j++)
            {
                if (CountFreeNearestCell(ListPassedСells[i][j]) == 1)
                {
                    deadEndCell.Add(ListPassedСells[i][j]);
                }
            }
        }
    }

    bool CheckMinCountCellInZone(int min)
    {
        for (int i = 0; i < ListPassedСells.Count; i++)
        {
            if (ListPassedСells[i].Count < min)
            {
                return true;
            }
        }

        return false;
    }

    public void AddNewWord()
    {

        int min = GetMinByMinList();

        CheckEmptyCells(mass, min);

        if (ListPassedСells.Count == 0)
        {
            bildIsDone = true;
            return;
        }

        if (CheckMinCountCellInZone(min))
        {
            ResetFillWord();
            return;
        }
        CheckTupicalCell(mass);

        int startCell = 0;

        //-------------------получение стартовой ячейки-----------------------------
        if (ListPassedСells.Count > 0)
        {
            if (deadEndCell.Count > 0)
            { 
                startCell = deadEndCell[Random.Range(0, deadEndCell.Count)];
            }
            else
            {
                while (true)
                {
                    startCell = Random.Range(0, mass.GetLength(0) * mass.GetLength(1));
                    if (GetValueByNubber(startCell) == 0)
                    {
                        break;
                    }
                }
            }
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        int x = startCell;
        int[,] buffMas = new int[mass.GetLength(0), mass.GetLength(1)];


        int numOfLetters = GetNumberLetersInWord(GetCountOfCellInZoneByNumberOfCell(startCell));
        // Debug.Log("numOfLetters = " + numOfLetters);
        SetValueByNumber(countOfAddedWord, x, ref mass);

       
        
            ReturnToPreMass(mass, ref buffMas);
            for (int i = 0; i < numOfLetters - 1; i++)
            {
                x = GetNextCell(mass, x); 
                if (x == -1)
                {
                ResetFillWord();
                break;
                }
                SetValueByNumber(countOfAddedWord, x, ref mass);
            }                 

        if (CheckMinCountCellInZone(min))
        { 
            ResetFillWord();
            return;
        }

        countOfAddedWord++;
        SetColors(mass);

    }

    void ReturnToPreMass(int[,] x, ref int[,] y)
    {
        for (int i = 0; i < x.GetLength(0); i++)
        {
            for (int j = 0; j < x.GetLength(1); j++)
            {
                y[i, j] = x[i, j];
            }
        }
    }
  
    int GetNumberLetersInWord(int count)
    {
        int min = GetMinByMinList();
        int max = DictionaryController.GetMax(); 

        if (count >= min && count <= max)
        {
            return Random.Range(min, count);// count;
        }
        else if (count < min)
        {
            return 0;
        }
        else if (count > max)
        {
            if (count - max >= min)
            {
                if (countOfAddedWord == 1)
                    return Random.Range(max - min, max);
                return Random.Range(min, max);
            }
            else
            {
                return count - (min - (count - max));
            }
        }
        Debug.Log("Not of all");
        return 0;
    }

    int GetCountOfCellInZoneByNumberOfCell(int numCell)
    {
        int count = -1;
        for (int i = 0; i < ListPassedСells.Count; i++)
        {
            for (int j = 0; j < ListPassedСells[i].Count; j++)
            {
                if (ListPassedСells[i][j] == numCell && ListPassedСells[i][j] != -1)
                {
                    count++;
                }
            }
            
        }
        return ListPassedСells[count].Count;
    }


    int GetMinByMinList() 
    {
        MinList m = minList.FindAll(x => x.count > 0).First(x => x.call == minList.Min(y => y.call));
        //minList = minList.Select(p => p.lengtn == m.lengtn && p.call == m.call
        //         ? new MinList { lengtn = m.lengtn, call = m.call, count = --m.count }
        //         : p)
        //         .ToList();

        return m.lengtn;
    }
    public void ShowMinByMinList()
    {
        string str = "";
        foreach (var item in minList)
        {
            str += "Len = " + item.lengtn + " call = " + item.call + " count = " + item.count + "\n";
        }
        Debug.Log(str); 

    }

    void ShowMassInDebugLog()
    {
        string s2 = "";
        for (int i = 0; i < mass.GetLength(0); i++)
        {
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                s2 += mass[i, j].ToString() + " ";
            }
            s2 += "\n";
        }
        Debug.Log(s2);
    }

    void SetColors(int[,] x)
    {
        for (int i = 0; i < x.GetLength(0); i++)
        {
            for (int j = 0; j < x.GetLength(1); j++)
            {
                int num = GetNumberByPosInArray(i, j);
                switch (mass[i, j])
                {
                    case 0:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = Color.white;
                        break;
                    case 1:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = Color.blue;
                        break;
                    case 2:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = Color.green;
                        break;
                    case 3:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = Color.cyan;
                        break;
                    case 4:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = Color.grey;
                        break;
                    case 5:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = Color.yellow;
                        break;
                    case 6:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = Color.red;
                        break;
                    case 7:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = Color.magenta;
                        break;
                    case 8:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = new Color(0.3f, 0.8f, 0.2f);
                        break;
                    case 9:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = new Color(0.5f, 0.9f, 0.5f);
                        break;
                    case 10:
                        CellGrid.transform.GetChild(num).GetComponent<Image>().color = new Color(0.2f, 0.4f, 0.8f);
                        break;
                }
            }
        }
    }


}
