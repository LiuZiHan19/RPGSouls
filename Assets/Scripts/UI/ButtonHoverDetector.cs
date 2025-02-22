using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        SoundManager.Instance.PlaySfx("Sound/001_Hover_01");
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