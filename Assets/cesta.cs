using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cesta : MonoBehaviour
{

    public Material fig;
    public Vector3 center = new Vector3(0, 0, 0); 
    public float radius = 2;
    public Vector2 limiteCam;
    public float cx=0;
    public float velo = 0.05f;

    private void Start(){
        limiteCam = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void Update(){
        limiteCam = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        if(Input.GetKey(KeyCode.LeftArrow)){
            cx -= velo;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            cx += velo;
        }
    }

    void OnPostRender(){
        Cesta();        
    }

    void Cesta(){
        GL.PushMatrix();
        fig.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        float cy = -limiteCam.y;
        radius = 1.5f;
        for (float t = center.x ; t < ((Mathf.PI)); t += 0.01f)
        {
           Vector3 ci = (new Vector3(Mathf.Cos(t) * radius*-1 + cx, Mathf.Sin(t) * radius*-1, center.z));
           GL.Vertex3(ci.x, ci.y, ci.z);
        }        
        
        GL.Vertex3(cx-radius, cy, 0);
        GL.Vertex3(cx+radius, cy, 0);

        GL.End();
        GL.PopMatrix();
    }
}
