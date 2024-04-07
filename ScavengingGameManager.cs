using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//The Gamemanager that handles the Scavenging minigame

public class ScavengingGameManager : MonoBehaviour
{
    public static ScavengingGameManager Instance;
    public GameObject thePlayer;
    public GameObject globalController;
    
    [SerializeField] private Transform scrap;
    //Text fields that can be edited
    [SerializeField] private TMP_Text tier1ScoreText;
    [SerializeField] private TMP_Text tier2ScoreText;
    [SerializeField] private TMP_Text tier3ScoreText;
    //Visual representations of the hearts on the hud
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    //Scrap Scores initialized to 0
    private int tier1Score = 0;
    private int tier2Score = 0;
    private int tier3Score = 0;
    
    //Invulnerability variables for keeping player invulnerable after taking damage. Time can be modified
    private bool invulnerable = false;
    private float invulnerabilityTime = 3.0f;

    //Lives initialized to 0
    public int lives = 3;

    public int Tier1Score => tier1Score;
    public int Tier2Score => tier2Score;
    public int Tier3Score => tier3Score;

    //rewards screen
    public GameObject rewardScreen;
    public Text scrapRewards;
    public Text expRewards;

    void Awake()
    {
        globalController = GameObject.Find("GlobalController");
        thePlayer = GameObject.Find("Player");
        rewardScreen.SetActive(false);
        globalController.GetComponent<GlobalController>().day++;
    }

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }


    //Sets default values for score and lives at the start of each scene load in
    private void NewGame()
    {
        SetScore(0,0,0);
        SetLives(3);
    }

    //Used for when scrap is picked up to add to score while removing item
    public void ScrapPickUp(Scrap scrap) {
        scrap.gameObject.SetActive(false);
        SetScore(tier1Score + scrap.tier1Points, tier2Score + scrap.tier2Points, tier3Score + scrap.tier3Points);
    }
    
    //Setter for score, updates text field UI
    private void SetScore(int tier1Score, int tier2Score, int tier3Score)
    {
        this.tier1Score = tier1Score;
        tier1ScoreText.text = tier1Score.ToString().PadLeft(2, '0');
        this.tier2Score = tier2Score;
        tier2ScoreText.text = tier2Score.ToString().PadLeft(2, '0');
        this.tier3Score = tier3Score;
        tier3ScoreText.text = tier3Score.ToString().PadLeft(2, '0');
    }

    //Setter for lives, updates the heart UIs
    private void SetLives(int lives)
    {
        this.lives = lives;
        if (lives == 3)
        {
            heart1.gameObject.GetComponentInChildren<Renderer>().enabled = true;
            heart2.gameObject.GetComponentInChildren<Renderer>().enabled = true;
            heart3.gameObject.GetComponentInChildren<Renderer>().enabled = true;
        }
        else if (lives == 2)
        {
            heart1.gameObject.GetComponentInChildren<Renderer>().enabled = true;
            heart2.gameObject.GetComponentInChildren<Renderer>().enabled = true;
            heart3.gameObject.GetComponentInChildren<Renderer>().enabled = false;
        }
        else if (lives == 1)
        {
            heart1.gameObject.GetComponentInChildren<Renderer>().enabled = true;
            heart2.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            heart3.gameObject.GetComponentInChildren<Renderer>().enabled = false;
        }
        else
        {
            heart1.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            heart2.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            heart3.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            //If out of lives, runs GameOver function
            GameOver();
        }
    }

    //Damage function, determines if player is invulnerable then will do damage
    public void Damage()
    {
        if (!invulnerable)
        {
            SetLives(lives - 1);
            Invincibility();

        }
        
    }

    //Enumerator to keep track of time for Invulnerability window
    private IEnumerator Invincibility()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        invulnerable = false;
    }

    //Used when player enters the win state by entering the exit. Meant to bring up a pop up with the final score, and add the score to global variables
    public void Win()
    {
        //Insert code for a popup displaying score
        StartCoroutine("rewards");
    }

    //Used when player runs out of lives, sets all scores to half, so player only gets half the scrap they received in the level
    public void GameOver()
    {
        tier1Score = tier1Score / 2;
        tier2Score = tier2Score / 2;
        tier3Score = tier3Score / 2;
        //Insert code for a popup displaying score
        StartCoroutine("rewards");
    }

    IEnumerator rewards()
    {
        //gain rewards
        thePlayer.GetComponent<Player>().tier1Scrap += tier1Score;
        thePlayer.GetComponent<Player>().tier2Scrap += tier2Score;
        thePlayer.GetComponent<Player>().tier3Scrap += tier3Score;

        // display rewards
        rewardScreen.SetActive(true);
        scrapRewards.text = tier1Score + "\n\n" + tier2Score + "\n\n" + tier3Score;

        yield return new WaitForSeconds(4f);


        // return to menu
        SceneManager.LoadScene("Menu");
    }
}

