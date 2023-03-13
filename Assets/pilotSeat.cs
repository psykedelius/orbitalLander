using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pilotSeat : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject helperUI;
    public GameObject player;
    public GameObject drivenDevice;
    public Camera targetCam;
    public Camera interiorCam;
    void Start()
    {
        helperUI.SetActive(false);
    }

    private void Update()
    {
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                takeSeat();
            }
        }
    }
    void takeSeat()
    {
        //     playerController _playerController = other.gameObject.GetComponent<playerController>();
            print("takeSeat");
            drivenDevice.SendMessage("toggleEngine", true);
            player.GetComponent<playerController>().canMove = false;
            player.GetComponent<Rigidbody2D>().Sleep();
            targetCam.enabled = true;
            interiorCam.enabled = false;
    }

    public void exitSeat()
    {
        print("exitSeat");
        player.GetComponent<playerController>().canMove = true;
        player.GetComponent<Rigidbody2D>().WakeUp();
        drivenDevice.SendMessage("toggleEngine", false);
        targetCam.enabled = false;
        interiorCam.enabled = true;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("OnTriggerEnter2D");
        playerController _playerController = other.gameObject.GetComponent<playerController>();
        if (_playerController != null)
        {
            print("playerController "+ _playerController.name);
            player = other.gameObject;
            helperUI.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        playerController _playerController = other.gameObject.GetComponent<playerController>();
        if (_playerController != null)
        {
            print("OnTriggerExit2D");
            if (player != null && other.gameObject == player)
            {
                helperUI.SetActive(false);
                player = null;
            }
        }
    }

}
