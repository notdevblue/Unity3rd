using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="ScriptableObject/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameStr;
    public Transform prefab;

    public ResourceGeneratorData resGenData;

    public Sprite sprite;

    public float minConstructRadius; // 건물간 최소 거리 유지

    public ResourceAmount[] buildResCostArray;
}
