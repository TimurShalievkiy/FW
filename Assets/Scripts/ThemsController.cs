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

    public static void SetValuesOfProgresInThemes()
    {
        CheckCountUnusedWordInTheme(DictionaryController.Topic.Animals);
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
    static void CheckCountUnusedWordInTheme(DictionaryController.Topic topic)
    {
        switch (topic)
        {
            case DictionaryController.Topic.Animals:
                int allWordsCount = DictionaryController.GetCountWordsInTopic(DictionaryController.Topic.Animals);
                int wordsWithMoreThenZeroCalls = DictionaryController.GetCountOfUusedWord(DictionaryController.Topic.Animals);

                //GameObject.Find("Animals").transform.Find("Count").GetComponent<Text>().text = (wordsWithMoreThenZeroCalls + " / " + allWordsCount );
                Debug.Log("all = " + allWordsCount + " - " + wordsWithMoreThenZeroCalls + " = " + (allWordsCount - wordsWithMoreThenZeroCalls));
                break;
        }
    }
     void CheckCountUnusedWordInTheme2(DictionaryController.Topic topic)
    {
        switch (topic)
        {
            case DictionaryController.Topic.Animals:
                int allWordsCount = DictionaryController.GetCountWordsInTopic(DictionaryController.Topic.Animals);
                int wordsWithMoreThenZeroCalls = DictionaryController.GetCountOfUusedWord(DictionaryController.Topic.Animals);

                GameObject.Find("Animals").transform.Find("Count").GetComponent<Text>().text = (wordsWithMoreThenZeroCalls + " / " + allWordsCount );
                Debug.Log("all = " + allWordsCount + " - " + wordsWithMoreThenZeroCalls + " = " + (allWordsCount - wordsWithMoreThenZeroCalls));
                break;
        }
    }
}
