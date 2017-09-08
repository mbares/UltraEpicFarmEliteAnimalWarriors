using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

    private LevelManager levelManager;

	void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
	}

    public void NextLevel()
    {
        if(levelManager.playerGroup.Count == 3)
        {
            levelManager.NextLevel();
        }
        else
        {
            Debug.Log("Choose party");
        }
    }

    public void Back()
    {
        levelManager.LoadLevel("00a_Start_Menu");
    }
}
