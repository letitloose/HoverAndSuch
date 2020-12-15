using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depot : MonoBehaviour
{
    [SerializeField] int inputCapacity = 2;
    [SerializeField] ResourceType inputResource = default;
    [SerializeField] GameObject outputResource = default;

    [SerializeField] int stockPile;

    public int GetInputCapacity()
    {
        return inputCapacity;
    }

    public int GetCurrentStockpile()
    {
        return stockPile;
    }

    // Start is called before the first frame update
    void Start()
    {
        stockPile = 0;
    }
    public ResourceType GetInputType()
    {
        return inputResource;
    }

    public ResourceType GetOutputType()
    {
        return outputResource.GetComponent<Cargo>().GetCargoResourceType();
    }

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        Cargo incomingCargo = (Cargo) otherCollider.gameObject.GetComponent("Cargo");
        if (incomingCargo)
        {
            DepositCargo(incomingCargo);
        }
    }

    public void DepositCargo(Cargo incomingCargo)
    {
        incomingCargo.transform.parent = null;
        if (incomingCargo.GetCargoResourceType().Equals(inputResource))
        {
            Destroy(incomingCargo.gameObject);
            if (stockPile + 1 < inputCapacity)
            {
                stockPile++;
            }
            else
            {
                Output();
            }
        }

        GetComponentInChildren<DepotUI>()?.UpdateDepotText();

    }

    private void Output()
    {
        stockPile = 0;
        Transform outputLocation = GetOutputLocation();
        GameObject outputCargo = Instantiate(outputResource, outputLocation.position, outputLocation.rotation);
    }

    public Transform GetOutputLocation()
    {
        return transform.Find("Output Location");
    }
}
