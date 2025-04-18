using Cinemachine;
using Managers;
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
        private bool isJumping;
        private bool isGrounded;
        [SerializeField] private bool isSprinting;
        private bool isSprintInCoolDown = false;

        [SerializeField] private float currentStamina;
        [SerializeField] private float staminaRegenRate;

        private void Awake()
        {
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            currentStamina   = data.SprintDuration;
            staminaRegenRate = data.SprintDuration / data.SprintCooldown;
        }

        private void Start()
        {
            MyPLayerManager.Instance.SetPlayerMovementInstance(this);
            povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        }

        private void Update()
        {
            inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            float yaw = povComponent.m_HorizontalAxis.Value;
            playerBody.rotation = Quaternion.Euler(0f, yaw, 0f);

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                isJumping = true;
            }
            
            if (currentStamina <= 0f)
                isSprintInCoolDown = true;
            
            bool wantsSprint = Input.GetButton("Sprint") && inputDirection.magnitude > 0.3f && isGrounded && !isSprintInCoolDown;
            isSprinting = wantsSprint && currentStamina > 0f;

            if (isSprinting)
            {
                currentStamina -= Time.deltaTime;
            }
            else
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }

            currentStamina = Mathf.Clamp(currentStamina, 0f, data.SprintDuration);
            
            if (isSprintInCoolDown && currentStamina >= data.MinStaminaToSprint)
                isSprintInCoolDown = false;
        }

        private void FixedUpdate()
        {
            if (isJumping)
            {
                rb.AddForce(Vector3.up * data.JumpForce, ForceMode.Impulse);
                isJumping = false;
            }

            rb.AddForce(CalculateMovement(), ForceMode.VelocityChange);
        }

        private Vector3 CalculateMovement()
        {
            Vector3 dir = (transform.right * inputDirection.x + transform.forward * inputDirection.y).normalized;
            float control = isGrounded ? 1f : data.AirControl;
            float baseSpeed = data.Speed * control;
            float sprintMod = isSprinting ? data.SprintMultiplier : 1f;

            Vector3 targetVel = dir * (baseSpeed * sprintMod);
            Vector3 velChange = targetVel - rb.velocity;
            velChange.x = Mathf.Clamp(velChange.x, -data.MaxVelocity, data.MaxVelocity);
            velChange.z = Mathf.Clamp(velChange.z, -data.MaxVelocity, data.MaxVelocity);
            velChange.y = 0f;

            return inputDirection.magnitude > 0.3f ? velChange : Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == data.GroundLayerIndex)
            {
                isGrounded = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == data.GroundLayerIndex)
            {
                isGrounded = false;
            }
        }

        private void OnEnable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (povComponent != null) povComponent.enabled = true;
        }

        private void OnDisable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (povComponent != null) povComponent.enabled = false;
        }
    }
}
