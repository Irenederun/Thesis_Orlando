using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBehavior : BasicBehavior
{
    public List<Vector3> chipPos;
    private Vector3 chipDesPos = Vector3.zero;
    public float speed;
    public float chipWorth;
    public GameManager.chipsInfo thisChip;
    public string chipCategory;

    private int clickTimes;
    private SpriteRenderer sp;
    private Vector4 chipStartColor;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.5f;

    [SerializeField]
    private enum ChipState
    {
        Default,//defualt = not selected
        Selected,
        Submitted,
        DeleteFromInvent,
    }
    [SerializeField]
    private ChipState chipState;

    [SerializeField]
    private GameObject selectedEffect;

    void Start()
    {
        chipState = ChipState.Default;
        selectedEffect = transform.GetChild(0).gameObject;

        sp = GetComponent<SpriteRenderer>();
        chipStartColor = sp.color;
    }

    void Update()
    {
        if (chipState == ChipState.Submitted)
        {
            if (chipDesPos != Vector3.zero)
            {
                transform.position = Vector3.SmoothDamp(transform.position, chipDesPos, ref velocity, smoothTime, Mathf.Infinity);
            }
        }
    }

    public override void ClickedByMouse()
    {
        base.ClickedByMouse();

        switch (clickTimes)
        {
            case 0:
                if (ScalingManager.instance.selectedChips.Count < 3)
                {
                    ChipSelection();
                    clickTimes++;
                }
                break;
            case 1:
                ChipUnselection();
                clickTimes--;
                break;
        }
    }

    public void ChipSelection()
    {
        ChipSelected();
    }

    private void ChipSelected()
    {
        chipState = ChipState.Selected;
        selectedEffect.SetActive(true);
        ScalingManager.instance.selectedChips.Add(gameObject);
    }

    private void ChipUnselection()
    {
        ChipUnselected();
    }

    private void ChipUnselected()
    {
        chipState = ChipState.Default;
        Debug.Log("Unselected");
        selectedEffect.SetActive(false);
        ScalingManager.instance.selectedChips.Remove(gameObject);
    }

    public void ChipSubmission(int chipNumber)
    {
        ChipSubmitted(chipNumber);
    }

    private void ChipSubmitted(int chipNo)
    {
        chipState= ChipState.Submitted;

        switch (chipCategory)
        {
            case "CAT1":
                chipWorth = GameManager.instance.chipWorthChart[GameManager.instance.currentLevel].Value1;
                break;

            case "CAT2":
                chipWorth = GameManager.instance.chipWorthChart[GameManager.instance.currentLevel].Value2;
                break;

            case "CAT3":
                chipWorth = GameManager.instance.chipWorthChart[GameManager.instance.currentLevel].Value3;
                break;

            case "CAT4":
                chipWorth = GameManager.instance.chipWorthChart[GameManager.instance.currentLevel].Value4;
                break;
        }

        selectedEffect.SetActive(false);
        chipDesPos = chipPos[chipNo];
        ScalingManager.instance.CalculateWorth(chipWorth);
        IEnumerator coroutine = ChipDeletion();
        StartCoroutine(coroutine);
    }

    private IEnumerator ChipDeletion()
    {
        yield return new WaitForSeconds(2f);

        Color endCol = new Vector4(chipStartColor.x, chipStartColor.y, chipStartColor.z, 0);
        IEnumerator coroutin = DoAThingOverTime(chipStartColor, endCol, 0.5f);
        StartCoroutine(coroutin);
        ChipDeleting();

        yield return new WaitForSeconds(1f);
        //ChipDeleting();
        chipState = ChipState.DeleteFromInvent;
        Destroy(gameObject);
    }

    //fade out
    private IEnumerator DoAThingOverTime(Color start, Color end, float duration)
    {
        Color someColorValue;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            someColorValue = Color.Lerp(start, end, normalizedTime);
            sp.color = someColorValue;
            yield return null;
        }
        someColorValue = end; //without this, the value will end at something like 0.9992367
        sp.color = someColorValue;
    }

    private void ChipDeleting()
    {
        //chipState = ChipState.DeleteFromInvent;
        ScalingManager.instance.ScalingPageObjs.Remove(gameObject);
        ScalingManager.instance.selectedChips.Remove(gameObject);
        GameManager.instance.myChips.Remove(thisChip);
        //Destroy(gameObject);
    }
}
