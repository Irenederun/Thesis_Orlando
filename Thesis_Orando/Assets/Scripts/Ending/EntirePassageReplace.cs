using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class EntirePassageReplace : MonoBehaviour
{
    public TextMeshPro text;
    public List<string> originals;

    private List<string> savedInputs = new List<string>();

    public void Replace(int originalPos, string newString)
    {
        text.text = text.text.Replace(originals[originalPos], "<b>"+newString+"</b> ");
    }

    public void SaveInput(string newInput, int pos)
    {
        savedInputs.Add(pos.ToString() + newInput);
    }

    public void UploadSavedInputs()
    {
        string toUpload = "";
        for (int i=0; i<savedInputs.Count; i++)
        {
            toUpload += savedInputs[i];
            toUpload += ",";
        }
        toUpload += "station" + RestartManager.instance.station;
        print("uploading input string: " + toUpload);

        StartCoroutine(upload(toUpload));
    }

    IEnumerator upload(string inputString)
    {
        string postURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdQ5_rKzIQnia8rPkIwkflBQoS7mMy6Whsf0DjeBKQWg9kAZQ/formResponse";
        string entryID = "entry.25533166";

        WWWForm form = new WWWForm();
        form.AddField(entryID, inputString);

        UnityWebRequest req = UnityWebRequest.Post(postURL, form);
        yield return req.SendWebRequest();

        print(req.result);
    }
}