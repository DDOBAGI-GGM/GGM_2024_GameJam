using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] private GameObject inGamePanel, settingPanel;
    [SerializeField] private bool esc = false;      // SerializeField 지워도 됨.

    private void Start()
    {
        //inGamePanel.SetActive(esc);
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")      // 게임씬일 때만. 임의로 SE 로 해줌.
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                esc = !esc;
                inGamePanel.SetActive(esc);
            }
        }
    }

    public void OnSetting(bool value)
    {
        if (SceneManager.GetActiveScene().name == "Game") inGamePanel.SetActive(!value);            // 게임씬일때만
        settingPanel.SetActive(value);
    }

    public void OnGame()
    {
        Debug.Log("게임누름");
        inGamePanel.SetActive(false);
        esc = !esc;
    }
}
