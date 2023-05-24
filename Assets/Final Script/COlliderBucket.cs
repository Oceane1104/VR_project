using UnityEngine;

public class COlliderBucket : MonoBehaviour
{
    // La variable pour stocker la position de l'objet A avant la collision
    private Vector3 previousPosition;
    public int nombreDeCraneDansPanier = 0;
    public AudioClip clip;
    public Transform BucketObject;
    private GameObject containerObject;

    private void Start()
    {
        containerObject = new GameObject("ContainerObject");
        containerObject.transform.SetParent(BucketObject); // Set the container as the parent

        // Set the position and rotation of the container object to match the container
        containerObject.transform.position = BucketObject.position;
        containerObject.transform.rotation = BucketObject.rotation;
    }


    void OnCollisionEnter(Collision collision)
    {
        // Si la collision implique le crane
        if (collision.gameObject.CompareTag("skull"))
        {
            // Stocker la position actuelle du crane
            previousPosition = transform.position;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Si la collision implique l'objet A
        if (collision.gameObject.CompareTag("skull"))
        {
            // Restaurer la position précédente du crane
            transform.position = previousPosition;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        

        // Vérifier si l'objet entrant dans le trigger est un crâne et le panier
        if (other.gameObject.CompareTag("GoodSkull"))
        {
            GameObject SkullObject = other.gameObject;
            SkullObject.transform.SetParent(BucketObject);
            SkullObject.transform.SetParent(containerObject.transform);
            
            SkullObject.GetComponent<Rigidbody>().isKinematic = true;
            SkullObject.GetComponent<Rigidbody>().useGravity = false;
            SkullObject.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
            // Augmenter le nombre de crânes dans le panier
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
            nombreDeCraneDansPanier++; 
            Debug.Log("Nombre de crânes dans le panier : " + nombreDeCraneDansPanier);
        }

        if (SkullObject.CompareTag("BadSkull"))
        {
            //Add time i.e 5s
        }

            if (nombreDeCraneDansPanier >= 10)
        {
            //Porte s'ouvre + musiaue de victoire
        }
    }


    //Fonction quand collision avec le sol -> isKinematic=true gravity=yes freeze rotations ?

}
