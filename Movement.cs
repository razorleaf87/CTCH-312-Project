using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls Widget's movement
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    //Movement Speed can be modifiable in Unity
    public float movementSpeed = 1.0f;
    public new Rigidbody2D rigidbody;
    public Vector2 movementDirection;

    // Start is called before the first frame update

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        //Gets input from wasd and arrow keys to decide direction of movement
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        //Moves Widget's rigidbody according to the movement direction * speed variable
        rigidbody.velocity = movementDirection * movementSpeed;
    }
}
