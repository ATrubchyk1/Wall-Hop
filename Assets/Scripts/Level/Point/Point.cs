using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Point : MonoBehaviour
{
    public event Action<Point> PointCollected;
    
    [SerializeField] private PointMissedTrigger _pointMissedTrigger;

    public int Reward { get; set; }

    public event Action<Point> PointMissed;

    private void Start()
    {
        _pointMissedTrigger.PointMissed += OnPointMissed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            PointCollected?.Invoke(this);
        }
    }

    private void OnPointMissed()
    {
        PointMissed?.Invoke(this);
    }

    private void OnDestroy()
    {
        _pointMissedTrigger.PointMissed -= OnPointMissed;
    }
}
