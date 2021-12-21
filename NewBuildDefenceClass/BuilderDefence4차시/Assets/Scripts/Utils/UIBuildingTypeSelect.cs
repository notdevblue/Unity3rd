using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingTypeSelect : MonoBehaviour
{
    [SerializeField]
    private Transform selectedImgTrm;

    // 화살표 버튼 처리 관련 변수
    [SerializeField]
    private Sprite arrowSprite;
    private Transform arrowBtnTrm;

    // 무시할 버튼 리스트 관리용
    [SerializeField]
    private List<BuildingTypeSO> ignoreBuildingTypeList;

    void Awake()
    {
        Transform btnTmpTrm = transform.Find("btnTemplate");
        btnTmpTrm.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        int index = 0;

        // 생성 및 활성화
        arrowBtnTrm = Instantiate(btnTmpTrm, transform);
        arrowBtnTrm.gameObject.SetActive(true);

        // 위치 적용
        float offsetAmount = 130f;
        arrowBtnTrm.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        // 이미지 & 사이즈 적용
        Transform imgTrm = arrowBtnTrm.Find("image");
        imgTrm.GetComponent<Image>().sprite = arrowSprite;
        imgTrm.GetComponent<RectTransform>().sizeDelta = new Vector2(-30, -30);

        // 버튼 기능 함수 적용
        arrowBtnTrm.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            BuilderManager.Instance.SetActiveBuildingType(null);

            selectedImgTrm.position = arrowBtnTrm.position;
        }); 

        index++;

        foreach(BuildingTypeSO buildingType in buildingTypeList.btList)
        {
            if(ignoreBuildingTypeList.Contains(buildingType)) continue; // 무시할 버튼들 제외

            // 생성 및 활성화
            Transform btnTrm = Instantiate(btnTmpTrm, transform);
            btnTrm.gameObject.SetActive(true);

            // 위치 적용
            offsetAmount = 130f;
            btnTrm.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            // 이미지 적용
            btnTrm.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            // 버튼 기능 함수 적용
            btnTrm.GetComponent<Button>().onClick.AddListener(delegate()
            {
                BuilderManager.Instance.SetActiveBuildingType(buildingType);

                selectedImgTrm.position = btnTrm.position;
            });

            // 툴팁 처리를 위한 마우스이벤트 코드
            MouseEnterExitEvents mouseEnterExitEvents = btnTrm.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += () => {
                // string coststr = "";
                // for (int i = 0; i < buildingType.buildResCostArray.Length; ++i) 
                // {
                //     coststr += buildingType.buildResCostArray[i].resourceType.ToString().Split(' ')[0] + ": " + buildingType.buildResCostArray[i].amount + ", ";
                // }

                // coststr = coststr.Substring(0, coststr.Length - 2);
                // UIToolTip.Instance.Show($"{buildingType.nameStr}\r\n{coststr}");
                UIToolTip.Instance.Show(buildingType.nameStr + System.Environment.NewLine + buildingType.GetBuildingNameAndCostStr());
            };

            mouseEnterExitEvents.OnMouseExit += () => {
                UIToolTip.Instance.Hide();
            };

            index++;
        }
    }

    void Start()
    {
        BuilderManager.Instance.SetActiveBuildingType(null);
        selectedImgTrm.SetAsLastSibling();
    }
}
