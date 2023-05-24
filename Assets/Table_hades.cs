using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_hades : MonoBehaviour
{
    private COlliderBucket bucket;
    public Door_script DoorHades;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ColliderBucket"))
        {
            if (bucket.nombreDeCraneDansPanier >= 5)
            {
                bool door_open = DoorHades.open_the_door();
                Debug.LogWarning("Challenge Hades succedeed");
            }
        }
    }
        
}
