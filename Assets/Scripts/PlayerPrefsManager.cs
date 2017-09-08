using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour 
{

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";

    private void Start()
    {
        SetMasterVolume(80f);
        SetDifficulty(2);
    }

    public static void SetMasterVolume(float volume)
	{
		if(volume >= 0f && volume <= 1f)
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		else 
			Debug.LogError("Master volume out of range");
	}
	public static float GetMasterVolume()
	{
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
	
	public static void SetDifficulty(float difficulty)
	{
		if(difficulty >= 1f && difficulty <= 3f)
			PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
		else 
			Debug.LogError("Difficulty out of range");
	}
		
	public static float GetDifficulty()
	{
		return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
	}
}
