using UnityEngine;

public class SkullCounter : MonoBehaviour
{
    public int nombreDeCraneDansPanier = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sphere") && gameObject.CompareTag("Bucket"))
        {
            nombreDeCraneDansPanier++;
            Debug.Log("Nombre de crânes dans le panier : " + nombreDeCraneDansPanier);
        }
    }

}