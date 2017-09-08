using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class barScript : MonoBehaviour {

    public Text statusText;
    public Image content;

    private static Player[] players;
    private Player player;   
    private float fillAmount;   
    private float maxHealth;

    private void Start()
    {
        players = FindObjectsOfType<Player>();
        switch (tag)
        {
            case "hb1":
                player = players[2];
                break;
            case "hb2":
                player = players[1];
                break;
            case "hb3":
                player = players[0];
                break;
            default:
                break;
        }
        maxHealth = player.GetComponent<CharacterStats>().maxHealth;
        GetComponentInChildren<Text>().text = player.name;
    }

    void Update () 
    {      
        HealthBar();       
	}

    public void TargetOnClick()
    {
        if (player != null)
        {
            CharacterStats.mouseTargetedCharacter = player.gameObject;
        }
    }

    void HealthBar()
    {
        if (player == null)
        {
            statusText.text = "DEAD";
            return;
        }
        statusText.text = player.GetComponent<CharacterStats>().GetHealth().ToString();
        fillAmount = player.GetComponent<CharacterStats>().GetHealth() / maxHealth;
        content.fillAmount = fillAmount;
    }

 }
