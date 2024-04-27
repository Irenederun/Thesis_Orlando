using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("isHovering", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("isHovering", false);
    }
}