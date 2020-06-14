using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public RenderTexture rt;
    public RenderTexture depthTexture;

    public Transform[] cubeTransforms;
    public Mesh cubeMesh;
    public Material pureColorMaterial;
    public SkyboxDraw skyboxDraw;

    // Start is called before the first frame update
    void Start()
    {
        rt = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
        depthTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.Depth, RenderTextureReadWrite.Linear);
    }

    private void OnPostRender(){
        Camera cam = Camera.current;

        // rt设置为当前的渲染目标
        Graphics.SetRenderTarget(rt);

        // 将手动创建的rt执行clear操作，使其变成一个白色的贴图
        GL.Clear(true, true, Color.white);

        // start drawcall
        pureColorMaterial.color = new Color(0, 0.5f, 0.8f);
        pureColorMaterial.SetPass(0);

        // 使用同一个材质，绘制多个模型
        foreach(Transform transform in cubeTransforms){
            Graphics.DrawMeshNow(cubeMesh, transform.localToWorldMatrix);
        }
        
        // end drawcall

        skyboxDraw.DrawSkybox(cam, rt.colorBuffer, depthTexture.depthBuffer);

        // 将rt绘制到cam.targetTexture，cam.targetTexture如果为null，则绘制到屏幕上
        Graphics.Blit(rt, cam.targetTexture);
    }
}
