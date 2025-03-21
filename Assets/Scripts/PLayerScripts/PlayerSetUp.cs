using UnityEngine;

namespace PLayerScripts
{
    public class PlayerSetUp : MonoBehaviour
    {
        [SerializeField] private GameObject camera;
        [SerializeField] private GameObject virtualCamera;
        [SerializeField] private Movement movement;
        [SerializeField] private NameTag nameTagComponent;

        public void StartUpLocalPlayer(string playerName)
        {
            camera.SetActive(true);
            virtualCamera.SetActive(true);
            movement.enabled = true;
            nameTagComponent.SetName(playerName);
        }
        
    }
}