using UnityEngine;

[CreateAssetMenu(fileName = "newLevelVariables", menuName = "Level/Variables")]
public class LevelVariables : ScriptableObject
{
    
    [Header("Configuration")]
    [SerializeField] private int pointsToWin;


    public int PointsToWin => pointsToWin;
}