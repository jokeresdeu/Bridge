using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWiner : MonoBehaviour
{
    [SerializeField] MenuController menu;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.name == "Finish")
        {
            menu.GameWone();
        }
    }
}
