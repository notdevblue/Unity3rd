using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIResources : MonoBehaviour
{
    private ResourceTypeListSO resTypeList;
    private Dictionary<ResourceTypeSO, Transform> resTypeTrmDictionary;

    private void Awake()
    {
        resTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
	resTypeTrmDictionary = new Dictionary<ResourceTypeSO, Transform>();

	Transform resTmpTrm = transform.Find("ResourceTemplate");
	resTmpTrm.gameObject.SetActive(false);

	int index = 0;

        foreach(ResourceTypeSO resType in resTypeList.resList)
        {
            Transform resTrm = Instantiate(resTmpTrm, transform);
            resTrm.gameObject.SetActive(true);

            // Location Apply
            float offsetAmount = -160.0f;
            resTrm.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            // Image Apply
            resTrm.Find("Image").GetComponent<Image>().sprite = resType.sprite;

            // Dictionary Apply
            resTypeTrmDictionary[resType] = resTrm;

            ++index;
        }
    }

    private void Start()
    {
	// Resource Amount Update Event
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
	    Transform resTrm = resTypeTrmDictionary[resType];

	    // Resource apply
	    int resAmount = ResourceManager.Instance.GetResourceAmount(resType);
	    resTrm.Find("text").GetComponent<TextMeshProUGUI>().SetText(resAmount.ToString());
	}
    }
}
