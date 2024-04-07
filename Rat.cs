using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    //widget gameObject for following
    public GameObject widget;
    //Modifiable speed, ideally less than Widget's so player can outrun easily
    public float speed = 0.7f;
    public Rigidbody2D rb;
    public Vector2 movementDirection;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        //Get rigidbody component of current rat and widget game object via tag
        widget = GameObject.FindWithTag("Widget");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        //Movement direction calculated by distance between widget and rat
        movementDirection = new Vector2((widget.transform.position.x - transform.position.x), (widget.transform.position.y - transform.position.y));
    }

    private void FixedUpdate()
    {
        //Rat moves toward Widget, no AI to avoid walls, can get trapped
        rb.velocity = movementDirection * speed;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //On collision with Widget will enter the damage function in ScavengingGameManager
        if (collision.gameObject.layer == LayerMask.NameToLayer("Widget"))
        {
            FindObjectOfType<ScavengingGameManager>().Damage();
        }

    }
}
