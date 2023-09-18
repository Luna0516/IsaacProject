using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Serializable]
    public enum PatternSellect
    {
        TestMode = 0,
        HostPattern = 1,
        MuligunAndBloodMan = 2,
        ShitAndHost = 3,
        ShitAndMuligun = 4,
        FlyAndBloodMan = 5,
        Rava = 6,
        RavaAndShit = 7,
        BossRoom = 8
    }

    [Serializable]
    public struct AllMonsterSpawners
    {
        public AllMonsterSpawners(PoolObjectType monster = PoolObjectType.EnemyMulligun, int spawncount = 1, Vector2 pos = default(Vector2))
        {
            this.monsterList = monster;
            this.spawnCount = spawncount;
            this.spawnArea = pos;
        }
        public PoolObjectType monsterList;
        public int spawnCount;
        public Vector2 spawnArea;
    }

    MonsterSpChilde[] mssp;

    List<GameObject> spawnGameObject;
    public List<EnemyBase> enemies;

    [Header("패턴 선택기")]
    public PatternSellect patternSellector;

    [Header("스폰 리스트")]
    public AllMonsterSpawners[] allspawn;
    [Space(10)]

    private int spawnercount = 0;
    [Header("스폰 몬스터 갯수")]
    public int allspawncount = 0;
    [Space(15)]
    [Header("죽은 몬스터 갯수")]
    public int deadCount = 999;
    [Space(15)]


    [Header("스폰 버튼")]
    public bool SpawnNow = false;

    [Header("랜덤 버튼")]
    public bool RandomButton = true;

    public Action playerIn;
    public Action onAllEnemyDied;

    public int DeadCount
    {
        get
        {
            return deadCount;
        }
        set
        {
            if (value < 40)
            {
                if (deadCount != value)
                {
                    deadCount = value;
                }
                if (deadCount == allspawncount)
                {
                    onAllEnemyDied?.Invoke();
                }
            }
        }
    }

    int selpa = 1;

    private void Awake()
    {
        spawnercount = transform.childCount;
        spawnGameObject = new List<GameObject>(spawnercount);
        GetChilding(this.transform);
        allspawn = new AllMonsterSpawners[spawnercount];
        mssp = new MonsterSpChilde[spawnercount];
        loadSpawnDatas();
    }
    private void OnEnable()
    {
        playerIn += () => spawnActive(true);
        onAllEnemyDied += () => { Debug.Log("모든 몬스터는 사망했다."); };
    }
    private void Start()
    {
        if (RandomButton)
        {
            selpa = UnityEngine.Random.Range(1, 8);
        }

        patterSwitchSys((PatternSellect)selpa);
        loadSpawnDatas();
    }

    private void Update()
    {
        spawnActive(SpawnNow);
    }

    /// <summary>
    /// 자식 스포너 개체들에게 스폰 명령
    /// </summary>
    /// <param name="spawnCheck"></param>
    void spawnActive(bool spawnCheck)
    {
        if (spawnCheck)
        {
            SpawnInitialized();//입력 정보를 한번 동기화 하고
            foreach (var objectspawn in mssp)//자식 개체들에게 스폰하라고 신호전달
            {
                objectspawn.SapwnActive.Invoke();
                if(objectspawn.gameObject.activeSelf)
                {
                allspawncount += objectspawn.allMonsterSpawners.spawnCount;//스폰카운트만큼 집계해서 몬스터 수를 구하고
                }
            }
            SpawnNow = false;//다음 프레임에 스폰 함수 실행방지
            DeadCount = 0;//데드카운트를 0으로 만들어서 프로퍼티 실행 시작(20 이상의 수가 들어오지 못하게 막아둠)
            foreach (var objectspawn in mssp)
            {
                if (objectspawn.monsters != null)
                {
                    objectspawn.monsters.IsDead += DeadCounnting;//이 스포너로 소환된 모든 몬스터들 IsDead 델리게이트에 DeadCount를 증가시키는 함수 연결
                    if (objectspawn.monsters.AddableSpawnEnemy != 0)
                    {
                        allspawncount += objectspawn.monsters.AddableSpawnEnemy;
                        objectspawn.monsters.count += DeadCounnting;
                    }
                }
            }
        }
    }
    void DeadCounnting(bool obj)
    {
        DeadCount++;//그래서 소환된 애들은 죽었을때 Dead 카운트가 증가하나, 파리같은 추가 소환은 증가 안해요
        Debug.Log($"{deadCount}/{allspawncount}");
    }
    void SpawnInitialized()
    {
        int county = 0;
        foreach (var objectspawn in spawnGameObject)
        {
            mssp[county].allMonsterSpawners.monsterList = allspawn[county].monsterList;
            mssp[county].allMonsterSpawners.spawnCount = allspawn[county].spawnCount;
            mssp[county].allMonsterSpawners.spawnArea = allspawn[county].spawnArea;
            county++;
        }
    }
    void GetChilding(Transform parent)
    {
        foreach (Transform obj in parent)
        {
            spawnGameObject.Add(obj.gameObject);
            GetChilding(obj);
        }
    }
    void loadSpawnDatas()
    {
        int county = 0;
        foreach (GameObject obj in spawnGameObject)
        {
            mssp[county] = obj.GetComponent<MonsterSpChilde>();
            allspawn[county] = mssp[county].allMonsterSpawners;
            county++;
        }
    }
    //---------------------------------------<이 아래, 패턴 선택기>--------------------------------------------
    void patterSwitchSys(PatternSellect pi)
    {
        foreach (GameObject obj in spawnGameObject)
        {
            obj.SetActive(true);
        }
        switch (pi)
        {
            case PatternSellect.HostPattern:
                HostPattern();
                break;
            case PatternSellect.MuligunAndBloodMan:
                MuligunAndBloodMan();
                break;
            case PatternSellect.ShitAndHost:
                ShitAndHost();
                break;
            case PatternSellect.ShitAndMuligun:
                ShitAndMuligun();
                break;
            case PatternSellect.FlyAndBloodMan:
                FlyAndBloodMan();
                break;
            case PatternSellect.Rava:
                Rava();
                break;
            case PatternSellect.RavaAndShit:
                RavaAbdShit();
                break;
            case PatternSellect.BossRoom:
                BossRoom();
                break;
        }
    }

    void patternChildControl(PoolObjectType mon, int num, int spawncount, Vector2 pos, Vector2 transform)
    {
        spawnGameObject[num].transform.localPosition = transform;
        mssp[num].allMonsterSpawners.monsterList = mon;
        mssp[num].allMonsterSpawners.spawnCount = spawncount;
        mssp[num].allMonsterSpawners.spawnArea = pos;
    }
    void patternChild4Cornur(PoolObjectType mon, int num, int spawncount, Vector2 pos, Vector2 transform)
    {
        Vector2 tras = transform;
        for (int i = num; i < num + 4; i++)
        {
            transform = tras;
            if (i == num)
            {
                transform.x = -(transform.x);
            }
            else if (i == num + 1)
            {
                transform.y = -(transform.y);
            }
            else if (i == num + 2)
            {
                transform.x = -(transform.x);
                transform.y = -(transform.y);
            }
            else
            {

            }
            spawnGameObject[i].transform.localPosition = transform;
            mssp[i].allMonsterSpawners.monsterList = mon;
            mssp[i].allMonsterSpawners.spawnCount = spawncount;
            mssp[i].allMonsterSpawners.spawnArea = pos;
        }
    }
    /// <summary>
    /// 자식개체 숫자를 맞추는 함수
    /// </summary>
    /// <param name="num">num을 초과한 숫자의 자식은 비활성화시킨다.</param>
    void DeActiveAtors(int num)
    {
        for (int i = num + 1; i < spawnGameObject.Count; i++)
        {
            mssp[i].allMonsterSpawners.spawnCount = 0;
            spawnGameObject[i].SetActive(false);
        }
    }
    void HostPattern()
    {
        Vector2 spawnArea = new Vector2(0.5f, 0.5f);
        patternChild4Cornur(PoolObjectType.EnemyHost, 0, 1, spawnArea, new Vector2(5, 2));
        DeActiveAtors(3);
    }
    void Rava()
    {
        Vector2 spawnArea = new Vector2(0.5f, 0.5f);
        patternChild4Cornur(PoolObjectType.EnemyRava, 0, 1, spawnArea, new Vector2(5, 2));
        DeActiveAtors(3);
    }
    void RavaAbdShit()
    {
        Vector2 spawnArea = new Vector2(0.5f, 0.5f);
        patternChild4Cornur(PoolObjectType.EnemyShiter, 0, 1, spawnArea, new Vector2(4, 2));
        patternChildControl(PoolObjectType.EnemyRava, 4, 1, spawnArea, new Vector2(-1, 0));
        patternChildControl(PoolObjectType.EnemyRava, 5, 1, spawnArea, Vector2.zero);
        patternChildControl(PoolObjectType.EnemyRava, 6, 1, spawnArea, new Vector2(1, 0));
        DeActiveAtors(6);
    }
    void MuligunAndBloodMan()
    {
        Vector2 spawnArea = new Vector2(0.5f, 0.5f);
        patternChild4Cornur(PoolObjectType.EnemyMulligun, 0, 1, spawnArea, new Vector2(4, 2));
        patternChildControl(PoolObjectType.EnemyBloodMan, 4, 1, spawnArea, new Vector2(-1, 0));
        patternChildControl(PoolObjectType.EnemyBloodMan, 5, 1, spawnArea, Vector2.zero);
        patternChildControl(PoolObjectType.EnemyBloodMan, 6, 1, spawnArea, new Vector2(1, 0));
        DeActiveAtors(6);
    }
    void ShitAndHost()
    {
        Vector2 spawnArea = new Vector2(0.5f, 0.5f);
        patternChild4Cornur(PoolObjectType.EnemyHost, 0, 1, spawnArea, new Vector2(5, 2));
        patternChildControl(PoolObjectType.EnemyShiter, 4, 1, spawnArea, new Vector2(2, 0));
        patternChildControl(PoolObjectType.EnemyShiter, 5, 1, spawnArea, new Vector2(-2, 0));
        DeActiveAtors(5);
    }
    void ShitAndMuligun()
    {
        Vector2 spawnArea = new Vector2(0.5f, 0.5f);
        patternChild4Cornur(PoolObjectType.EnemyShiter, 0, 1, spawnArea, new Vector2(4, 2));
        patternChildControl(PoolObjectType.EnemyMulligun, 4, 1, spawnArea, new Vector2(2, 0));
        patternChildControl(PoolObjectType.EnemyMulligun, 5, 1, spawnArea, new Vector2(-2, 0));
        DeActiveAtors(5);
    }
    void FlyAndBloodMan()
    {
        Vector2 spawnArea = new Vector2(0.5f, 0.5f);
        patternChild4Cornur(PoolObjectType.EnemyFly, 0, 1, spawnArea, new Vector2(5, 2));
        patternChildControl(PoolObjectType.EnemyBloodMan, 4, 1, spawnArea, new Vector2(-1, 0));
        patternChildControl(PoolObjectType.EnemyBloodMan, 5, 1, spawnArea, Vector2.zero);
        patternChildControl(PoolObjectType.EnemyBloodMan, 6, 1, spawnArea, new Vector2(1, 0));
        DeActiveAtors(6);
    }
    void BossRoom()
    {
        Vector2 spawnArea = new Vector2(1, 1);
        patternChildControl(PoolObjectType.EnemyMonstro, 0, 1, spawnArea, Vector2.zero);
        DeActiveAtors(0);
    }


}