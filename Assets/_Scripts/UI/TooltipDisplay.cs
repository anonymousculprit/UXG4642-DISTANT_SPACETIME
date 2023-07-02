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

public class TooltipDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public string tooltipDescription;
    public float waitForSeconds = 3;
    public float fadeTime = 0.5f;
    public GameObject tooltipGameObject;
    public RectTransform tooltipCanvas;

    RectTransform tt_PanelRTransform, tt_GORTransform;
    Image tt_Panel;
    TextMeshProUGUI tt_Text;
    Color col_textFade, col_panelFade, col_textO, col_panelO;

    private void Start()
    {
        // TODO: Assign correct Components to correct Variables
        tt_GORTransform = tooltipGameObject.GetComponent<RectTransform>();
        tt_Text.text = tooltipDescription;

        // get color to lerp from
        col_textFade = tt_Text.color;
        col_panelFade = tt_Panel.color;
        col_textFade.a = 0;
        col_panelFade.a = 0;

        // get color to lerp to
        col_textO = tt_Text.color;
        col_panelO = tt_Panel.color;
    }

    private void Update()
    {
        // placing tooltip on cursor, where appropriate based on tooltip box size
        if (tooltipGameObject.activeInHierarchy)
        {
            Vector2 anchorPos = Input.mousePosition / tooltipCanvas.localScale.x;

            if (anchorPos.x + tt_PanelRTransform.rect.width > tooltipCanvas.rect.width)
                anchorPos.x = tooltipCanvas.rect.width - tt_PanelRTransform.rect.width;

            if (anchorPos.x + tt_PanelRTransform.rect.width > tooltipCanvas.rect.width)
                anchorPos.x = tooltipCanvas.rect.width - tt_PanelRTransform.rect.width;

            tt_GORTransform.anchoredPosition = anchorPos;
        }
    }


    IEnumerator ShowTooltip()
    {
        yield return new WaitForSeconds(waitForSeconds);
        tooltipGameObject.SetActive(true);

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
        tooltipGameObject.SetActive(false);
        tt_Panel.color = col_panelFade;
        tt_Text.color = col_textFade;
    }

    public void OnPointerEnter(PointerEventData eventData) => StartCoroutine(ShowTooltip());
    public void OnPointerExit(PointerEventData eventData) => ResetTooltip();
    public void OnPointerMove(PointerEventData eventData) => ResetTooltip();
}
