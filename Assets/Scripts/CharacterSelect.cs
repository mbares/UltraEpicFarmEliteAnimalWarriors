using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour {

    public GameObject character;

    private bool selected = false;
    private bool alphaChangeable = true;
    private SpriteRenderer spriteRenderer;
    private LevelManager levelManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if (!selected && levelManager.playerGroup.Count == 3 && alphaChangeable)
        {
            alphaChangeable = false;
            changeAlpha(0f);
        }        
        else if(!selected && !alphaChangeable && levelManager.playerGroup.Count < 3)
        {
            alphaChangeable = true;
            changeAlpha(0.1f);
        }
    }
    private void OnMouseEnter()
    {
        if (!selected && levelManager.playerGroup.Count < 3)
        {
            changeAlpha(0.5f);
        }          
    }

    private void OnMouseExit()
    {
        if (!selected && levelManager.playerGroup.Count < 3)
        {
            changeAlpha(0.1f);
        }
    }

    private void OnMouseDown()
    {
        if (levelManager.playerGroup.Count < 3 && !levelManager.playerGroup.Contains(character))
        {
            levelManager.playerGroup.Add(character);
            changeAlpha(1f);
            selected = true;
        }
        else 
        {
            levelManager.playerGroup.Remove(character);
            selected = false;
            if (levelManager.playerGroup.Count < 3)
            {
                changeAlpha(0.1f);
            }
        }
    }

    private void changeAlpha(float value)
    {
        Color tmp = spriteRenderer.color;
        tmp.a = value;
        spriteRenderer.color = tmp;
    }
}
