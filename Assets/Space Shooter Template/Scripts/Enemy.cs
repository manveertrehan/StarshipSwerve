using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Enemy : MonoBehaviour {

    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    
    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path

    public Sprite[] sprites;
    public float prob;
    
    #endregion

    private void Start()
    {
        shotChance = 10;
        int i = Random.Range(0,100);
        if (i > prob) {
            i = Random.Range(0,2);
            GetComponent<SpriteRenderer>().sprite = sprites[i];
            if (i == 0) {
                gameObject.transform.localScale = new Vector3(0.8375f, 1, 1);
                GetComponent<CircleCollider2D>().offset = new Vector2(-0.03904571f, -0.005094528f);
                GetComponent<CircleCollider2D>().radius = 0.5741432f;
            } else {
                gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 1);
                GetComponent<CircleCollider2D>().offset = new Vector2(0.02186744f, -0.004138931f);
                GetComponent<CircleCollider2D>().radius = 0.1781055f;
            }
        } else {
            GetComponent<SpriteRenderer>().sprite = sprites[2];
            gameObject.transform.localScale = new Vector3(1.9f,2,1);
            GetComponent<CircleCollider2D>().offset = new Vector2(0.02786357f, 0.004658762f);
            GetComponent<CircleCollider2D>().radius = 0.4818111f;
        }
        Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
    }

    //coroutine making a shot
    void ActivateShooting() 
    {
        if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, making a shot
        {                         
            Instantiate(Projectile,  gameObject.transform.position, Quaternion.identity);             
        }
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage) 
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
    }    

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                Player.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {   
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
}
