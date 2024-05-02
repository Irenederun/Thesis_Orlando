using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WordDragLoader : MonoBehaviour
{
    public GameObject wordDragPrefab;
    public Transform holderTransform;
    public string usedVerbC;
    public string usedNounB;

    [System.Serializable]
    public struct Sentences
    {
        public List<string> sentence;
    }
    public List<Sentences> listOfSentence;

    void Start()
    {
        //if ()
        StartCoroutine(Wait());
        //usedNounB = DialogueManager.instance.myFlowchart.GetStringVariable("usedNounB");
        //usedVerbC = DialogueManager.instance.myFlowchart.GetStringVariable("usedVerbC");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        usedNounB = DialogueManager.instance.myFlowchart.GetStringVariable("usedNounB");
        usedVerbC = DialogueManager.instance.myFlowchart.GetStringVariable("usedVerbC");
    }

    public void UsedVerbC(string text)
    {
        usedVerbC = text;
        listOfSentence[6].sentence[3] = usedVerbC;
    }

    public void UsedNounB(string text)
    {
        usedNounB = text;
        listOfSentence[7].sentence[1] = usedNounB;
    }
    
    public void LoadWordDragPrefab()
    {
        int prefabNo = GameManager.instance.currentSentenceNo;
        GameObject thisPrefab = Instantiate(wordDragPrefab);
        thisPrefab.GetComponent<WordDragManager>().loader = this;
        if (prefabNo == 6)
        {
            thisPrefab.GetComponent<WordDragManager>().SetDeterminedText(0, usedVerbC);
            UsedVerbC(usedVerbC);
        }
        else if (prefabNo == 7)
        {
            thisPrefab.GetComponent<WordDragManager>().SetDeterminedText(1, usedNounB);
            UsedNounB(usedNounB);
        }
        thisPrefab.GetComponent<WordDragManager>().sentenceNo = prefabNo;
        thisPrefab.transform.parent = holderTransform;
        thisPrefab.transform.localPosition = Vector3.zero;
        thisPrefab.GetComponent<WordDragManager>().sentence.Clear();
        thisPrefab.GetComponent<WordDragManager>().sentence.AddRange(listOfSentence[prefabNo].sentence);
    }
    
}
