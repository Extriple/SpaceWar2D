using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSorting : MonoBehaviour
{
    [SerializeField] private bool runOnces;
    [SerializeField] private float positionOffsetY;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    private void LateUpdate()
    {
        float precisionMultiplier = 5f;
        _spriteRenderer.sortingOrder = (int) ((-transform.position.y * positionOffsetY) * precisionMultiplier);

        if (runOnces)
        {
            Destroy(this);
        }
    }
}
