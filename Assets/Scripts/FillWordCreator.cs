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

    int[,] mass;

    //public int numberLetersInFirstWord = 6;
    int rankOfListPassedCell = 0; // номер пустой зоны


    public static int columns;
    public static int rows ;

    int countOfAddedWord = 0;// номер добавляемого слова

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

    public static List<MinList> minList;//получение списка доступных слов для заполнения

    // Use this for initialization
    void Start()
    {
        minList = DictionaryController.GetMinList();
        
        rows =  CellGrid.GetComponent<GridLayoutGroup>().constraintCount;
        columns = CellGrid.transform.childCount / rows;
  
    }


    public void ResetFillWord()
    {
        //Debug.Log("reset");
        rows = CellGrid.GetComponent<GridLayoutGroup>().constraintCount;
        columns = CellGrid.transform.childCount / rows;

        bildIsDone = false;

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
        countOfAddedWord = 0;
        SetCellNumbers();

        // FillingFirstWord(mass);

        // ShowMinByMinList();

        countOfAddedWord++;

        while (!bildIsDone)
        {
            AddNewWord();
        }
        // if(bildIsDone)
        // ShowMinByMinList();

        SetColors(mass);
    }


    public void AddNewWord()
    {

        int min = GetMinByMinList();

        CheckEmptyCells(min);

        if (ListPassedСells.Count == 0)
        {
            //Debug.Log("Done 1");
            bildIsDone = true;
            return;
        }

        if (CheckMinCountCellInZone(min))
        {
            ResetWordsInMinZone(min);

            CheckEmptyCells(min);

            if (ListPassedСells.Count == 0)
            {
               // Debug.Log("Done 2");
                bildIsDone = true;
                return;
            }

            if (CheckMinCountCellInZone(min))
            {
                ResetFillWord();
                return;
            }
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
                int i = Random.Range(0, ListPassedСells.Count);
                int count = ListPassedСells[i].Count;
                startCell = ListPassedСells[i][Random.Range(0, count)];

            }
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        int x = startCell;
        int[,] buffMas = new int[mass.GetLength(0), mass.GetLength(1)];


        int numOfLetters = GetNumberLetersInWord(GetCountOfCellInZoneByNumberOfCell(startCell));
        // Debug.Log("numOfLetters = " + numOfLetters);
        SetValueByNumber(countOfAddedWord, x);

        ReturnToPreMass(mass, ref buffMas);
        for (int i = 0; i < numOfLetters - 1; i++)
        {
            x = GetNextCell(x);
            if (x == -1)
            {
                ResetFillWord();
                break;
            }
            SetValueByNumber(countOfAddedWord, x);
        }
        DecrementCountInMinList(numOfLetters);
        if (CheckMinCountCellInZone(min))
        {
            ResetFillWord();
            return;
        }

        countOfAddedWord++;
    }

    //перписать значения с одного массива во второй
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

    //задать ячейке значение по номеру
    void SetValueByNumber(int value, int number)
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
    }

    void SetCellNumbers()
    {
        int index = 0;
       // Debug.Log(CellGrid.transform.childCount);
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

    int GetNextCell( int numberCurrentCell)
    {
        int i = numberCurrentCell / mass.GetLength(1);
        int j = numberCurrentCell - i * mass.GetLength(1);

        int[] dir = { -1, -1, -1, -1 };

        int index = 0;


        if (i - 1 >= 0)
        {
            int up = GetNumberByPosInArray(i - 1, j);


            //Debug.Log("Current = " + numberCurrentCell + " Up cell = " + up);
            if (GetValueByNubber(up) == 0)
            {
                if (CountFreeNearestCell(up) == 1)
                    return up;
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
                if (CountFreeNearestCell(down) == 1)
                    return down;
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
                if (CountFreeNearestCell(left) == 1)
                    return left;
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
                if (CountFreeNearestCell(right) == 1)
                    return right;
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

    //получить значени по номеру ячейки с массива
    int GetValueByNubber(int number)
    {
        int i = number / mass.GetLength(1);
        int j = number - i * mass.GetLength(1);

        // Debug.Log("number = " + number + " i = " + i + " j = " + j);
        return mass[i, j];
    }

    //получить номер ячейки по индексу в массиве
    int GetNumberByPosInArray(int i, int j)
    {
        //Debug.Log("i = " + i + " j = " + j);
        return i * mass.GetLength(1) + j;
    }

    bool CheckEmptyCells(int min)
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

    // проверка наличия номера ячейки в списке пройденных
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

    //формирование списка зон с номерами ячеек
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

    //получить количество ближайших пустых ячеек
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

    // заполнение списка тупиковых ячеек
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

    //проверка зон на вместимость минимальной длины слова
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

    //получить количество букв в слове по количеству свободных ячеек в зоне
    int GetNumberLetersInWord(int count)
    {
        int min = GetMinByMinList();
        int max = GetMaxByMinList();
        //int max = DictionaryController.GetMax();

        if (count >= min && count <= max)
        {
            int res = Random.Range(min, count);
            //Debug.Log("count >= min && count <= max ==== " + res);
            return res;// count;
        }
        if (count < min)
        {
           // Debug.Log("count < min");
            return 0;
        }
        if (count > max)
        {
            //Debug.Log("count > max");
            if (count - max >= min)
            {
               // Debug.Log("count - max >= min");
                if (countOfAddedWord == 0)
                {
                    int res = Random.Range(max - min, max);
                   // Debug.Log("countOfAddedWord == 0   = " + res);
                    return res;
                }
                int res1 = Random.Range(min, max);
               // Debug.Log("min,max   = " + res1);
                return res1;
            }
            else
            {
                int res = Random.Range(min,max);
               // Debug.Log("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE = " + res);
                return res;

            }
        }
       // Debug.Log("Not of all");
        return 0;
    }

    //получить количество пустых ячеек в зоне по ноmеру ячейки
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

    // получить значение миниmума со списка доступных слов
    int GetMinByMinList()
    {
        MinList m = minList.FindAll(x => x.count > 0).First(x => x.call == minList.Min(y => y.call));
        return m.lengtn;
    }

    int GetMaxByMinList()
    {
        int max = minList.Max(y => y.lengtn);
        MinList m = new MinList(0, 0, 0);

     

        int max2 = minList.FindAll(x => x.count > 0).Max(x => x.lengtn);

        m = minList.FindAll(x => x.count > 0).Where(x => x.lengtn == max2).First();

        return m.lengtn;
    }
    void DecrementCountInMinList(int length)
    {
        for (int i = 0; i < minList.Count; i++)
        {
            if (minList[i].lengtn == length)
            {
                minList[i] = new MinList(minList[i].lengtn, minList[i].call, minList[i].count - 1);
            }
        }
        // minList.Find(x => x.lengtn == length).count--;
    }

    // получить значение мини ума со списка доступных слов
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


    int CheckNearestCellForEndTheWord(int number)
    {
        int i = number / mass.GetLength(1);
        int j = number - i * mass.GetLength(1);


        if (i - 1 >= 0)
        {
            int up = GetNumberByPosInArray(i - 1, j);
            for (int k = 0; k < GameProcess.cellNumbers.Count; k++)
            {
                if (GameProcess.cellNumbers[k][0] == up || GameProcess.cellNumbers[k][GameProcess.cellNumbers[k].Count - 1] == up)
                {
                    return up;
                }
            }
        }
        //проверка нижней ячейки на пустоту и запись
        if (i + 1 < mass.GetLength(0))
        {
            //int down = (mass.GetLength(0) * (i + 1)) + j;
            int down = GetNumberByPosInArray(i + 1, j);

            for (int k = 0; k < GameProcess.cellNumbers.Count; k++)
            {
                if (GameProcess.cellNumbers[k][0] == down || GameProcess.cellNumbers[k][GameProcess.cellNumbers[k].Count - 1] == down)
                {
                    return down;
                }
            }
        }
        //проверка левой ячейки на пустоту и запись
        if (j - 1 >= 0)
        {
            // int left = (mass.GetLength(0) * i) + j - 1;
            int left = GetNumberByPosInArray(i, j - 1);

            for (int k = 0; k < GameProcess.cellNumbers.Count; k++)
            {
                if (GameProcess.cellNumbers[k][0] == left || GameProcess.cellNumbers[k][GameProcess.cellNumbers[k].Count - 1] == left)
                {
                    return left;
                }

            }
        }

        if (j + 1 < mass.GetLength(1))
        {
            int right = GetNumberByPosInArray(i, j + 1);

            for (int k = 0; k < GameProcess.cellNumbers.Count; k++)
            {
                if (GameProcess.cellNumbers[k][0] == right || GameProcess.cellNumbers[k][GameProcess.cellNumbers[k].Count - 1] == right)
                {
                    return right;
                }
            }
        }

        return -1;
    }

    void ResetWordsInMinZone(int min)
    {
        //int res = -1;
        List<int> buff = new List<int>();
        for (int i = 0; i < ListPassedСells.Count; i++)
        {
            if (ListPassedСells[i].Count < min)
            {
                buff.Clear();
                for (int j = 0; j < ListPassedСells[i].Count; j++)
                {
                    buff.Add(ListPassedСells[i][j]);
                }
                ReplaceWord(buff);
            }
        }


    }

    void ReplaceWord(List<int> buff)
    {

        int res = -1;

        List<int> numberEndCell = new List<int>();

        for (int i = 0; i < buff.Count; i++)
        {
            int countFree = CountFreeNearestCell(buff[i]);
            if (countFree <= 1)
            {
                res = CheckNearestCellForEndTheWord(buff[i]);
                if (res != -1)
                    numberEndCell.Add(res);
                res = -1;
                continue;
            }
        }

        int minWord = 999;
        int sideToAdd = -1;
        bool side = false;
        List<int> numbersOfWord = new List<int>();

        for (int i = 0; i < GameProcess.cellNumbers.Count; i++)
        {
            for (int j = 0; j < numberEndCell.Count; j++)
            {
                if (GameProcess.cellNumbers[i].Exists(x => x == numberEndCell[j]))
                {
                    if (minWord > GameProcess.cellNumbers[i].Count)
                    {
                        numbersOfWord = GameProcess.cellNumbers[i];
                        minWord = GameProcess.cellNumbers[i].Count;
                        sideToAdd = numberEndCell[j];

                        if (GameProcess.cellNumbers[i].IndexOf(numberEndCell[j]) == 0)
                            side = true;
                        else
                            side = false;
                    }
                }
            }
        }
        AddFreeNesrestCell(side, buff.Count, minWord, numbersOfWord, buff); 

    }
    void AddFreeNesrestCell(bool side, int buffCount, int minWord, List<int> numbersOfWord, List<int> buff)
    {
        int maxLength = GetMaxByMinList();

        if (side)
            numbersOfWord.Reverse();

        if (buffCount + minWord <= maxLength)
        {
            AddFreeCellToWord(numbersOfWord);
        }
        else if (buffCount + minWord > maxLength)
        {
          //  Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }

        if (side)
            numbersOfWord.Reverse();
    }
    void AddFreeCellToWord(List<int> numbersOfWord)
    {
        int nextCell = numbersOfWord[numbersOfWord.Count - 1];
        int value = GetValueByNubber(nextCell);

        int counter = 0;
        //Debug.Log("start cell = " + nextCell);
        while (nextCell != -1)
        {

            nextCell = GetNextCell(nextCell);
           
            if (nextCell != -1)
            {
                //Debug.Log("Next cell = " + nextCell);
                numbersOfWord.Add(nextCell);

                int i = nextCell / mass.GetLength(1);
                int j = nextCell - i * mass.GetLength(1);
                mass[i, j] = value;
               // SetValueByNumber(value, nextCell);
            }
            else
                break;
        }


        //string s = "";
        //for (int i = 0; i < numbersOfWord.Count; i++)
        //{
        //    s += numbersOfWord[i] + " ";
        //}
        //Debug.Log("word = " + s);
    }

}
