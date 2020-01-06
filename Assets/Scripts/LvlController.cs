using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlController : MonoBehaviour
{
    [SerializeField] int lvl;
    [SerializeField] Image[] stars;
    [SerializeField] Sprite star;
    [SerializeField] GameObject locked;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        ActivateLvl();
    }

    void ActivateLvl()
    {
        int isLvlOpen = PlayerPrefs.GetInt("LvlOpened" + lvl, 0);
        if (isLvlOpen == 1||lvl==1)
        {
            locked.SetActive(false);
            button.enabled = true;
            int starsAmount = PlayerPrefs.GetInt("Stars" + lvl, 0);
            if (starsAmount > 0)
                for (int i = 0; i < starsAmount; i++)
                {
                    stars[i].sprite = star;
                }
        } 
    }
}
