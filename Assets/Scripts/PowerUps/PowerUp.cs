using Enums;
using Managers;
using Photon.Pun;
using UnityEngine;

public class PowerUp : MonoBehaviourPun
{
    public PowerUpType type;
    private bool shouldCollide = true;
    [SerializeField] private int playerLayerIndex = 7;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayerIndex)
        {
            if (!shouldCollide) return;

            shouldCollide = false;

            if (!PhotonNetwork.IsMasterClient)
                return;

            if (!other.TryGetComponent<PhotonView>(out var targetPv))
                return;

            double timestamp = PhotonNetwork.Time;

            GameManager.Instance.RegisterPowerUpPickup(
                powerUpViewID: photonView.ViewID,
                type: type,
                player: targetPv.Owner,
                timestamp: timestamp
            );

            PhotonNetwork.Destroy(gameObject);
        }
    }
}