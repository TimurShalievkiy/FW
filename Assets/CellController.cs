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
        Debug.Log("Exit");
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].GetComponent<Image>().color = Color.white;
            res += cells[i].transform.GetChild(0).GetComponent<Text>().text;
        }
        CheckAnsver(res);
        cells.Clear();
    }
    public void PointerUp()
{
        PointerExit();

}

    public void CheckAnsver(string str)
    {
        t.text = str;
    }
   
}
