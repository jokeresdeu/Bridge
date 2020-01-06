using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsPicker : MonoBehaviour
{
    [SerializeField] LayerMask whatIsObject;

    ObjectManager objectManager;
    Camera cam;
    bool takeObject = false;
    Vector2 position;
    void Start()
    {
        cam = Camera.main;
        objectManager = ObjectManager.instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            position = cam.ScreenToWorldPoint(Input.mousePosition);
            takeObject = true;
        }
    }
    private void FixedUpdate()
    {
        if (takeObject)
            TakeObject();
    }
    void TakeObject()
    {
        takeObject = false;
        Collider2D coll = Physics2D.OverlapPoint(position, whatIsObject);
        if (coll != null)
        {
            objectManager.TakeObject(coll.GetComponent<ObjectController>());
        }
    }
}
