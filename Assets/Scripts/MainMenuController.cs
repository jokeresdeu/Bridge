using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject inGameShop;
    [SerializeField] Image sound;
    [SerializeField] Sprite soundOff;
    [SerializeField] Sprite soundOn;
    [SerializeField] bool reset;
    private void Start()
    {
        if (reset)
            Reset();
    }
    public void LoadLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
        
    }

    private void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Sound()
    {
        int x;
        x = PlayerPrefs.GetInt("Sound", 0);
        if (x == 0)
        {
            x = 1;
            sound.sprite = soundOff;
        }
        else if (x == 1)
        {
            x = 0;
            sound.sprite = soundOn;
        }
        PlayerPrefs.SetInt("Sound", x);
    }

    public void InGameShop()
    {
        int x = PlayerPrefs.GetInt("Pause", 0);
        if (x == 1 && !inGameShop.activeInHierarchy)
            return;
        inGameShop.SetActive(!inGameShop.activeInHierarchy);
        x -= 1;
        x = Mathf.Abs(x);
        PlayerPrefs.SetInt("Pause", x);
    }
}
