using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GlobalController : MonoBehaviour
{
    public bool bossFight; // track if its a boss fight
    public int bossNumber; // track what boss your on
    public GameObject[] boss, enemyList; //list of all bosses in order (4), list of random enemies to fight
    public GameObject thePlayer;
    public static GlobalController Instance;
    public HUD playerHUD;
    public HUD enemyHUD;
    public int story = 0; // tracks story progress
    public int week = 1; //tracks week
    public int day = 1; //tracks day of the week
    public bool tutorial;

    public void Awake()
    {
        bossFight = false;
        tutorial = false;
        bossNumber = 0; 
        thePlayer = GameObject.Find("Player");
        DontDestroyOnLoad(thePlayer); //this is so player doesnt die between scenes;
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null)
        {
            Destroy(gameObject);    //if we already have a global controller then we do not make another
        }
        else
        {
            Instance = this;    //if we are first creating the world put this as the instance
        }
    }

    public void LoadBattleScene() 
    {
        SceneManager.LoadScene("BattleScene");
    }


    public void BossFight()
    {
        bossFight = true;
        LoadBattleScene();
    }

    public void LoadVisualNovel()
    {
        SceneManager.LoadScene("VisualNovel");
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadScavenging()
    {
        SceneManager.LoadScene("ScavengingMinigame");
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void LoadWorkshop()
    {
        SceneManager.LoadScene("Workshop");
    }

}
