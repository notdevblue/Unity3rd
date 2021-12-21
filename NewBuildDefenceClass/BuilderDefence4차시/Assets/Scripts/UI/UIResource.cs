using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIResource : MonoBehaviour
{
    private ResourceTypeListSO resTypeList;

    private Dictionary<ResourceTypeSO, Transform> resTypeTrmDic;

    void Awake()
    {
        resTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resTypeTrmDic = new Dictionary<ResourceTypeSO, Transform>();

        Transform resTmpTrm = transform.Find("resourceTemplate");
        resTmpTrm.gameObject.SetActive(false);

        int index = 0;

        foreach(ResourceTypeSO resType in resTypeList.resList)
        {
            // 생성 및 활성화
            Transform resTrm = Instantiate(resTmpTrm, transform);
            resTrm.gameObject.SetActive(true);

            // 위치 적용
            float offsetAmount = -160f;
            resTrm.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            // 이미지 적용
            resTrm.Find("image").GetComponent<Image>().sprite = resType.sprite;

            resTypeTrmDic[resType] = resTrm;

            index++;
        }
    }

    private void Start() 
    {
        ResourceManager.Instance.OnResourceAmountChanged += CallOnResourceAmountChanged;

        UpdateResourceAmount();
    }

    private void CallOnResourceAmountChanged()
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach(ResourceTypeSO resType in resTypeList.resList)
        {
            Transform resTrm = resTypeTrmDic[resType];

            // 갱신된 리소스양 적용
            int resAmount = ResourceManager.Instance.GetResourceAmount(resType);
            resTrm.Find("text").GetComponent<TextMeshProUGUI>().SetText(resAmount.ToString());
        }
    }
}
