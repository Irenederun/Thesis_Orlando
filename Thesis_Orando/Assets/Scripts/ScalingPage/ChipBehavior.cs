using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBehavior : BasicBehavior
{
    private int clickTimes;

    public List<Vector3> chipPos;
    private Vector3 chipDesPos = Vector3.zero;
    public float speed;
    public float chipWorth;
    public GameManager.chipsInfo thisChip;
    public string chipCategory;

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

    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        chipState = ChipState.Default;
        selectedEffect = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
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
        Debug.Log("Selected");
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
        print("coroutine");
        yield return new WaitForSeconds(2f);
        ChipDeleting();
    }

    private void ChipDeleting()
    {
        chipState = ChipState.DeleteFromInvent;
        GameManager.instance.myChips.Remove(thisChip);
        //print("deleted " + thisChip.ChipCategory);
    }
}
