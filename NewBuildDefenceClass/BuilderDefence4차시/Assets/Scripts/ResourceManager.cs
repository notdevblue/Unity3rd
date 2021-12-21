using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceTypeSO, int> resourceAmountDic;

    public Action OnResourceAmountChanged;  // 구독 이벤트 변수 추가

    private void Awake()
    {
        Instance = this;

        resourceAmountDic = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach(ResourceTypeSO resType in resTypeList.resList)
        {
            resourceAmountDic[resType] = 0;
        }

        //TestLogResAmountDic();
    }

    private void Update()
    {
        // �ӽ�Ű �������� ���ҽ� �߰� �ڵ�
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     ResourceTypeListSO resTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        //     AddResource(resTypeList.resList[0], 2);

        //     TestLogResAmountDic();
        // }
    }

    public void AddResource(ResourceTypeSO resType, int amount)
    {
        resourceAmountDic[resType] += amount;

        OnResourceAmountChanged?.Invoke();      // 1 

        // if(OnResourceAmountChanged != null)     // 2
        // {
        //     OnResourceAmountChanged();
        // }

       // TestLogResAmountDic();
    }

    public int GetResourceAmount(ResourceTypeSO resType)
    {
        return resourceAmountDic[resType];
    }

    void TestLogResAmountDic()
    {
        foreach(ResourceTypeSO resType in resourceAmountDic.Keys)
        {
            print(string.Format("{0}:{1}", resType.nameStr, resourceAmountDic[resType]));
        }
    }

    public bool CanBuildAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach(ResourceAmount resourceAmount in resourceAmountArray)
        {
            if(GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {
                // 생산 가능
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void SpendResource(ResourceAmount[] resourceAmountArray)
    {
        foreach(ResourceAmount resourceAmount in resourceAmountArray)
        {
            resourceAmountDic[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}
