using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuilderManager : MonoBehaviour
{
    public static BuilderManager Instance { get; private set; }

    private Camera mainCam;

    [SerializeField]
    private Transform mouseVisualTrm;
    [SerializeField]
    private Transform woodPrefabTrm;

    // ���� ���ҽ� �ε�
    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;

    public Action<BuildingTypeSO> OnActiveBuildingTypeChanged;

    Vector3 curMousePos;

    private void Awake()
    {
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        // activeBuildingType = buildingTypeList.btList[0];
    }

    void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            /// mouseVisualTrm.position = GetMouseWorldPos();
            curMousePos = UtilClass.GetMouseWorldPos();
            if (activeBuildingType != null && CanSpawnBuilding(activeBuildingType, curMousePos))
            {
                if(ResourceManager.Instance.CanBuildAfford(activeBuildingType.buildResCostArray))
                {
                    ResourceManager.Instance.SpendResource(activeBuildingType.buildResCostArray);
                    Instantiate(activeBuildingType.prefab, curMousePos, Quaternion.identity);
                }
                else
                {
                    // 자원이 부족합니다.
                }
            }
        }


        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     activeBuildingType = buildingTypeList.btList[0];
        // }

        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     activeBuildingType = buildingTypeList.btList[1];
        // }

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     activeBuildingType = buildingTypeList.btList[2];
        // }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(activeBuildingType);
    }


    /*
    1. 다른 오브젝트(자원, 다른 건물)와 겹치지 않을 것
    2. 같은 종류의 건물을 지을때는 최소한의 거리를 유지해서 짓게할 것
    3. 커맨드 센터를 기준으로 너무 멀리 떨어지면 건물을 못짓게 할 것
    */
    
    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 mousePos)
    {
        // 다른 오브젝트가 존재하는지 체크
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(mousePos + (Vector3)boxCollider2D.offset , boxCollider2D.size, 0);

        // 겹친 오브젝트가 있다면 건설 NO!
        bool bAreaClear = collider2DArray.Length == 0;
        if(!bAreaClear)
        {
            return false;   // 1
        }

        // 건물간 일정 거리 유지 체크
        collider2DArray = Physics2D.OverlapCircleAll(mousePos, buildingType.minConstructRadius);
        foreach(Collider2D collider in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                // 같은 건물인지 아닌지를 체크
                 if(buildingTypeHolder.buildingType == buildingType)
                 {
                     return false; // 2
                 }
            }
        }

        // 메인센터로부터 멀어지면 건설 NO!
        float maxConstructRadius = 50f;
        collider2DArray = Physics2D.OverlapCircleAll(mousePos, maxConstructRadius);
        foreach(Collider2D collider in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                return true;
            }
        }

        return false;   // 3
    }
}
