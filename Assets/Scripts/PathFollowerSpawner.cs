using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PathFollowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject followerPrefab;
    [SerializeField] private List<Transform> path;
    [SerializeField] private float cooldown;
    private float currentCooldown;
    
    private void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown > 0) return;
        currentCooldown = cooldown;
        SpawnFollower();
    }

    private void SpawnFollower()
    {
        var follower = Instantiate(followerPrefab, transform.position, quaternion.identity);
        var component = follower.GetComponent<PathFollower>();
        component.path.AddRange(path);
    }
}
