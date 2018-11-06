using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillWordCreator : MonoBehaviour
{

    public GameObject CellGrid; //
    public Slider NumberFirstCell;

    List<List<int>> ListPassedСells; // пройденые пустые ячейки с разделением на зоны
    List<int> deadEndCell; //тупиковые ячейки с которых удобнее всего начинать слова
    List<string> ListWordsForFillword;

    int[,] mass;

    public int numberLetersInFirstWord = 6;
    int rankOfListPassedCell = 0; // номер пустой зоны
    int lengthOfsmolestWord = 3;

    public int columns = 5;
    public int rows = 6;


    // Use this for initialization
    void Start()
    {
        ResetFillWord();


    }

    // Update is called once per frame
    void Update()
    {
        // ResetFillWord();
    }

    public void ChangMinCountLetters()
    {
        numberLetersInFirstWord = (int)NumberFirstCell.value;
    }

    public void ResetFillWord()
    {
        // Debug.Log("Reset");
        do
        {
            // Debug.Log("Reset in while");
            mass = new int[columns, rows];
            ListPassedСells = new List<List<int>>();
            deadEndCell = new List<int>();
            rankOfListPassedCell = 0;
            SetCellNumbers();

            FillingFirstWord(mass, numberLetersInFirstWord);


        } while (!CheckEmptyCells(mass, lengthOfsmolestWord));


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

    void FillingFirstWord(int[,] mass, int numberOfLetters)
    {
        int startCell = Random.Range(0, mass.GetLength(0) * mass.GetLength(1));

        //Debug.Log("Start cell = " + startCell);
        SetValueByNumber(1, startCell, ref mass);

        CellGrid.transform.GetChild(startCell).GetComponent<Image>().color = Color.blue;

        int x = startCell;
        for (int i = 0; i < numberOfLetters - 1; i++)
        {
            x = GetNextCell(mass, x);
            if (x == -1)
            {
                ResetFillWord();
                break;
            }
            //Debug.Log("Next cell = " + x);
            //CellGrid.transform.GetChild(x).GetComponent<Image>().color = Color.blue;
            SetValueByNumber(1, x, ref mass);

        }
        SetColors(mass);
    }

    void SetValueByNumber(int value, int number, ref int[,] mass)
    {
        int i = number / mass.GetLength(1);
        int j = number - i * mass.GetLength(1);
        mass[i, j] = value;

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

        return i * mass.GetLength(1) + j;
    }

    bool CheckEmptyCells(int[,] mass, int min)
    {
        //ListPassedСells = new List<List<int>>();
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

        if (CheckMinCountCellInZone(lengthOfsmolestWord))
        {
            return false;
        }

        string str = "";
        foreach (var x in ListPassedСells)
        {
            foreach (var y in x)
            {
                str += y.ToString() + " ";
            }
            str += "\n";
        }

        // Debug.Log("List count = " + ListPassedСells.Count + " \n" + str);
        return true;
    }

    bool FindCellInList(int number)
    {
        if (ListPassedСells.Count == 0)
            return false;

        foreach (var x in ListPassedСells)
        {
            if (x.Exists(y => y == number))
                return true;
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
                //Debug.Log("up = " + up + " number = " + number + " i = " + i + " j = " + j);
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
                //Debug.Log("down = " + down + " number = " + number + " i = " + i + " j = " + j);
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
            // Debug.Log("ltft = " + left + " number = " + number + " i = " + i + " j = " + j);
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
            // Debug.Log("right = " + right + " number = " + number + " i = " + i + " j = " + j);
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
        // Debug.Log("number = " + number + " count = " + count );
        return count;
    }

    void CheckTupicalCell(int[,] mass)
    {
        deadEndCell.Clear();
        foreach (var x in ListPassedСells)
            foreach (var y in x)
            {
                //Debug.Log("Count = " + CountFreeNearestCell(y));
                if (CountFreeNearestCell(y) == 1)
                {
                    deadEndCell.Add(y);
                }
            }
    }

    bool CheckMinCountCellInZone(int min)
    {

   

        foreach (var x in ListPassedСells)
        {
            if (x.Count < min)
            {
                Debug.Log(x.Count + " < " + min + " = false");
                return true;
            }
        }
        // Debug.Log("true");

        return false;
    }

    public void AddNewWord()
    {

        //вставить функцию определения минимального слова в словаре


        CheckEmptyCells(mass, lengthOfsmolestWord);

        CheckMinCountCellInZone(lengthOfsmolestWord);
        CheckTupicalCell(mass);



        int startCell = 0;

        //Debug.Log("deadcell count = " + deadEndCell.Count);



        string s = "";
        foreach (var item in deadEndCell)//вывод номеров пустых  ячеек
        {
            s += item.ToString() + " ";
        }
        Debug.Log(s);

        //-------------------получение стартовой ячейки-----------------------------
        if (ListPassedСells.Count > 0)
        {
            if (deadEndCell.Count > 0)
            {
                //метод проверки на количество тупиковых ячеек в зоне и в зависимости от возможности внести слово возвращает флаг
                //Debug.Log("deadcell = " + deadEndCell[0]);
                startCell = deadEndCell[0];

            }
            else
            {
                while (true)
                {
                    startCell = Random.Range(0, mass.GetLength(0) * mass.GetLength(1));
                    if (GetValueByNubber(startCell) == 0)
                    {
                        Debug.Log("random");
                        break;
                    }
                }
            }
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^




        int x = startCell;
        int[,] buffMas = new int[mass.GetLength(0), mass.GetLength(1)];



        Debug.Log(lengthOfsmolestWord);

        //определить количество букв в заполняемом слове 
        //
        //
        //
        //

        SetValueByNumber(2, x, ref mass);

        for (int i = 0; i < lengthOfsmolestWord - 1; i++)
        {
            x = GetNextCell(mass, x);
            if (x == -1)
            {
                Debug.Log("Error");
                ResetFillWord();
                break;
            }
            Debug.Log("Set = " + x);
            SetValueByNumber(2, x, ref mass);

        }

        // ReturnToPreMass(mass, ref buffMas);
        ShowMassInDebugLog();


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
                }
            }

        }

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
}
