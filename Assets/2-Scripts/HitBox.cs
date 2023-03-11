using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    GameObject parent;
    rocket playerRocket;

    private void Start()
    {
        parent = transform.parent.gameObject;
        playerRocket = parent.GetComponent<rocket>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 velocity = collision.relativeVelocity;
        playerRocket.ImpactForce.text = velocity.sqrMagnitude.ToString();
        if (velocity.sqrMagnitude > playerRocket.impactTolerance)
        {
            playerRocket.shieldRemain -= velocity.sqrMagnitude*2 - playerRocket.impactTolerance;
        }

        if (playerRocket.shieldRemain<0)
        { playerRocket.ondestroySpaceShip(); }
    }
}
