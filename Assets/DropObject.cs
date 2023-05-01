using UnityEngine;
using System.Collections;

public class DropObject : MonoBehaviour
{

    public GameObject objectToDrop; // L'objet que nous voulons lâcher
    public float dropHeight; // Hauteur de l'endroit où nous voulons lâcher l'objet
    public float dropInterval; // Intervalle entre chaque lâcher d'objet en secondes
    public float spawnAreaSize;
    private float timeSinceLastDrop = 0f; // Temps écoulé depuis le dernier lâcher d'objet
    private GameObject lastDroppedObject; // Stockage de la dernière instance de l'objet largué

    void Update()
    {
        timeSinceLastDrop += Time.deltaTime;

        if (timeSinceLastDrop >= dropInterval)
        { // Si l'intervalle est écoulé
            timeSinceLastDrop = 0f; // Remettre le compteur à zéro
            SpawnObject(); // Lâcher l'objet
        }
    }

    void SpawnObject()
    {


        // Calculer la position de largage de l'objet
        Vector3 dropPosition = new Vector3(Random.Range(transform.position.x - spawnAreaSize, transform.position.x + spawnAreaSize), dropHeight, Random.Range(transform.position.z - spawnAreaSize, transform.position.z + spawnAreaSize));

        // Instancier l'objet à la position calculée
        lastDroppedObject = Instantiate(objectToDrop, dropPosition, Quaternion.identity);
    }
}