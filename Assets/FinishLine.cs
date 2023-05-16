using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public MainPlayerController playerController;
    public float exitRad = 0.3f;

    // Update is called once per frame
    void Update()
    {
        Vector3 player = playerController.transform.position;
        float px = player.x;
        float py = player.y;
        float tx = this.transform.position.x;
        float ty = this.transform.position.y;
        if ((px - tx)*(px-tx) + (py-ty)*(py-ty) <= exitRad * exitRad)
        {
            GameObject.Find("Sounds").GetComponent<EndGame>().wonGame();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarningFormat("Collision with {0}", collision.gameObject.name);
    }
}
 