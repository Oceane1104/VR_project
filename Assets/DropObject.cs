using UnityEngine;
using System.Collections;

public class DropObject : MonoBehaviour
{

    public GameObject objectToDrop; // L'objet que nous voulons l�cher
    public float dropHeight; // Hauteur de l'endroit o� nous voulons l�cher l'objet
    public float dropInterval; // Intervalle entre chaque l�cher d'objet en secondes
    public float spawnAreaSize;
    private float timeSinceLastDrop = 0f; // Temps �coul� depuis le dernier l�cher d'objet
    private GameObject lastDroppedObject; // Stockage de la derni�re instance de l'objet largu�

    void Update()
    {
        timeSinceLastDrop += Time.deltaTime;

        if (timeSinceLastDrop >= dropInterval)
        { // Si l'intervalle est �coul�
            timeSinceLastDrop = 0f; // Remettre le compteur � z�ro
            SpawnObject(); // L�cher l'objet
        }
    }

    void SpawnObject()
    {


        // Calculer la position de largage de l'objet
        Vector3 dropPosition = new Vector3(Random.Range(transform.position.x - spawnAreaSize, transform.position.x + spawnAreaSize), dropHeight, Random.Range(transform.position.z - spawnAreaSize, transform.position.z + spawnAreaSize));

        // Instancier l'objet � la position calcul�e
        lastDroppedObject = Instantiate(objectToDrop, dropPosition, Quaternion.identity);
    }
}