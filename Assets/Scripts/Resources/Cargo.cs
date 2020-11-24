using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{

    //config params
    [SerializeField] ResourceType cargoResourceType = default;
    [SerializeField] float breakVelocityThreshold = 5f;
    [SerializeField] ParticleSystem destroyParticlePrefab = default;
    [SerializeField] Chunks destroyChunks = default;

    //reference vars
    Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (transform.parent)
        {
            MoveWithParent();
        }
    }

    public ResourceType GetCargoResourceType()
    {
        return cargoResourceType;
    }

    private void MoveWithParent()
    {
        float step = 10f; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, step);
    }

    public void ColliderEnabled(bool enabled)
    {
        GetComponent<BoxCollider2D>().enabled = enabled;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > breakVelocityThreshold)
        {
            ChunkCargo();
            DestroyCargo();
            Destroy(gameObject);
        }
    }

    private void DestroyCargo()
    {
        if(!destroyParticlePrefab) { return; }
        ParticleSystem destroyParticles = Instantiate(destroyParticlePrefab, transform.position, transform.rotation);
        Destroy(destroyParticles.gameObject, destroyParticles.main.startLifetime.constantMax);
    }

    private void ChunkCargo()
    {
        if (!destroyChunks) { return; }
        Instantiate(destroyChunks, transform.position, transform.rotation);
    }
}

public enum ResourceType
{
    none,
    food,
    water,
    villager,
    stone,
    refinedStone
}