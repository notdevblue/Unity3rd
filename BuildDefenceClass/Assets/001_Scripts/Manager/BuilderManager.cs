using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuilderManager : MonoBehaviour
{
    public static BuilderManager Instance { get; private set; }

    [SerializeField] private Transform mouseVisualTrm;
    [SerializeField] private Transform woodPrefabTrm;

    private Camera mainCam;

    // 빌딩 리소스 로딩
    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;

    private void Awake()
    {
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        activeBuildingType = buildingTypeList.btList[0];
    }

    private void Start()
    {
        mainCam = Camera.main;

        //Camera.main 은 하이라키에 있는 모든 카메라를 찾게 됨
    }

    private void Update()
    {
        // 마우스 왼쪽버튼 클릭하면 수확기 생성
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // mouseVisualTrm.position = GetMouseWorldPos();
            Instantiate(activeBuildingType.prefab, GetMouseWorldPos(), Quaternion.identity);
        
        }

        // // 임시키 세팅으로 수확기 바꿈
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
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        return mouseWorldPos;
    }
}
