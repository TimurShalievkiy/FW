using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThemsController : MonoBehaviour
{
    public Slider slider;
    private void Start()
    {
        SetValuesOfProgresInThemes2();
    }
    private void Update()
    {
        
    }
    public static float SetValuesOfProgresInThemes()
    {
       return CheckCountUnusedWordInTheme(DictionaryController.currentTopic);
    }
    public void SetValuesOfProgresInThemes2()
    {
        CheckCountUnusedWordInTheme2();
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
            case 2:
                DictionaryController.currentTopic = DictionaryController.Topic.Vegetables;

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
                    if (GameFieldCreator.difficulty == 0)
                    {
                        num = 0;
                    }
                    else
                    {
                        float percent = (allWordsCount - allWordsCount * 0.1f) / 100;
                        num = wordsWithMoreThenZeroCalls / percent;
                    }
     
                    return (float)System.Math.Round(num, 2);
                }
                    break;
        }
        return 0;
    }
     void CheckCountUnusedWordInTheme2()
    {
        
        SetCount(DictionaryController.Topic.Animals);
        SetCount(DictionaryController.Topic.Vegetables);
        //switch (topic)
        //{
        //    case DictionaryController.Topic.Animals:
        //        SetCount(topic)
        //            break;
        //}
    }
    void SetCount(DictionaryController.Topic topic)
    {
        int allWordsCount = DictionaryController.GetCountWordsInTopic(topic);

        int wordsWithMoreThenZeroCalls = DictionaryController.GetCountOfUnusedWord2(topic);
       // Debug.Log(topic + " all count = " + allWordsCount + " used = " + wordsWithMoreThenZeroCalls);
        if ((allWordsCount - allWordsCount * 0.1) - wordsWithMoreThenZeroCalls <= 0)
        {
            GameObject.Find(topic.ToString()).transform.Find("Count").GetComponent<Text>().text = "100%";
            slider.value = 100;
        }
        else
        {
            float num = 0;
            if (wordsWithMoreThenZeroCalls == 0)
                num = 0;
            else
            {
                float percent = (allWordsCount - allWordsCount * 0.1f) / 100;
                num = wordsWithMoreThenZeroCalls / percent;
            }

            GameObject.Find(topic.ToString()).transform.Find("Count").GetComponent<Text>().text = System.Math.Round(num, 2) + "%";
            slider.value = (float)System.Math.Round(num, 2);
        }
    }
}
