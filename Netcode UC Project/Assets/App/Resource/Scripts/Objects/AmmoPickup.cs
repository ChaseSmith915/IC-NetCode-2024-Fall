using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace App.Resource.Scripts.Obj
{
    public class AmmoPickup : NetworkBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("g");
            if(other.gameObject.tag == "Player")
            {

                Debug.Log("e");
                other.gameObject.GetComponent<BulletSpawner>().Ammo.Value++;

                Destroy(gameObject);
            }
        }
    }
}

