using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThemsController : MonoBehaviour
{

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
}
