using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    private Player player;
    private CharacterStats[] possibleTargets;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void Ability1()
    {
        player.ChooseTarget();
        if (!player.target)
        {
            player.CombatLog("Select target");
        }
        else if (player.target.isFriendly)
        {
            player.CombatLog("Can't attack friendly target");
        }
        else if (player.AttackRoll())
        {
            int damage = Random.Range(2, 6);
            player.target.health -= damage;
            player.CombatLog(gameObject.name + " damaged " + player.target.name + " for " + damage + " damage");
            player.EndTurn();
        }
        else
        {
            player.CombatLog(gameObject.name + " missed while trying to attack " + player.target.name);
            player.EndTurn();
        }
    }

    public void Ability2()
    {
        possibleTargets = GameObject.FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats character in possibleTargets)
        {
            if (!character.isFriendly)
            {
                player.target = character;
                if (player.AttackRoll())
                {
                    int damage = Random.Range(1, 4);
                    player.target.health -= damage;
                    player.CombatLog(gameObject.name + " damaged " + player.target.name + " for " + damage + " damage");
                }
            }
        }
        player.EndTurn();
    }

    public void Ability3()
    {
        CharacterStats characterStats = GetComponent<CharacterStats>();
        characterStats.TempAttackRoll(2, -1);
        player.EndTurn();
    }
}
