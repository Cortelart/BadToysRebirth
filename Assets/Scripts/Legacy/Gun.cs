using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    public bool isShooting;
    
    float timeSinceLastShot;
    public Animator GunAnim;
    public ParticleSystem Muzzleflash;
    public AudioSource GunSound;
    public AudioSource GunReload;

    private void Awake() 
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload() 
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload() 
    {
        gunData.reloading = true;
        Debug.Log("Reloading");
        if (gunData.reloading == true)
        {GunAnim.SetBool("IsReloading", true);
        GunReload.Play();
        }
        
        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
        if (gunData.reloading == false)
        {
            GunAnim.SetBool("IsReloading", false);
            GunReload.Stop();
        }
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    

    private void Shoot() 
    {
        if (gunData.currentAmmo > 0) 
        {
            if (CanShoot()) 
            {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        } 
    }

    private void Update() 
    {
        timeSinceLastShot += Time.deltaTime;
         Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);        
    }

    private void OnGunShot() 
    {
        Debug.Log("Shoot");
        GunAnim.SetTrigger("IsShooting");
        GunSound.Play();
        Muzzleflash.Play();
    }  
   
}
