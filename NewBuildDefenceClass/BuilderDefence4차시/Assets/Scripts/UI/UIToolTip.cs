using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIToolTip : MonoBehaviour
{
    static public UIToolTip Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTrm;

    private RectTransform rectTrm;

    private TextMeshProUGUI textMeshPro;

    private RectTransform backgroundRectTrm;

    private TooltipTimer tooltipTimer;


    private void Awake()
    {
        Instance = this;

        rectTrm = GetComponent<RectTransform>();
        backgroundRectTrm = transform.Find("background").GetComponent<RectTransform>();
        textMeshPro = backgroundRectTrm.transform.Find("text").GetComponent<TextMeshProUGUI>();

        Hide();
    }


    private void Update()
    {
        HandleFollowMouse();

        // 툴팁 타이버 처리
        if(tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if(tooltipTimer.timer <= 0)
            {
                Hide();
                tooltipTimer = null;
            }
        }
    }


    private void HandleFollowMouse()
    {
        Vector2 anchoredPos = Input.mousePosition / canvasRectTrm.localScale.x;

        if(anchoredPos.x + backgroundRectTrm.rect.width > canvasRectTrm.rect.width)
        {
            anchoredPos.x = canvasRectTrm.rect.width - backgroundRectTrm.rect.width;
        }

        if (anchoredPos.y + backgroundRectTrm.rect.height > canvasRectTrm.rect.height)
        {
            anchoredPos.y = canvasRectTrm.rect.height - backgroundRectTrm.rect.height;
        }

        rectTrm.anchoredPosition = anchoredPos;
    }

    private void SetText(string tooltopText)
    {
        textMeshPro.SetText(tooltopText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(7, 7);
        backgroundRectTrm.sizeDelta = textSize + padding;
    }

    public void Show(string tooltopText, TooltipTimer tooltipTimer = null)
    {
        // 타이머 세팅
        this.tooltipTimer = tooltipTimer;

        gameObject.SetActive(true);
        SetText(tooltopText);

        HandleFollowMouse();

        // Invoke(nameof(Hide), 3.0f);
    }

    public void Hide()
    {
        // CancelInvoke();
        gameObject.SetActive(false);
    }

    public class TooltipTimer
    {
        public float timer;

        public TooltipTimer(float timer) {
            this.timer = timer;
        }
    }
}
