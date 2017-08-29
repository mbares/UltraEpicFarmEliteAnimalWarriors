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
                player = players[0];
                break;
            case "hb2":
                player = players[1];
                break;
            case "hb3":
                player = players[2];
                break;
        }
        maxHealth = player.GetComponent<CharacterStats>().health;
        GetComponentInChildren<Text>().text = player.name;
    }

    void Update () 
    {      
        HealthBar();       
	}

    void HealthBar()
    {        
        statusText.text = player.GetComponent<CharacterStats>().health.ToString();
        fillAmount = player.GetComponent<CharacterStats>().health / maxHealth;
        content.fillAmount = fillAmount;
        if (player.GetComponent<CharacterStats>().health <= 0)
        {
            statusText.text = "DEAD";
        }        
    }
   
 }
