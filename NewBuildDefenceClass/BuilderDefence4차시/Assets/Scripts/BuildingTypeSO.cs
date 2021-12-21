using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameStr;
    public Transform prefab;

    public ResourceGeneratorData resGeneratorDt;

    public Sprite sprite;

    // 건물 사이 최소 거리값
    public float minConstructRadius;

    // 빌딩 코스트 추가 변수
    public ResourceAmount[] buildResCostArray;

    public string GetBuildingNameAndCostStr()
    {
        string str = "";

        foreach(var res in buildResCostArray)
        {
            str += res.resourceType.nameStr + ": " + res.amount + " ";
        }

        return str;
    }

    public int healthAmountMax;
}
