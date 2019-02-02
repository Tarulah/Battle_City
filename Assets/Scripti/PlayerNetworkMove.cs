using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNetworkMove : Photon.MonoBehaviour {

    public Camera myCam;
    public GameObject bullet;

    private ShotScript shot;

    // Update is called once per frame
    void Update () {
	    if(photonView.isMine)
        {
            GetComponent<PlayerHealth>().enabled = true;
            GetComponent<PlayerMove>().enabled = true;
            GetComponent<WeaponScript>().enabled = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<PlayerAnimationMove>().enabled = true;
            GetComponent<PhotonPositionSync>().enabled = true;
            myCam.enabled = true;
        }
	    else
	    {
            GetComponent<PlayerHealth>().enabled = false;
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<WeaponScript>().enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<PlayerAnimationMove>().enabled = false;
            GetComponent<PhotonPositionSync>().enabled = false;
            myCam.enabled = false;
        }
	    ammoJuttu();
	}

    void ammoJuttu()
    {
        if (photonView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetMouseButtonDown(0))
            {
                PhotonView Photonview = this.photonView;
                string owner = Photonview.owner.ToString();
                Photonview.RPC("CreateAmmo", PhotonTargets.Others, owner);
            }
        }
    } 

    [PunRPC]
    void CreateAmmo(string owner)
    {
        Debug.Log("Tehdään muissa pelaajissa lokaali ammus. Omistaja: " + owner);

        MoveScript move = bullet.gameObject.GetComponent<MoveScript>();
        WeaponScript weapon = bullet.gameObject.GetComponent<WeaponScript>();

        if (move != null)
        {
            move.direction = this.transform.up; // towards in 2D space is the right of the sprite

            // Tässä kohtaa tehdään muissa pelaajissa ammus
            GameObject clone = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        }
    }
}
