using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using Unity.Netcode;

namespace Assets.App.Scripts.Player
{
    public class PlayerMovement : NetworkBehaviour
=======

namespace Assets.App.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
>>>>>>> c5a9ab3552d92cf8ee81e36e3a2477d05d40579f
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
<<<<<<< HEAD
            if(IsOwner)
            {
                Vector3 moveDirection = new Vector3(0, 0, 0);
                if (Input.GetKey(KeyCode.W)) moveDirection.z = +1f;
                if (Input.GetKey(KeyCode.S)) moveDirection.z = -1f;
                if (Input.GetKey(KeyCode.A)) moveDirection.x = -1f;
                if (Input.GetKey(KeyCode.D)) moveDirection.x = +1f;

                float moveSpeed = 3f;
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            }
=======
            Vector3 moveDirection = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W)) moveDirection.z = +1f;
            if (Input.GetKey(KeyCode.S)) moveDirection.z = -1f;
            if (Input.GetKey(KeyCode.A)) moveDirection.x = -1f;
            if (Input.GetKey(KeyCode.D)) moveDirection.x = +1f;

            float moveSpeed = 3f;
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
>>>>>>> c5a9ab3552d92cf8ee81e36e3a2477d05d40579f
        }
    }
}
