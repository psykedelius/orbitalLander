using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceShipCam : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -15);
        transform.position = newPos;
    }
}
