using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class barScript : MonoBehaviour {
     
    private GameObject player;   
    private float fillAmount;   
    private Image content;   
    private Text statusText;
    private int maxHealth;

    private void Start()
    {
        maxHealth = player.GetComponent<CharacterStats>().health;
    }

    void Update () 
    {      
        HealthBar();       
	}

    void HealthBar()
    {        
        statusText.text = player.GetComponent<CharacterStats>().health.ToString();
        fillAmount = player.GetComponent<CharacterStats>().health / 100;
        content.fillAmount = fillAmount;
        if (player.GetComponent<CharacterStats>().health <= 0)
        {
            statusText.text = "DEAD";
        }        
    }
   
 }
