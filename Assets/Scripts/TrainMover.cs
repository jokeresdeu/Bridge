using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMover : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]float speed;
    bool move = false;
    ObjectManager @object;

    bool rGrounded;
    bool lGrounded;
    
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform rWheel;
    [SerializeField] Transform lWheel;
    [SerializeField] float radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(rWheel.position, radius);
        Gizmos.DrawWireSphere(lWheel.position, radius);
    }

    // Start is called before the first frame update
    void Start()
    {
        @object = ObjectManager.instance;
        rb = GetComponent<Rigidbody2D>();
    }
    public void MoveTheTrain()
    {
        if(@object.IsStarted)
        {
            move = true;
            @object.TrainMoved = true;
        }
           
    }
    private void FixedUpdate()
    {
        rGrounded = Physics2D.OverlapCircle(rWheel.position, radius, layerMask);
        lGrounded = Physics2D.OverlapCircle(lWheel.position, radius, layerMask);
        if (move&&(rGrounded || lGrounded))
                rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!move && collision.collider.CompareTag("Objects"))
        {
            Destroy(gameObject);
        }

    }
}
