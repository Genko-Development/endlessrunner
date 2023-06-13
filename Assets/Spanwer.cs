using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanwer : MonoBehaviour
{
    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //debug.log ("hello world"); when the gameobject is triggererd
    void OnTriggerEnter(Collider other)
    {
        //check if the object that triggered the event has the tag platform
        if (other.gameObject.tag != "Platform")
        {
            //if not, exit the function
            Destroy(other.gameObject);
            return;
        }
        // Get a random index from the prefabs array
        int randomIndex = Random.Range(0, prefabs.Length);
        // Get the prefab from the array using the random index
        GameObject randomPrefab = prefabs[randomIndex];
        // Spawn the game object at 0,0,0
        GameObject newObject = Instantiate(randomPrefab, new Vector3(-57.5f, 0, 0), Quaternion.identity);
        // Set script active on the new object
        newObject.GetComponent<PlatformMovementScript>().enabled = true;
        // Destroy the object that triggered the event
        Destroy(other.gameObject);
    }
}
