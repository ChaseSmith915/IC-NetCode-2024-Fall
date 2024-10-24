using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace Assets.App.Scripts.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private OwnerNetworkAnimator _ownerNetworkAnimator;
        [SerializeField] private Animator _myAnimator;

        void Start()
        {
            if (_myAnimator == null)
            {
                _myAnimator = gameObject.GetComponent<Animator>();
            }

            if (_ownerNetworkAnimator == null)
            {
                _ownerNetworkAnimator = gameObject.GetComponent<OwnerNetworkAnimator>();
            }

        }

        void FixedUpdate()
        {
            if(IsOwner)
            {
                Vector3 moveDirection = new Vector3(0, 0, 0);
                if (Input.GetKey(KeyCode.W)) moveDirection.z = +1f;
                if (Input.GetKey(KeyCode.S)) moveDirection.z = -1f;
                if (Input.GetKey(KeyCode.A)) moveDirection.x = -1f;
                if (Input.GetKey(KeyCode.D)) moveDirection.x = +1f;

                _myAnimator.SetBool("IsWalking", moveDirection.z != 0 || moveDirection.x != 0);

                if (Input.GetKey(KeyCode.Z)) _ownerNetworkAnimator.SetTrigger("PunchTrigger");
                if (Input.GetKey(KeyCode.Space)) _ownerNetworkAnimator.SetTrigger("JumpTrigger");
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _myAnimator.SetBool("IsSprinting", true);
                }
                else
                {
                    _myAnimator.SetBool("IsSprinting", false);
                }


                float moveSpeed = 3f;
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            }
        }
    }
}
