using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Type { WEAPON, FRAME, LEGS, BACK };

public class Item : MonoBehaviour
{
    public Sprite itemImageFront;
    public Sprite itemImageBack;
    public Sprite costImage;
    public int price;
    public int tier;
    public Type itemType;
}
