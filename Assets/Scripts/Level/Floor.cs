using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public event Action<Floor> PlayerPassedCurrentFloor;
    
    private BoxCollider2D _boxCollider;
    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public Vector2 GetSize()
    {
        return _boxCollider.size;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            PlayerPassedCurrentFloor?.Invoke(this);
        }
    }
}
