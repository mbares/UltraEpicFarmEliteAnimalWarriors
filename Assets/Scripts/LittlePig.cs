using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittlePig : MonoBehaviour
{
    public GameObject[] dwarves;

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
        if (CharacterStats.onTurn == this.GetComponent<CharacterStats>() && !enemy.attacked)
        {
            enemy.attacked = true;
            Attack();
        }   
        
        if (GameObject.FindObjectsOfType<LittlePig>().Length == 2 && !naturalArmorBoosted)
        {
            naturalArmorBoosted = true;
            naturalArmor = 3;
            characterStats.armor += 2;
        }
        else if (GameObject.FindObjectsOfType<LittlePig>().Length == 1 && !naturalArmorBoosted2)
        {
            naturalArmorBoosted2 = true;
            naturalArmor = 5;
            characterStats.armor += 2;
        }
    }

    public void Attack()
    {
        int randomAbility;
        randomAbility = Random.Range(1, 3);
        string ability = "Ability" + randomAbility.ToString();
        Invoke(ability, 2f);
    }

    //Attack for 2-4dmg + tempArmor
    public void Ability1()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            animator.SetTrigger("attack");
            enemy.Attack(2, 5, naturalArmor + characterStats.tempArmor);           
        }
        else
        {
            enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        }
        enemy.EndTurn();
    }

    //Buff self for +2 armor for 1 turn
    public void Ability2()
    {
        characterStats.TempArmor(1,2);
        enemy.CombatLog(gameObject.name + " missed while trying to attack " + enemy.target.name);
        enemy.EndTurn();
    }



}
