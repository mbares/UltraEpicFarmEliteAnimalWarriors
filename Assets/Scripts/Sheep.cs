using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character4 : MonoBehaviour
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

    //Attack enemy target for 2-8 dmg and steal health equal to half the damage done
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
            int damage = player.Attack(2, 9);
            characterStats.health += damage/2;
            player.CombatLog(gameObject.name + " healed himself for " + damage / 2);
            player.EndTurn();
        }
        else
        {
            player.CombatLog(gameObject.name + " missed while trying to attack " + player.target.name);
            player.EndTurn();
        }

    }

    //Heal all friendly targets for 2 but damage self for 4
    public void Ability2()
    {
        CharacterStats[] possibleTargets;
        possibleTargets = GameObject.FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats character in possibleTargets)
        {
            if (character.isFriendly && character.gameObject != this.gameObject)
            {
                character.health += 2;
                player.CombatLog(character + " healed for 2");
            }
        }
        characterStats.health -= 4;
        player.CombatLog(gameObject.name + " suffers 4 damage for healing his party members");
        player.EndTurn();
    }

    //Heal self
    public void Ability3()
    {
        characterStats.health += 4;
        player.CombatLog(gameObject.name + " healed himself for 4");
        player.EndTurn();
    }

}
