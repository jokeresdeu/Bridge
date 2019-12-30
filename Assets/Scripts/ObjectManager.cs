using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    [Header("InventorySlots")]
    [SerializeField] Image[] inventorySlots;
    [SerializeField] Sprite defaultSprite;

    [Header("Mover")]
    [SerializeField] HandController hand;

    [SerializeField] int objectsToPick;
    ObjectController temp;

    bool isStarted = false;
    public bool IsStarted { get { return isStarted; }  }
    bool trainMoved;
    public bool TrainMoved { set { trainMoved = value; } }
    List<ObjectController> objects = new List<ObjectController>();
    public List<ObjectController> Objects { get { return objects; } }
    int count=0;
    public static int restartsAmount;
  

   

    #region SingLeton
    public static ObjectManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    #endregion


    public void TakeObject(ObjectController obj)
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1)
            return;
        if (isStarted)
            return;
        obj.Coll.enabled = false;
        obj.Sprite.enabled = false;
        obj.Rb.bodyType = RigidbodyType2D.Static;
        
        objects.Add(obj);
        if(objects.Count <=3)
            inventorySlots[objects.Count - 1].sprite = obj.SavedSprite;
        if (objects.Count == objectsToPick)
        {
            isStarted = true;
            StartMovement();
        }
            
    }

    public void StartMovement()
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1)
            return;
        if(count<objects.Count)
        {
           
            temp = objects[count];
            temp.transform.rotation = temp.Original;
            temp.Coll.enabled = true;
            Vector2 offset = new Vector2(0f, objects[count].Coll.bounds.size.y / 2);
            hand.TakeObject(objects[count].gameObject, offset);
            temp.Sprite.enabled = true;
        }
        else
            hand.TakeObject(null, Vector2.zero);
    }
    public void Release()
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1)
            return;
        if (temp != null)
        {
            temp.Rb.velocity = Vector2.zero;
            temp.Rb.bodyType = RigidbodyType2D.Dynamic;
            temp.Rb.gravityScale = 3;
            temp = null;
            hand.TakeObject(null, Vector2.zero);
            RemoveObject();
            Invoke("StartMovement", 1f);
        }
    }
    public void RemoveObject()
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1)
            return;
        
        if (objects.Count - count - 1 > 2)
        {
            count++;
            for (int i = 0; i < 3; i++)
                inventorySlots[i].sprite = objects[count + i].SavedSprite;
        }
        else
        {
            Debug.Log(count);
            inventorySlots[objects.Count - count - 1].sprite = defaultSprite;
            count++;
            for (int i = 0; i < objects.Count - count; i++)
            {
                inventorySlots[i].sprite = objects[count + i].SavedSprite;
                Debug.Log(objects[count + i].SavedSprite.name);
            }
        }
    }
    public void UndoStart()
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1|| trainMoved)
            return;
        for (int i = 0; i < objects.Count; i++)
        {

            objects[i].Coll.enabled = false;
            objects[i].Sprite.enabled = false;
            objects[i].Rb.gravityScale = 0;
            if (i < 3)
                inventorySlots[i].sprite = objects[i].SavedSprite;

        }
        count = 0;
        StartMovement();
    }

}
