using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailController : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    SpriteRenderer sprite;
    ObjectManager @object;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll= GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        @object = ObjectManager.instance;
    }
    public void Play()
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1)
            return;
        if (!@object.IsStarted)
            return;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.mass = 20;
        coll.enabled = true;
        sprite.color = Color.white;
    }
}
