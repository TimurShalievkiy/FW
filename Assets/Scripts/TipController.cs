using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipController : MonoBehaviour
{
    public CellController cellController;
    public Text tipTextView;
    public int countOfTips = 500;
    public List<int> word;
    public bool wordIsDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        tipTextView.text = countOfTips.ToString();
    }
    public void ResetTip()
    {
        for (int i = 0; i < cellController.CellGrid.transform.childCount; i++)
        {
            cellController.CellGrid.transform.GetChild(i).transform.GetComponent<Cell>().isTipCell = false;
        }
        wordIsDone = false;
        word = null;
    }
    public void AddTipToWord()
    {
 
        string res = "";
        if (countOfTips > 0)
        {

            if (word == null)
            {
                word = cellController.GetNextWordForTip();
                //Debug.Log("is null");
            }
            if (word.Count == 0)
            {
                word = cellController.GetNextWordForTip();
              //  Debug.Log("count == 0");
            }
            //Debug.Log("count = "+ word.Count);

            for (int i = 0; i < word.Count; i++)
            {
                res += cellController.CellGrid.transform.GetChild(word[i]).transform.GetChild(0).GetComponent<Text>().text;
                if (!cellController.CellGrid.transform.GetChild(word[i]).transform.GetComponent<Cell>().isTipCell)
                {
                    cellController.CellGrid.transform.GetChild(word[i]).transform.GetComponent<Cell>().isTipCell = true;
                    cellController.CellGrid.transform.GetChild(word[i]).transform.GetComponent<Image>().color = CollorManager.tipColor;
                    countOfTips--;
                   // Debug.Log(word[i] + " ");
                    if (i == word.Count - 1)
                    {
                        Debug.Log(res);
                        wordIsDone = true; 
                        cellController.CheckAnsver(res);
                        word = null;
                    }
                                      
                    break;
                }
            }
      
        }
        else
        {

        }
       
    }
    public bool CheckWordIsCurret(List<int> listIndex)
    {
        if (word == null)
            return false;
        for (int i = 0; i < listIndex.Count; i++)
        {
            if (!word.Exists(x => x == listIndex[i]))
            {
                return false;
            }            
        }
        return true;
    }
    void LoadTipData()
    { }
}
