using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xbr : MonoBehaviour
{
    [Range(1.0f, 256)]
    public int v_resolution = 224;

    private int scale = 4;
    private Vector2 texSize = new Vector2(200, 200);
    private Material mat;
    public Shader sh;

    private RenderTexture renT;

    void Start()
    {
        texSize = new Vector2(Mathf.RoundToInt(((float)v_resolution / (float)Screen.height) * (float)Screen.width), v_resolution);
        mat = new Material(sh);
        renT = new RenderTexture((int)(texSize.x), (int)texSize.y, 0);
        renT.filterMode = FilterMode.Point;
    }

    void OnPreRender()
    {
        GetComponent<Camera>().targetTexture = renT;
    }

    void OnRenderImage(RenderTexture reTex, RenderTexture desTex)
    {

        desTex = RenderTexture.GetTemporary((int)(texSize.x * scale), (int)(texSize.y * scale), 0);

        reTex.filterMode = FilterMode.Point;
        mat.SetVector("texture_size", texSize);
        mat.SetTexture("decal", reTex);
        mat.SetTexture("_BackgroundTexture", reTex);
        mat.SetTexture("_MainTex", reTex);

        Graphics.Blit(reTex, desTex, mat);
        GetComponent<Camera>().targetTexture = null;
        Graphics.Blit(desTex, null as RenderTexture);

        //记得释放内存，不然电脑会炸
        RenderTexture.ReleaseTemporary(desTex);
        reTex.DiscardContents();
    }
}