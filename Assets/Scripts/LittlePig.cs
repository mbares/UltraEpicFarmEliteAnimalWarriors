using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittlePig : MonoBehaviour
{
    private CharacterStats characterStats;
    private Enemy enemy;
    private Animator animator;
    private int naturalArmor = 1;
    private bool naturalArmorBoosted = false;
    private bool naturalArmorBoosted2 = false;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();

        characterStats.armor += naturalArmor;
    }

    private void Update()
    {
        if (CharacterStats.onTurn == GetComponent<CharacterStats>() && !enemy.attacked)
        {
            enemy.attacked = true;
            Attack();
        }   
        
        if (FindObjectsOfType<LittlePig>().Length == 2 && !naturalArmorBoosted)
        {
            naturalArmorBoosted = true;
            naturalArmor = 3;
            characterStats.armor += 2;
            characterStats.StatusEffect(Color.white);
            enemy.CombatLog(enemy.name + " looks more determined after you killed his brother");
        }
        else if (FindObjectsOfType<LittlePig>().Length == 1 && !naturalArmorBoosted2)
        {
            naturalArmorBoosted2 = true;
            naturalArmor = 5;
            characterStats.armor += 2;
            characterStats.StatusEffect(Color.white);
            enemy.CombatLog(enemy.name + " looks so determined after you killed his brother that his name is now Determinator");
            enemy.name = "Determinator";
        }
    }

    public void Attack()
    {
        if (Random.Range(1, 6) % 2 == 0)
        {
            Invoke("Ability2", 2f);
        }
        else
        {
            Invoke("Ability1", 2f);
        }
    }

    //Attack for 2-5 dmg + tempArmor
    public void Ability1()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            animator.SetTrigger("attack");
            enemy.Attack(2, 6, naturalArmor + characterStats.GetTempArmor());           
        }
        enemy.EndTurn();
    }

    //Buff self for +2 armor for 1 turn
    public void Ability2()
    {
        characterStats.SetTempArmor(2,2);
        characterStats.StatusEffect(Color.white);
        enemy.CombatLog(enemy.name + " buffs himself for +2 armor for 1 turn");
        enemy.EndTurn();
    }



}
