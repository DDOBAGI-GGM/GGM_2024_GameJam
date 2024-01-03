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
    [SerializeField] private List<GameObject> stageObj = new List<GameObject>();
    [SerializeField] private int currentStageMax;
    [SerializeField] private int currentStage = 1;
    [SerializeField] private int beforeStage;

    [Header("Dust")]
    [SerializeField] private int dustCnt;
    [HideInInspector] public int DustCnt => dustCnt;

    [Header("Star")]
    [SerializeField] private int starCnt;
    [HideInInspector] public int StarCnt => starCnt;

    [Header("Clear")]
    [SerializeField] private bool isClear = false;
    [SerializeField] private bool isReset = false;
    [HideInInspector] public bool IsReset => isReset;  

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

            if (currentStage >= stageValue.Count)
                UIManager.Instance.ChangeScene("Clear");
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

        //stageObj[0].GetComponent<IReset>().Reset();

        foreach (var obj in stageObj)
        {
            IReset reset = obj.GetComponent<IReset>();
            reset.Reset();
            //tlqf(reset);
        }

        //ReGenerate();
    }

    //private void tlqf(IReset test)
    //{
    //    test.Reset();
    //}

    void ReGenerate()
    {
        Debug.Log(objs.Count() + " " + stars.Count());  
        while (objs.Count() > stageValue[beforeStage])
        {
            Dust temp = objs.Pop();
            temp.obj.transform.position = temp.transform.position;
            // 성은이 함수 호출
        }
        
        while (stars.Count() > stageValue[beforeStage])
        {
            GameObject temp = stars.Pop();
            temp.SetActive(true);
        }
    }
}
