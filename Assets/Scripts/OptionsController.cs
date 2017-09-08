using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
	public Slider difficultySlider;
    public Text volumeValue;
    public Text difficultyValue;    

    private LevelManager levelManager;
	private MusicPlayer musicPlayer;
	// Use this for initialization
	void Start () 
	{
        levelManager = FindObjectOfType<LevelManager>();
		musicPlayer = FindObjectOfType<MusicPlayer>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		difficultySlider.value = PlayerPrefsManager.GetDifficulty();
	}

    // Update is called once per frame
    void Update()
    {
        musicPlayer.SetVolume(volumeSlider.value);
        volumeValue.text = (volumeSlider.value * 100).ToString("F2") + "%";
        if (difficultySlider.value == 1)
        {
            difficultyValue.text = "Easy";
        }
        else if (difficultySlider.value == 2)
        {
            difficultyValue.text = "Medium";
        }
        else if (difficultySlider.value == 3)
        {
            difficultyValue.text = "Hard";
        }
    }

    public void SaveAndExit()
	{
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		PlayerPrefsManager.SetDifficulty(difficultySlider.value);
		levelManager.LoadLevel("00a_Start_Menu");
	}
	
	public void SetDefaults()
	{
		volumeSlider.value = 0.8f;
		difficultySlider.value = 2f;
	}
}
