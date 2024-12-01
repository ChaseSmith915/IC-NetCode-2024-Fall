using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletSpawner : NetworkBehaviour
{
    [SerializeField] public NetworkVariable<int> Ammo = new NetworkVariable<int>();
    [SerializeField] private Transform _startingPoint;
    [SerializeField] private NetworkObject _ProjectilePrefab;

    [Rpc(SendTo.Server, RequireOwnership = false)]
    public void FireProjectileRPC(RpcParams rpcParams = default)
    {
        if(Ammo.Value > 0)
        {
            NetworkObject newProjectile = NetworkManager.Instantiate(_ProjectilePrefab, _startingPoint.position, _startingPoint.rotation);
            newProjectile.SpawnWithOwnership(rpcParams.Receive.SenderClientId);
            Ammo.Value--;
        }
    }
}
