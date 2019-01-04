using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool used = false;

    // Use this for initialization
    void Start()
    {

    }

    //// Update is called once per frame
    //void Update () {

    //}
    public void PointerEnter()
    {
        if (!used)
        {
            if (CellController.cells.Count == 0)
            {
                CellController.cells.Add(this.gameObject);
                this.gameObject.GetComponent<Image>().color = CellController.SetColor();
               // return;
            }
            else if (!CellController.cells.Exists(x => x == this.gameObject)
                      && IsNearest(this.gameObject.transform.GetSiblingIndex()))
            {
                this.gameObject.GetComponent<Image>().color = CellController.SetColor();
                CellController.cells.Add(this.gameObject);
            }
            else if (CellController.cells.Exists(x => x == this.gameObject))
            {
                int index = CellController.cells.FindIndex(x =>x == this.gameObject);

                if (index != 0)
                {
                   // Debug.Log("index = " + index + " count = " + CellController.cells.Count);
                    for (int i = CellController.cells.Count - 1; i > index; i--)
                    {
                        CellController.cells[i].GetComponent<Image>().color = Color.white;
                        CellController.cells.Remove(CellController.cells[i]);
                       // Debug.Log(i);
                    }
                }
                else if (index == 0)
                {
                    //Debug.Log("index = " + index + " count = " + CellController.cells.Count);
                    for (int i = CellController.cells.Count - 1; i > 0; i--)
                    {
                        CellController.cells[i].GetComponent<Image>().color = Color.white;
                        CellController.cells.Remove(CellController.cells[i]);
                    }
                }

            }
            string s = "";
            for (int i = 0; i < CellController.cells.Count; i++)
            {
                s += CellController.cells[i].transform.GetChild(0).GetComponent<Text>().text;
            }
            GameObject.Find("word").GetComponent<Text>().text = s;
            GameObject.Find("word").GetComponent<Text>().color = Color.white;
        }
    }

    public bool IsNearest(int num)
    {
        int i = num / FillWordCreator.rows;
        int j = num - i * FillWordCreator.rows;

        int index = CellController.cells[CellController.cells.Count - 1].transform.GetSiblingIndex();
        if (i - 1 >= 0)
        {
            if (GetNumberByPosInArray(i - 1, j) == index)
            {
                //Debug.Log("i - 1 >= 0 ");
                return true;
            }

        }
        if (i + 1 < FillWordCreator.columns)
        {
            if (GetNumberByPosInArray(i + 1, j) == index)
            {
                // Debug.Log("i + 1 < FillWordCreator.columns");
                return true;
            }

        }
        if (j - 1 >= 0)
        {
            if (GetNumberByPosInArray(i, j - 1) == index)
            {
                //Debug.Log("j - 1 >= 0");
                return true;
            }

        }
        if (j + 1 < FillWordCreator.rows)
        {
            if (GetNumberByPosInArray(i, j + 1) == index)
            {
                //Debug.Log("j + 1 < FillWordCreator.rows");
                return true;
            }

        }
        // Debug.Log("None");
        return false;
    }

    int GetNumberByPosInArray(int i, int j)
    {
        return i * FillWordCreator.rows + j;
    }

}
