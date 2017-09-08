using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horgana : MonoBehaviour
{

    private Enemy enemy;
    private CharacterStats[] possibleTargets;
    private Animator animator;
    private bool usedDebuff = false;
    int debuffTurnCounter = 0;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CharacterStats.onTurn == GetComponent<CharacterStats>() && !enemy.attacked)
        {
            enemy.attacked = true;
            Attack();           
        }
    }
        
    public void Attack()
    {
        int randomAbility;
        
        if (usedDebuff && debuffTurnCounter < 2)
        {
            randomAbility = Random.Range(1, 3);
            debuffTurnCounter++;
        }
        else
        {
            debuffTurnCounter = 0;
            usedDebuff = false;
            randomAbility = Random.Range(1, 4);
        }
       
        string ability = "Ability" + randomAbility.ToString();
        Invoke(ability, 2f);
    }

    //Attack a target for 4-8 dmg
    public void Ability1()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            animator.SetTrigger("boogie");
            enemy.Attack(4, 9);
        }
        enemy.EndTurn();
    }

    //Attack all targets for 2 - 5 dmg
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
                    enemy.Attack(2, 6);
                }
            }
        }
        enemy.EndTurn();
    }

    //Debuff all targets for -3 armor for 3 turns
    public void Ability3()
    {
        usedDebuff = true;
        possibleTargets = FindObjectsOfType<CharacterStats>();
        foreach (CharacterStats character in possibleTargets)
        {
            if (character.isFriendly)
            {
                enemy.target = character;
                if (enemy.AttackRoll())
                {
                    animator.SetTrigger("blah");
                    enemy.target.SetTempArmor(3, -3);
                    enemy.target.StatusEffect(new Color(0.22f, 0, 0.02f));
                    enemy.CombatLog(gameObject.name + " debuffed " + enemy.target.GetComponent<Player>().name + "'s armor");
                }
            }
        }
        enemy.EndTurn();
    }
}
