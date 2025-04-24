using UnityEngine;

[CreateAssetMenu(fileName = "newLevelVariables", menuName = "Level/Variables")]
public class LevelVariables : ScriptableObject
{
    
    [Header("Configuration")]
    [Tooltip("Puntos que necesita un equipo para ganar")][SerializeField] private int pointsToWin;
    [Tooltip("Tiempo para respawn luego de morir (segundos)")][SerializeField] private float timeToRespawn;
    [Tooltip("Tiempo máximo de partida (segundos)")][SerializeField] private float timeToEndLevel;
    [Tooltip("Tiempo para que caiga el primer piso (segundos)")][SerializeField] private float timeToFloorDown1;
    [Tooltip("Tiempo para que caiga el segundo piso (segundos)")][SerializeField] private float timeToFloorDown2;


    public int PointsToWin => pointsToWin;
    public float TimeToRespawn => timeToRespawn;
    public float TimeToEndLevel => timeToEndLevel;
    public float TimeToFloorDown1 => timeToFloorDown1;
    public float TimeToFloorDown2 => timeToFloorDown2;
}