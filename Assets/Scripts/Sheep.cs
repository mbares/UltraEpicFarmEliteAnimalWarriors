using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Player player;
    private Animator animator;
    private CharacterStats characterStats;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    //Attack enemy target for 2-6 dmg and steal health equal to half the damage done
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
            animator.SetTrigger("attack");
            int damage = player.Attack(2, 7);
            characterStats.IncreaseHealth(damage / 2);
            player.CombatLog(player.name + " healed herself for " + damage / 2);
            player.EndTurn();
        }
        else
        {
            player.EndTurn();
        }

    }

    //Heal all friendly targets for 3 and give them +2 armor and +1 attack roll for 2 turns but damage self for 5
    public void Ability2()
    {
        CharacterStats[] possibleTargets;
        possibleTargets = FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats character in possibleTargets)
        {
            if (character.isFriendly && character.gameObject != this.gameObject)
            {
                character.IncreaseHealth(3);
                character.SetTempArmor(2, 2);
                character.SetTempAttackRoll(2, 1);
                character.StatusEffect(new Color(1f, 0.96f, 0f));
                player.CombatLog(character.GetComponent<Player>().name + " healed for 3 and has +2 armor and + 1 atk roll for 1 turn");
            }
        }
        characterStats.ReduceHealth(5);
        player.CombatLog(player.name + " suffers 5 damage for healing her party members");
        player.EndTurn();
    }

    //Heal friendly target for 2-6 and self for half
    public void Ability3()
    {
        Player[] players = FindObjectsOfType<Player>();
        player.ChooseTarget();
        if (!player.target && players.Length > 1)
        {
            player.CombatLog("Select friendly target");
        }
        else if (!player.target.isFriendly)
        {
            player.CombatLog("Can't heal enemy target");
        }
        else
        {
            int heal = Random.Range(2, 7);
            characterStats.IncreaseHealth(heal / 2);
            player.CombatLog(player.name + " healed herself for " + heal);
            if (player.target != characterStats)
            {
                player.target.IncreaseHealth(heal);
                player.CombatLog(player.name + " healed " + player.target.GetComponent<Player>().name + " for " + heal / 2);
            }
            player.EndTurn();
        }
    }

}
