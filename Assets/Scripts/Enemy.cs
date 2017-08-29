using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterStats characterStats;    
    private CharacterStats[] possibleTargets;
    private GameManager gameManager;

    public int bonusAttackRoll = 0;
    public CharacterStats target;
    public bool attacked = false;
    public string name;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ChooseTarget()
    {        
        possibleTargets = GameObject.FindObjectsOfType<CharacterStats>();        
        foreach (CharacterStats character in possibleTargets)
        {
            if(character.isFriendly && character.taunt)
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

    public void Attack(int minDmg, int maxDmg, int bonusDamage = 0)
    {
        int damage = Random.Range(minDmg, maxDmg) + characterStats.bonusDamage + bonusDamage;
        target.health -= damage;
        CombatLog(name + " damaged " + target.GetComponent<Player>().name + " for " + damage + " damage");
    }

    public bool AttackRoll()
    {
        
        if ((Random.Range(1, 21) + characterStats.tempAttackRoll + bonusAttackRoll) >= (target.armor + target.tempArmor))
        {
            return true;
        }
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
