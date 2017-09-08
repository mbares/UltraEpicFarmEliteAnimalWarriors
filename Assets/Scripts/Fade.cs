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
        if (Time.timeSinceLevelLoad >= 0.5f && Input.GetKey(KeyCode.Space))
        {
            panel = GetComponent<Image>();
            panel.CrossFadeAlpha(1, fadeTime, false);
            Invoke("NextLevel", fadeTime);
        }
    }

    void NextLevel()
    {
        if(SceneManager.GetActiveScene().name == "03b_Lose" || SceneManager.GetActiveScene().name == "03a_Win")
        {
            levelManager.LoadLevel("00a_Start_Menu");
        }
        else
        {
            levelManager.NextLevel();
        }
       
    }
}
