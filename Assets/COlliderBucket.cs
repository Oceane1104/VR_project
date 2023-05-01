using UnityEngine;

public class COlliderBucket : MonoBehaviour
{
    // La variable pour stocker la position de l'objet A avant la collision
    private Vector3 previousPosition;
    private int nombreDeCraneDansPanier = 0;

    void OnCollisionEnter(Collision collision)
    {
        // Si la collision implique le crane
        if (collision.gameObject.CompareTag("Sphere"))
        {
            // Stocker la position actuelle du crane
            previousPosition = transform.position;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Si la collision implique l'objet A
        if (collision.gameObject.CompareTag("Sphere"))
        {
            // Restaurer la position pr�c�dente du crane
            transform.position = previousPosition;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // V�rifier si l'objet entrant dans le trigger est un cr�ne et le panier
        if (other.gameObject.CompareTag("Sphere") && gameObject.CompareTag("Bucket"))
        {
            // Augmenter le nombre de cr�nes dans le panier
            nombreDeCraneDansPanier++; 
            Debug.Log("Nombre de cr�nes dans le panier : " + nombreDeCraneDansPanier);
        }
    }

    /*public int GetNombreDeCraneDansPanier()
    {
        Debug.Log("Nombre de cr�nes dans le panier : " + nombreDeCraneDansPanier);
        //return nombreDeCraneDansPanier;
    }*/

}
