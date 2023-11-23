using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Watering : MonoBehaviour
{
    SpriteShapeController controller;
    Spline spline;
    int pointcount = 0;
    Vector2 pos = Vector2.zero;
    BuoyancyEffector2D effector2d;
    void Start()
    {
        controller = transform.GetComponent<SpriteShapeController>();
        effector2d = transform.GetComponent<BuoyancyEffector2D>();
        spline = controller.spline;
        pointcount = spline.GetPointCount() - 2;
    }
    void Update()
    {
        for (int i = 0; i < pointcount; i++)
        {
            pos = spline.GetPosition(i);
            if (i%2==0)
            {                
                pos.y = Mathf.Sin(Time.time)*0.5f;
                effector2d.surfaceLevel = pos.y;
            }
            else
            {
                pos.y = Mathf.Cos(Time.time) * 0.5f;
            }
            spline.SetPosition(i, pos);
        }        
    }
}
