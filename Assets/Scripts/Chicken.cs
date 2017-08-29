using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{

    private Player player;
    private CharacterStats[] possibleTargets;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    //Attack enemy target for 2-6 dmg
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
            player.Attack(2, 7);
            player.EndTurn();
        }
        else
        {
            player.CombatLog(gameObject.name + " missed while trying to attack " + player.target.name);
            player.EndTurn();
        }
    }

    //Attack all enemies for  1-4 dmg
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
                    animator.SetTrigger("fire");
                    player.Attack(1, 5);
                }
            }
        }
        player.EndTurn();
    }

    //Self buff for +2 dmg but -1 atk roll for 2 turns
    public void Ability3()
    {
        player.CombatLog(gameObject.name + "buffed himself +2dmg -1atk roll for 2 turns");
        CharacterStats characterStats = GetComponent<CharacterStats>();
        characterStats.TempAttackRoll(2, -1);
        characterStats.BonusDamage(2, 2);
        player.EndTurn();
    }
}
