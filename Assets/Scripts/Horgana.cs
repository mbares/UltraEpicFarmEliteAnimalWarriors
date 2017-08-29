using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horgana : MonoBehaviour
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
       
        randomAbility = Random.Range(1, 4);
        string ability = "Ability" + randomAbility.ToString();
        Invoke(ability, 2f);
    }

    //Attack a target for 3-5 dmg
    public void Ability1()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            animator.SetTrigger("boogie");
            enemy.Attack(3, 6);
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        enemy.EndTurn();
    }

    //Attack all targets for 2 dmg
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
                    animator.SetTrigger("fire");
                    enemy.Attack(2, 3);
                }
                else
                {
                    enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
                }
            }
        }
        enemy.EndTurn();
    }

    //Debuff all targets for -3 armor for 3 turns
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
                    animator.SetTrigger("blah");
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
