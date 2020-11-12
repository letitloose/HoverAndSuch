using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    [SerializeField] ResourceType cargoResourceType = default;

    public ResourceType GetCargoResourceType()
    {
        return cargoResourceType;
    }

    private void Update()
    {
        if (transform.parent)
        {
            MoveWithParent();
        }
    }

    private void MoveWithParent()
    {
        float step = 10f; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, step);
    }

    
}

public enum ResourceType
{
    food,
    water,
    villager,
    stone
}