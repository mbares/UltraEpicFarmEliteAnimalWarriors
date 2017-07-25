using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private Player player;
    private CharacterStats characterStats;

    private void Start()
    {
        player = GetComponent<Player>();
        characterStats = GetComponent<CharacterStats>();
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
            int damage = Random.Range(2, 4);
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
        characterStats.taunt = true;
        player.CombatLog("Taunt activated");
        player.EndTurn();
    }

    public void Ability3()
    {
        player.ChooseTarget();
        if (!player.target)
        {
            player.CombatLog("Select target");
        }
        else if (!player.target.isFriendly)
        {
            player.CombatLog("Can't target enemy character with this ability");
        }
        else
        {            
            player.target.TempArmor(2, 2);
            player.CombatLog("Buffed " + player.target.name);
            player.EndTurn();
        }

    }
}
