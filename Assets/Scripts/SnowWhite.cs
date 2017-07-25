using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowWhite : MonoBehaviour
{
    public GameObject [] dwarves;
    
    
    private Enemy enemy;
    private GameManager gameManager;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (CharacterStats.onTurn == this.GetComponent<CharacterStats>() && !enemy.attacked)
        {
            Attack();
            enemy.attacked = true;
        }
    }

    public void Attack()
    {
        int randomAbility;
        randomAbility = Random.Range(1, 3);
        string ability = "Ability" + randomAbility.ToString();
        Invoke(ability, 1f);
    }

    public void Ability1()
    {
        enemy.ChooseTarget();
        if(enemy.AttackRoll())
        {
            enemy.target.health -= 1;
            enemy.target.Poisoned(2, Random.Range(1,3));
            enemy.CombatLog(gameObject.name + " poisoned " + enemy.target.name);
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        enemy.EndTurn();
    }

    public void Ability2()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
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
