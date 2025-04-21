using UnityEngine;

[CreateAssetMenu(fileName = "newBasePLayerStats", menuName = "PLayer/BasePLayerStats")]
public class BasePLayerStats : ScriptableObject
{
    [Header("Vida")]
    [SerializeField] private int maxHealth;
    [Header("Movimiento")]
    [Tooltip("Velocidad base")]
    [SerializeField] private float speed;
    [SerializeField] private float airControl;
    [Tooltip("Velocidad máxima")]
    [SerializeField] private float maxVelocity;
    //[SerializeField] private float mouseSensitivity;

    [Header("Salto")]
    [SerializeField] private float jumpForce;
    [SerializeField] private int groundLayerIndex;

    [Header("Sprint")]
    [Tooltip("Tiempo máximo de sprint (segundos)")]
    [SerializeField] private float sprintDuration = 5f;
    [Tooltip("Tiempo para recargar sprint desde 0 a full (segundos)")]
    [SerializeField] private float sprintCooldown = 2.5f;
    [Tooltip("Multiplicador de velocidad al sprintar")]
    [SerializeField] private float sprintMultiplier = 1.5f;
    [Tooltip("Minimo de Stamina para volver a usar el sprint una vez llega a 0")]
    [SerializeField] private float minStaminaToSprint = 1f;
    [Header("Materials")]
    [SerializeField] private Material[] teamMaterials;

    public int MaxHealth => maxHealth;
    public float Speed => speed;
    public float JumpForce => jumpForce;
    public float AirControl => airControl;
    public float MaxVelocity => maxVelocity;
    //public float MouseSensitivity => mouseSensitivity;
    public int GroundLayerIndex => groundLayerIndex;

    public float SprintDuration => sprintDuration;
    public float SprintCooldown => sprintCooldown;
    public float SprintMultiplier => sprintMultiplier;
    public float MinStaminaToSprint => minStaminaToSprint;
    public Material[] TeamMaterials => teamMaterials;
}