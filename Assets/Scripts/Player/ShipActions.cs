using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipActions : MonoBehaviour
{

    MountPoint shipMountPoint;
    // Start is called before the first frame update
    void Start()
    {
        shipMountPoint = GetComponentInChildren<MountPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (shipMountPoint.HasCargo())
            {
                shipMountPoint.DropCargo();
            }
            else
            {
                GameObject interactable = shipMountPoint.GetClosestInteractable();

                if (interactable)
                {
                    Cargo objectIsCargo = interactable.GetComponent<Cargo>();
                    if (objectIsCargo)
                    {
                        shipMountPoint.PickupCargo(objectIsCargo);
                    }
                    Resource objectIsResource = interactable.GetComponent<Resource>();
                    if (objectIsResource)
                    {
                        shipMountPoint.HarvestResource(objectIsResource);
                    }
                }
            }
        }
    }
}
