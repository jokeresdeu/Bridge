using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    Rigidbody2D rb;
    public Rigidbody2D Rb { get{ return rb; } }
    Collider2D coll;
    public Collider2D Coll { get { return coll; } }
    [SerializeField] Sprite savedSprite;
    public Sprite SavedSprite { get { return savedSprite; } }
    SpriteRenderer sprite;
    Quaternion original;
    public Quaternion Original { get { return original; } }
    public SpriteRenderer Sprite { get { return sprite; } }
    private void Start()
    {
        original = transform.rotation;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }
}
