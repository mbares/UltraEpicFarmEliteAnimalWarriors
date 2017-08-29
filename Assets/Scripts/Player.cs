using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterStats characterStats;
    private GameManager gameManager;

    public CharacterStats target;
    public string ability1Text;
    public string ability2Text;
    public string ability3Text;
    public string name;

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

    public int Attack(int minDmg, int maxDmg)
    {
        int damage = Random.Range(minDmg, maxDmg) + characterStats.bonusDamage;
        target.health -= damage;
        CombatLog(name + " damaged " + target.name + " for " + damage + " damage");
        return damage;
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

    public void Ability1()
    {
        string tag = this.tag;
        switch (tag)
        {
            case "cow":
                GetComponent<Cow>().Ability1();
                break;
            case "horse":
                GetComponent<Horse>().Ability1();
                break;
            case "chicken":
                GetComponent<Chicken>().Ability1();
                break;
        }
    }

    public void Ability2()
    {
        string tag = this.tag;
        switch (tag)
        {
            case "cow":
                GetComponent<Cow>().Ability2();
                break;
            case "horse":
                GetComponent<Horse>().Ability2();
                break;
            case "chicken":
                GetComponent<Chicken>().Ability2();
                break;
        }
    }

    public void Ability3()
    {
        string tag = this.tag;
        switch (tag)
        {
            case "cow":
                GetComponent<Cow>().Ability3();
                break;
            case "horse":
                GetComponent<Horse>().Ability3();
                break;
            case "chicken":
                GetComponent<Chicken>().Ability3();
                break;
        }
    }
}
