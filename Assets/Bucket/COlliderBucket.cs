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





    void OnTriggerEnter(Collider other)
    {
        

        // V�rifier si l'objet entrant dans le trigger est un cr�ne et le panier
        if (other.gameObject.CompareTag("Skull"))
        {
            GameObject SkullObject = other.gameObject;
            SkullObject.transform.SetParent(BucketObject);
            SkullObject.transform.SetParent(containerObject.transform);
            
            SkullObject.GetComponent<Rigidbody>().isKinematic = true;
            SkullObject.GetComponent<Rigidbody>().useGravity = false;
            SkullObject.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
            // Augmenter le nombre de cr�nes dans le panier
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
            nombreDeCraneDansPanier++; 
            Debug.LogWarningFormat("Nombre de cr�nes dans le panier :{0}",nombreDeCraneDansPanier);
        }

        if (other.gameObject.CompareTag("BadSkull"))
        {
            GameObject BadSkullObject = other.gameObject;
            //Add time i.e 5s
        }

            
    }


    //Fonction quand collision avec le sol -> isKinematic=true gravity=yes freeze rotations ?

}
