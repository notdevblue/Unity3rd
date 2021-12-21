using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    //private BuildingTypeSO buildingType;

    private ResourceGeneratorData resGenDt;

    private float timer;
    private float timeMax;

    private void Awake()
    {
        timeMax = 1f;

        resGenDt = GetComponent<BuildingTypeHolder>().buildingType.resGeneratorDt;
        timeMax = resGenDt.timerMax;
    }

    void Start()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resGenDt.detectionRadius);

        int nearByResourceAmount = 0;
        foreach(Collider2D collider in collider2DArray)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if(resourceNode != null)
            {
                // nearByResourceAmount++;
                // 수확기 타입이랑 자원 타입이랑 비교
                if(resourceNode.resType == resGenDt.resType)
                {
                    nearByResourceAmount++;
                }
            }
        }

        // 자원수 최대 제한을 두기 위한 코드
        // 배치했을 때 근처에 자원이 없다면 리소스 생산 처리 안함
        nearByResourceAmount = Mathf.Clamp(nearByResourceAmount, 0, resGenDt.maxResAmount);
        if(nearByResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            // 생산속도 조절 공식 적용
            // 자원 갯수에 비례하여 생산속도가 빨라지도록
            // 아무리 갯수가 많아도 최고 0.5f 이상으로 빨라지지 않도록 제한
            timeMax = (resGenDt.timerMax / 2f) +
                       resGenDt.timerMax * (1 - (float)nearByResourceAmount / resGenDt.maxResAmount);
        }

        print(string.Format("근처에 있는 자원 수:{0}, 생산속도:{1}", nearByResourceAmount, timeMax ));
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timeMax;

            ResourceManager.Instance.AddResource(resGenDt.resType, 1);
        }
    }
}
