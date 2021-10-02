using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject 
{
    public float moveSpeedCap;
    public float moveSpeedAccelerationRate;
    public float jumpVelocity;
    public float dashSpeed;
    public float dashCooldown;
    public float dashTime;
    public float shootCooldown;
    public float bulletSpeed;
    public float maxHealth;
    public float healthGain;
}