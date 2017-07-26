using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private Player player;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("attack");
            int damage = Random.Range(1, 4);
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
            int heal = Random.Range(1, 6);
            player.target.health += heal;
            player.CombatLog(gameObject.name + " healed " + player.target.name + " for " + heal);
            player.EndTurn();
        }

    }

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
            player.CombatLog(gameObject.name + " debuffed " + player.target.name + "'s armor and attack roll");
            player.target.TempAttackRoll(2, -2);
            player.target.TempArmor(2, -2);
            player.EndTurn();
        }
        else
        {
            player.CombatLog(gameObject.name + " missed while trying to attack " + player.target.name);
            player.EndTurn();
        }
       
    }

}
