using UnityEngine;

namespace LevelScripts
{
    public class FloorDeactivateSelf : MonoBehaviour
    {
        public void OuterFloorDeactivate()
        {
            gameObject.SetActive(false);
        }
    }
}