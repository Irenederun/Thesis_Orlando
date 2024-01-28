using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingManager : MonoBehaviour
{
    public static ScalingManager instance;
    public List<GameObject> ScalingPageObjs = new List<GameObject>();
    public List<GameObject> selectedChips = new List<GameObject>();
    public List<Vector3> chipPosInitial = new List<Vector3>();
    public GameObject chipPrefab;
    public GameObject chipParent;
    public float allChipsWorthThisTurn;
    public GameObject scale;

    //one time use
    private bool coroutineHasStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
       
    // Start is called before the first frame update
    void Start()
    {
        LoadAvailableChips();
        //i think should be changing interactability of everything to true in here
    }

    public void LoadAvailableChips()
    {
        for (int i = 0; i < GameManager.instance.myChips.Count; i++)
        {
            GameObject thisChip = Instantiate(chipPrefab, chipPosInitial[i], Quaternion.identity);
            thisChip.transform.SetParent(chipParent.transform);
            thisChip.GetComponent<SpriteRenderer>().color = GameManager.instance.myChips[i].ChipColor;
            thisChip.GetComponent<ChipBehavior>().thisChip = GameManager.instance.myChips[i];
            thisChip.GetComponent<ChipBehavior>().chipCategory = GameManager.instance.myChips[i].ChipCategory;
            ScalingPageObjs.Add(thisChip);
        }
    }

    public void ChipsSubmission()
    {
        SwitchInteractabilityForAll(false);

        for (int i = 0; i < selectedChips.Count; i++)
        {
            if (selectedChips[i] != null)
            {
                selectedChips[i].GetComponent<ChipBehavior>().ChipSubmission(i);    
            }
        }
    }

    public void SwitchInteractabilityForAll(bool interactability)
    {
        foreach (GameObject obj in ScalingPageObjs)
        {
            obj.layer = LayerMask.NameToLayer("Default");
        }
    }

    public void CalculateWorth(float chipworth)
    {
        allChipsWorthThisTurn += chipworth;
        if (!coroutineHasStarted)
        {
            StartCoroutine("WorthCalculated");
            coroutineHasStarted = true;
        }
    }

    IEnumerator WorthCalculated()
    {
        yield return new WaitForSeconds(0.5f);
        print(allChipsWorthThisTurn);
    }

    public void DestroySelfOnClose()
    {
        Destroy(gameObject);
    }
}
