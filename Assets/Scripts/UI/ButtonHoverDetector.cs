using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        SoundManager.Instance.PlaySfx("Sound/sfx_btn_hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    private void Update()
    {
        if (isHovering)
        {
        }
    }
}