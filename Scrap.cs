using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for Scrap, Scrap values can be edited in their prefabs in Unity
[RequireComponent(typeof(Collider2D))]
public class Scrap : MonoBehaviour
{
    public int tier1Points;
    public int tier2Points;
    public int tier3Points;

    //Uses Pickup to transfer gameObject ID to GameManager and modifies while taking in score
    protected void PickUp()
    {
        FindObjectOfType<ScavengingGameManager>().ScrapPickUp(this);

    }

    //Trigger collider for when Widget runs into scrap
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Widget"))
        {
            PickUp();
        }
    }
}
