using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
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

    //Attack enemy target for 1-4 dmg and poison for 1-4 for 3 rounds
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
            player.Attack(1, 5);
            player.target.SetPoisoned(3, Random.Range(1, 5 + characterStats.GetBonusDamage()));
            player.CombatLog(player.target.GetComponent<Enemy>().name + " is poisoned");
            player.EndTurn();
        }
        else
        {
            player.EndTurn();
        }

    }

    //All poisoned enemy targets gain 2 more poison rounds
    public void Ability2()
    {
        animator.SetTrigger("poison");
        CharacterStats[] possibleTargets;
        possibleTargets = GameObject.FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats character in possibleTargets)
        {
            if (!character.isFriendly && character.IsPoisoned())
            {
                character.SetPoisonTurnCounter(3);
                player.CombatLog(player.name + " reapplied poison on " + character.name);
            }
        }
        player.EndTurn();
    }

    //Buff for +3 atk roll and + 2dmg for 2 turns but poison self for 2 dmg 2 turns
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
            player.target.SetTempAttackRoll(3, 3);
            player.target.SetBonusDamage(3, 2);
            player.target.SetPoisoned(3, 2);
            player.CombatLog(player.name + "poisoned " + player.target.GetComponent<Player>().name + " but also buffed +3 attack roll and +2 damage for 2 turns.");
            player.EndTurn();
        }
    }

}
