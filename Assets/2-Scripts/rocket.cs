using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class rocket : MonoBehaviour
{
    public bool hasPilot = false;
    gameManager _gameManager;
    public bool isGrounded = false;
    public bool isDestroyed = false;
    public float gravity; // strength of the gravitational force

    public GameObject parentObject;
    private Vector3 prevPosition;
    private Vector3 prevRotation;
    private bool isParented = false;

    public float thrustForce; // strength of the rocket's thrust
    public float thrustSpeed = 5f;
    public float thrustValue = 0f;
    public float fuelMax = 15;
    public float fuelRemain = 15;
    public TMP_Text fuelRemainUI;
    public float shieldMax = 15;
    public float shieldRemain = 15;
    public TMP_Text shieldRemainUI;

    public float rotationSpeed; // speed of the rocket's rotation
    public ParticleSystem plumeFx;
    private Rigidbody2D rb; // reference to the rocket's rigidbody component
    public TMP_Text ImpactForce;
    public float impactTolerance = 2;
    float flipDelay = 0.5f;
    float coolDown = 0;
    // Use this for initialization
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType <gameManager> ();
        rb = GetComponent<Rigidbody2D>();
    }

    public void toggleEngine(bool isToggled)
    {
        hasPilot = isToggled;
    }
    // Update is called once per frame
    void Update()
    {
        coolDown += Time.deltaTime;

        // rotate the rocket based on the input
        float rotationInput = Input.GetAxis("Horizontal");
        if ( !isDestroyed && !isGrounded && hasPilot)
        {
            rb.rotation -= rotationInput * rotationSpeed * Time.deltaTime;
        }

        // apply the rocket's thrust based on the input
        float thrustInput = Input.GetAxis("Vertical");

        //thrusters on
        if (thrustInput > 0 && thrustInput > thrustValue && fuelRemain>0 && !isDestroyed && hasPilot)
        {
            isGrounded   = false;
            var emission = plumeFx.emission;
            emission.rateOverTime = 40;
            thrustValue += thrustSpeed * Time.deltaTime;
            fuelRemain  -= thrustValue * Time.deltaTime;
            if (fuelRemain < 0) { fuelRemain = 0; }
        }
        //backflip on
        else if (thrustInput < 0 && coolDown> flipDelay && !isGrounded && hasPilot)
        {
            Vector3 globalRotation = transform.eulerAngles - Vector3.up;
            Vector3 flipTarget = new Vector3(globalRotation.x, globalRotation.y, globalRotation.z+180);
            transform.DORotate(flipTarget, 0.25f);
            coolDown = 0;
        }
        //thrusters off
        else
        {
            var emission = plumeFx.emission;
            emission.rateOverTime = 0;
            plumeFx.emissionRate = 0;
            if (thrustValue>0)
            {
                thrustValue -= thrustSpeed * Time.deltaTime;
            }
            else { thrustValue = 0; }
        }
        rb.AddRelativeForce(Vector2.up * thrustValue* thrustForce * Time.deltaTime);
        updateUI();
    }

    void updateUI()
    {
        fuelRemainUI.text   = fuelRemain + "";
        shieldRemainUI.text = shieldRemain + "";
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        parentObject = collision.transform.gameObject;
        Vector2 velocity = collision.relativeVelocity;
        ImpactForce.text = velocity.sqrMagnitude.ToString();
        if (velocity.sqrMagnitude > impactTolerance)
        {
            shieldRemain -= velocity.sqrMagnitude - impactTolerance;
        }
        if (shieldRemain <= 0)
        {
            ondestroySpaceShip();
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
        Vector2 velocity = collision.relativeVelocity;
        if (velocity.sqrMagnitude < 0.1f) { isGrounded = true; }
        //Debug.Log("velocity.sqrMagnitude ! " + velocity.sqrMagnitude);
    }

    public void onSpawnSpaceShip()
    {
        print("Respawn!");
        fuelRemain   = fuelMax;
        shieldRemain = shieldMax;
        isDestroyed  = false;
        transform.position = new Vector3(0, 8.5f, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _gameManager.onRestart();
    }
    public void ondestroySpaceShip()
    {
        print("BOOM!");
        isDestroyed = true;
        Invoke("onSpawnSpaceShip", 1);
    }

    void FixedUpdate()
    {

        // Check distance between player and object1
        float distance = Vector3.Distance(transform.position, parentObject.transform.position);

            // If distance is greater than threshold and player is still parented to object1
            if (isGrounded && !isParented)
            {
                // Unparent player from object1
                transform.parent = parentObject.transform;
                isParented = true;

                // Apply global momentum and torque rotation
                Vector3 velocity = (transform.position - prevPosition) / Time.deltaTime;
                rb.velocity = velocity;
                rb.angularVelocity = (transform.eulerAngles.z - prevRotation.z) / Time.deltaTime;
            }
            else if (!isGrounded && isParented)
            {
                // Unparent player from object1
                transform.parent = null;
                isParented = false;

                // Apply global momentum and torque rotation
                Vector3 velocity = (transform.position - prevPosition) / Time.deltaTime;
                rb.velocity = velocity;
                rb.angularVelocity = (transform.eulerAngles.z - prevRotation.z) / Time.deltaTime;
            }

        // Update previous position and rotation for momentum and torque calculation
        prevPosition = transform.position;
        prevRotation = transform.eulerAngles;
        
    }
}
