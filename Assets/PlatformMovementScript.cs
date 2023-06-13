using UnityEngine;

public class PlatformMovementScript : MonoBehaviour
{
    //get the game manager script
    private GameManagerScript gameManager;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        //get the game manager script
        gameManager = GameObject.Find("EventSystemOverlay").GetComponent<GameManagerScript>();


    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * playerSpeed);
        speed = gameManager.PlatformSpeed;
        gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);   
    }
}
