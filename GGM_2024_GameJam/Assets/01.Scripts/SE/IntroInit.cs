using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroInit : MonoBehaviour
{
    public static IntroInit Instance;

    [SerializeField] private Button settingBtn;
    [SerializeField] private ArrowChoose arrowChoose;

    private void Awake()
    {
       Instance = this;
    }

    public void SettingCancel()
    {
        arrowChoose.enabled = true;
    }

    private void OnEnable()
    {
        settingBtn.onClick.AddListener(() => SettingManager.Instance?.OnSetting(true));
    }

    private void OnDisable()
    {
        settingBtn.onClick.RemoveListener(() => SettingManager.Instance?.OnSetting(true));
    }
}
