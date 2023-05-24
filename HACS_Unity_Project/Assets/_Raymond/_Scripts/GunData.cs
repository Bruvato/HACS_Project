using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public bool onEnemy;
    public bool isProjectile;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;
    public LayerMask targets;
    public float forceMultiplier;
    public GameObject projectilePrefab;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public bool reloading;

    [Header("Recoil/aim")]
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public float kickZ;
    public float snappiness;
    public float returnAmount;
    public float aimSpeedMultiplier;
    public float aimSpeed = 0.5f;

    public GameObject playerPrefab; //for camera holder, view
    public float aimfov;
    public GameObject weaponPrefab; //for aimLocation, muzzle
}
