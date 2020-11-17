using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountPoint : MonoBehaviour
{

    [SerializeField] float pickupDistance = 1f;
    [SerializeField] float lineWidth = .05f;
    [SerializeField] Color lineColor = Color.green;
    [SerializeField] Material lineMaterial;

    LineRenderer mountLine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpLine();

    }

    private void SetUpLine()
    {
        mountLine = GetComponent<LineRenderer>();
        if (mountLine)
        {
            if (!lineMaterial)
            {
                lineMaterial = new Material(Shader.Find("Default/Line"));
            }
            mountLine.material = lineMaterial;
            mountLine.startWidth = lineWidth;
            mountLine.startColor = lineColor;
            mountLine.endColor = lineColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawLineToClosestInteractable();
    }

    public bool HasCargo()
    {
        return transform.childCount > 0;
    }

    private void DrawLineToClosestInteractable()
    {
        if (mountLine)
        {
            GameObject closestInteractable = GetClosestInteractable();
            if (closestInteractable && !HasCargo())
            {
                mountLine.positionCount = 2;
                mountLine.SetPosition(0, transform.position);
                mountLine.SetPosition(1, closestInteractable.transform.position);
            }
            else
            {
                mountLine.positionCount = 0;
            }
        }
    }

    public GameObject GetClosestInteractable()
    {
        float closestDistance = pickupDistance + 1;
        GameObject closestInteractable = null;
        foreach (GameObject loopInteractable in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            float distance = Vector2.Distance(loopInteractable.transform.position, transform.position);
            if (distance <= pickupDistance)
            {
                if (distance <= closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = loopInteractable;
                }
            }
        }
        return closestInteractable;
    }

    public void HarvestResource(Resource resource)
    {
        Cargo newCargo =  resource.HarvestCargo().GetComponent<Cargo>();
        PickupCargo(newCargo);
    }


    public void DropCargo()
    {
        Transform cargo = transform.GetChild(0);
        cargo.position = transform.position;
        cargo.gameObject.GetComponent<Rigidbody2D>().velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
        cargo.parent = null;
        cargo.gameObject.GetComponent<Cargo>().ColliderEnabled(true);

    }

    public void PickupCargo(Cargo cargo)
    {
        if (cargo)
        {
            cargo.transform.SetParent(transform);
            cargo.ColliderEnabled(false);
        }
    }

    public Cargo GetCargo()
    {
        return GetComponentInChildren<Cargo>();
    }
}
