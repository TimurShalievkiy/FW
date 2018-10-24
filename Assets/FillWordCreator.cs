using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillWordCreator : MonoBehaviour {

    public GameObject CellGrid;
    public Slider NumberFirstCell;
    int[,] mass;
    public int numberLetersInFirstWord = 6;
    // Use this for initialization
    void Start () {
        mass = new int[5,5];

        SetCellNumbers();

        FillingFirstWord(mass, numberLetersInFirstWord);

    }
	
	// Update is called once per frame
	void Update () {
        ResetFillWord();
    }

    public void ChangMinCountLetters()
    {
        numberLetersInFirstWord = (int)NumberFirstCell.value;
    }
    public void  ResetFillWord()
    {
        mass = new int[5, 5];

        SetCellNumbers();

        FillingFirstWord(mass, numberLetersInFirstWord);
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
        int startCell = Random.Range(0, mass.GetLength(0) * mass.GetLength(1) );

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

        int index2 = Random.Range(0, index );
        //Debug.Log("index2 = " + index2 + " index1 = " + index);
        return dir[index2];
    }

    int GetValueByNubber(int number)
    {
        int i = number / mass.GetLength(0);
        int j = number - i * mass.GetLength(0);
        return mass[i, j];
    }
}
