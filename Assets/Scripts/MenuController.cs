using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("WinMenu")]
    [SerializeField] GameObject winMenu;
    [SerializeField] Image[] stars;

    [Header("Sound")]
    [SerializeField] Image sound;
    [SerializeField] Sprite soundOff;
    [SerializeField] Sprite soundOn;

    [Space]
    [SerializeField] GameObject inGameShop;

    [Header("Hints")]
    [SerializeField] GameObject hint;
    [SerializeField] TMP_Text hintsAmount;
   
    string lvlKey;
    private void Start()
    {
        PlayerPrefs.SetInt("Hints", 10);
        lvlKey = SceneManager.GetActiveScene().buildIndex.ToString();
        PlayerPrefs.SetInt("Pause", 0);
        hintsAmount.text = PlayerPrefs.GetInt("Hints", 0).ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void GameWone()
    {
        winMenu.SetActive(true);
        int r = ObjectManager.restartsAmount;
        int starsAmount = 3 - r / 2;
        if (starsAmount < 0)
            starsAmount = 0;
        PlayerPrefs.SetInt("LvlFinished" + lvlKey, 1);
        for(int i =0; i< starsAmount; i++)
        {
            stars[i].color = Color.white; 
        }
        if(PlayerPrefs.GetInt("Stars"+lvlKey,0)<starsAmount)
            PlayerPrefs.SetInt("Stars" + lvlKey, starsAmount);
        PlayerPrefs.SetInt("Pause", 1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void InGameShop()
    {
        int x = PlayerPrefs.GetInt("Pause", 0);
        if (x == 1&&!inGameShop.activeInHierarchy)
            return;
        inGameShop.SetActive(!inGameShop.activeInHierarchy);
        x -= 1;
        x = Mathf.Abs(x);
        PlayerPrefs.SetInt("Pause", x);
    }
    public void ShowHint()
    {
          
        if (hint.activeInHierarchy)
        {
            hint.SetActive(false);
            PlayerPrefs.SetInt("Pause", 0);
        }
        else if (PlayerPrefs.GetInt("Pause",0)==0)
        {
            int hints = PlayerPrefs.GetInt("Hints", 0);
            if (hints < 1)
                return;
            hint.SetActive(true);
            hints--;
            hintsAmount.text = hints.ToString();
            PlayerPrefs.SetInt("Hints", hints);
            PlayerPrefs.SetInt("Pause", 1);
        }
       
       

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
    public void NextLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
