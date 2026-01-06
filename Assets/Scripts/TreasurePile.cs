using System.Collections.Generic;
using UnityEngine;

public class TreasurePile : MonoBehaviour
{
    [SerializeField]
    CharacterStatsSO characterStats;
    public Health treasureHealth;

    [SerializeField]
    List<Sprite> sprites;
    
    private SpriteRenderer spriteRenderer;

    private float subdivision = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        treasureHealth.maxHitpoints = characterStats.treasureHealth;
        treasureHealth.currentHitpoints = characterStats.treasureHealth;
        subdivision = treasureHealth.maxHitpoints / 5;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[5];
        treasureHealth.healthChanged.AddListener(UpdateSprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateSprite()
    {
        if (treasureHealth.currentHitpoints == 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else
        {
            spriteRenderer.sprite = sprites[(int) (treasureHealth.currentHitpoints / subdivision)];
        }
    }
}
