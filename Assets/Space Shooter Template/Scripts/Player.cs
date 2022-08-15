using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public static Player instance;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
            GetComponent<PanelScript>().Hide();
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)   
    {
        Destruction();
    }    

    //'Player's' destruction procedure
    void Destruction()
    {
        if (GetComponent<PlayerMoving>().score > PlayerPrefs.GetInt("score")) {
            PlayerPrefs.SetInt("score", (int)GetComponent<PlayerMoving>().score);
        }

        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
        GetComponent<PanelScript>().Show();
    }
}
















