using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private void Awake()
    {
        player = GameManager.instance.player.transform;
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -100);
    }
}
