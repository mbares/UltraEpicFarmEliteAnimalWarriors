using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowWhite : MonoBehaviour
{
    public GameObject [] dwarves;    
    
    private Enemy enemy;
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
        randomAbility = Random.Range(1, 3);
        string ability = "Ability" + randomAbility.ToString();
        Invoke(ability, 2f);
    }

    //Attack for 2 dmg and poison the target for 2-3 dmg for 2 rounds
    public void Ability1()
    {
        enemy.ChooseTarget();
        if(enemy.AttackRoll())
        {
            animator.SetTrigger("attack");
            enemy.Attack(2, 3);
            enemy.target.Poisoned(2, Random.Range(2, 4));
            enemy.CombatLog(gameObject.name + " poisoned " + enemy.target.name);
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        enemy.EndTurn();
    }

    //Debuff the target for -3 atk roll 
    public void Ability2()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            animator.SetTrigger("summon");
            enemy.target.TempAttackRoll(2, -3);
            enemy.CombatLog(gameObject.name + " debuffed " + enemy.target.name + "'s attack roll");
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        enemy.EndTurn();
    }

    

}
