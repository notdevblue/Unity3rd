using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO buildingType;

    private float timer;
    private float timeMax;

    private void Awake()
    {
        timeMax = 1.0f;

        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        timeMax = buildingType.resGenData.timerMax;

        
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer += timeMax;
            // 실제로 틱당 얼만큼씩 채워지는지 로그 로드

            print($"Tick: {buildingType.resGenData.resType.nameStr}");

            ResourceManager.Instance.AddResource(buildingType.resGenData.resType, 2);
        }
    }
}
