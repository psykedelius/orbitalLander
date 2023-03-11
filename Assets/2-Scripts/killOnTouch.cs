using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class killOnTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if ()
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "playerShip")
        {
            print("killOnTouch.OnTriggerEnter " + other.gameObject.name);
            other.gameObject.GetComponent<rocket>().ondestroySpaceShip();
        }

        // check if the rocket has collided with the planet
        // if (other.transform == planet)
        //  {
        //      Debug.Log("Landed!");
        //  }
    }

}
