using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Slider volumeSlider;
    public Text volumeValue;

    private LevelManager levelManager;
    private MusicPlayer musicPlayer;

    void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
        musicPlayer = FindObjectOfType<MusicPlayer>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
    }

    void Update()
    {
        musicPlayer.SetVolume(volumeSlider.value);
        volumeValue.text = (volumeSlider.value * 100).ToString("F2") + "%";
    }

    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToStart()
    {
        levelManager.LoadLevel("00a_Start_Menu");
    }
}
