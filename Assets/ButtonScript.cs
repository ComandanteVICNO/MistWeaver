using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public Button button;
    public TMP_Text buttonText;
    

    public Color32 originalTextColor = new Color32(255,255,255,255);
    public Color32 newTextColor = new Color32(11,11,11,255);

    Image buttonImage;

    public Sprite originalButtonSprite;
    public Sprite newButtonSprite;

    private ColorBlock buttonColorBlock;

    private void Start()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
        buttonImage = GetComponent<Image>();
        buttonText.color = originalTextColor;
        buttonImage.sprite = originalButtonSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = newButtonSprite;
        buttonText.color = newTextColor;
    }
 

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = originalButtonSprite;
        buttonText.color = originalTextColor;
    }
    private void OnDisable()
    {
        buttonImage.sprite = originalButtonSprite;
        buttonText.color = originalTextColor;
    }
}
