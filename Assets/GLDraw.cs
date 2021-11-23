using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLDraw : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;
    public List<Food> foods = new List<Food>();

    private void Start()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        foods.Add(new Food(4, sb.y, mat));
        foods.Add(new Food(0, sb.y, mat));
        foods.Add(new Food(-4, sb.y, mat));
        InvokeRepeating("RainFood", 2.0f, 0.5f);
    }

    private void Update()
    {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void OnPostRender()
    {
        Ground();
        
        foods.ForEach(delegate(Food food)
        {
            food.DrawFood();
        });

        // Food food = new Food(0, 0, mat);
        // food.DrawFood();
    }

    private void RainFood()
    {
        foods.ForEach(delegate(Food food)
        {
            food.y -= 1;
        });

        foods.RemoveAll(food => food.y < sb.y * (-1) + 3);
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
        public float x;
        public float y;
        public Material mat;

        public Food(float xPos, float yPos, Material material)
        {
            x = xPos;
            y = yPos;
            mat = material;
        }

        public void DrawFood()
        {
            GL.PushMatrix();
            mat.SetPass(0);
            GL.Begin(GL.QUADS);
            GL.Color(Color.red);

            GL.Vertex3(x, y, 0);
            GL.Vertex3(x, y + 1, 0);
            GL.Vertex3(x + 1, y + 1, 0);
            GL.Vertex3(x + 1, y, 0);

            GL.End();
            GL.PopMatrix();
        }
    }
}
