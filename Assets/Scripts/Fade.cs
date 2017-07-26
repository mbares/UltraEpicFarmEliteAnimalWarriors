using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {

    private float fadeTime = 2.5f;
    private Image panel;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        panel = GetComponent<Image>();
        panel.CrossFadeAlpha(0, fadeTime, false);
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad >= fadeTime && Input.GetKey(KeyCode.Space))
        {
            panel = GetComponent<Image>();
            panel.CrossFadeAlpha(1, fadeTime, false);
            Invoke("NextLevel", fadeTime);
        }
    }

    void NextLevel()
    {
        if(SceneManager.GetActiveScene().name == "Lose" || SceneManager.GetActiveScene().name == "Win")
        {
            levelManager.LoadLevel("Start Menu");
        }
        else
        {
            levelManager.NextLevel();
        }
       
    }
}
