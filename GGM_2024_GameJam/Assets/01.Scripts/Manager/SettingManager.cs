using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] private GameObject inGamePanel, settingPanel;
    [SerializeField] private Button settingBackBtn;
    [SerializeField] private bool esc = false;      // SerializeField ������ ��.

    private void Start()
    {
        //inGamePanel.SetActive(esc);
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        //Debug.Log("�� �̵� �̺�Ʈ ���");
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void OnDisable()
    {
        //Debug.Log("�� �̵� �̺�Ʈ ����");
        SceneManager.sceneLoaded -= LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log(scene.name + "���� ����Ǿ����ϴ�.");
        if (SceneManager.GetActiveScene().name == "SE")
        {
            settingBackBtn.onClick.RemoveAllListeners();
            settingBackBtn.onClick.AddListener(() => { OnSetting(false); });
            settingBackBtn.onClick.AddListener(() => { IntroInit.Instance.SettingCancel(); });
        }
        else
        {
            settingBackBtn.onClick.RemoveAllListeners();
            settingBackBtn.onClick.AddListener(() => { OnSetting(false); });
        }
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
        UIManager.Instance.ChangeScene("tjfdk(obj)");
    }
}
