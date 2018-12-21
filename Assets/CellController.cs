using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour {
    public Text t;
    public GameObject CellGrid;


    // Use this for initialization
    public static List<GameObject> cells;
    void Start () {

        cells = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //public void PointerDown()
    //{
    //    t.text = "Down";

    //}
    public void PointerExit()
    {
        string res = "";
        //Debug.Log("Exit");
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].GetComponent<Image>().color = Color.white;
            res += cells[i].transform.GetChild(0).GetComponent<Text>().text;
        }
        CheckAnsver(res);
        
    }
    public void PointerUp()
{
        PointerExit();

}

    public void CheckAnsver(string str)
    {
        t.text = str;
        bool flag = false;

        for (int i = 0; i < GameProcess.cellNumbers.Count; i++)
        {
            for (int j = 0; j < GameProcess.cellNumbers[i].Count; j++)
            {
                if (cells.Count == GameProcess.cellNumbers[i].Count )
                {
                    Debug.Log("true");
                    for (int k = 0; k < cells.Count; k++)
                    {
                        if (cells[k].transform.GetSiblingIndex() == GameProcess.cellNumbers[i][k])
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
                
            if (flag) break;
        }

        if (flag)
        {
            t.color = Color.green;
        }
        else
            t.color = Color.red;
        cells.Clear();
    }
   
}
