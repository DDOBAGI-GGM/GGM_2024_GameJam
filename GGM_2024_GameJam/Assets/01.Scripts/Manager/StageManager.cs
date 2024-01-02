using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEditor.Rendering;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private List<int> valueCnt = new List<int>();
    [SerializeField] private int currentStage;

    [Header("Star")]
    [SerializeField] private int starCnt;
    [SerializeField] private int maxStarCnt;

    [Header("Clear")]
    [SerializeField] private bool isClear = false;
    [SerializeField] private bool isEndt = false;

    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        maxStarCnt = valueCnt[currentStage];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            starCnt++;
        if (Input.GetKeyDown(KeyCode.N))
            NextStage();
    }

    public void GetStar()
    {
        starCnt++;

        if (starCnt >= maxStarCnt)
            isClear = true;
    }

    private void NextLevel()
    {
        isClear = false;
        starCnt = 0;
        maxStarCnt = valueCnt[currentStage];
    }

    public void NextStage()
    {
        if (isClear)
        {
            currentStage += 1;

            if (currentStage == valueCnt.Count)
                isEndt = true;
            else
                NextLevel();    
        }
    }

    public void ReSet()
    {
        // �ϴ� ����ٴϴ� ��� ������Ű��
        // ��� ��ġ...�� ���ؼ� �ٽ� ����?...
        isClear = false;
        starCnt = 0;
    }
}
