using System;
using UnityEngine;

namespace PLayerScripts
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private BasePLayerStats data;
        [SerializeField] private Rigidbody rb;
        
        private Vector2 inputDirection;
        
        void Update()
        {
            inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
    }
}
