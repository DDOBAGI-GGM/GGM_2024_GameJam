using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public struct Dust
{
    public Transform transform;
    public GameObject obj;

    public Dust(Transform _transform, GameObject _obj)
    {
        transform = _transform;
        obj = _obj;
    }
}

public class StageManager : Singleton<StageManager>
{
    [Header("Stage")]
    [SerializeField] private List<int> stageValue = new List<int>();
    [SerializeField] private int currentStageMax;
    [SerializeField] private int currentStage;
    [SerializeField] private int beforeStage;

    [Header("Dust")]
    [SerializeField] private int dustCnt;
    [HideInInspector] public int DustCnt => dustCnt;
    //[SerializeField] private int maxDustCnt;

    [Header("Star")]
    [SerializeField] private int starCnt;
    [HideInInspector] public int StarCnt => starCnt;
    //[SerializeField] private int maxStarCnt;

    [Header("Clear")]
    [SerializeField] private bool isClear = false;
    [SerializeField] private bool isEndt = false;

    public Stack<Dust> objs = new Stack<Dust>();

    public Stack<GameObject> stars = new Stack<GameObject>();

    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        currentStageMax = stageValue[currentStage];
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

    public void GetStar(GameObject star)
    {
        starCnt++;
        stars.Push(star);
    }

    public void GetDust(GameObject dust)
    {
        dustCnt++;
        objs.Push(new Dust(dust.transform, dust));
        //Debug.Log(objs.Count());
    }   

    // 다음 스테이지로 넘어갈 때
    public void NextStage()
    {
        // 현재 클리어가 된다면 스테이지를 한 칸 높인다 / 클리어 풀어주고 별 초기화...
        if (isClear)
        {
            beforeStage = currentStage;
            currentStage += 1;

            if (currentStage == stageValue.Count)
                isEndt = true;
            else
            {
                isClear = false;
                currentStageMax = stageValue[currentStage];
            }
        }
    }

    public void ReSet()
    {
        isClear = false;

        if (currentStage > 0)
        {
            beforeStage = currentStage - 1;
            dustCnt = stageValue[beforeStage];
            starCnt = stageValue[beforeStage];
        }
        else
        {
            dustCnt = 0;
            starCnt = 0;
        }

        ReGenerate();
    }

    void ReGenerate()
    {
        while (objs.Count() > stageValue[beforeStage] - 1)
        {
            Dust temp = objs.Pop();
            temp.obj.transform.position = temp.transform.position;
            // 성은이 함수 호출
        }
        
        while (stars.Count() > stageValue[beforeStage] - 1)
        {
            GameObject temp = stars.Pop();
            temp.SetActive(true);
        }
    }
}
