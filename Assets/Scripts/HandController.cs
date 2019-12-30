using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] GameObject pointOfConection;
    Vector2 offset;
    [SerializeField]float speed;
    int direction = 1;
    GameObject objectToMove;
    bool needToMove;
    bool start;
    void Update()
    {
        if (needToMove)
        {
            Move();
        }     
    }
    public void TakeObject(GameObject obj, Vector2 offset)
    {
        if (obj == null)
        {
            needToMove = false;
            objectToMove = null;
        } 
        else
        {
            objectToMove = obj;
            this.offset = offset;
            objectToMove.transform.position = (Vector2)pointOfConection.transform.position - offset;
            needToMove = true;
        }
    }
    private void Move()
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1)
            return;
        if (objectToMove != null)
             objectToMove.transform.position = (Vector2)pointOfConection.transform.position - offset;
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Borders"))
        {
            direction *= -1;
        }
    }

}
