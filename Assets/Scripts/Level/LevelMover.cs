using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    [SerializeField] private float _floorSpeed; 
    [SerializeField] private float _speedIncreaseDelta = 0.15f;
    void Update()
    {
        transform.Translate(-Time.deltaTime * _floorSpeed, 0, 0);
    }
    
    public void IncreaseSpeed()
    {
        _floorSpeed += _speedIncreaseDelta;
    }
}
