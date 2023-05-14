using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform player;
    public float offsetX = 100f;
    public float offsetY = 100f;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + player.forward * offsetX + player.up * offsetY;
        rectTransform.position = Camera.main.WorldToScreenPoint(targetPosition);
    }
}
