using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horgana : MonoBehaviour
{

    private Enemy enemy;
    private GameManager gameManager;
    private CharacterStats[] possibleTargets;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        gameManager = FindObjectOfType<GameManager>();
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
       
        randomAbility = Random.Range(1, 4);
        string ability = "Ability" + randomAbility.ToString();
        Invoke(ability, 1f);
    }

    public void Ability1()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            int damage = Random.Range(3, 5);
            enemy.target.health -= damage;
            enemy.CombatLog(gameObject.name + " damaged " + enemy.target.name + " for " + damage + " damage");
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        enemy.EndTurn();
    }

    public void Ability2()
    {
        possibleTargets = GameObject.FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats character in possibleTargets)
        {
            if (character.isFriendly)
            {
                enemy.target = character;
                if (enemy.AttackRoll())
                {
                    int damage = 2;
                    enemy.target.health -= damage;
                    enemy.CombatLog(gameObject.name + " damaged " + enemy.target.name + " for " + damage + " damage");
                }
                else
                {
                    enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
                }
            }
        }
        enemy.EndTurn();
    }

    public void Ability3()
    {
        possibleTargets = GameObject.FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats character in possibleTargets)
        {
            if (character.isFriendly)
            {
                enemy.target = character;
                if (enemy.AttackRoll())
                {
                    enemy.target.TempArmor(3, -3);
                    enemy.CombatLog(gameObject.name + " debuffed " + enemy.target.name + "'s armor");
                }
                else
                {
                    enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
                }
            }
        }
        enemy.EndTurn();
    }
}
