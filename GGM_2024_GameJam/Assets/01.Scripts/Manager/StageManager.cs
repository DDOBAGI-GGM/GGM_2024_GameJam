using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Stage
{
    public Transform reStartPos;
    public int pointCnt;
    public List<GameObject> obj;
}

public class StageManager : Singleton<StageManager>
{
    [Header("Stage")]
    [SerializeField] private List<Stage> stageValue = new List<Stage>();
    [HideInInspector] public List<Stage> StageValue => stageValue;
    //[SerializeField] private List<GameObject> stageObj = new List<GameObject>();
    [SerializeField] private int currentStageMax;
    [SerializeField] private int currentStage;
    [HideInInspector] public int CurrentStage => currentStage;
    [SerializeField] private int beforeStage;

    [Header("Dust")]
    [SerializeField] private int dustCnt;
    //[HideInInspector] public int DustCnt => dustCnt;

    [Header("Star")]
    [SerializeField] private int starCnt;
    //[HideInInspector] public int StarCnt => starCnt;

    [Header("Clear")]
    [SerializeField] private bool isClear = false;
    [HideInInspector] public bool IsClear => isClear;
    [SerializeField] private bool isReset = false;

    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        currentStageMax = stageValue[currentStage].pointCnt;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            starCnt++;
        if (Input.GetKeyDown(KeyCode.N))
            NextStage();

        if (Input.GetKeyDown(KeyCode.P))
            ReSet();

        if (starCnt == currentStageMax && dustCnt == currentStageMax)
            isClear = true;
    }

    public void GetStar()
    {
        starCnt++;
    }

    public void GetDust()
    {
        dustCnt++;
    }   

    public void NextStage()
    {
        if (isClear)
        {
            beforeStage = currentStage;
            currentStage += 1;

            if (currentStage >= stageValue.Count)
                UIManager.Instance.ChangeScene("Clear");
            else
            {
                isClear = false;
                currentStageMax = stageValue[currentStage].pointCnt;
            }
        }
    }

    public void ReSet()
    {
        isClear = false;

        if (currentStage > 0)
        {
            beforeStage = currentStage - 1;
            dustCnt = stageValue[beforeStage].pointCnt;
            starCnt = stageValue[beforeStage].pointCnt;
        }
        else
        {
            dustCnt = 0;
            starCnt = 0;
        }

        foreach (var stage in stageValue[currentStage].obj)
        {
            IReset reset = stage.GetComponent<IReset>();
            reset.Reset();
        }
    }
}
