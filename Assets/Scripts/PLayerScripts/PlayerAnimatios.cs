using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatios : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    
    void Update()
    {
        
    }

    public void ShootAnimation()
    {
        playerAnimator.SetTrigger("ShootTrigger");
    }

    public void MotionAnimation(float motion)
    {
        //Motion= 0 IDLE
        //Motion= 0.5 WALK
        //Motion= 1 RUN
        playerAnimator.SetFloat("SpeedMotion",motion);
        playerAnimator.SetBool("IsOnAir",false);
    }

    public void JumpAnimation()
    {
        playerAnimator.SetTrigger("JumpTrigger");
        playerAnimator.SetBool("IsOnAir", true);
    }
    public void OnAirAnimation()
    {
        playerAnimator.SetBool("IsOnAir", true);
    }
    public void FallAnimation()
    {
        playerAnimator.SetTrigger("FallingTrigger");
        playerAnimator.SetBool("IsOnAir", false);
    }
}
