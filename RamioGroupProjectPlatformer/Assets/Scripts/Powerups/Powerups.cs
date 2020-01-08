using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerups : MonoBehaviour
{
    //script attatches to player
    public float paraGrav = 0.5f;
    public int regularGrav = 2; //set to whatever gravity scale is
    int jumpCount;
    public int jumpMax = 2;
    int bootUses;
    public int maxBootUses = 3;
    int bootUseDisplay;
    bool paraActive = false;
    bool bootActive = false;
    bool paraUsed = false;
    public Text powerupText;

    void Start()
    {
        powerupText.text = " ";
        bootUseDisplay = maxBootUses;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && paraActive)
        {
            Paraglider();
        }

        if (Input.GetButton("Jump") && !PlatformerMovement.grounded && bootActive)
        {
            JumpBoots();
        }

        if (paraActive && PlatformerMovement.grounded && paraUsed)
        {
            GetComponent<Rigidbody2D>().gravityScale = regularGrav;
            paraActive = false;
            paraUsed = false;
            powerupText.text = " ";
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Paraup") //tag paragliders as Paraup
        {
            Destroy(collision.gameObject);
            paraActive = true;
            powerupText.text = "This paraglider looks fragile... It will break after once use. Press Enter while in the air to use it.";
        }

        if(collision.gameObject.tag == "Bootup") //tag double jump boots as Bootup
        {
            Destroy(collision.gameObject);
            bootActive = true;
            JumpBoots();
            powerupText.text = "Jump boots! You can double jump with them" + bootUseDisplay + " more times!";
        }

        if (collision.gameObject.tag == "Paraup" && bootActive) //tag paragliders as Paraup
        {
            bootActive = false;
            bootUseDisplay = maxBootUses;
            Destroy(collision.gameObject);
            paraActive = true;
            powerupText.text = "This paraglider looks fragile... It will break after once use. Press Enter while in the air to use it.";
        }

        if (collision.gameObject.tag == "Bootup") //tag double jump boots as Bootup
        {
            paraActive = false;
            GetComponent<Rigidbody2D>().gravityScale = regularGrav;
            Destroy(collision.gameObject);
            bootActive = true;
            JumpBoots();
            powerupText.text = "Jump boots! You can double jump with them " + bootUseDisplay + " more times!";
        }
    }

    void Paraglider()
    {
        if (!PlatformerMovement.grounded)
        {
            GetComponent<Rigidbody2D>().gravityScale = paraGrav;
            paraUsed = true;
        }
    }

    void JumpBoots() //jump boots is NOT working rn, will fix later
    {
        if(Input.GetButtonDown("Jump") && jumpCount < jumpMax && bootUses < maxBootUses)
        {
            jumpCount++;
            bootUses++;
            bootUseDisplay--;
            powerupText.text = "Jump boots! You can double jump with them" + bootUseDisplay + " more times!";
            Jump();
        }
        else
        {
            bootActive = false;
            bootUseDisplay = maxBootUses;
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100 * PlatformerMovement.jumpSpeed));
    }
}
