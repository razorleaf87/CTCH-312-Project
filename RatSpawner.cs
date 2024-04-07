using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Spawns the rats according to the week number. Week 1 spawns 1 rat, week 2 spawns 2 rats, etc.
public class RatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rat;
    public GameObject globalController;


    
    // Start is called before the first frame update
    void Start()
    {
        globalController = GameObject.Find("GlobalController");
        InvokeRepeating("MakeRats", 0, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeRats()
    {
        //Week 1 spawns only in spawner 1
        if (globalController.GetComponent<GlobalController>().week == 1)
        {
            if (gameObject.CompareTag("spawner1"))
            {
                GameObject newRat = Instantiate(rat, gameObject.transform);
            }
           
                
        }
        //Week 2 spawns in spawner 1 and 2
        else if (globalController.GetComponent<GlobalController>().week == 2)
        {
            if (gameObject.CompareTag("spawner1"))
            {
                GameObject newRat = Instantiate(rat, gameObject.transform);
            }
            if (gameObject.CompareTag("spawner2"))
            {
                GameObject newRat = Instantiate(rat, gameObject.transform);
            }
        }
        //Week 3 spawns in all spawners
        else if (globalController.GetComponent<GlobalController>().week == 3)
        {
            if (gameObject.CompareTag("spawner1"))
            {
                GameObject newRat = Instantiate(rat, gameObject.transform);
            }
            if (gameObject.CompareTag("spawner2"))
            {
                GameObject newRat = Instantiate(rat, gameObject.transform);
            }
            if (gameObject.CompareTag("spawner3"))
            {
                GameObject newRat = Instantiate(rat, gameObject.transform);
            }
        }
    }
}
