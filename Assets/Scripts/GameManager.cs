using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private CharacterStats[] characters;
    public List<CharacterStats> turnOrder = new List<CharacterStats>();
    private int turnCounter = 0;
    private LevelManager levelManager;
    private int combatLogCounter = 0;
    
    public GameObject players;
    public GameObject enemies;
    public Text combatLog;
    public Text turnText;
    public Text ability1Cow;
    public Text ability2Cow;
    public Text ability3Cow;
    public Text ability1Horse;
    public Text ability2Horse;
    public Text ability3Horse;
    public Text ability1Chicken;
    public Text ability2Chicken;
    public Text ability3Chicken;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        characters = GameObject.FindObjectsOfType<CharacterStats>();
        foreach(CharacterStats character in characters)
        {
            turnOrder.Add(character);
        }
        turnOrder = turnOrder.OrderByDescending(turnOrder => turnOrder.initiative).ToList();
        NextTurn();
        for(int i=0; i<turnOrder.Count; i++)
        {
            Debug.Log(turnOrder[i]);
        }
        
    }

    private void Update()
    {
        if(players.transform.childCount == 0)
        {
            levelManager.LoadLevel("Lose");
        }

        if (enemies.transform.childCount == 0)
        {
            levelManager.NextLevel();
        }
    }

    public void AddCharacterToTurnOrder(CharacterStats newCharacter)
    {
        turnOrder.Add(newCharacter);
        turnOrder = turnOrder.OrderByDescending(turnOrder => turnOrder.initiative).ToList();
        for (int i = 0; i < turnOrder.Count; i++)
        {
            Debug.Log(turnOrder[i]);
        }
    }

    public void NextTurn()
    {
        Abilities();
        CharacterStats.onTurn = turnOrder[turnCounter];
        turnOrder[turnCounter].TurnStatCalculation();
        turnText.text = turnOrder[turnCounter].gameObject.name + " on turn ";
        
        turnCounter++;
        if(turnCounter >= turnOrder.Count)
        {
            turnCounter = 0;
        }        
    }

    public void CombatLog(string log)
    {
        if(combatLogCounter >= 8)
        {
            combatLog.text = log + "\n";
            combatLogCounter = 0;
        }
        else
        {
            combatLog.text += log + "\n";
        }
        combatLogCounter++;
    }

    public void Abilities()
    {
        if(turnOrder[turnCounter].gameObject.name == "Cow")
        {
            ability1Cow.gameObject.SetActive(true);
            ability2Cow.gameObject.SetActive(true);
            ability3Cow.gameObject.SetActive(true);

            ability1Horse.gameObject.SetActive(false);
            ability2Horse.gameObject.SetActive(false);
            ability3Horse.gameObject.SetActive(false);

            ability1Chicken.gameObject.SetActive(false);
            ability2Chicken.gameObject.SetActive(false);
            ability3Chicken.gameObject.SetActive(false);


        }
        else if(turnOrder[turnCounter].gameObject.name == "Horse")
        {
            ability1Horse.gameObject.SetActive(true);
            ability2Horse.gameObject.SetActive(true);
            ability3Horse.gameObject.SetActive(true);

            ability1Cow.gameObject.SetActive(false);
            ability2Cow.gameObject.SetActive(false);
            ability3Cow.gameObject.SetActive(false);

            ability1Chicken.gameObject.SetActive(false);
            ability2Chicken.gameObject.SetActive(false);
            ability3Chicken.gameObject.SetActive(false);
        }
        else if(turnOrder[turnCounter].gameObject.name == "Chicken")
        {
            ability1Chicken.gameObject.SetActive(true);
            ability2Chicken.gameObject.SetActive(true);
            ability3Chicken.gameObject.SetActive(true);

            ability1Cow.gameObject.SetActive(false);
            ability2Cow.gameObject.SetActive(false);
            ability3Cow.gameObject.SetActive(false);

            ability1Horse.gameObject.SetActive(false);
            ability2Horse.gameObject.SetActive(false);
            ability3Horse.gameObject.SetActive(false);
        }
        else
        {
            ability1Cow.gameObject.SetActive(false);
            ability2Cow.gameObject.SetActive(false);
            ability3Cow.gameObject.SetActive(false);

            ability1Horse.gameObject.SetActive(false);
            ability2Horse.gameObject.SetActive(false);
            ability3Horse.gameObject.SetActive(false);

            ability1Chicken.gameObject.SetActive(false);
            ability2Chicken.gameObject.SetActive(false);
            ability3Chicken.gameObject.SetActive(false);
        }
    }

}
