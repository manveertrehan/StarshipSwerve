using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshPro HighScoreText;

    // Start is called before the first frame update
    void Start()
    {
        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
