using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLDraw : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;
    public Food food;

    private void Start()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        food = new Food(1f, 2f, mat);
    }

    private void Update()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void OnPostRender()
    {
        Ground();

        food.DrawFood();
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

    public class Food
    {
        private float x;
        private float y;
        private Material mat;

        public Food(float xPos, float yPos, Material material)
        {
            x = xPos;
            y = yPos;
            mat = material;
        }

        public float getX() {
            return x;
        }

        public float getY() {
            return y;
        }

        public void DrawFood()
        {
            GL.PushMatrix();
            mat.SetPass(0);
            GL.Begin(GL.QUADS);
            GL.Color(Color.grey);

            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 1, 0);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(1, 0, 0);

            GL.End();
            GL.PopMatrix();
        }
    }
}
