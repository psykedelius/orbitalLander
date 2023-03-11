using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchPad : MonoBehaviour
{
    public enum platformType { refuelPlateform, repairPlatform };
    public platformType thisPlatformType = platformType.refuelPlateform;

    public GameObject landedObject;
    public float refuelSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("No Rigidbody component found on this GameObject.");
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;

        }
        else
        {
            Debug.Log("Rigidbody component found on this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "playerShip")
        {
            landedObject = other.gameObject;
            print("launchPad.OnTriggerEnter " + other.gameObject.name);
           // other.gameObject.GetComponent<rocket>().ondestroySpaceShip();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D otherRigidbody = other.GetComponent<Rigidbody2D>();

        switch (thisPlatformType)
        {
            case platformType.refuelPlateform:
                if (otherRigidbody != null)
                {
                    rocket spaceShip = other.gameObject.GetComponent<rocket>();
                    if (spaceShip != null)
                    {
                        if (spaceShip.fuelRemain < spaceShip.fuelMax)
                        {
                            spaceShip.fuelRemain += refuelSpeed * Time.deltaTime;
                            Debug.Log("spaceShip.fuelRemain: " + spaceShip.fuelRemain);
                        }
                        else
                        {
                            spaceShip.fuelRemain = spaceShip.fuelMax;
                        }
                    }
                }
                break;


            case platformType.repairPlatform:
                if (otherRigidbody != null)
                {
                    rocket spaceShip = other.gameObject.GetComponent<rocket>();
                    if (spaceShip != null)
                    {
                        if (spaceShip.shieldRemain < spaceShip.fuelMax)
                        {
                            spaceShip.shieldRemain += refuelSpeed * Time.deltaTime;
                            Debug.Log("spaceShip.fuelRemain: " + spaceShip.shieldRemain);
                        }
                        else
                        {
                            spaceShip.shieldRemain = spaceShip.shieldMax;
                        }
                    }
                }
                break;
        }


    }
}
