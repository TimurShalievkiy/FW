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
                return;
            }
            else if (!CellController.cells.Exists(x => x == this.gameObject)
                      && IsNearest(this.gameObject.transform.GetSiblingIndex()))
            {
                this.gameObject.GetComponent<Image>().color = CellController.SetColor();
                CellController.cells.Add(this.gameObject);
            }
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
