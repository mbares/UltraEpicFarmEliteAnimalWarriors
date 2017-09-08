using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CharacterStats target;
    public bool attacked = false;
    public string name;

    private CharacterStats characterStats;    
    private CharacterStats[] possibleTargets;
    private GameManager gameManager;
    private int maxHealth;
    private bool badMessageSent = false;
    private bool worseMessageSent = false;
    private bool worstMessageSent = false;


    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        gameManager = FindObjectOfType<GameManager>();
        maxHealth = characterStats.maxHealth;
    }

    private void Update()
    {
        if (((float)characterStats.GetHealth() / (float)maxHealth) < 0.5f && !badMessageSent && !characterStats.dead)
        {
            badMessageSent = true;
            CombatLog(name + " is starting to look bad");
        }
        
        else if (((float)characterStats.GetHealth() / (float)maxHealth) < 0.25f && !worseMessageSent && !characterStats.dead)
        {
            worseMessageSent = true;
            CombatLog(name + " is looking really bad ");
        }
        else if (((float)characterStats.GetHealth() / (float)maxHealth) < 0.1f && !worstMessageSent && !characterStats.dead)
        {
            worstMessageSent = true;
            CombatLog(name + " is looking really, really bad ");
        }
    }

    public void ChooseTarget()
    {        
        possibleTargets = FindObjectsOfType<CharacterStats>();        
        foreach (CharacterStats character in possibleTargets)
        {
            if(character.isFriendly && character.IsTaunting())
            {               
                target = character;              
                return;
            }
        }
        while (!target)
        {
            int randomTarget = Random.Range(0, possibleTargets.Length);
            if (possibleTargets[randomTarget].isFriendly)
            {
                target = possibleTargets[randomTarget];
            }
        }
    }

    public void Attack(int minDmg, int maxDmg, int extraDamage = 0)
    {
        int damage = Random.Range(minDmg, maxDmg) + characterStats.GetBonusDamage() + characterStats.GetDifficultyDamage() + extraDamage;
        target.ReduceHealth(damage);
        CombatLog(name + " damaged " + target.GetComponent<Player>().name + " for " + damage + " damage");
    }

    public bool AttackRoll()
    {
        if ((Random.Range(1, 21) + characterStats.GetTempAttackRoll() + characterStats.GetDifficultyAttackRoll() + characterStats.attackRoll) >= (target.armor + target.GetTempArmor()))
        {
            return true;
        }
        CombatLog(name + " missed while trying to attack " + target.GetComponent<Player>().name);
        return false;
    }

    public void EndTurn()
    {
        Invoke("EndEnemyTurn", 1.5f);    
    }

    public void CombatLog(string log)
    {
        gameManager.CombatLog(log);
    }

    private void EndEnemyTurn()
    {
        attacked = false;
        gameManager.NextTurn();
        target = null;
    }    
 
}
