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

    //Attack enemy target for 2-8 dmg
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
            player.Attack(2, 9);
            player.EndTurn();
        }
        else
        {
            player.EndTurn();
        }
    }

    //Attack all enemies for  1-3 dmg
    public void Ability2()
    {
        possibleTargets = FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats character in possibleTargets)
        {
            if (!character.isFriendly)
            {
                player.target = character;
                if (player.AttackRoll())
                {
                    player.Attack(1, 4);
                }
            }
        }
        animator.SetTrigger("fire");
        player.EndTurn();
    }

    //Self buff for +2 dmg but -1 atk roll for 2 turns
    public void Ability3()
    {
        player.CombatLog(player.name + " buffed himself for +2 damage but debuffed his attack roll for -1 for 2 turns");
        CharacterStats characterStats = GetComponent<CharacterStats>();
        characterStats.StatusEffect(new Color(1, 0.65f, 0.16f));
        characterStats.SetTempAttackRoll(3, -1);
        characterStats.SetBonusDamage(3, 2);
        player.EndTurn();
    }
}
