using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWobble : MonoBehaviour
{
   TMP_Text textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        TMP_MeshInfo[] cachedMeshInfo = textMesh.textInfo.CopyMeshInfoVertexData();

        for (int i = 0; i < cachedMeshInfo.Length; i++)
        {
            TMP_MeshInfo meshInfo = cachedMeshInfo[i];

            for (int j = 0; j < meshInfo.vertices.Length; j++)
            {
                Vector3 offset = Wobble(Time.time + j);

                meshInfo.vertices[j] = meshInfo.vertices[j] + offset;
            }

            textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }
    }

    Vector3 Wobble(float time)
    {
        return new Vector3(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 2.5f), 0f);
    }
}
