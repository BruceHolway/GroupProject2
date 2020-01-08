using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    //script attatches to player
    public float paraGrav = 0.5f;
    public int regularGrav = 2;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Paraup")
        {
            Destroy(collision.gameObject);
            Paraglider();
        }

        if(collision.gameObject.tag == "Bootup")
        {
            Destroy(collision.gameObject);
            JumpBoots();
        }
    }

    void Paraglider()
    {
        GetComponent<Rigidbody2D>().gravityScale = paraGrav;
        if(PlatformerMovement.grounded == true)
        {
            GetComponent<Rigidbody2D>().gravityScale = regularGrav;
        }
    }

    void JumpBoots()
    {

    }
}
