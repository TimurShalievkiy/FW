using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFieldCreator : MonoBehaviour
{
    public GameObject CellGreed;
    public GameObject Cell;

    public static int difficulty = 0;


    private void Start()
    {
       // CreateGameField();
    }
    int GetDifficultyByTheme(DictionaryController.Topic topic)
    {
        if (PlayerPrefs.HasKey(DictionaryController.currentTopic.ToString() + "difficulty"))
        {
            difficulty = int.Parse(PlayerPrefs.GetString(DictionaryController.currentTopic.ToString() + "difficulty"));
        }
        else
        {
            SaveDifficulty();
        }

 
        return difficulty;
    }
    void SaveDifficulty()
    {
        PlayerPrefs.SetString(DictionaryController.currentTopic.ToString() + "difficulty", difficulty.ToString());
        PlayerPrefs.Save();
    }
    public void IncrementDifficulty()
    {
        //Debug.Log("increment difficulty = " + difficulty);
        difficulty++;
        SaveDifficulty();
    }
    public void CreateGameField()
    {

        //if (difficulty > 6)
        //{
        //    difficulty = 1;          
        //}
        difficulty = GetDifficultyByTheme(DictionaryController.currentTopic);

        DestroyChildren();
        switch (difficulty)
        {
            case 0:
                AddCellToField(2, 2);
                break;
            case 1:       
                AddCellToField(2, 2);
                break;
            case 2:
                AddCellToField(2, 2);               
                break;
            case 3:
                AddCellToField(3, 3);                
                break;
            case 4:
                AddCellToField(3, 3);
                break;
            case 5:
                AddCellToField(3, 3);
                break;
            case 6:
                AddCellToField(4, 4);
                break;
            case 7:
                AddCellToField(4, 4);
                break;
            case 8:
                AddCellToField(4, 4);
                break;
            case 9:
                AddCellToField(5, 5);
                break;
            case 10:
                AddCellToField(5, 5);
                break;
            case 11:
                AddCellToField(5, 5);
                break;
            case 12:
                AddCellToField(5, 6);
                break;
            case 13:
                AddCellToField(5, 6);
                break;
            case 14:
                AddCellToField(5, 6);
                break;
            case 15:
                AddCellToField(5, 7);
                break;
            default:
                AddCellToField(5, 7);
                Debug.Log("difficulty = " + difficulty);
                difficulty--;
                break;
        }
    }
    void AddCellToField(int x, int y)
    {
       

        if (x > 5)
            x = 5;
        if (x < 2)
            x = 2;
        if (y > 7)
            y = 7;
        if (y < 2)
            y = 2;

        if (x * y > CellGreed.transform.childCount)
        {
          

            CellGreed.GetComponent<GridLayoutGroup>().constraintCount = x;

          int countCell = (x * y) - CellGreed.transform.childCount;


        for (int i = 0; i < countCell; i++)
            {
                GameObject newCell = GameObject.Instantiate(Cell);
                newCell.transform.parent = CellGreed.transform;
            }

            SetCellSize();

        }
    }

    public void DestroyChildren()
    {
        for (var i = CellGreed.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(CellGreed.transform.GetChild(i).gameObject);
        }
        CellGreed.transform.DetachChildren();
    }

    public void SetCellSize()
    {
        float x = (CellGreed.transform.GetComponent<LayoutElement>().preferredWidth / CellGreed.GetComponent<GridLayoutGroup>().constraintCount) - CellGreed.transform.GetComponent<GridLayoutGroup>().spacing.x;
        CellGreed.GetComponent<GridLayoutGroup>().cellSize = new Vector2(x, x);
    }
    
}
