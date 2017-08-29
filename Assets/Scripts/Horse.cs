using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    private Player player;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    //Attack enemy target for 1-4 dmg
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
            player.EndTurn();
        }
        else
        {
            player.CombatLog(player.name + " missed while trying to attack " + player.target.name);
            player.EndTurn();
        }
       
    }

    //Heal friendly target for 1-6
    public void Ability2()
    {
        player.ChooseTarget();
        if (!player.target)
        {
            player.CombatLog("Select target");
        }
        else if (!player.target.isFriendly)
        {
            player.CombatLog("Can't heal enemy target");            
        }       
        else
        {
            animator.SetTrigger("heal");
            int heal = Random.Range(1, 7);
            player.target.health += heal;
            player.CombatLog(player.name + " healed " + player.target.name + " for " + heal);
            player.EndTurn();
        }

    }

    //Debuff enemy target's atk roll and armor by -2 for 2 turns
    public void Ability3()
    {
        player.ChooseTarget();
        if(!player.target)
        {
            player.CombatLog("Select target");
        }
        else if (player.target.isFriendly)
        {
            player.CombatLog("Can't attack friendly target");
        }        
        else if (player.AttackRoll())
        {
            player.CombatLog(player.name + " debuffed " + player.target.name + "'s armor and attack roll");
            player.target.TempAttackRoll(2, -2);
            player.target.TempArmor(2, -2);
            player.EndTurn();
        }
        else
        {
            player.CombatLog(player.name + " missed while trying to attack " + player.target.name);
            player.EndTurn();
        }
       
    }

}
