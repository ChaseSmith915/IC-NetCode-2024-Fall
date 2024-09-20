using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


[RequireComponent(typeof(CharacterController))]
public class ServerPlayerMovement : NetworkBehaviour
{
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

        if (IsServer)
        {
            Move(moveInput);
        }
        else if (IsClient)
        {
            MoveServerRPC(moveInput);
        }
    }

    private void Move(Vector2 input)
    {
        Vector3 moveDirection = input.x * _pTransform.right + input.y * _pTransform.forward;

        CC.Move(moveDirection * _pSpeed * Time.deltaTime);
    }

    [Rpc(SendTo.Server)]
    private void MoveServerRPC(Vector2 input)
    {
        Move(input);
    }
}
