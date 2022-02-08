using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntroduceCometUI : MonoBehaviour
{
    [SerializeField] Transform previewContent;
    [SerializeField] GameObject previewOrigin;
    [SerializeField] RenderTexture renderTextureOrigin;

    public void CreatePreview()
    {
        foreach (st_CometSpawnInfo info in CometSpawner.instance.list_spawnInfo)
        {
            GameObject tmpPreview = Instantiate(previewOrigin, previewContent);
            // preview renderer
            RenderTexture rt = new RenderTexture(renderTextureOrigin);
            tmpPreview.transform.GetChild(1).GetComponent<RawImage>().texture = rt;
            tmpPreview.transform.GetChild(4).GetComponent<Camera>().targetTexture = rt;
            // change mesh & material 
            Transform previewCometPoint = tmpPreview.transform.GetChild(4).GetChild(0);
            previewCometPoint.GetComponent<MeshFilter>().mesh = info.cometPrefab.GetComponent<MeshFilter>().sharedMesh;
            previewCometPoint.GetComponent<MeshRenderer>().materials = info.cometPrefab.GetComponent<MeshRenderer>().sharedMaterials;
            
            tmpPreview.SetActive(true);
        }
        Destroy(previewOrigin);
    }

}
