using UnityEngine;

[CreateAssetMenu(fileName = "newLevelVariables", menuName = "Level/Variables")]
public class LevelVariables : ScriptableObject
{
    
    [Header("Configuration")]
    [Tooltip("Puntos que necesita un equipo para ganar")][SerializeField] private int pointsToWin;
    [Tooltip("Tiempo para respawn luego de morir (segundos)")][SerializeField] private float timeToRespawn;
    [Tooltip("Tiempo máximo de partida (segundos)")][SerializeField] private float timeToEndLevel;


    public int PointsToWin => pointsToWin;
    public float TimeToRespawn => timeToRespawn;
    public float TimeToEndLevel => timeToEndLevel;
}