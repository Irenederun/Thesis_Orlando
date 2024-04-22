using System.Collections.Generic;
using UnityEngine;

public class WordDragLoader : MonoBehaviour
{
    public GameObject wordDragPrefab;
    public Transform holderTransform;

    [System.Serializable]
    public struct Sentences
    {
        public List<string> sentence;
    }
    public List<Sentences> listOfSentence;
    
    public void LoadWordDragPrefab()
    {
        int prefabNo = GameManager.instance.currentSentenceNo;
        GameObject thisPrefab = Instantiate(wordDragPrefab);
        if (prefabNo == 6)
        {
            listOfSentence[6].sentence[1] = WordDragManager.instance.usedVerbC;
            thisPrefab.GetComponent<WordDragManager>().SetDeterminedText(0, WordDragManager.instance.usedVerbC);
        }
        else if (prefabNo == 7)
        {
            listOfSentence[6].sentence[1] = WordDragManager.instance.usedNounB;
            thisPrefab.GetComponent<WordDragManager>().SetDeterminedText(1, WordDragManager.instance.usedNounB);
        }
        thisPrefab.GetComponent<WordDragManager>().sentenceNo = prefabNo;
        thisPrefab.transform.parent = holderTransform;
        thisPrefab.transform.localPosition = Vector3.zero;
        thisPrefab.GetComponent<WordDragManager>().sentence.Clear();
        thisPrefab.GetComponent<WordDragManager>().sentence.AddRange(listOfSentence[prefabNo].sentence);
    }
    
}
