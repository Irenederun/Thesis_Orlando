using System.Collections.Generic;
using UnityEngine;

public class OtherWordLibrary : MonoBehaviour
{
    public static OtherWordLibrary instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<string> output;

    [System.Serializable]
    public class OtherWord
    {
        public string otherWordPos;
        public string otherWordText;
        public string otherWordLimb;
    }

    [System.Serializable]
    public class OtherDictionary
    {
        public string level;
        public List<OtherWord> otherWords;
    }
    public List<OtherDictionary> otherDictionary;

    
    public List<string> Find(string inputLevel, string inputPos)
    {
        for (int i = 0; i < otherDictionary.Count; i++)
        {
            if (otherDictionary[i].level == inputLevel)
            {
                for (int j = 0; j < otherDictionary[i].otherWords.Count; j++)
                {
                    if (otherDictionary[i].otherWords[j].otherWordPos == inputPos)
                    {
                        output[0] = otherDictionary[i].otherWords[j].otherWordText;
                        output[1] = i.ToString();
                        output[2] = ( j.ToString());
                        output[3] = (otherDictionary[i].otherWords[j].otherWordLimb);
                        print(output[0] + output[1] + output [2] + output [3]);
                    }
                }
            }
        }

        return output;
    }
    
    
    public void Modify(int inputLevel, int inputPos, string text, string limbName)
    {
       //text.Replace(otherDictionary[inputLevel].otherWords[inputPos].otherWordText, text);
       otherDictionary[inputLevel].otherWords[inputPos].otherWordText = text;
       otherDictionary[inputLevel].otherWords[inputPos].otherWordLimb = limbName;
    }
}
