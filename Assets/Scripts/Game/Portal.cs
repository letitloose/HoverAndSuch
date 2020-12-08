using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.GetComponent<ShipMovement>()) 
        {
            Debug.Log("the ship touched me!");
            SceneManager.LoadScene("Win Screen");
        }
    }
}
