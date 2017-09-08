using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth;
    public int initiative;
    public int armor;
    public int attackRoll;
    public int difficultyHealthModifier;
    public int difficultyArmorModifier;
    public int difficultyDamageModifier;
    public int difficultyAttackRollModifier;
    public bool isFriendly = false;
    public SpriteRenderer spotLightRenderer;
    public GameObject statusEffect;
    public bool dead = false;

    public static GameObject mouseTargetedCharacter = null;
    public static CharacterStats onTurn = null;

    private SpriteRenderer statusEffectRenderer;
    private Animator animator;
    private GameManager gameManager;
    private bool taunting = false;
    private bool isPoisoned = false;
    private int difficultyAttackRoll;
    private int difficultyDamage;
    [SerializeField]    
    private int health;
    private int tempArmor = 0;
    private int tempAttackRoll = 0;
    private int bonusDamage = 0;
    private int poisonDamage = 0;
    private int poisonTurnCounter = 0;
    private int tempArmorCounter = 0;
    private int tempAttackRollCounter = 0;    
    private int bonusDamageCounter = 0;


    private void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        statusEffectRenderer = statusEffect.GetComponent<SpriteRenderer>();
        statusEffect.SetActive(false);
        if (PlayerPrefsManager.GetDifficulty() == 1 && !isFriendly)
        {
            health -= difficultyHealthModifier;
            armor -= difficultyArmorModifier;
            difficultyAttackRoll = -1;
            difficultyDamage = -difficultyDamageModifier;
            
        }
        else if (PlayerPrefsManager.GetDifficulty() == 3 && !isFriendly)
        {
            health += difficultyHealthModifier;
            armor += difficultyArmorModifier;
            difficultyAttackRoll = difficultyAttackRollModifier;
            difficultyDamage = difficultyDamageModifier;
        }
    }

    private void Update()
    {
        if (mouseTargetedCharacter != gameObject)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (onTurn == this)
        {
            Color onTurn = spotLightRenderer.color;
            onTurn.a = 1f;
            spotLightRenderer.color = onTurn;
        }
        else
        {
            Color notOnTurn = spotLightRenderer.color;
            notOnTurn.a = 0f;
            spotLightRenderer.color = notOnTurn;
        }
    }

    private void LateUpdate()
    {
        if (mouseTargetedCharacter == gameObject)
        {
            if (isFriendly)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.36f, 0.96f, 0.23f);
            }
            else if (!isFriendly)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.97f, 0.25f, 0.25f);
            }
        }
    }

    public bool IsPoisoned()
    {
        return isPoisoned;
    }

    public void SetPoisonTurnCounter (int turns)
    {
        poisonTurnCounter = turns;
    }

    public int GetHealth()
    {
        return health;
    }

    public void ReduceHealth(int hp)
    {
        animator.SetTrigger("attacked");
        health -= hp;
        if(health <= 0)
        {
            dead = true;
            if (isFriendly)
            {
                Player player = GetComponent<Player>();
                player.CombatLog(player.name + " died");
            }
            else
            {
                Enemy enemy = GetComponent<Enemy>();
                enemy.CombatLog(enemy.name + " died");
            }
            DeadBlinkOff();
            Invoke("DeadBlinkOn", 0.2f);
            Invoke("DeadBlinkOff", 0.4f);
            Invoke("DeadBlinkOn", 0.6f);
            Invoke("DeadBlinkOff", 0.8f);
            Invoke("DeadBlinkOn", 1.0f);
            Invoke("DeadBlinkOff", 1.2f);
            Invoke("Die", 1.2f);
        }
    }

    public void IncreaseHealth(int hp)
    {
        if ((health + hp) > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += hp;
        }
        StatusEffect(new Color(1f, 0.96f, 0f));
    }

    void Die()
    {
        if (mouseTargetedCharacter = gameObject)
        {
            mouseTargetedCharacter = null;
        }
        if (onTurn == this)
        {
            gameManager.NextTurn();
        }

        gameManager.turnOrder.Remove(this);
        if (isFriendly)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void DeadBlinkOff()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void DeadBlinkOn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnMouseDown()
    {
        mouseTargetedCharacter = gameObject;
    }

    public void SetPoisoned(int turns, int damagePerTurn)
    {
        isPoisoned = true;
        poisonDamage = damagePerTurn;
        poisonTurnCounter = turns;
        StatusEffect(Color.green);
    }

    public void StatusEffect(Color color)
    {
        Color tmp = color;
        tmp.a = 0.7f;
        statusEffectRenderer.color = tmp;
        statusEffect.SetActive(true);
        Invoke("StatusEffectOff", 1.3f);
    }

    private void StatusEffectOff()
    {
        Color tmp = Color.white;
        tmp.a = 0f;
        statusEffectRenderer.color = tmp;
        statusEffect.SetActive(false);
    }

    public void SetTaunting(bool taunting)
    {
        this.taunting = taunting;
    }

    public bool IsTaunting()
    {
        return taunting;
    }

    public void SetTempArmor(int turns, int value)
    {
        tempArmor = value;
        tempArmorCounter = turns;
    }

    public int GetTempArmor()
    {
        return tempArmor;
    }

    public void SetTempAttackRoll(int turns, int value)
    {
        tempAttackRoll = value;
        tempAttackRollCounter = turns;
    }

    public int GetTempAttackRoll()
    {
        return tempAttackRoll;
    }

    public void SetBonusDamage(int turns, int value)
    {
        bonusDamage = value;
        bonusDamageCounter = turns;
    }

    public int GetBonusDamage()
    {
        return bonusDamage;
    }

    public int GetDifficultyAttackRoll()
    {
        return difficultyAttackRoll;
    }

    public int GetDifficultyDamage()
    {
        return difficultyDamage;
    }

    public void TurnStatCalculation()
    {
        if(taunting)
        {
            taunting = false;
        }
        if (onTurn && isPoisoned)
        {
            poisonTurnCounter--;
            if (GetComponent<Player>() != null)
            {
                GetComponent<Player>().CombatLog(GetComponent<Player>().name + " suffers " + poisonDamage + " poison damage.");
            }
            else
            {
                GetComponent<Enemy>().CombatLog(GetComponent<Enemy>().name + " suffers " + poisonDamage + " poison damage.");
            }
            ReduceHealth(poisonDamage);
            if (poisonTurnCounter == 0)
            {
                isPoisoned = false;
            }
        }

        if (onTurn && tempArmor != 0)
        {
            tempArmorCounter--;
            if(tempArmorCounter == 0)
            {
                tempArmor = 0;
            }
        }

        if (onTurn && bonusDamage != 0)
        {
            bonusDamageCounter--;
            if (bonusDamageCounter == 0)
            {
                bonusDamage = 0;
            }
        }

        if (onTurn && tempAttackRoll != 0)
        {
            tempAttackRollCounter--;
            if (tempAttackRollCounter == 0)
            {
                tempAttackRoll = 0;
            }
        }        
    }
}
