using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CharacterStats[] characters;
    private int turnCounter = 0;
    private LevelManager levelManager;
    private int combatLogCounter = 0;
    private int scrollCounter = 0;
    private CharacterStats characterOnTurn;

    public List<CharacterStats> turnOrder = new List<CharacterStats>();
    public GameObject menu;
    public GameObject players;
    public GameObject[] playerPositions;
    public GameObject enemies;
    public Scrollbar scrollbar;
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
            Instantiate(selectedPlayers[i], playerPositions[i].transform.position, Quaternion.identity, playerPositions[i].transform);
        }
        characters = FindObjectsOfType<CharacterStats>();
        foreach(CharacterStats character in characters)
        {
            character.initiative += Random.Range(1, 21);
            turnOrder.Add(character);
        }
        turnOrder = turnOrder.OrderByDescending(turnOrder => turnOrder.initiative).ToList();
        NextTurn();
        
    }

    private void Update()
    {
        if (players.transform.childCount == 0)
        {
            levelManager.LoadLevel("03b_Lose");
        }

        if (enemies.transform.childCount == 0 || (SceneManager.GetActiveScene().buildIndex == 4 && FindObjectOfType<SnowWhite>() == null))
        {
            levelManager.NextLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeInHierarchy);
        }
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
        if (combatLogCounter >= 50)
        {
            combatLog.text = log + "\n";
            combatLogCounter = 0;
            scrollCounter = 3;
            scrollbar.value = 1;
        }
        else
        {
            combatLog.text += log + "\n";
        }
        if (log.Length > 65)
        {
            scrollCounter++;
            combatLogCounter++;
        }
        scrollCounter++;
        combatLogCounter++;
        if (scrollCounter == 7 || scrollCounter == 8)
        {
            scrollbar.value -= 0.08f;
            scrollCounter = 3;
        }
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
