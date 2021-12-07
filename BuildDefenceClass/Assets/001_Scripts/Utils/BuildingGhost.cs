using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    // private GameObject spriteObj = null;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // spriteObj = transform.Find("sprite").gameObject;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        BuilderManager.Instance.OnActiveBuildingTypeChanged += CallOnActiveBuildingChanged;
        Hide();
    }

    void CallOnActiveBuildingChanged(BuildingTypeSO activeBuildingType)
    {
        if(activeBuildingType == null) // 화살표
        {
            Hide();
        }
        else // 타입
        {
            Show(activeBuildingType.sprite);
        }
    }

    private void Update()
    {
        transform.position = UtilClass.GetMouseWorldPos();
    }


    void Hide()
    {
        gameObject.SetActive(false);
    }

    void Show(Sprite ghostSprite)
    {
        gameObject.SetActive(true);
        spriteRenderer.sprite = ghostSprite;
    }
}
