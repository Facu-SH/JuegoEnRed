using System;
using Managers;
using UnityEngine;

namespace LevelScripts
{
    public class FloorController : MonoBehaviour
    {
        [SerializeField] private Animator firstFloorAnimator;
        [SerializeField] private Animator secondFloorAnimator;

        private void Awake()
        {
            firstFloorAnimator.SetBool("IsTimeToGoDown", false);
            secondFloorAnimator.SetBool("IsTimeToGoDown", false);
        }

        private void Start()
        {
            GameManager.Instance.SetFloorControllerInstance(this);
        }

        public void FirstFloorDown()
        {
            firstFloorAnimator.SetBool("IsTimeToGoDown", true);
        }
        public void SecondFloorDown()
        {
            secondFloorAnimator.SetBool("IsTimeToGoDown", true);
        }
    }
}