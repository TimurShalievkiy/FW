using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThemsController : MonoBehaviour
{
    
    private void Start()
    {
        SetValuesOfProgresInThemes2();
    }

    public static float SetValuesOfProgresInThemes()
    {
       return CheckCountUnusedWordInTheme(DictionaryController.currentTopic);
    }
    public void SetValuesOfProgresInThemes2()
    {
        CheckCountUnusedWordInTheme2(DictionaryController.Topic.Animals);
    }

    public void SetTheme(int index)
    {

        switch (index)
        {
            case 0:

                break;
            case 1:
                DictionaryController.currentTopic = DictionaryController.Topic.Animals;

                Camera.main.GetComponent<ScenesController>().GoToScene(ScenesController.Scenes.mainGame);
                //GameObject.Find("Main Camera").GetComponent<ScenesController>().GoToScene(ScenesController.Scenes.mainGame);

                break;
        }
    }
    static float CheckCountUnusedWordInTheme(DictionaryController.Topic topic)
    {
        switch (topic)
        {
            case DictionaryController.Topic.Animals:

                int allWordsCount = DictionaryController.GetCountWordsInTopic(DictionaryController.Topic.Animals);
                int wordsWithMoreThenZeroCalls = DictionaryController.GetCountOfUnusedWord(DictionaryController.Topic.Animals);

                if ((allWordsCount - allWordsCount * 0.1) - wordsWithMoreThenZeroCalls <= 0)
                {
                    Debug.Log("Done");
                    return 100;
                }
                else
                {
                    float num = 0;
                    //Debug.Log("GameFieldCreator.difficulty = " + GameFieldCreator.difficulty);
                    if (GameFieldCreator.difficulty == 0)
                    {
                        num = 0;
                       // Debug.Log(0000000000000000000000000);
                    }
                    else
                    {
                        float percent = (allWordsCount - allWordsCount * 0.1f) / 100;
                        num = wordsWithMoreThenZeroCalls / percent;
                    }
                   // Debug.Log(wordsWithMoreThenZeroCalls);
                   // Debug.Log(System.Math.Round(num, 3) + "%");
     
                    return (float)System.Math.Round(num, 3);
                }
                    break;
        }
        return 0;
    }
     void CheckCountUnusedWordInTheme2(DictionaryController.Topic topic)
    {
        switch (topic)
        {
            case DictionaryController.Topic.Animals:
                int allWordsCount = DictionaryController.GetCountWordsInTopic(DictionaryController.Topic.Animals);
               
                int wordsWithMoreThenZeroCalls = DictionaryController.GetCountOfUnusedWord2(DictionaryController.Topic.Animals);

                if ((allWordsCount - allWordsCount * 0.1) - wordsWithMoreThenZeroCalls <= 0)
                {
                    GameObject.Find("Animals").transform.Find("Count").GetComponent<Text>().text = "100%";
                    Debug.Log("Done");
                }
                else
                {
                    //Debug.Log("all = " + (allWordsCount - allWordsCount * 0.1) + " - " + wordsWithMoreThenZeroCalls + " = " + ((int)(allWordsCount - allWordsCount * 0.1) - wordsWithMoreThenZeroCalls));
                    float num = 0;
                    if (wordsWithMoreThenZeroCalls == 0)
                        num = 0;
                    else
                    {
                        float percent = (allWordsCount - allWordsCount * 0.1f) / 100;
                        num = wordsWithMoreThenZeroCalls / percent;
                    }
                    //Debug.Log(System.Math.Round(num, 2) + "%");
                    GameObject.Find("Animals").transform.Find("Count").GetComponent<Text>().text = System.Math.Round(num,2) + "%";
                   // Debug.Log("all = " + allWordsCount + " - " + wordsWithMoreThenZeroCalls + " = " + ((int)(allWordsCount - allWordsCount * 0.1) - wordsWithMoreThenZeroCalls));
                }
                    break;
        }
    }
}
