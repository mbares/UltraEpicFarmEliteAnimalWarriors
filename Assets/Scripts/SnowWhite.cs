using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowWhite : MonoBehaviour
{

    public List<GameObject> dwarfs;

    private Enemy enemy;
    private Animator animator;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        ShuffleDwarfList();
        for (int i = 0; i < 3; i++)
        {
            GameObject newDwarf = Instantiate(dwarfs[i]);
            gameManager.turnOrder.Add(newDwarf.GetComponent<CharacterStats>());
            dwarfs.Remove(dwarfs[i]);
        }
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
        if (FindObjectsOfType<Dwarf>().Length >= 3 || dwarfs.Count <= 0)
        {
            randomAbility = Random.Range(1, 3);
        }
        else
        {
            randomAbility = Random.Range(1, 12);
            if (randomAbility < 6)
            {
                randomAbility = 1;
            } 
            else if (randomAbility < 11)
            {
                randomAbility = 2;
            }
            else
            {
                randomAbility = 3;
            }
        }
        string ability = "Ability" + randomAbility.ToString();
        Invoke(ability, 2f);
    }

    //Attack for 2-6 dmg and poison the target for 1-3 dmg for 2 rounds
    public void Ability1()
    {
        enemy.ChooseTarget();
        if(enemy.AttackRoll())
        {
            animator.SetTrigger("attack");
            enemy.Attack(2, 7, FindObjectsOfType<Dwarf>().Length * 1);
            enemy.target.SetPoisoned(2, Random.Range(1, 4));
            enemy.CombatLog(enemy.name + " poisoned " + enemy.target.GetComponent<Player>().name);
        }
        enemy.EndTurn();
    }

    //Debuff the target for -3 atk roll 
    public void Ability2()
    {
        enemy.ChooseTarget();
        if (enemy.AttackRoll())
        {
            enemy.target.SetTempAttackRoll(3, -3);
            enemy.target.StatusEffect(new Color(0.24f, 0, 0.47f)); 
            enemy.CombatLog(enemy.name + " debuffed " + enemy.target.GetComponent<Player>().name + "'s attack roll for 2 turns");
        }
        enemy.EndTurn();
    }

    //Summon another dwarf
    public void Ability3()
    {
        animator.SetTrigger("summon");
        GameObject newDwarf = Instantiate(dwarfs[0]);
        gameManager.turnOrder.Add(newDwarf.GetComponent<CharacterStats>());
        dwarfs.Remove(dwarfs[0]);
        enemy.CombatLog(enemy.name + " called for help and another dwarf appeared");
        enemy.EndTurn();
    }

    private void ShuffleDwarfList()
    {
        for (int i = 0; i < dwarfs.Count; i++)
        {
            GameObject temp = dwarfs[i];
            int randomIndex = Random.Range(i, dwarfs.Count);
            dwarfs[i] = dwarfs[randomIndex];
            dwarfs[randomIndex] = temp;
        }
    }

}
