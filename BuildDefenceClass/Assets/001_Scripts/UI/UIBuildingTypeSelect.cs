using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingTypeSelect : MonoBehaviour
{
    // 선택된 커서 이미지
    [SerializeField] private Transform selectedImgTrm;

    // 화살표 버튼 관련 변수
    [SerializeField] private Sprite arrowSprite;
    private Transform arrowButtonTrm;

    private void Awake()
    {
        Transform btnTmpTrm = transform.Find("ButtonTemplate");
        btnTmpTrm.gameObject.SetActive(false);
        
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        int index = 0;


        // 화살표 버튼 추가
        arrowButtonTrm = Instantiate(btnTmpTrm, transform);
        arrowButtonTrm.gameObject.SetActive(true);

        // 위치 적용
        float offsetAmount = 130.0f;
        arrowButtonTrm.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        // 이미지 & 사이즈
        Transform imgTrm = arrowButtonTrm.Find("Image");
        imgTrm.GetComponent<Image>().sprite = arrowSprite;
        imgTrm.GetComponent<RectTransform>().sizeDelta = new Vector3(-30.0f, -30.0f);

        // Button function link
        arrowButtonTrm.GetComponent<Button>().onClick.AddListener(() => {
            BuilderManager.Instance.SetActiveBuildingType(null);

            // 선택된 이미지 트렌스폼 위치 동기화
            selectedImgTrm.position = arrowButtonTrm.position;
        });

        ++index;

        foreach(BuildingTypeSO buildingType in buildingTypeList.btList)
        {
            Transform btnTrm = Instantiate(btnTmpTrm, transform);
            btnTrm.gameObject.SetActive(true);

            // Location Apply
            btnTrm.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            // Image Apply
            btnTrm.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            // Button function link
            btnTrm.GetComponent<Button>().onClick.AddListener(() => {
                BuilderManager.Instance.SetActiveBuildingType(buildingType);

                // 선택된 이미지 트렌스폼 위치 동기화
                selectedImgTrm.position = btnTrm.position;
                // BuildingGhost.Instance.ChangeGhostSprite(buildingType.sprite);
            });

            ++index;
        }
    }

    private void Start()
    {
        // 최초 시작시에는 
        selectedImgTrm.SetAsLastSibling();
    }
}
