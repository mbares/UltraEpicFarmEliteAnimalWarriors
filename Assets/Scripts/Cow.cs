using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    private Player player;
    private CharacterStats characterStats;
    private Animator animator;

    private void Start()
    {
        player = GetComponent<Player>();
        characterStats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
    }

    //Attack enemy target for 2-4 dmg
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
            player.Attack(2, 5);
            player.EndTurn();
        }
        else
        {
            player.EndTurn();
        }
    }

    //Taunt enemy targets making it attack you its next turn
    public void Ability2()
    {
        animator.SetTrigger("taunt");
        characterStats.SetTaunting(true);
        player.CombatLog("Taunt activated");
        player.EndTurn();
    }

    //Buff friendly with +2 armor for 2 turns
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
            player.target.SetTempArmor(3, 2);
            player.target.StatusEffect(Color.white);
            player.CombatLog("Buffed " + player.target.GetComponent<Player>().name + "'s armor by 2 for 2 turns");
            player.EndTurn();
        }

    }
}
