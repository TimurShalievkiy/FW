using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWord : MonoBehaviour
{
    enum Directions {zero = 0, top, down, left, right };
    List<List<int>> ListPassedСells;
    int rankOfListPassedCell = 0; // номер пустой зоны
    int[,] mass;  //массив для генерации филворда

    // Use this for initialization
    void Start()
    {
        ListPassedСells = new List<List<int>>();
        mass = new int[9, 9];
   
        CreateFillWord();

    }

    // Update is called once per frame
    void Update()
    {

    }
    int[,] CreateFillWord()
    {
        FillMassZero();

        int x = Random.Range(0, mass.GetLength(0) - 1);
        int y = Random.Range(0, mass.GetLength(1) - 1);
        Debug.Log(x + " " + y);
        // mass[x, y] = 1;


        FillingFirstWord(mass, 5);

        CheckEmptyCells(mass);
        ShowMassFillword(mass);
        return null;
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
    void CheckEmptyCells(int[,] mass)
    {

        for (int i = 0; i < mass.GetLength(0); i++)
        {
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                if (mass[i, j] == 0)
                {
                    if (!FindCellInList(mass.GetLength(0) * i  + j))
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
            foreach (var y in x)
                str += y.ToString() + " ";
            str += "\n";
        }
        Debug.Log("List count = " + ListPassedСells.Count + " \n" + str);
    }

    //проверка ближайших клеток рекурсивный метод
    void CheckNearest(int number)
    {      
        int i = number / mass.GetLength(0);
        int j = number - i * mass.GetLength(0);
        //проверка вурхней ячейки на пустоту и запись
        if (i - 1 > 0)
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
        if (i+1 < mass.GetLength(0))
        {         
            int down = (mass.GetLength(0) * (i + 1)) + j;
            //Debug.Log("down = " + down +" number = " + number + " i = " + i + " j = " + j);
            if (GetValueByNubber(down) == 0 && !FindCellInList(down))
            {
                ListPassedСells[rankOfListPassedCell].Add(down);
                CheckNearest(down);
                //Debug.Log("Added in CheckNearest " + down);
            }
        }
        //проверка левой ячейки на пустоту и запись
        if (j - 1 > 0)
        {
            int left = (mass.GetLength(0) * i ) + j-1;
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
    //метод для отображения в консоли массива
    void ShowMassFillword(int[,] m)
    {
        string outString = "";
        for (int i = 0; i < m.GetLength(0); i++)
        {
            outString += "\n";
            for (int j = 0; j < m.GetLength(1); j++)
                outString += m[i, j].ToString() + " ";
        }
        Debug.Log(outString);
    }


    //заполнение массива пустыми значениями
    void FillMassZero()
    {
        for (int i = 0; i < mass.GetLength(0); i++)
        {
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                mass[i, j] = 0;
            }

        }
    }

    //получение значения ячейки по ее порядковому номеру 
    int GetValueByNubber(int number)
    {
        int i = number / mass.GetLength(0);
        int j = number - i * mass.GetLength(0);
        return mass[i, j];
    }

    //=============================================================
    //    Filling in the first word
    void FillingFirstWord(int[,] mass, int numberOfLetters)
    {
        int startCell = Random.Range(0, mass.GetLength(0) * mass.GetLength(1) - 1);

        Debug.Log("Start cell = " + startCell);
        SetValueByNumber(1, startCell, ref mass);

        int x = 0;
        for (int i = 0; i < numberOfLetters-1; i++)
        {
            x = GetNextCell(mass, startCell);
            Debug.Log("Next cell = " + x);
            SetValueByNumber(1, x, ref mass);
        }
    }
    void SetValueByNumber(int value, int number,ref int [,]mass)
    {
        int i = number / mass.GetLength(0);
        int j = number - i * mass.GetLength(0);
        mass[i, j] = value;
      
    }
    int GetNextCell(int[,] mass,int numberCurrentCell)
    {
        int i = numberCurrentCell / mass.GetLength(0);
        int j = numberCurrentCell - i * mass.GetLength(0);

        int[] dir = { 0, 0, 0, 0 };

        int index = 0;

        int up = (mass.GetLength(0) * (i - 1)) + j;
        if (i - 1 > 0 && GetValueByNubber(up) == 0)
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
        if (j - 1 > 0 && GetValueByNubber(left) == 0)
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
        Debug.Log("index = " + index);
            return dir[Random.Range(0,index-1)];
    }
}
