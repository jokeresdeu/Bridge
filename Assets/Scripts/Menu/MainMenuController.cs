using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Image sound;
    [SerializeField] Sprite soundOff;
    [SerializeField] Sprite soundOn;
    [SerializeField] bool reset;
    private void Start()
    {
        if (reset)
            Reset();
        int x = PlayerPrefs.GetInt("Sound", 0);
        if (x == 1)
            sound.sprite = soundOff;
        else 
            sound.sprite = soundOn;

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
}
