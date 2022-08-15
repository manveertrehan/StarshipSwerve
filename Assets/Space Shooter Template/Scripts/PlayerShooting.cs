using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; 
}

public class PlayerShooting : MonoBehaviour {

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    [Tooltip("firing time")]
    public float fireTime;

    //time for a new shot
    [HideInInspector] public float nextFire;
    [HideInInspector] public float shootDuration;


    [Tooltip("current weapon power")]
    [Range(1, 4)]       //change it if you wish
    public int weaponPower = 3; 

    public Guns guns;
    public bool shootingIsActive = false; 
    [HideInInspector] public int maxweaponPower = 4; 
    public static PlayerShooting instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void Start()
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
        shootDuration = Time.time + fireTime;
    }

    private void Update()
    {
        
        if (shootingIsActive && Time.time > nextFire)
        {
            MakeAShot();                                                         
            nextFire = Time.time + 1 / fireRate;
        }
        if (Time.time > shootDuration) {
            shootingIsActive = false;
        }
    }

    //method for a shot
    void MakeAShot() 
    {
        CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
        guns.centralGunVFX.Play();
        CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
        guns.leftGunVFX.Play();
        CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
        guns.rightGunVFX.Play();
    }

    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        Instantiate(lazer, pos, Quaternion.Euler(rot));
    }
}
