using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Unity.VisualScripting;
using UnityEngine;

public class PointMissedTrigger : MonoBehaviour
{
    public event Action PointMissed;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            PointMissed?.Invoke();
        }
    }
}
