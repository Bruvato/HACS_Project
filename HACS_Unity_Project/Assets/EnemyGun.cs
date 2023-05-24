using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class EnemyGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private WeaponRecoil recoil;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float forceMultiplier;

    private Transform muzzle;
    private string muzzlePath;
    private LayerMask targets;

    private float timeSinceLastShot;

    public void Awake()
    {
        EnemyShoot.shootAction += Shoot;
        EnemyShoot.reloadAction += StartReload;
        targets = gunData.targets;
        // muzzlePath = "Enemy Weapon Holder/"+gunData.name+"/Muzzle";
        // muzzle = GameObject.Find(muzzlePath).transform;
        muzzle = gameObject.transform.GetChild(1);

    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {

        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())

            {
                Debug.Log("shooting");
  
                GameObject bullet = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(muzzle.forward*forceMultiplier, ForceMode.Impulse);

                recoil.recoil();

                // gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();

            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward*gunData.maxDistance, Color.red);
    }

    private void OnGunShot()
    { }

    
}
