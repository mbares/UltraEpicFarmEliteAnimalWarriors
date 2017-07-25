using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterStats : MonoBehaviour
{
    public int health;
    public int initiative = 0;
    public int armor;
    public int tempArmor = 0;
    public int tempAttackRoll = 0;   
    public int bonusDamage = 0;
    public bool isFriendly = false;
    public SpriteRenderer spotLightRenderer;

    public static GameObject isMouseTargeted = null;
    public static CharacterStats onTurn = null;

    private GameManager gameManager;
    public bool taunt = false;
    private bool isPoisoned = false;
    private int poisonDamage = 0;
    private int poisonTurnCounter = 0;
    private int tempArmorCounter = 0;
    private int tempAttackRollCounter = 0;    
    private int bonusDamageCounter = 0;
    private bool died = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        initiative += Random.Range(1, 21);
    }

    private void Update()
    {
        if (isMouseTargeted != this.gameObject)
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

        if(health <= 0 && !died)
        {
            died = true;
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
    void Die()
    {
        Destroy(gameObject);
        foreach (CharacterStats character in gameManager.turnOrder)
        {
            if(character == null)
            {
                gameManager.turnOrder.Remove(character);
            }
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
        isMouseTargeted = this.gameObject;
        
        if (isFriendly && isMouseTargeted == this.gameObject)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (!isFriendly && isMouseTargeted == this.gameObject)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void Poisoned(int turns, int damagePerTurn)
    {
        isPoisoned = true;
        poisonDamage = damagePerTurn;
        poisonTurnCounter = turns;
    }

    public void TempArmor(int turns, int value)
    {
        tempArmor = value;
        tempArmorCounter = turns;
    }

    public void TempAttackRoll(int turns, int value)
    {
        tempAttackRoll = value;
        tempAttackRollCounter = turns;
    }

    public void BonusDamage(int turns, int value)
    {
        bonusDamage = value;
        bonusDamageCounter = turns;
    }


    public void TurnStatCalculation()
    {
        if(taunt)
        {
            taunt = false;
        }
        if(onTurn && isPoisoned)
        {
            health -= poisonDamage;
            poisonTurnCounter--;
            Debug.Log(gameObject.name + " suffers " + poisonDamage + " poison damage");
            if(poisonTurnCounter == 0)
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
