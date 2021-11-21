using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLDraw : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;

    private void Start()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void Update()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void OnPostRender()
    {
        Ground();
    }

    void Ground()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.grey);

        GL.Vertex3(sb.x  * (-1), sb.y * (-1), 0);
        GL.Vertex3(sb.x  * (-1), sb.y * (-1) + 3, 0);
        GL.Vertex3(sb.x, sb.y * (-1) + 3, 0);
        GL.Vertex3(sb.x, sb.y * (-1), 0);

        GL.End();
        GL.PopMatrix();
    }
}
