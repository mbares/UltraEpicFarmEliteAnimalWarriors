using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CharacterStats.onTurn.GetComponent<Dwarf>() == this && !enemy.attacked)
        {
            enemy.attacked = true;
            Invoke("Attack", 2f);           
        }
    }

    private void Attack()
    {
        enemy.ChooseTarget();
        animator.SetTrigger("attack");
        if (enemy.AttackRoll())
        {
            enemy.Attack(1, 5);
        }
        enemy.EndTurn();
    }



}
