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
        Debug.Log("Reset");
        do
        {
            mass = new int[5, 5];
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
            SetValueByNumber(1, x, ref mass);
        }
    }

    void SetValueByNumber(int value, int number, ref int[,] mass)
    {
        int i = number / mass.GetLength(0);
        int j = number - i * mass.GetLength(0);
        mass[i, j] = value;

        CellGrid.transform.GetChild(number).GetComponent<Image>().color = Color.blue;
        //CellGrid.transform.GetChild(number).transform.GetChild(0).GetComponent<Text>().
    }

    int GetNextCell(int[,] mass, int numberCurrentCell)
    {
        int i = numberCurrentCell / mass.GetLength(0);
        int j = numberCurrentCell - i * mass.GetLength(0);

        int[] dir = { 0, 0, 0, 0 };

        int index = 0;

        int up = (mass.GetLength(0) * (i - 1)) + j;
        //Debug.Log("Current = " + numberCurrentCell + " Up cell = " + up);
        if (i - 1 >= 0 && GetValueByNubber(up) == 0)
        {
            // Debug.Log("Up cell Free ");
            dir[index] = up;
            index++;
        }

        int down = (mass.GetLength(0) * (i + 1)) + j;
        if (i + 1 < mass.GetLength(0) && GetValueByNubber(down) == 0)
        {
            //Debug.Log("down cell Free ");
            dir[index] = down;
            index++;
        }

        int left = (mass.GetLength(0) * i) + j - 1;
        if (j - 1 >= 0 && GetValueByNubber(left) == 0)
        {
            //Debug.Log("left cell Free ");
            dir[index] = left;
            index++;
        }

        int right = (mass.GetLength(0) * i) + j + 1;
        if (j + 1 < mass.GetLength(1) && GetValueByNubber(right) == 0)
        {
            //Debug.Log("right cell Free ");
            dir[index] = right;
            index++;
        }

        if (index == 0)
            return -1;

        int index2 = Random.Range(0, index);
        //Debug.Log("index2 = " + index2 + " index1 = " + index);
        return dir[index2];
    }

    int GetValueByNubber(int number)
    {
        int i = number / mass.GetLength(0);
        int j = number - i * mass.GetLength(0);
        return mass[i, j];
    }



    bool CheckEmptyCells(int[,] mass, int min)
    {

        for (int i = 0; i < mass.GetLength(0); i++)
        {
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                if (mass[i, j] == 0)
                {
                    if (!FindCellInList(mass.GetLength(0) * i + j))
                    {
                        ListPassedСells.Add(new List<int>());
                        ListPassedСells[rankOfListPassedCell].Add(mass.GetLength(0) * i + j);
                        //Debug.Log("Added in CheckEmptyCells " + (mass.GetLength(0) * i + j));
                        CheckNearest(mass.GetLength(0) * i + j);
                        rankOfListPassedCell++;
                    }
                }
            }
        }

        string str = "";
        foreach (var x in ListPassedСells)
        {
            if (!CheckMinCountCellInZone(lengthOfsmolestWord))
            {
                return false;
            }

            foreach (var y in x)
            {
                str += y.ToString() + " ";
            }
            str += "\n";
        }
        
      //  Debug.Log("List count = " + ListPassedСells.Count + " \n" + str);
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
        int i = number / mass.GetLength(0);
        int j = number - i * mass.GetLength(0);
        //проверка вурхней ячейки на пустоту и запись
        if (i - 1 >= 0)
        {
            int up = (mass.GetLength(0) * (i - 1)) + j;

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
            int down = (mass.GetLength(0) * (i + 1)) + j;

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
            int left = (mass.GetLength(0) * i) + j - 1;
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
            int right = (mass.GetLength(0) * i) + j + 1;
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
        int i = number / mass.GetLength(0);
        int j = number - i * mass.GetLength(0);

        int count = 0;
        
        //проверка вурхней ячейки на пустоту и запись
        if (i - 1 >= 0)
        {         
            int up = (mass.GetLength(0) * (i - 1)) + j;
            if (GetValueByNubber(up) == 0 )
            {
                count++;
            }
        }
        //проверка нижней ячейки на пустоту и запись
        if (i + 1 < mass.GetLength(0))
        {
            int down = (mass.GetLength(0) * (i + 1)) + j;
            if (GetValueByNubber(down) == 0 )
            {
                count++;
            }
        }
        //проверка левой ячейки на пустоту и запись
        if (j - 1 >= 0)
        {
            int left = (mass.GetLength(0) * i) + j - 1;
            if (GetValueByNubber(left) == 0 )
            {
                count++;
            }
        }
        //проверка правой ячейки на пустоту и запись
        if (j + 1 < mass.GetLength(1))
        {
            int right = (mass.GetLength(0) * i) + j + 1;
            if (GetValueByNubber(right) == 0 )
            {
                count++;
            }
        }
       // Debug.Log("number = " + number + " count = " + count );
        return count;
    }
    void CheckTupicalCell(int[,] mass)
    {
        deadEndCell = new List<int>();
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
               // Debug.Log(x.Count + " > " + min +" = false");
                return false;
            }
        }
       // Debug.Log("true");
            return true;
    }

    public void AddNewWord()
    {
        ;
        //вставить функцию определения минимального слова в словаре
        CheckMinCountCellInZone(lengthOfsmolestWord);


        int startCell = 0;

        if (deadEndCell.Count > 0)
        {
            startCell = deadEndCell[0];

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

        //Debug.Log("Start cell = " + startCell);
        SetValueByNumber(1, startCell, ref mass);

        int x = startCell;
        int[,] buffMas = mass;
        bool checker = true;
        int counter = 0;
        int counter2 = 0;
        while (checker)
        {
            mass = buffMas;
            counter = 0;
            for (int i = 0; i < numberLetersInFirstWord - 1; i++)
            {
                x = GetNextCell(mass, x);
                if (x == -1)
                {
                    Debug.Log(" x == -1");
                    //CellGrid.transform.GetChild(x).GetComponent<Image>().color = Color.red;
                    break; 
                }
                counter++;
                //Debug.Log("Next cell = " + x);
                SetValueByNumber(2, x, ref mass);
                CellGrid.transform.GetChild(x).transform.GetChild(0).GetComponent<Text>().text = 2.ToString();
                CellGrid.transform.GetChild(x).GetComponent<Image>().color = Color.green;
            }
            if(counter == numberLetersInFirstWord)
                checker = false;
            counter2++;
            if (counter2 == 1000)
            {
                checker = false;
                Debug.Log("eror");
            }
        }

    }
}
