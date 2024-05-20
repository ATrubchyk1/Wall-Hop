using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class ObstaclePassedTrigger : MonoBehaviour
{
    public event Action PlayerPassedObstacle;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            PlayerPassedObstacle?.Invoke();
        }
    }
}
