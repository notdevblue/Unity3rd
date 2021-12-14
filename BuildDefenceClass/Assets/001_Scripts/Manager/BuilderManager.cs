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

    private Vector3 curMousePosition;


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
            curMousePosition = UtilClass.GetMouseWorldPos();
            if(activeBuildingType != null && CanSpawnBuilding(activeBuildingType, curMousePosition))
            {
                if(ResourceManager.Instance.CanBuildAfford(activeBuildingType.buildResCostArray))
                {
                    ResourceManager.Instance.SpendResource(activeBuildingType.buildResCostArray);
                    Instantiate(activeBuildingType.prefab, curMousePosition, Quaternion.identity);
                }

            }

        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(buildingType);
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 mousePosition)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(mousePosition + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0.0f);

        bool bAreaClear = collider2DArray.Length == 0;
        if(!bAreaClear)
        {
            return false;
        }

        collider2DArray = Physics2D.OverlapCircleAll(mousePosition, buildingType.minConstructRadius);
        foreach(Collider2D collider in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                if(buildingTypeHolder.buildingType == buildingType)
                {
                    return false;
                }
            }
        }

        float maxConstructRadius = 50.0f;
        collider2DArray = Physics2D.OverlapCircleAll(mousePosition, maxConstructRadius);
        foreach (Collider2D collider in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                return true;
            }
        }

        return false;
    }
}
