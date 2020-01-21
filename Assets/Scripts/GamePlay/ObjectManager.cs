using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectManager : MonoBehaviour
{
    AudioManager audio;

    [Header("InventorySlots")]
    [SerializeField] Image[] inventorySlots;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] int objectsToPick;

    [Header("Mover")]
    [SerializeField] HandController hand;
    Animator animator;

    [Header("ForStart")]
    [SerializeField] TrainMover train;
    [SerializeField] RailController rail;
    [SerializeField] LayerMask layerMask;

    ObjectController temp;
    bool isStarted = false;
    public bool IsStarted { get { return isStarted; }  }
    bool trainMoved;
    public bool TrainMoved { set { trainMoved = value; } }
    List<ObjectController> objects = new List<ObjectController>();
    public List<ObjectController> Objects { get { return objects; } }
    int count=0;
    public static int restartsAmount;
    bool canBeReleased;

   

    #region SingLeton
    public static ObjectManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        animator = hand.GetComponent<Animator>();
        audio = AudioManager.instanse;
    }
    public void TakeObject(ObjectController obj)
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1)
            return;
        if (isStarted)
            return;
        audio.Play("FigureClick");
        obj.Coll.enabled = false;
        obj.Sprite.enabled = false;
        obj.Rb.bodyType = RigidbodyType2D.Static;
        
        objects.Add(obj);
        if(objects.Count <=3)
        {
            inventorySlots[objects.Count - 1].sprite = obj.Sprite.sprite;
            inventorySlots[objects.Count - 1].rectTransform.rotation = obj.Original;

        }
           
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
            temp.CanPlay = true;
            temp.transform.rotation = temp.Original;
            temp.Coll.enabled = true;
            hand.TakeObject(objects[count].gameObject, temp.Offset);
            temp.Sprite.enabled = true;
            canBeReleased = true;
        }
    }
    private void Update()
    {
        if (canBeReleased && Input.GetButtonDown("Fire1"))
            Release();
    }
    public void Release()
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1||IsPointerOverUi())
            return;
       
        if (temp != null)
        {
            audio.Play("FigureClick");
            hand.TakeObject(null, Vector2.zero);
            temp.Rb.bodyType = RigidbodyType2D.Dynamic;
            temp.Rb.velocity = Vector2.zero;
            temp.Rb.gravityScale = 2;
            temp = null;
            RemoveObject();
            Invoke("StartMovement", 1f);
            animator.SetBool("Release", true);
            canBeReleased = false;
        }
    }
    bool IsPointerOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
    }
    public void RemoveObject()
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1)
            return;
        if (objects.Count - count - 1 > 2)
        {
            count++;
            for (int i = 0; i < 3; i++)
            {
                inventorySlots[i].sprite = objects[count + i].Sprite.sprite;
                inventorySlots[i].rectTransform.rotation = objects[count + i].Original;
            }
        }
        else
        {
            inventorySlots[objects.Count - count - 1].sprite = defaultSprite;
            count++;
            for (int i = 0; i < objects.Count - count; i++)
            {
                inventorySlots[i].sprite = objects[count + i].Sprite.sprite;
                inventorySlots[i].rectTransform.rotation = objects[count + i].Original;
            }
        }
    }
    public void UndoStart()
    {
        if (PlayerPrefs.GetInt("Pause", 0) == 1|| trainMoved||!isStarted)
            return;
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].CanPlay = false;
            objects[i].Coll.enabled = false;
            objects[i].Sprite.enabled = false;
            objects[i].Sprite.color = Color.white;
            objects[i].Rb.bodyType = RigidbodyType2D.Static;
            if (i < 3)
            {
                inventorySlots[i].sprite = objects[i].Sprite.sprite;
                inventorySlots[i].rectTransform.rotation = objects[i].Original;
            }
        }
        count = 0;
        restartsAmount++;
        StartMovement();
    }
    public void Play()
    {
        if (count != objects.Count)
            return;
        Collider2D[] colls = Physics2D.OverlapBoxAll(transform.position, new Vector2 (20,0.1f), 0, layerMask);
        if(colls.Length !=0)
        {
            foreach(Collider2D coll in colls)
            {
                coll.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].CanPlay = false;
            }
            train.MoveTheTrain();
            rail.Play();
        }
    }

}
