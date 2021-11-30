using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingTypeSelect : MonoBehaviour
{


    private void Awake()
    {
        Transform btnTmpTrm = transform.Find("ButtonTemplate");
        btnTmpTrm.gameObject.SetActive(false);
        
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        int index = 0;

        foreach(BuildingTypeSO buildingType in buildingTypeList.btList)
        {
            Transform btnTrm = Instantiate(btnTmpTrm, transform);
            btnTrm.gameObject.SetActive(true);

            // Location Apply
            float offsetAmount = 130f;
            btnTrm.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            // Image Apply
            btnTrm.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            // Button function link
            btnTrm.GetComponent<Button>().onClick.AddListener(() => {
                BuilderManager.Instance.SetActiveBuildingType(buildingType);
            });

            ++index;
        }
    }
}
