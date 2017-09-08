using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRidingHood : MonoBehaviour
{

    private Enemy enemy;
    private CharacterStats[] possibleTargets;
    private Animator animator;
    private bool angry = false;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CharacterStats.onTurn == this.GetComponent<CharacterStats>() && !enemy.attacked)
        {
            enemy.attacked = true;
            Attack();
        }
    }

    public void Attack()
    {
        int randomAbility;
        string ability = "Ability1";
        CharacterStats wolf = FindObjectOfType<BigBadWolf>().GetComponent<CharacterStats>();
        if (wolf != null)
        {
            if (wolf.GetHealth() >= wolf.maxHealth)
            {
                randomAbility = Random.Range(1, 3);
            }
            else
            {
                randomAbility = Random.Range(1, 4);
            }
            ability = "Ability" + randomAbility.ToString();
        }        
        else if (!angry)
        {
            angry = true;
            enemy.CombatLog(enemy.name + " is angry that you killed her pet and her dmg is increased");
        }
        Invoke(ability, 2f);
    }

    //Attack a target for 1-3 dmg and poison them for 1 dmg 1 turn if wolf is dead, dmg is doubled
    public void Ability1()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            animator.SetTrigger("attack");
            if (FindObjectOfType<BigBadWolf>().GetComponent<CharacterStats>() != null)
            {
                enemy.Attack(1, 4);
                enemy.target.SetPoisoned(1, 1);
            }
            else
            {
                enemy.Attack(2, 8);
                enemy.target.SetPoisoned(1, 2);
            }
            enemy.CombatLog(enemy.target.GetComponent<Player>().name + " is poisoned");
        }
        enemy.EndTurn();
    }


    //Buff Big Bad Wolf for +2 dmg and +1 atk roll for 2 turns
    public void Ability2()
    {
        enemy.target = FindObjectOfType<BigBadWolf>().GetComponent<CharacterStats>();
        enemy.target.SetBonusDamage(3, 2);
        enemy.target.SetTempAttackRoll(3, 1);
        enemy.CombatLog(enemy.name + " buffs Big Bad Wolf for +2dmg and +1 atk roll for 2 turns");
        enemy.EndTurn();
    }

    //Heal Big Bad Wolf for 3-7
    public void Ability3()
    {
        enemy.target = FindObjectOfType<BigBadWolf>().GetComponent<CharacterStats>();
        int heal = Random.Range(3, 8);
        enemy.target.IncreaseHealth(heal);
        enemy.CombatLog(enemy.name + " heals Big Bad Wolf for " + heal);
        enemy.EndTurn();
    }

}
