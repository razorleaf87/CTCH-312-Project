using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for entering the exit zone of the map, will result in the player leaving with all scrap in tact
public class Exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Widget"))
        {
            FindObjectOfType<ScavengingGameManager>().Win();

        }
        
    }

}
