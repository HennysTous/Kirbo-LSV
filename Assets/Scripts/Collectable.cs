using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    healthHeart,
    croix,
    coin
}

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.coin;

    private SpriteRenderer sprite;

    private Collider2D itemCollider;

    bool hasBeenCollected = false;

    public int value = 1;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Collect();
        }
    }

    void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
    }

    void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;

    }

    void Collect()
    {
        Hide();
        hasBeenCollected = true;

        switch (this.type)
        {
            case CollectableType.coin:
                GameManager.sharedInstance.CollectObject(this);
                break;

            case CollectableType.croix:
                break;

            case CollectableType.healthHeart:
                break;


        }
    }
}
