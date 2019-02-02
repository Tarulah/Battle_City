using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkHealth : Photon.MonoBehaviour {

    public float currentHealth;
    //public bool isEnemyShot = true;

    public void OnTriggerEnter(Collider other)
    {
        PhotonView Photonview = this.photonView;
        Debug.Log("Pelaaja osui johonkin.");

        if (other.gameObject.name == "Bullet(Clone)")
        {
            Debug.Log(("Otettiin osuma ammuksesta."));

            MoveScript ammo = other.GetComponent<MoveScript>();

            //string owner = ammo.owner;
            string owner = GetComponent<PhotonView>().owner.name;

            if (GetComponent<PhotonView>() != null)
            {
                Debug.Log("Kappaleessa on photonview.");

                if (Photonview.owner != null)
                {
                    Debug.Log("Kappaleella on omistaja, eli se ei kuulu Scenelle.");

                    if (owner != photonView.owner.ToString())
                    {
                        Debug.Log("Ollaan osuttu johonkin toiseen kappaleeseen.");
                        AddDamage(3);
                    }
                    else
                    {
                        Debug.Log("Ollaan osuttu itseen.");
                        // Ei tehdä mitään = ei anneta vahinkoa
                    }
                }
                else
                {
                    //Debug.Log("Osuttu kappale kuuluu Scenelle, koska owner on null.");
                    AddDamage(2);
                }
            }
            else
            {
                //Debug.Log("Ei photonviewtä, eli osui staattiseen objektiin.");
                AddDamage(1);
            }
        }
    }

    void AddDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (GetComponent<PhotonView>() == null)
        {
            // Tuhotaan staattinen kappale
            Destroy(gameObject);
        }
        else
        {
            // Tällä kappaleella on photonview, eli synkranoitu kaikkien pelaajien kanssa
            if (photonView.isMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
