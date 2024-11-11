using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using Unity.Netcode.Components;


[RequireComponent(typeof(CharacterController))]
public class ServerPlayerMovement : NetworkBehaviour
{
    [SerializeField] private Animator _myAnimator;
    [SerializeField] private NetworkAnimator _myNetworkAnimator;


    [SerializeField] private float _pSpeed;
    [SerializeField] private Transform _pTransform;

    public CharacterController CC;
    private MyPlayerInputAction _playerInput;
    void Start()
    {
        _playerInput = new MyPlayerInputAction();
        _playerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;

        Vector2 moveInput = _playerInput.Player.Movement.ReadValue<Vector2>();

        bool isJumping = _playerInput.Player.Jumping.triggered;
        bool isPunching = _playerInput.Player.Punching.triggered;
        bool isSprinting = _playerInput.Player.Sprinting.triggered;

        if (IsServer)
        {
            Move(moveInput, isPunching, isSprinting, isJumping);
        }
        else if (IsClient)
        {
            MoveServerRPC(moveInput, isPunching, isSprinting, isJumping);
        }
    }

    private void Move(Vector2 input, bool isPunching, bool isSprinting, bool isJumping)
    {
        Vector3 moveDirection = input.x * _pTransform.right + input.y * _pTransform.forward;

        _myAnimator.SetBool("IsWalking", moveDirection.x != 0 || moveDirection.y != 0);

        if (isPunching) _myNetworkAnimator.SetTrigger("PunchTrigger");
        if(isJumping) _myNetworkAnimator.SetTrigger("JumpTrigger");
        _myAnimator.SetBool("IsSprinting", isSprinting);

        if(isSprinting)
        {

            CC.Move(moveDirection * (_pSpeed * 1.3f) * Time.deltaTime);
        }
        else
        {

            CC.Move(moveDirection * _pSpeed * Time.deltaTime);
        }
    }

    [Rpc(SendTo.Server)]
    private void MoveServerRPC(Vector2 input, bool isPunching, bool isSprinting, bool isJumping)
    {
        Move(input, isPunching, isSprinting, isJumping);
    }
}
