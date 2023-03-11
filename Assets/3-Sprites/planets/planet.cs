using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet : MonoBehaviour
{
   
    public List<GameObject> landingPads = new List<GameObject>();
    public List<landingPad> landingPadList = new List<landingPad>();
    public GameObject ownerID ;
    public Collider2D planetCollider;
    public Vector2 impulseForce;

    [System.Serializable]
    public class landingPad 
    {
        public GameObject PadObject;
        public int ownerID = -1;
        public bool isActive = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
        foreach (GameObject landingPad in landingPads)
        {
            landingPad padObject = new landingPad();
            padObject.PadObject  = landingPad;
            padObject.isActive   = false;
            landingPadList.Add(padObject);
            circleToFlat(5, landingPad);
           // if (landingPad.GetComponent<Collider2D>.)
        }
        FindObjectsOfType<CanvasRenderer>();

        //apply inputForce
        GetComponent<Rigidbody2D>().AddForce(impulseForce, ForceMode2D.Impulse);


        // landingPad pads = transform.get
    }

    public void updateLandingPad(landingPad padToUpdate, int ownerID ,bool isPadActive)
    {
        //print("updateLandingPad " + padToUpdate.ownerID);

        foreach (landingPad Pad in landingPadList)
        {
            
            if (Pad.PadObject == padToUpdate.PadObject)
            {
                //print("landingPad Pad " + Pad.PadObject.name + " landed by " + ownerID);
                Pad.isActive = isPadActive;
                Pad.ownerID = ownerID;
                
            }
        }

        int PlaneteOwner = -1;
        foreach (landingPad Pad in landingPadList)
        {
            if (Pad.ownerID == ownerID)
            {
                PlaneteOwner = ownerID;
               // print("updateLandingPad " + Pad.ownerID);
            }else 
            { PlaneteOwner = -1;
                break; }
        }
    }

    public void circleToFlat (float planetRadius, GameObject landedObject )
    {
        Vector2 objectToPlanetVector =  landedObject.transform.position- Vector3.up ;
        float objectToPlanetAngle = Vector2.Angle(Vector2.up, objectToPlanetVector); ;
        float circlePerimeter = 2 * 3.14f * planetRadius;
        float polarDistance = 2 * 3.14f + planetRadius * (objectToPlanetAngle / 360);
        print("circlePerimeter = " + circlePerimeter + " polarDistance =" + polarDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
