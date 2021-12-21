using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSort : MonoBehaviour
{
    [SerializeField]
    private bool bRunOnce = true;

    [SerializeField]
    private float posOffsetY;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        posOffsetY = -transform.localPosition.y;
    }

    private void LateUpdate() 
    {
        float precisionMultiplier = 5f; // 정밀도 증가를 위해 곱해줌

        spriteRenderer.sortingOrder = (int)(-(transform.position.y + posOffsetY) * precisionMultiplier);

        if(bRunOnce)
        {
            Destroy(this);
        }
    }
}
