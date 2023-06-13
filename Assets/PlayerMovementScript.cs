using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsHit = false;

    private GameManagerScript gameManager;
    private float speed;

    public GameObject GameOverlayCanvas;


    //private bool IsGrounded = true;
    void Start()
    {
        gameManager = GameObject.Find("EventSystemOverlay").GetComponent<GameManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        //check if the player is above -6 y
        
        if (gameObject.transform.position.y < -6)
        {
            IsHit = true;
        }

        //check for a and d key press
        if (Input.GetKey(KeyCode.A ))
        {
            //move left
            gameObject.transform.Translate(Vector3.back * Time.deltaTime * 5f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //move right
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        }

        

    }

    //check if the Player is on someting with the tag Platform


    //check on collision
    void OnCollisionEnter(Collision collision)
    {
        //checkif the object that triggered the event has the tag platform
        if (collision.gameObject.tag == "obstacle")
        {
            //if not, exit the function
            Debug.Log("hit");
            IsHit = true;
            return;
        }
    }
}
