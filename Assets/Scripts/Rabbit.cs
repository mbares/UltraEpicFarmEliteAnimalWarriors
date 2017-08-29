using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character5 : MonoBehaviour
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

    //Attack enemy target for 1 dmg and poison for 1-4 for 2 rounds
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
            player.Attack(1, 2);
            player.target.Poisoned(2, Random.Range(1, 5));
            player.CombatLog(player.target.name + " is poisoned");
            player.EndTurn();
        }
        else
        {
            player.CombatLog(gameObject.name + " missed while trying to attack " + player.target.name);
            player.EndTurn();
        }

    }

    //All poisoned targets gain 2 more poison rounds
    public void Ability2()
    {
        CharacterStats[] possibleTargets;
        possibleTargets = GameObject.FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats character in possibleTargets)
        {
            if (!character.isFriendly && character.IsPoisoned())
            {
                character.AddPoisonTurnCounter(2);
                player.CombatLog(gameObject.name + " reapplied poison on " + character.name);
            }
        }
        player.EndTurn();
    }

    //Buff self for +3 atk roll for 2 turns but poison self for 2 dmg 2 turns
    public void Ability3()
    {
        characterStats.TempAttackRoll(2, 3);
        characterStats.Poisoned(2, 2);
        player.CombatLog(gameObject.name + " poisoned himself but buffed his atk roll +3");
        player.EndTurn();
    }

}
