using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Text buttonText;  // 버튼 이미지
    public Color normalColor; // 기본 색상 (흰색)
    public Color highlightedColor; // 마우스 올렸을 때 색상 (녹색)

    void Start()
    {
        // 버튼의 Image 컴포넌트 가져오기
        buttonText = transform.GetChild(0).GetComponent<Text>();
        normalColor = Color.black;
        highlightedColor = Color.white;
    }


    // 색상 변경 (마우스 올렸을 때)
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("마우스 올라감");
        if (buttonText != null)
        {
            buttonText.color = highlightedColor;  // 색상 변경 (녹색)
        }
    }

    // 색상 되돌리기 (마우스 벗어날 때)
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("마우스 내려감");
        if (buttonText != null)
        {
            buttonText.color = normalColor;  // 원래 색상으로 변경 (흰색)
        }
    }

}
