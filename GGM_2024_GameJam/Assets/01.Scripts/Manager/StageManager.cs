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

public class StageManager : MonoBehaviour
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

    public static StageManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentStageMax = stageValue[currentStage].pointCnt;
    }

    private void Update()
    {
        if (starCnt == currentStageMax && dustCnt == currentStageMax)
            isClear = true;
    }

    public bool GetStar()
    {
        if (starCnt + 1 <= dustCnt)
        {
            starCnt++;
            return true;
        }
        return false;
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

            Debug.Log(currentStage + " ÇöÀç ¾À" + stageValue.Count);

            if (currentStage >= stageValue.Count)
            {
                UIManager.Instance?.ChangeScene("Clear");
                Debug.Log("¤»¤»");
            }
            else
            {
                isClear = false;
                starCnt = 0;
                PlayerStar.Instance.UseStar();
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
            starCnt = 0;
            //starCnt = stageValue[beforeStage].pointCnt;
        }
        else
        {
            dustCnt = 0;
            starCnt = 0;
        }

        PlayerStar.Instance.UseStar();

        foreach (var stage in stageValue[currentStage].obj)
        {
            IReset reset = stage.GetComponent<IReset>();
            reset?.Reset();
        }

        PlayerSupporter.Instance.ReStart();
    }
}
