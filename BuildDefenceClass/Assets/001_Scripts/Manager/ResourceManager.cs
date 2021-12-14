using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    static public ResourceManager Instance { get; private set; }

    private Dictionary<ResourceTypeSO, int> resourceAmountDict;

    public Action OnResourceAmountChanged;

    private void Awake()
    {
        Instance = this;
        resourceAmountDict = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        
        foreach(ResourceTypeSO resType in resTypeList.resList)
        {
            resourceAmountDict[resType] = 0;
        }

        TestLogResAmountDict();
    }


    private void Update()
    {
        // 임시키 세팅으로 리소스 추가 코드
        if(Input.GetKeyDown(KeyCode.A))
        {
            ResourceTypeListSO resTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            AddResource(resTypeList.resList[0], 2);
        }
    }

    public void AddResource(ResourceTypeSO resType, int amount)
    {
        resourceAmountDict[resType] += amount;

	OnResourceAmountChanged?.Invoke();
    }

    public int GetResourceAmount(ResourceTypeSO resType)
    {
	    return resourceAmountDict[resType];
    }

    public bool CanBuildAfford(ResourceAmount[] amount)
    {
        foreach(ResourceAmount resourceAmount in amount)
        {
            if(GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {

            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void SpendResource(ResourceAmount[] amount)
    {
        foreach (ResourceAmount resourceAmount in amount)
        {
            resourceAmountDict[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }



    // public bool Buy(ResourceAmount[] amount)
    // {
    //     bool canBuy = true;

    //     for (int i = 0; i < amount.Length; ++i)
    //     {
    //         if(resourceAmountDict[amount[i].resourceType] < amount[i].amount) {
    //             canBuy = false;
    //             break;
    //         }
    //     }

    //     if(canBuy) {
    //         for (int i = 0; i < amount.Length; ++i)
    //         {
    //             resourceAmountDict[amount[i].resourceType] -= amount[i].amount;
    //         }
    //     }

    //     return canBuy;
    // }

    private void TestLogResAmountDict()
    {
        // foreach(ResourceTypeSO restype in resourceAmountDict.Keys) // Keys 만 돌림
        // {
        //     print(string.Format("{0}:{1}", restype.nameStr, resourceAmountDict[restype]));
        // }
    }
}
