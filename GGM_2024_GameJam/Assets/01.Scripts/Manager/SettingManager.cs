using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] private GameObject inGamePanel, settingPanel;
    [SerializeField] private bool esc = false;      // SerializeField ������ ��.

    private void Start()
    {
        //inGamePanel.SetActive(esc);
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")      // ���Ӿ��� ����. ���Ƿ� SE �� ����.
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
        if (SceneManager.GetActiveScene().name == "Game") inGamePanel.SetActive(!value);            // ���Ӿ��϶���
        settingPanel.SetActive(value);
    }

    public void OnGame()
    {
        Debug.Log("���Ӵ���");
        inGamePanel.SetActive(false);
        esc = !esc;
    }
}
