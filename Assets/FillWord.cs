using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWord : MonoBehaviour
{
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

        //int x = Random.Range(0, mass.GetLength(0) - 1);
        //int y = Random.Range(0, mass.GetLength(1) - 1);
        //Debug.Log(x + " " + y);
        // mass[x, y] = 1;
        mass[1, 1] = 1;
        mass[1, 2] = 1;
        mass[1, 3] = 1;
        mass[2, 1] = 1;
        mass[3, 1] = 1;

        mass[7, 1] = 1;
        mass[7, 2] = 1;
        mass[7, 3] = 1;
        mass[6, 1] = 1;
        mass[5, 1] = 1;

        mass[1, 7] = 1;
        mass[1, 6] = 1;
        mass[1, 5] = 1;
        mass[2, 7] = 1;
        mass[3, 7] = 1;

        mass[7, 7] = 1;
        mass[7, 6] = 1;
        mass[7, 5] = 1;
        mass[6, 7] = 1;
        mass[5, 7] = 1;



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

}
