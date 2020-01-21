using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] ObjectManager manager;
    [SerializeField] Animator[] animators;
    [SerializeField] Image play;
    int count;
    bool lastAnim;
    float step = 0.01f;
    public int Count { get { return count; } }
    void Start()
    {
        //PlayerPrefs.SetInt("Tutorial", 0);
        if (PlayerPrefs.GetInt("Tutorial",0)==0)
            Invoke("StartTutorial", 0.5f);
    }
    void StartTutorial()
    {
        if (count > 2)
            return;
        animators[count].SetBool("Animation", true);
        if (count == 2)
            lastAnim = true;
    }
    public void EndAnimation()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            animators[count].SetBool("Animation", false);
            count++;
            Invoke("StartTutorial", 0.5f);
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !IsPointerOverUi()&&lastAnim)
        {
            if (count == 2)
            {
                count++;
            }
            else if(count ==3)
            {
                animators[2].SetBool("Animation", false);
                count++;
            }
        }
        if(count==4&&lastAnim)
        {
            if (play.color.a <= 0.5 || play.color.a >= 1)
            {
                step *= -1;
            }
            play.color = new Color(1, 1, 1, play.color.a + step);
        }
    }
    public void Play()
    {
        if(count== 4)
        {
            lastAnim = false;
            PlayerPrefs.SetInt("Tutorial", 1);
            play.color = Color.white;
        }
    }

    bool IsPointerOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
    }
}
