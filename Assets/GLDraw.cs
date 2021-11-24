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
    InvokeRepeating("RainFood", 0.0f, 0.1f);
    InvokeRepeating("CreateMeatball", 0.0f, 1.3f);
    InvokeRepeating("CreatePizza", 7.0f, 7.0f);
    InvokeRepeating("CreateHamburger", 10.0f, 10.0f);
  }

  private void Update()
  {
    sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
  }

  private void OnPostRender()
  {
    Background();
    Ground();

    foods.ForEach(delegate (Food food)
    {
      food.DrawFood();
    });
  }

  private void RainFood()
  {
    foods.ForEach(delegate (Food food)
    {
      food.y -= food.dropSpeed;
      food.yBoundaries[0] -= food.dropSpeed;
      food.yBoundaries[1] -= food.dropSpeed;
    });

    foods.RemoveAll(food => food.yBoundaries[0] < sb.y * (-1) + 3);
  }

  private void CreateMeatball()
  {
    float x = Random.Range(sb.x * -1, sb.x - 1);

    if (foods.Count < 5) foods.Add(new Meatball(x, sb.y, mat));
  }

  private void CreatePizza()
  {
    float x = Random.Range(sb.x * -1, sb.x - 1);

    if (foods.Count < 5) foods.Add(new Pizza(x, sb.y, mat));
  }

  private void CreateHamburger()
  {
    float x = Random.Range(sb.x * -1, sb.x - 1);

    if (foods.Count < 5) foods.Add(new Hamburger(x, sb.y, mat));
  }

  void Ground()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    Color color = new Color(0.27f, 0.72f, 0.19f, 1f);
    GL.Color(color);

    GL.Vertex3(sb.x * (-1), sb.y * (-1), 0);
    GL.Vertex3(sb.x * (-1), sb.y * (-1) + 3, 0);
    GL.Vertex3(sb.x, sb.y * (-1) + 3, 0);
    GL.Vertex3(sb.x, sb.y * (-1), 0);

    GL.End();
    GL.PopMatrix();
  }

  void Background()
  {
    Sky();
    Clouds();
  }

  void Sky()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    Color color = new Color(0.4f, 0.47f, 0.61f, 1f);
    GL.Color(color);

    GL.Vertex3(sb.x * (-1), sb.y * (-1), 0);
    GL.Vertex3(sb.x * (-1), sb.y, 0);
    GL.Vertex3(sb.x, sb.y, 0);
    GL.Vertex3(sb.x, sb.y * (-1), 0);

    GL.End();
    GL.PopMatrix();
  }

  void Clouds()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.LINES);
    Color color = new Color(0.62f, 0.62f, 0.62f, 1f);
    GL.Color(color);

    Vector3 center = new Vector3(sb.x * (-1), sb.y, 0);
    drawCircle(center, 3.0f);

    center = new Vector3(sb.x * (-1) + 2.5f, sb.y, 0);
    drawCircle(center, 2.0f);

    center = new Vector3(sb.x * (-1) + 6.5f, sb.y + 2, 0);
    drawCircle(center, 4.0f);

    center = new Vector3(sb.x * (-1) + 9.5f, sb.y + 1, 0);
    drawCircle(center, 3.5f);

    center = new Vector3(sb.x * (-1) + 13f, sb.y + 1, 0);
    drawCircle(center, 3.5f);

    center = new Vector3(sb.x * (-1) + 16.5f, sb.y + 1.5f, 0);
    drawCircle(center, 3.5f);

    center = new Vector3(sb.x * (-1) + 19.5f, sb.y, 0);
    drawCircle(center, 2.5f);

    center = new Vector3(sb.x * (-1) + 23.5f, sb.y - 0.5f, 0);
    drawCircle(center, 2.5f);

    center = new Vector3(sb.x * (-1) + 25.5f, sb.y, 0);
    drawCircle(center, 1.5f);

    center = new Vector3(sb.x * (-1) + 28.5f, sb.y, 0);
    drawCircle(center, 2.5f);

    GL.End();
    GL.PopMatrix();
  }

  void drawCircle(Vector3 center, float radius)
  {
    for (float t = 0.0f; t < (2 * Mathf.PI); t += 0.001f)
    {
        Vector3 ci = (
            new Vector3(Mathf.Cos(t) * radius + center.x,
            Mathf.Sin(t) * radius + center.y,
            center.z
        ));

            GL.Vertex3(center.x, center.y, center.z);
            GL.Vertex3(ci.x, ci.y, ci.z);
            GL.Vertex3(center.x, center.y, center.z);
        }
  }

    public class Food
  {
    public float x;
    public float y;
    public Material mat;
    public float dropSpeed;
    public int score;
    public float[] xBoundaries;
    public float[] yBoundaries;

    public Food(float xPos, float yPos, Material material)
    {
      x = xPos;
      y = yPos;
      mat = material;
      xBoundaries = new float[2] {x, x + 1};
      yBoundaries = new float[2] {y, y + 1};
    }

    public virtual void DrawFood() {}
  }

  public class Meatball : Food
  {
    public Meatball(float xPos, float yPos, Material material) : base(xPos, yPos, material)
    {
      score = 100;
      dropSpeed = 0.2f;
    }

    public override void DrawFood()
    {
      Color color = new Color(0.6f, 0.15f, 0.15f, 1f);

      GL.PushMatrix();
      mat.SetPass(0);
      GL.Begin(GL.QUADS);
      GL.Color(color);

      GL.Vertex3(x + 0.1f, y + 0.1f, 0);
      GL.Vertex3(x + 0.1f, y + 0.9f, 0);
      GL.Vertex3(x + 0.9f, y + 0.9f, 0);
      GL.Vertex3(x + 0.9f, y + 0.1f, 0);

      GL.End();
      GL.PopMatrix();

      GL.PushMatrix();
      mat.SetPass(0);
      GL.Begin(GL.QUADS);
      GL.Color(color);

      GL.Vertex3(x + 0.2f, y, 0);
      GL.Vertex3(x + 0.2f, y + 1, 0);
      GL.Vertex3(x + 0.8f, y + 1, 0);
      GL.Vertex3(x + 0.8f, y, 0);

      GL.End();
      GL.PopMatrix();

      GL.PushMatrix();
      mat.SetPass(0);
      GL.Begin(GL.QUADS);
      GL.Color(color);

      GL.Vertex3(x, y + 0.2f, 0);
      GL.Vertex3(x, y + 0.8f, 0);
      GL.Vertex3(x + 1, y + 0.8f, 0);
      GL.Vertex3(x + 1, y + 0.2f, 0);

      GL.End();
      GL.PopMatrix();
    }
  }

  public class Pizza : Food
  {
    public Pizza(float xPos, float yPos, Material material) : base(xPos, yPos, material)
    {
      score = 300;
      dropSpeed = 0.35f;
    }

    public override void DrawFood()
    {
      GL.PushMatrix();
      mat.SetPass(0);
      GL.Begin(GL.TRIANGLES);
      GL.Color(Color.yellow);

      GL.Vertex3(x + 0.5f, y, 0);
      GL.Vertex3(x , y + 1.25f, 0);
      GL.Vertex3(x + 1, y + 1.25f, 0);

      GL.End();
      GL.PopMatrix();

      GL.PushMatrix();
      mat.SetPass(0);
      GL.Begin(GL.QUADS);
      GL.Color(Color.red);

      GL.Vertex3(x + 0.25f, y + 1.00f, 0);
      GL.Vertex3(x + 0.25f, y + 1.20f, 0);
      GL.Vertex3(x + 0.40f, y + 1.20f, 0);
      GL.Vertex3(x + 0.40f, y + 1.00f, 0);

      GL.End();
      GL.PopMatrix();

      GL.PushMatrix();
      mat.SetPass(0);
      GL.Begin(GL.QUADS);
      GL.Color(Color.red);

      GL.Vertex3(x + 0.60f, y + 0.80f, 0);
      GL.Vertex3(x + 0.60f, y + 0.95f, 0);
      GL.Vertex3(x + 0.75f, y + 0.95f, 0);
      GL.Vertex3(x + 0.75f, y + 0.80f, 0);

      GL.End();
      GL.PopMatrix();

      GL.PushMatrix();
      mat.SetPass(0);
      GL.Begin(GL.QUADS);
      GL.Color(Color.red);

      GL.Vertex3(x + 0.45f, y + 0.30f, 0);
      GL.Vertex3(x + 0.45f, y + 0.45f, 0);
      GL.Vertex3(x + 0.60f, y + 0.45f, 0);
      GL.Vertex3(x + 0.60f, y + 0.30f, 0);

      GL.End();
      GL.PopMatrix();
    }
  }

  public class Hamburger : Food
  {
    public Hamburger(float xPos, float yPos, Material material) : base(xPos, yPos, material)
    {
      score = 500;
      dropSpeed = 0.4f;
    }

    public override void DrawFood()
    {
      Color color = new Color(0.6f, 0.15f, 0.15f, 1f);

      GL.PushMatrix();
      mat.SetPass(0);
      GL.Begin(GL.QUADS);
      GL.Color(Color.yellow);

      GL.Vertex3(x + 0.1f, y + 0.1f, 0);
      GL.Vertex3(x + 0.1f, y + 0.9f, 0);
      GL.Vertex3(x + 0.9f, y + 0.9f, 0);
      GL.Vertex3(x + 0.9f, y + 0.1f, 0);

      GL.End();
      GL.PopMatrix();

      GL.PushMatrix();
      mat.SetPass(0);
      GL.Begin(GL.QUADS);
      GL.Color(color);

      GL.Vertex3(x, y + 0.25f, 0);
      GL.Vertex3(x, y + 0.75f, 0);
      GL.Vertex3(x + 1, y + 0.75f, 0);
      GL.Vertex3(x + 1, y + 0.25f, 0);

      GL.End();
      GL.PopMatrix();
    }
  }
}
