using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGenData;

    private float timer;
    private float timeMax;

    private void Awake()
    {
        timeMax = 1.0f;

        resourceGenData = GetComponent<BuildingTypeHolder>().buildingType.resGenData;
        timeMax = resourceGenData.timerMax;
    }

    private void Start()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGenData.detectRadius);

        int nearByResourceAmount = 0;
        foreach(Collider2D  collider in collider2DArray)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if(resourceNode != null)
            {
                // nearByResourceAmount++;

                if(resourceNode.resType == resourceGenData.resType)
                {
                    ++nearByResourceAmount;
                }
                
            }
        }

        // 자원수 최대 제한갯수
        // 배치 시 근처에 자원이 없다면  리소스 생산처리 안함
        nearByResourceAmount = Mathf.Clamp(nearByResourceAmount, 0, resourceGenData.maxResourcesAmount);
        if(nearByResourceAmount == 0)
        {
            enabled = false;
        }
        else 
        {
            // 생산속도 조절
            // 자원 갯수 비례하여 생산속도가 빨라지게
            // 아무리 갯수가 많아도 최소 0.5f 이상으로 빨라지지 않음
            timeMax = (resourceGenData.timerMax / 2.0f) + resourceGenData.timerMax * (1 - (float)nearByResourceAmount / resourceGenData.maxResourcesAmount);
        }
        
        print(string.Format("근처에 있는 자원 갯수:{0}, 생선속도:{1}", nearByResourceAmount, timeMax));
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer += timeMax;
            // 실제로 틱당 얼만큼씩 채워지는지 로그 로드
            ResourceManager.Instance.AddResource(resourceGenData.resType, 2);
        }
    }
}
