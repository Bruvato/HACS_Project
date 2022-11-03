using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitscanShoot : MonoBehaviour
{
    private float timeBeforeShoot = 0f;
    public float firerate;

    public float range;
    public LayerMask targets;
    public LayerMask enemies;
    public WeaponRecoil recoil;
    public Transform bulletEmitter;
    private Vector3 Source;
    private Ray bulletPath;
    private AudioSource noise;
    private RaycastHit hit;
    public ParticleSystem impactParticleSystem;
    private GameObject hitObject;
    private ObjectPool<ParticleSystem> pool;
    private ParticleSystem particle;
    private Enemy stats;

    // Start is called before the first frame update
    void Start()
    {
        noise = GetComponent<AudioSource>();
        firerate = 1 / firerate;
        pool = new ObjectPool<ParticleSystem>(() =>
        {
            return Instantiate(impactParticleSystem);
        }, particle =>
        {
            particle.gameObject.SetActive(true);
        }, particle =>
        {
            particle.gameObject.SetActive(false);
        }, particle =>
        {
            Destroy(particle.gameObject);
        }, false, 10, 20);
        
    }

    // Update is called once per frame
    void Update()
    {
        Source = new Vector3(bulletEmitter.position.x, bulletEmitter.position.y, bulletEmitter.position.z);
        bulletPath = new Ray(Source, bulletEmitter.forward);
        if (timeBeforeShoot > 0f)
        {
            timeBeforeShoot -= Time.deltaTime;
        }
        if (Input.GetButton("Fire1"))
        {
            if (timeBeforeShoot <= 0f)
            {
                timeBeforeShoot = firerate;
                noise.PlayOneShot(noise.clip);
                
                if (Physics.Raycast(bulletPath, out hit, range, targets))
                {
                    hitObject = hit.collider.gameObject;
                    var particle = pool.Get();
                    particle.transform.position = hit.point;
                    particle.transform.forward = hit.normal;
                    
                    StartCoroutine(killParticle(particle));
                    if(Physics.Raycast(bulletPath, out hit, range, enemies))
                    {
                        stats = hitObject.GetComponent<Enemy>();

                        stats.hp -= 10;
                    }
                }
                recoil.recoil();
            }
        }
    }
    IEnumerator killParticle(ParticleSystem ptcle){
        yield return new WaitForSeconds(5);
        pool.Release(ptcle);
    }
}
