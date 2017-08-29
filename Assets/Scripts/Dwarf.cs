using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{
    private Enemy enemy;    

    private void Start()
    {
        enemy = GetComponent<Enemy>();        
    }

    private void Update()
    {
        if (CharacterStats.onTurn == this.GetComponent<CharacterStats>() && !enemy.attacked)
        {
            enemy.attacked = true;
            Invoke("Attack", 2f);           
        }
    }

    private void Attack()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            enemy.Attack(1, 4);
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        enemy.EndTurn();
    }



}
