using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    Image btnImage;
    private Text buttonText;  // 버튼 이미지
    public Color textBlack; // 기본 색상 (블랙)
    public Color textWhite; // 마우스 올렸을 때 색상 (흰색)
    public Color imageGrean = new Color(0.092f, 0.2196079f, 0.2196079f);

    void Start()
    {
        btnImage = GetComponent<Image>();
        // 버튼의 Image 컴포넌트 가져오기
        buttonText = transform.GetChild(0).GetComponent<Text>();
        textBlack = Color.black;
        textWhite = Color.white;
    }


    // 색상 변경 (마우스 올렸을 때)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            //btnImage.color = imageGrean;
            buttonText.color = textWhite;  // 색상 변경 (흰색)
        }
    }

    // 색상 되돌리기 (마우스 벗어날 때)
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            btnImage.color = textWhite;
            buttonText.color = textBlack;  // 원래 색상으로 변경 (흰색)
        }
    }


}
