using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBadWolf : MonoBehaviour
{

    private Enemy enemy;
    private CharacterStats[] possibleTargets;
    private Animator animator;
    private bool doDoubleAttack = true;

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
        Ability1();
    }

    //Attack one target twice or two targets once for 3-5 dmg
    public void Ability1()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            animator.SetTrigger("attack");
            enemy.Attack(3, 6);
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        if(doDoubleAttack)
        {
            doDoubleAttack = false;
            Ability1();            
        }
        else
        {
            doDoubleAttack = true;
            enemy.EndTurn();
        }
        
    }        
}
