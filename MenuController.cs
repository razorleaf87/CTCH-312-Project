using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject globalController;
    public Text time;

    public void Awake()
    {
        globalController = GameObject.Find("GlobalController");
        


    }
    private void Update()
    {
        time.text = "Week " + globalController.GetComponent<GlobalController>().week + ": Day " + globalController.GetComponent<GlobalController>().day;

        // if its end of the week go to Visual Novel before boss fight
        if (globalController.GetComponent<GlobalController>().day == 7)
        {
            globalController.GetComponent<GlobalController>().week++;
            globalController.GetComponent<GlobalController>().day = 1;
            globalController.GetComponent<GlobalController>().story++;
            globalController.GetComponent<GlobalController>().LoadVisualNovel();
        }
        
    }

    public void OnTrain()
    {
        globalController.GetComponent<GlobalController>().LoadBattleScene();
    }

    public void OnScavenge()
    {
        globalController.GetComponent<GlobalController>().LoadScavenging();
    }

    public void OnWorkshop()
    {
        globalController.GetComponent<GlobalController>().LoadWorkshop();
    }

    public void OnShop()
    {
        globalController.GetComponent<GlobalController>().LoadShop();
    }

    public void OnRest()
    {
        globalController.GetComponent<GlobalController>().day++;

    }
}
