using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : Photon.MonoBehaviour {
    public GameObject playerCam, bulletPrefab;
    public Transform whereToShootFrom;
    private float moveSpeed = 14f;
    private bool dead = false;
    public bool left = false, right = false;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    public AudioClip pistolSound;
    private AudioSource aSource;

	void Awake () {
		if (photonView.isMine)
        {
            GameObject sceneCam = GameObject.FindGameObjectWithTag("MainCamera");
            playerCam.SetActive(true);
            sceneCam.SetActive(false);
            GameObject.FindGameObjectWithTag("PlayerControls").GetComponent<playerButtons>().pControl = this;  
        } else
        {
            
        }
        aSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		if (photonView.isMine && !dead)
        {
            CheckMovement();
        }
	}

    void CheckMovement()
    {
        //var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        //transform.position += move * moveSpeed * Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Vector3 playerScale = transform.localScale;
        //    playerScale.x = Mathf.Abs(playerScale.x);
        //    transform.localScale = playerScale;
        //    photonView.RPC("lookRight", PhotonTargets.Others);
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Vector3 playerScale = transform.localScale;
        //    playerScale.x = -Mathf.Abs(playerScale.x);
        //    transform.localScale = playerScale;
        //    photonView.RPC("lookLeft", PhotonTargets.Others);
        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, whereToShootFrom.position, Quaternion.identity, 0) as GameObject;
        //    if (transform.localScale.x < 0)
        //    {
        //        bullet.GetComponent<Bullet>().movingRight = false;
        //    }
        //}      

        int movement = 0;
        if (left)
        {
            movement -= 1;
        }  
        if (right)
        {
            movement += 1;
        }
        if (movement > 0)
        {
            Vector3 playerScale = transform.localScale;
            playerScale.x = Mathf.Abs(playerScale.x);
            transform.localScale = playerScale;
            photonView.RPC("lookRight", PhotonTargets.Others);
        }
        if (movement < 0)
        {
            Vector3 playerScale = transform.localScale;
            playerScale.x = -Mathf.Abs(playerScale.x);
            transform.localScale = playerScale;
            photonView.RPC("lookLeft", PhotonTargets.Others);
        }
        //if (Mathf.Abs(movement) > 0)
        //{
            Vector2 vel = rb.velocity;
            vel.x = movement * moveSpeed;
            rb.velocity = vel;
        //}
        
    }

    public void Shoot ()
    {
        if (!dead && photonView.isMine)
        {
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, whereToShootFrom.position, Quaternion.identity, 0) as GameObject;
            if (transform.localScale.x < 0)
            {
                bullet.GetComponent<Bullet>().switchTheDir(); 
            }
            photonView.RPC("bulletSound", PhotonTargets.All);
        }
        
    }

    [PunRPC]
    public void bulletSound ()
    {
        aSource.PlayOneShot(pistolSound);
    }

    //[PunRPC]
    //public void ChangeBullDir (Bullet bull)
    //{
    //    bull.movingRight = false;
    //} 

    public void Jump ()
    {
        if (!dead && isGrounded)
        {
            //isGrounded = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 16), ForceMode2D.Impulse);
        }
    }

    [PunRPC]
    private void lookRight ()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x = Mathf.Abs(playerScale.x);
        transform.localScale = playerScale;
    }

    [PunRPC]
    private void lookLeft()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x = -Mathf.Abs(playerScale.x);
        transform.localScale = playerScale;
    }

    [PunRPC]
    private void BulletHit ()
    {
        Vector3 playerRot = transform.localEulerAngles;
        if (transform.localScale.x > 0)
        {
            playerRot.z = 90;
        }
        else
        {
            playerRot.z = -90;
        }
        transform.localEulerAngles = playerRot;
        Invoke("Respawn", 2f);
    }

    private void Respawn ()
    {
        transform.position = Vector3.zero;
        Vector3 playerRot = transform.localEulerAngles;
        playerRot.z = 0;
        transform.localEulerAngles = playerRot;
        dead = false;
        if (photonView.isMine)
        {
            playerCam.transform.parent = transform;
            Vector3 camPos = playerCam.transform.position;
            camPos.y = 0f;
            camPos.x = 0f;
            playerCam.transform.position = camPos;
        }
    }

    public void BulletEncounter ()
    {
        if (photonView.isMine)
        {
            photonView.RPC("BulletHit", PhotonTargets.All);
            dead = true;
            //Vector3 camRot = playerCam.transform.localEulerAngles;
            //if (transform.localScale.x > 0)
            //{
            //    camRot.z = -90;
            //}
            //else
            //{
            //    camRot.z = 90;
            //}
            //Debug.Log(camRot);
            //playerCam.transform.localEulerAngles = camRot;
            playerCam.transform.parent = GameObject.FindGameObjectWithTag("CamParent").transform;
            Vector3 camRot = playerCam.transform.localEulerAngles;
            if (transform.localScale.x > 0)
            {
                camRot.z = 0;
            }
            else
            {
                camRot.z = 0;
            }
            playerCam.transform.localEulerAngles = camRot;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor" || collision.tag == "Player")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
