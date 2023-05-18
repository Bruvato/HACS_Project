using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Camera cam;
    [SerializeField] private WeaponRecoil recoil;
    private Transform muzzle;
    private string muzzlePath;
    private LayerMask targets;

    private float timeSinceLastShot;

    public void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        targets = gunData.targets;
        muzzlePath = "Weapon Holder/"+gunData.name+"/Muzzle";
        muzzle = GameObject.Find(muzzlePath).transform;
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

    private void Shoot()
    {
        Debug.Log("recoil");

        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())

            {
                if (Physics.Raycast(/*cam.transform.position, cam.transform.forward,*/muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance, targets))
                {

                    Debug.Log(hitInfo.transform.name);
                    

                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);

                }
                recoil.recoil();

                gunData.currentAmmo--;
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
