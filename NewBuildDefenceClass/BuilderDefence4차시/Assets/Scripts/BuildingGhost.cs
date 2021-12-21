using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    // 건물을 선택하면 빌딩 고스트 이미지가 show(빌딩 타입에 따라서 스프라이트 변경 되어야 함)
    // 화살표 선택하면 hide
    // 구독, 발행
    // update 함수에서 마우스 포지션 얻어와야 함

    private GameObject spriteObj;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteObj = transform.Find("sprite").gameObject;

        spriteRenderer = spriteObj.GetComponent<SpriteRenderer>();

        Hide();
    }
    
    void Start()
    {
        BuilderManager.Instance.OnActiveBuildingTypeChanged += CallOnActiveBuildingTypeChanged;
    }

    void Update()
    {
        // 마우스 커서 동기화
        transform.position = UtilClass.GetMouseWorldPos();
    }

    void CallOnActiveBuildingTypeChanged(BuildingTypeSO activeBuildingType)
    {
        if(activeBuildingType == null)
        {
            Hide();
        }
        else
        {
            Show(activeBuildingType.sprite);
        }
    }

    void Show(Sprite ghostSprite)
    {
        spriteObj.SetActive(true);
        spriteRenderer.sprite = ghostSprite;
    }

    void Hide()
    {
        spriteObj.SetActive(false);
    }
}
