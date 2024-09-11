using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 moveDirection = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W)) moveDirection.z = +1f;
            if (Input.GetKey(KeyCode.S)) moveDirection.z = -1f;
            if (Input.GetKey(KeyCode.A)) moveDirection.x = -1f;
            if (Input.GetKey(KeyCode.D)) moveDirection.x = +1f;

            float moveSpeed = 3f;
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        }
    }
}
