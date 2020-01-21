using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMover : MonoBehaviour
{
    Rigidbody2D rb;
    ObjectManager @object;
    bool dead;
    bool move = false;
    bool rGrounded;
    bool lGrounded;
    int current = 0;
    AudioManager audioManager;
    [Header("Move")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform rWheel;
    [SerializeField] Transform lWheel;
    [SerializeField] float radius;
    [SerializeField] float speed;

    [Header("Death")]
    [SerializeField] GameObject[] deathEfects;
    [SerializeField] GameObject[] trainParts;
    [SerializeField] float timeDelay;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(rWheel.position, radius);
        Gizmos.DrawWireSphere(lWheel.position, radius);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instanse;
        @object = ObjectManager.instance;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
    public void MoveTheTrain()
    {
        if(@object.IsStarted)
        {
            audioManager.Play("TrainMovement");
            move = true;
            rb.constraints = RigidbodyConstraints2D.None;
            @object.TrainMoved = true;
        }
           
    }
    private void FixedUpdate()
    {
        rGrounded = Physics2D.OverlapCircle(rWheel.position, radius, layerMask);
        lGrounded = Physics2D.OverlapCircle(lWheel.position, radius, layerMask);
        if (move&&(rGrounded || lGrounded))
                rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
        if ((rb.position.y < -3||Mathf.Abs(rb.rotation) > 60) && !dead)
        {
            Death();
            dead = true;
        }
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!move && collision.collider.CompareTag("Objects"))
        {
            Death();
        }
    }
    void Death()
    {
        if (transform.position.x > 16)
            return;
        if(!audioManager.IsPlaying("Explosion")&& (audioManager.IsPlaying("TrainMovement")||current!=0))
           audioManager.Play("Explosion");
        audioManager.Stop("TrainMovement");
        int x = Random.Range(0, deathEfects.Length-1);
        Destroy(Instantiate(deathEfects[x], trainParts[current].transform.position, Quaternion.identity), timeDelay - 0.1f);
        trainParts[current].GetComponent<SpriteRenderer>().enabled = false;
        trainParts[current].GetComponent<CapsuleCollider2D>().enabled = false;
        try
        {
            Destroy(trainParts[current].GetComponent<SpringJoint2D>());
        }
        catch
        {
            Debug.Log("Here");
        }
        speed = 0;
        current++;
        if (current < trainParts.Length)
        {
            Invoke("Death", timeDelay);
        }
    }
}
