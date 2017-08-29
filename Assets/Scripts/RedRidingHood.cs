using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRidingHood : MonoBehaviour
{

    private Enemy enemy;
    private CharacterStats[] possibleTargets;
    private Animator animator;

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
        if (GameObject.FindObjectOfType<BigBadWolf>().GetComponent<CharacterStats>() != null)
        {
            randomAbility = Random.Range(1, 4);
            ability = "Ability" + randomAbility.ToString();
        }        
        Invoke(ability, 2f);
    }

    //Attack a target for 1-3 dmg
    public void Ability1()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            animator.SetTrigger("attack");
            enemy.Attack(1, 4);
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        enemy.EndTurn();
    }

    //Heal Big Bad Wolf for 3-7
    public void Ability2()
    {
        enemy.target = GameObject.FindObjectOfType<BigBadWolf>().GetComponent<CharacterStats>();
        enemy.target.health += Random.Range(3, 8);
        enemy.EndTurn();
    }

    //Buff Big Bad Wolf for +2 dmg and +1 atk roll for 2 turns
    public void Ability3()
    {
        enemy.target = GameObject.FindObjectOfType<BigBadWolf>().GetComponent<CharacterStats>();
        enemy.target.BonusDamage(2, 2);
        enemy.target.TempAttackRoll(2, 1);
        enemy.EndTurn();
    }
}
