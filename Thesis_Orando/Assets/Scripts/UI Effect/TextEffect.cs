using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    public bool randomize;
    public TMP_Text textComponent;
    public float floatRange = 0.1f; // range
    public float floatSpeed = 1.0f; // speed

    void Start()
    {
        if (randomize)
        {
            floatRange = Random.Range(0.05f, 0.12f);
            floatSpeed = Random.Range(1f, 1.5f);
        }
    }

    void Update()
    {
        textComponent.ForceMeshUpdate(); //force update info of texts
        var textInfo = textComponent.textInfo;

        float deltaTime = Time.deltaTime; // 获取时间间隔

        for(int i = 0; i < textInfo.characterCount; ++i){
            var charInfo = textInfo.characterInfo[i];

            if(!charInfo.isVisible){
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            
            for (int j = 0; j < 4; ++j){
                var orig = verts[charInfo.vertexIndex + j]; // get the original vert
                // adjust float based on
                float floatOffset = Mathf.Sin((Time.time + orig.x) * floatSpeed) * floatRange;
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, floatOffset, 0); // apply it on y
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i){
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices; //apply on new mesh
            textComponent.UpdateGeometry(meshInfo.mesh, i); // new geometry
        }
    }
}
