using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class Villager : MonoBehaviour
{
    //config params
    [SerializeField] float walkSpeed = 1f;
    [SerializeField] float interactionDelay = 1f;

    [Header("Debug Only")]
    [SerializeField] ResourceType resourceType;

    //references
    Animator animator;
    MountPoint hand;

    //state variables
    float walkDirection;
    bool isWalking;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hand = GetComponentInChildren<MountPoint>();
        walkDirection = 1;
        StopWalking();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            Walk();
        }
    }

    private void ChooseWalkDirection()
    {
        float closest;
        if (hand.HasCargo())
        {
            //walk towards Depot
            closest = FindClosestDropoff();
        }
        else
        {
            //walk torwards resource
            closest = FindClosestPickup();
        }

        transform.localScale = new Vector2(Mathf.Sign(closest), transform.localScale.y);
        walkDirection = Mathf.Sign(closest);
    }

    private float FindClosestPickup()
    {

        float closest = FindClosestResource();

        foreach (Depot depot in FindObjectsOfType<Depot>())
        {
            if (depot.GetOutputType() == resourceType)
            {
                if (Mathf.Abs(depot.transform.position.x - transform.position.x) < Mathf.Abs(closest))
                {
                    closest = depot.transform.position.x - transform.position.x;
                }
            }
        }


        return closest;
    }

    private float FindClosestResource()
    {
        float closest = 100f;
        foreach (Resource resource in FindObjectsOfType<Resource>())
        {
            if (resource.GetResourceType() == resourceType)
            {
                if (Mathf.Abs(resource.transform.position.x - transform.position.x) < Mathf.Abs(closest))
                {
                    closest = resource.transform.position.x - transform.position.x;
                }
            }
                
        }
        return closest;
    }

    private float FindClosestDropoff()
    {
        float closest = 100f;
        foreach (Depot depot in FindObjectsOfType<Depot>())
        {
            if (depot.GetInputType() == hand.GetComponentInChildren<Cargo>().GetCargoResourceType())
            {
                if (Mathf.Abs(depot.transform.position.x - transform.position.x) < Mathf.Abs(closest))
                {
                    closest = depot.transform.position.x - transform.position.x;
                }
            }
        }

        return closest;
    }

    private void Walk()
    {
        float newXPosition = transform.position.x + (walkSpeed * Time.deltaTime * walkDirection);
        Vector2 newPosition = new Vector2(newXPosition, transform.position.y);
        transform.position = newPosition;
        
    }

    private void StopWalking()
    {
        isWalking = false;
        animator.SetBool("isWalking", false);
    }

    private void ResumeWalking()
    {
        isWalking = true;
        animator.SetBool("isWalking", true);
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {

        if (otherCollider.gameObject.tag != "Ground")
        {
            if (otherCollider.gameObject.tag == "Interactable")
            {
                ResourceInteraction(otherCollider.gameObject.GetComponent<Resource>());
                CargoInteraction(otherCollider.gameObject.GetComponent<Cargo>());
            }

            if (otherCollider.gameObject.layer == LayerMask.NameToLayer("Depot"))
            {
                DepotInteraction(otherCollider.gameObject.GetComponent<Depot>());
            }

            ChooseWalkDirection();
        }
    }

    private void DepotInteraction(Depot depot)
    {
        if (depot)
        {
            if (GetComponentInChildren<MountPoint>().HasCargo())
            {
                Cargo carriedCargo = GetComponentInChildren<MountPoint>().GetCargo();
                if (depot.GetInputType().Equals(carriedCargo.GetCargoResourceType()))
                {
                    DepositCargo(depot);
                }
            }
            else
            {
                Debug.Log("setting resource type: " + depot.GetOutputType());
                resourceType = depot.GetOutputType();
                StopWalking();
                transform.position = depot.GetOutputLocation().position;
            }
        }
    }

    private void CargoInteraction(Cargo cargo)
    {
        if (cargo)
        {
            PickUpCargo(cargo);
            ResumeWalking();
        }
    }

    private void ResourceInteraction(Resource resource)
    {
        if (resource)
        {
            HarvestResource(resource);
            resourceType = resource.GetResourceType();
            ResumeWalking();
        }
    }

    private void DepositCargo(Depot depot)
    {
        depot.DepositCargo(hand.GetCargo());
    }

    private void HarvestResource(Resource resource)
    {
        if (!GetComponentInChildren<MountPoint>().HasCargo())
        {
            GetComponentInChildren<MountPoint>().HarvestResource(resource);
        }
    }

    private void PickUpCargo(Cargo cargo)
    {
        if (!GetComponentInChildren<MountPoint>().HasCargo())
        {
            GetComponentInChildren<MountPoint>().PickupCargo(cargo);
        }
    }

}
