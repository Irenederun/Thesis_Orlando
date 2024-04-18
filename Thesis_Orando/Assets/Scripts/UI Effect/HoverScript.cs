using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("IsHovered", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("IsHovered", false);
    }
}
