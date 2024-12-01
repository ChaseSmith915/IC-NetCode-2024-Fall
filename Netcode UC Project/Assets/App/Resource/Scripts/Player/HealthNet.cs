using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

namespace App.Resource.Scripts.Player
{
    public class HealthNet : NetworkBehaviour
    {
        [SerializeField] public float StartingHealth = 100f;
        [SerializeField] public float Cooldown = 1.5f;
        [SerializeField] private bool _canDamage = true;
        [SerializeField] private NetworkVariable<float> _health = new NetworkVariable<float>(100);
        public Image HealthBar;


        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            _health.Value = StartingHealth;

            _health.OnValueChanged += UpdateHealth;
        }

        private void UpdateHealth(float previousValue, float newValue)
        {
            if (HealthBar != null)
            {
                HealthBar.fillAmount = newValue / StartingHealth;
            }

            if (IsOwner)
            {
                if (newValue < 0f)
                {
                    HasDiedRPC();
                }
            }
        }

        [Rpc(target: SendTo.Server)]
        public void HasDiedRPC()
        {
            NetworkObject.Despawn();
        }

        [Rpc(SendTo.Server, RequireOwnership = false)]
        public void DamageObjRPC(float dmg)
        {
            if (!_canDamage) return;
            _health.Value -= dmg;
            StartCoroutine(nameof(DamageCooldown));
        }

        private IEnumerable DamageCooldown()
        {
            _canDamage = false;
            yield return new WaitForSeconds(Cooldown);
            _canDamage = true;
        }
    }
}

