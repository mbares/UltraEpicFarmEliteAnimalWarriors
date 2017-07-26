using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterStats characterStats;
    private GameManager gameManager;

    public CharacterStats target;
    

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public bool AttackRoll()
    {        
        if ((Random.Range(1, 21) + characterStats.tempAttackRoll) >= (target.armor + target.tempArmor))
        {
            return true;
        }
        return false;
    }

    public void ChooseTarget()
    {
        target = CharacterStats.mouseTargetedCharacter.GetComponent<CharacterStats>();
    }

    public void EndTurn()
    {
        gameManager.NextTurn();
    }

    public void CombatLog(string log)
    {
        gameManager.CombatLog(log);
    }

}
