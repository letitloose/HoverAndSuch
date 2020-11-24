using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunks : MonoBehaviour
{

    [SerializeField] GameObject chunkPrefab = default;
    [SerializeField] Color chunkColor = Color.green;
    [SerializeField] int maxChunks = 6;
    [SerializeField] float minVelocity = -1f;
    [SerializeField] float maxVelocity = 1f;
    [SerializeField] float minLife = 1f;
    [SerializeField] float maxLife = 5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateChunks());
        Destroy(gameObject, maxLife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CreateChunks()
    {
        for(int i = 0; i < maxChunks; i++)
        {
            GameObject chunk = Instantiate(chunkPrefab, transform.position, transform.rotation);
            chunk.GetComponent<SpriteRenderer>().color = chunkColor;

            Rigidbody2D chunkBody = chunk.GetComponent<Rigidbody2D>();
            if (chunkBody)
            {
                Vector2 chunkVelocity = new Vector2(Random.Range(minVelocity, maxVelocity), Random.Range(minVelocity, maxVelocity));
                chunkBody.velocity = chunkVelocity;
            }

            Destroy(chunk, Random.Range(minLife, maxLife));
            yield return null;
        }
    }
}
