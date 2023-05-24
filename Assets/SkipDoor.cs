using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// if vase is grabbed => skip door
// remove 3 minutes from timer

public class SkipDoor : MonoBehaviour
{
    public Transform door; // door to skip 
    float sub = -60 * 3f;
    void Update()
    {
        if (transform.parent != null && GetComponentInParent<HandController>() != null && door != null)
        {
            // is being held! => wanna skip
            Debug.Log("Destroying the door!");
            Destroy(door.gameObject);
            door = null;
            GameObject.Find("Sounds").GetComponent<GameMusic>().addTime(sub); // subtract 3 mins
            Destroy(gameObject);
        }
    }
}
