using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    AudioManager audioManager;
    Rigidbody2D rb;
    public Rigidbody2D Rb { get{ return rb; } }
    Collider2D coll;
    public Collider2D Coll { get { return coll; } }
    [SerializeField] Transform pointForOffset;
    SpriteRenderer sprite;
    public SpriteRenderer Sprite { get { return sprite; } }
    Quaternion original;
    public Quaternion Original { get { return original; } }
    Vector2 offset;
    public Vector2 Offset { get { return offset; } }
    bool canPlay;
    public bool CanPlay { set{ canPlay=value; } }

    private void Start()
    {
        original = transform.rotation;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        if (pointForOffset != null)
            offset = pointForOffset.position - transform.position;
        else offset = new Vector2(0, coll.bounds.size.y / 2);
        audioManager = AudioManager.instanse;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canPlay||audioManager.IsPlaying("FigureDrop"))
            return;
        if(rb.velocity.y<0.2f)
            audioManager.Play("FigureDrop");
    }
}
