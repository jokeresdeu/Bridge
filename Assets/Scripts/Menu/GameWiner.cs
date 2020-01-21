using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWiner : MonoBehaviour
{
    AudioManager audioManager;
    private void Start()
    {
        audioManager = AudioManager.instanse;
    }
    [SerializeField] MenuController menu;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        audioManager.Stop("TrainMovement");
        if (coll.name == "Finish")
        {
            menu.GameWone();
        }
    }
}
