using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Photon.MonoBehaviour {

    public bool movingRight = true;
    //public GameObject playerOwner;
    float speed = 30f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight)
        {
            Vector3 bulletPos = transform.position;
            bulletPos.x += speed * Time.deltaTime;
            transform.position = bulletPos;
        } else
        {
            Vector3 bulletPos = transform.position;
            bulletPos.x -= speed * Time.deltaTime;
            transform.position = bulletPos;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //photonView.RPC("DestroyTheBullet", PhotonTargets.All);
            
            collision.gameObject.GetComponent<playerControls>().BulletEncounter();
        }
        DestroyTheBullet();
    }

    //[PunRPC]
    private void DestroyTheBullet ()
    {
        Destroy(gameObject);
    }

    [PunRPC] 
    public void switchDir ()
    {
        movingRight = false;
    }

    public void switchTheDir ()
    {
        photonView.RPC("switchDir", PhotonTargets.AllViaServer);
    }
}
