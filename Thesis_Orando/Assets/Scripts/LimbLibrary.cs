using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LimbLibrary")]
public class LimbLibrary : ScriptableObject
{
    [System.Serializable]
    public struct LimbData
    {
        public string name;
        public Sprite limbSprite;
    }

    public List<LimbData> availableLimbs = new List<LimbData>();

    public Sprite GetLimb(string limbName)
    {
        for (int i = 0; i < availableLimbs.Count; i++)
        {
            if (availableLimbs[i].name == limbName)
            {
                if (availableLimbs[i].limbSprite != null)
                    return availableLimbs[i].limbSprite;
            }
        }

        return null;
    }
}
