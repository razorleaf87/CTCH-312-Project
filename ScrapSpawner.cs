using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

//Spawner brings in scrap into the scrap layer on the grid. If you want to add more or remove some, you can paint and remove it
// by going into the tile pallette after selecting the scrap grid and selecting the leftmost bottom blank tile under the tilemap,
// and click the paintbrush or eraser respectively to paint. Unfortunately they are all invisible until you play the game
// 
public class ScrapSpawner : MonoBehaviour
{
    //The six types of scrap objects stored in Unity object
    [SerializeField] private GameObject tier1ScrapType1;
    [SerializeField] private GameObject tier1ScrapType2;
    [SerializeField] private GameObject tier2ScrapType1;
    [SerializeField] private GameObject tier2ScrapType2;
    [SerializeField] private GameObject tier3ScrapType1;
    [SerializeField] private GameObject tier3ScrapType2;

    //Random numbers to decide which scrap is chosen
    private int randomNumber1 = 0;
    private int randomNumber2 = 0;
    //Replace week variable with global week variable
    private int week = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Spawns all scrap on startup and never again. Can be modified to constantly spawn scrap after a certain length of time
        InvokeRepeating("MakeScrap", 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeScrap()
    {
        //Determines Scrap Tier
        randomNumber1 = Random.Range(0, 100);
        //Determines Scrap type
        randomNumber2 = Random.Range(0, 2);
        //Week 1
        if (week == 1)
        {
            //If the random number is low it will spawn tier 3 scrap
            if (randomNumber1 >= 0 && randomNumber1 <= 5)
            {
                //Depending on the random number between 0 and 1, it will spawn either a gear or screw
                if (randomNumber2 == 0)
                {
                    GameObject newScrap = Instantiate(tier3ScrapType1, gameObject.transform);
                }
                else if (randomNumber2 == 1)
                {
                    GameObject newScrap = Instantiate(tier3ScrapType2, gameObject.transform);
                }

            }
            //Second lowest spawns tier 2 scrap
            else if (randomNumber1 > 5 && randomNumber1 <= 15)
            {
                if (randomNumber2 == 0)
                {
                    GameObject newScrap = Instantiate(tier2ScrapType1, gameObject.transform);
                }
                else if (randomNumber2 == 1)
                {
                    GameObject newScrap = Instantiate(tier2ScrapType2, gameObject.transform);
                }
            }
            //Remaining chance toward tier 1 scrap. Similar logic for all weeks
            else if (randomNumber1 > 15)
            {
                if (randomNumber2 == 0)
                {
                    GameObject newScrap = Instantiate(tier1ScrapType1, gameObject.transform);
                }
                else if (randomNumber2 == 1)
                {
                    GameObject newScrap = Instantiate(tier1ScrapType2, gameObject.transform);
                }
            }
        }
        //Week 2
        else if (week == 2)
        {
            if (randomNumber1 >= 0 && randomNumber1 <= 5)
            {
                if (randomNumber2 == 0)
                {
                    GameObject newScrap = Instantiate(tier1ScrapType1, gameObject.transform);
                }
                else if (randomNumber2 == 1)
                {
                    GameObject newScrap = Instantiate(tier1ScrapType2, gameObject.transform);
                }

            }
            else if (randomNumber1 > 5 && randomNumber1 <= 15)
            {
                if (randomNumber2 == 0)
                {
                    GameObject newScrap = Instantiate(tier3ScrapType1, gameObject.transform);
                }
                else if (randomNumber2 == 1)
                {
                    GameObject newScrap = Instantiate(tier3ScrapType2, gameObject.transform);
                }
            }
            else if (randomNumber1 > 15)
            {
                if (randomNumber2 == 0)
                {
                    GameObject newScrap = Instantiate(tier2ScrapType1, gameObject.transform);
                }
                else if (randomNumber2 == 1)
                {
                    GameObject newScrap = Instantiate(tier2ScrapType2, gameObject.transform);
                }
            }
        }
        //Week 3
        else if (week == 3)
        {
            if (randomNumber1 >= 0 && randomNumber1 <= 5)
            {
                if (randomNumber2 == 0)
                {
                    GameObject newScrap = Instantiate(tier2ScrapType1, gameObject.transform);
                }
                else if (randomNumber2 == 1)
                {
                    GameObject newScrap = Instantiate(tier2ScrapType2, gameObject.transform);
                }

            }
            else if (randomNumber1 > 5)
            {
                if (randomNumber2 == 0)
                {
                    GameObject newScrap = Instantiate(tier3ScrapType1, gameObject.transform);
                }
                else if (randomNumber2 == 1)
                {
                    GameObject newScrap = Instantiate(tier3ScrapType2, gameObject.transform);
                }
            }
        }
    }

}
