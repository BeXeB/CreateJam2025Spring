using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public float speed = 5;
    public float timeAtStop = 0;
    public List<Transform> path = new ();
    public bool looping = false;
    private int currentGoalIndex = 0;
    private float cooldown = 0;
    public bool destroyAtEnd = true;
    private bool reachedEnd = false;

    private void FixedUpdate()
    {
        if (cooldown > 0 || reachedEnd) return;
        var goal = path[currentGoalIndex].position;
        var direction = (goal - transform.position).normalized;
        transform.up = direction;
        transform.position += direction * (speed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, goal) < 0.1f)
        {
            cooldown = timeAtStop;
            GoalReached();
        }
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
    }

    private void GoalReached()
    {
        currentGoalIndex++;
        if (currentGoalIndex < path.Count) return;
        if (looping)
        {
            currentGoalIndex = 0;
        }
        else if (destroyAtEnd)
        {
            Destroy(gameObject);
        }
        reachedEnd = true;
    }
}
