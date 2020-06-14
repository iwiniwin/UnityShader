using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkyboxDraw 
{
    private static Mesh m_Mesh;
    public static Mesh fullScreenMesh{
        get{
            if(m_Mesh != null){
                return m_Mesh;
            }
            m_Mesh = new Mesh();
            m_Mesh.vertices = new Vector3[]{
                new Vector3(-1, -1, 0),
                new Vector3(-1, 1, 0),
                new Vector3(1, 1, 0),
                new Vector3(1, -1, 0),
            };
            m_Mesh.uv = new Vector2[]{
                new Vector2(0, 1),
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
            };
            m_Mesh.SetIndices(new int[]{0, 1, 2, 3}, MeshTopology.Quads, 0);
            return m_Mesh;
        }
    }

    // 根据属性名称获取一个唯一ID
    public static int _Corner = Shader.PropertyToID("_Corner");
    public Material skyboxMaterial;
    private static Vector4[] corners = new Vector4[4];
    public void DrawSkybox(Camera cam, RenderBuffer colorBuffer, RenderBuffer depthBuffer){
        // 将四个远裁剪面角传入到shader中
        corners[0] = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.farClipPlane));
        corners[1] = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.farClipPlane));
        corners[2] = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.farClipPlane));
        corners[3] = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.farClipPlane));
        skyboxMaterial.SetVectorArray(_Corner, corners);
        skyboxMaterial.SetPass(0);
        Graphics.SetRenderTarget(colorBuffer, depthBuffer);
        Graphics.DrawMeshNow(fullScreenMesh, Matrix4x4.identity);
    }
}
