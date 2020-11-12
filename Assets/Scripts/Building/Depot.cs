using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depot : MonoBehaviour
{
    [SerializeField] int inputCapacity = 2;
    [SerializeField] ResourceType inputResource = default;
    [SerializeField] GameObject outputResource = default;

    [SerializeField] int stockPile;


    // Start is called before the first frame update
    void Start()
    {
        stockPile = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        Cargo incomingCargo = (Cargo) otherCollider.gameObject.GetComponent("Cargo");
        if (incomingCargo)
        {
            if (incomingCargo.GetCargoResourceType().Equals(inputResource))
            {
                Destroy(otherCollider.gameObject);
                if (stockPile + 1 < inputCapacity)
                {
                    stockPile++;
                }
                else
                {
                    Output();
                }
            }
        }
    }

    private void Output()
    {
        stockPile = 0;
        Transform outputLocation = transform.Find("Output Location");
        GameObject outputCargo = Instantiate(outputResource, outputLocation.position, outputLocation.rotation);
    }
}
