using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private CharacterStats[] characters;
    private int turnCounter = 0;
    private LevelManager levelManager;
    private int combatLogCounter = 0;
    private CharacterStats characterOnTurn;

    public Vector3[] playerPos;
    public List<CharacterStats> turnOrder = new List<CharacterStats>();
    public GameObject players;
    public GameObject enemies;
    public Text combatLog;
    public Text turnText;
    public Text ability1Button;
    public Text ability2Button;
    public Text ability3Button;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        List<GameObject> selectedPlayers = levelManager.playerGroup;
        for (int i = 0; i < selectedPlayers.Count; i++)
        {
            Instantiate(selectedPlayers[i], playerPos[i], Quaternion.identity, players.transform);
        }
        characters = GameObject.FindObjectsOfType<CharacterStats>();
        foreach(CharacterStats character in characters)
        {
            turnOrder.Add(character);
        }
        turnOrder = turnOrder.OrderByDescending(turnOrder => turnOrder.initiative).ToList();
        NextTurn();
        
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
    }

    public void NextTurn()
    {
        if (turnCounter >= turnOrder.Count)
        {
            turnCounter = 0;
        }        
        characterOnTurn = turnOrder[turnCounter];
        CharacterStats.onTurn = characterOnTurn;
        characterOnTurn.TurnStatCalculation();
        if(characterOnTurn.isFriendly)
        {
            turnText.text = characterOnTurn.GetComponent<Player>().name + " on turn";
        }
        else
        {
            turnText.text = characterOnTurn.GetComponent<Enemy>().name + " on turn ";
        }
         
        playerAbilities();

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

    public void playerAbilities()
    {
        if (characterOnTurn.isFriendly)
        {
            Player player = characterOnTurn.GetComponent<Player>();
            ability1Button.text = player.ability1Text;
            ability2Button.text = player.ability2Text;
            ability3Button.text = player.ability3Text;
        }
        else
        {
            ability1Button.text = "";
            ability2Button.text = "";
            ability3Button.text = "";
        }
    }

    public void playerAbility1()
    {
        characterOnTurn.GetComponent<Player>().Ability1();
    }

    public void playerAbility2()
    {
        characterOnTurn.GetComponent<Player>().Ability2();
    }

    public void playerAbility3()
    {
        characterOnTurn.GetComponent<Player>().Ability3();
    }

}
