using System;
using System.Threading;
using UnityEngine;
[Serializable]
public struct coolClock
{
    public float time;
    public bool coolStart;
    public bool coolEnd;
}
public class Test_Cooltime : Singleton<Test_Cooltime>
{
    public coolClock[] coolclocks = new coolClock[10];
    Action[] Checker = new Action[10];
    Action updateCoolTime;
    public float timeCount = 0f;
    void noNull(){}
    void Start()
    {
        updateCoolTime = noNull;
        for (int i = 0; i < 10; i++)
        {
            coolclocks[i].time = 0f;
            coolclocks[i].coolStart = false;
            coolclocks[i].coolEnd = true;
        }
    }
    private void Update()
    {
        updateCoolTime();
    }
    public void CoolTimeStart(int index, float time)
    {
        Checker[index] += () => { timeCheck(index, time); };
        if (startCheck())
        {
            coolclocks[index].time = time;
            updateCoolTime += timeCounting;
            coolclocks[index].coolStart = true;
            coolclocks[index].coolEnd = false;
        }
        else
        {
            coolclocks[index].time += time;
        }
        updateCoolTime += Checker[index];
    }
    void timeCheck(int index, float time)
    {
        if (timeCount > time)
        {
            coolclocks[index].time = 0f;
            coolclocks[index].coolEnd = true;
            coolclocks[index].coolStart = false;
            updateCoolTime -= Checker[index];
            Checker[index] = null;
            if (EndCheck())
            {
                updateCoolTime -= timeCounting;
                timeCount = 0f;
            }
        }
    }
    void timeCounting()
    {
        timeCount += Time.deltaTime;
    }
    bool startCheck()
    {
        foreach (coolClock c in coolclocks)
        {
            if (c.coolStart)
            {
                return false;
            }
        }
        return true;
    }
    bool EndCheck()
    {
        foreach (coolClock c in coolclocks)
        {
            if (!c.coolEnd)
            {
                return false;
            }
        }
        return true;
    }
}
