using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuilderManager : MonoBehaviour
{
    public static BuilderManager Instance { get; private set; }
    public event Action<BuildingTypeSO> OnActiveBuildingTypeChanged;


    [SerializeField] private Transform mouseVisualTrm;
    [SerializeField] private Transform woodPrefabTrm;

    // 빌딩 리소스 로딩
    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;
    

    private void Awake()
    {
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        // activeBuildingType = buildingTypeList.btList[0];
    }

    private void Update()
    {
        // 마우스 왼쪽버튼 클릭하면 수확기 생성
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(activeBuildingType != null)
            {
                Instantiate(activeBuildingType.prefab, UtilClass.GetMouseWorldPos(), Quaternion.identity);

            }

        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(buildingType);
    }
}
