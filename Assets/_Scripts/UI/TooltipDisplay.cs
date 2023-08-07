using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipDescription;
    public float waitForSeconds = 1;
    public float fadeTime = 0.5f;
    public GameObject tooltipGO;
    public RectTransform tooltipCanvas;

    RectTransform tt_PanelRTransform;
    public Image tt_Panel;
    public TextMeshProUGUI tt_Text;
    public Color col_textO, col_panelO;
    Color col_textFade, col_panelFade;

    private void Start()
    {
        tt_PanelRTransform = tt_Panel.gameObject.GetComponent<RectTransform>();
        tt_Text.text = tooltipDescription;

        // get color to lerp from
        col_textFade = col_textO;
        col_panelFade = col_panelO;
        col_textFade.a = 0;
        col_panelFade.a = 0;

        tt_Text.color = col_textFade;
        tt_Panel.color = col_panelFade;
    }

    private void Update()
    {
        // placing tooltip on cursor, where appropriate based on tooltip box size
        if (tooltipGO.activeInHierarchy)
        {
            Vector2 displayPos = Input.mousePosition;

            float screenTop = Screen.height;
            float screenRight = Screen.width;

            float screenToCanvasRatio_Height = screenTop / tooltipCanvas.rect.height;
            float screenToCanvasRatio_Width = screenRight / tooltipCanvas.rect.width;

            float halfOfTooltipWidth = tt_PanelRTransform.rect.width / 2;
            float halfOfTooltipHeight = tt_PanelRTransform.rect.height / 2;

            displayPos.x -= halfOfTooltipWidth;
            displayPos.y += halfOfTooltipHeight;

            if (displayPos.x + halfOfTooltipWidth > screenRight)
                displayPos.x = screenRight - halfOfTooltipWidth;

            if (displayPos.x < halfOfTooltipWidth)
                displayPos.x = halfOfTooltipWidth;

            if (displayPos.y + halfOfTooltipHeight > screenTop)
                displayPos.y = screenTop - halfOfTooltipHeight;

            if (displayPos.y < halfOfTooltipHeight )
                displayPos.y = halfOfTooltipHeight;

            tt_PanelRTransform.anchoredPosition = new Vector2(displayPos.x / screenToCanvasRatio_Width, displayPos.y / screenToCanvasRatio_Height);
        }
    }


    IEnumerator ShowTooltip()
    {
        tt_Text.text = tooltipDescription;
        yield return new WaitForSeconds(waitForSeconds);
        tooltipGO.SetActive(true);

        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            tt_Text.color = Color.Lerp(col_textFade, col_textO, t / fadeTime);
            tt_Panel.color = Color.Lerp(col_panelFade, col_panelO, t / fadeTime);
            yield return null;
        }
        tt_Text.color = col_textO;
        tt_Panel.color = col_panelO;
        yield return null;
    }

    void ResetTooltip()
    {
        StopAllCoroutines();
        tooltipGO.SetActive(false);
        tt_Panel.color = col_panelFade;
        tt_Text.color = col_textFade;
    }

    public void OnDisable() => ResetTooltip();

    public void OnPointerEnter(PointerEventData eventData) => StartCoroutine(ShowTooltip());
    public void OnPointerExit(PointerEventData eventData) => ResetTooltip();
    //public void OnPointerMove(PointerEventData eventData) => ResetTooltip();
}
