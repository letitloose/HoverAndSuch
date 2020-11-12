using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{

    //config params
    [SerializeField] Vector2 parallaxRate = new Vector2(.5f, .1f);
    [SerializeField] CinemachineVirtualCamera shipCamera = default;

    //refs

    //state vars
    Vector3 lastCameraPosition;
    float spriteUnitSizeX;

    // Start is called before the first frame update
    void Start()
    {
        lastCameraPosition = shipCamera.transform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        spriteUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = shipCamera.transform.position - lastCameraPosition;
        transform.position += new Vector3(delta.x * parallaxRate.x, delta.y * parallaxRate.y, 0);
        lastCameraPosition = shipCamera.transform.position;

        if(Mathf.Abs(shipCamera.transform.position.x - transform.position.x) >= spriteUnitSizeX)
        {
            transform.position = new Vector2(shipCamera.transform.position.x, transform.position.y);

        }
    }
}
