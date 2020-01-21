using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialParts : MonoBehaviour
{
    [SerializeField] int pos;
    [SerializeField] TutorialController controller;
    private void OnMouseDown()
    {
        if (controller.Count == pos)
            controller.EndAnimation();
    }
}
