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
            Attack();
            enemy.attacked = true;
        }
    }

    private void Attack()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            int damage = Random.Range(1, 3);
            enemy.target.health -= damage;
            enemy.CombatLog(gameObject.name + " damaged " + enemy.target.name + " for " + damage + " damage");
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        enemy.EndTurn();
    }



}
