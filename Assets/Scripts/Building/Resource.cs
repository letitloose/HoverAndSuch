using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    [SerializeField] GameObject cargoPrefab = default;
    [SerializeField] Vector2 harvestOffset = new Vector2(1f, 1f);

    public ResourceType GetResourceType()
    {
        return cargoPrefab.GetComponent<Cargo>().GetCargoResourceType();
    }

    public GameObject HarvestCargo()
    {;

        Vector2 harvestedPosition = new Vector2(transform.position.x + harvestOffset.x, transform.position.y + harvestOffset.y);
        GameObject harvestedCargo = Instantiate(cargoPrefab, harvestedPosition, transform.rotation);
        return harvestedCargo;

    }
}

