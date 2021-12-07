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


    private void TestLogResAmountDict()
    {
        // foreach(ResourceTypeSO restype in resourceAmountDict.Keys) // Keys 만 돌림
        // {
        //     print(string.Format("{0}:{1}", restype.nameStr, resourceAmountDict[restype]));
        // }
    }
}
