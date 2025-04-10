using System;
using Cinemachine;
using Managers;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

namespace PLayerScripts
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private BasePLayerStats data;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform playerBody;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        
        private CinemachinePOV povComponent;
        private Vector2 inputDirection;

        private void Awake()
        {
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        void Start()
        {
            MyPLayerManager.Instance.SetPlayerMovementInstance(this);
            povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        }
        void Update()
        {
            inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            float yaw = povComponent.m_HorizontalAxis.Value;
            playerBody.rotation = Quaternion.Euler(0f, yaw, 0f);
        }

        private void FixedUpdate()
        {
            rb.AddForce(CalculateMovement(),ForceMode.VelocityChange);
        }

        Vector3 CalculateMovement()
        {
            Vector3 vecDir = (transform.right * inputDirection.x + transform.forward * inputDirection.y).normalized;
            vecDir *= data.Speed;
            
            Vector3 vel = rb.velocity;

            if (inputDirection.magnitude >0.3f)
            {
                Vector3 velocityChange = vecDir - vel;
                velocityChange.x = Mathf.Clamp(velocityChange.x, -data.MaxVelocity, data.MaxVelocity);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -data.MaxVelocity, data.MaxVelocity);
                velocityChange.y = 0;
                
                return velocityChange;
            }
            
            return new Vector3();
        }

        private void OnEnable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (povComponent != null)
                povComponent.enabled = true;
        }

        private void OnDisable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (povComponent != null)
                povComponent.enabled = false;
        }
    }
}
