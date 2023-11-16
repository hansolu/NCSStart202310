using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//깔아둔 타일을 코드에서 변경 해야할 때..    
public class ForTilemapTest : MonoBehaviour
{
    public Camera camera;
    public Tilemap tilemap; //타일들을 그린 그 타일 맵
    public TileBase[] tiles; //타일 한장한장... 팔레트에 올렸던 타일들 하나하나..
    Vector3 vec = Vector3.zero; //int 말고 float도 됨.. 즉 어쩌다보면 
    Vector3Int vecInt = Vector3Int.zero;
    Color[] colors = new Color[] { Color.white, Color.red, Color.blue, Color.green};

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 클릭을 하면
        {
            vec = camera.ScreenToWorldPoint(Input.mousePosition);
            vecInt = tilemap.WorldToCell(vec);
            //tilemap.SwapTile(타일 A, 타일 B); //A타일들을 B타일로 바꿈
            tilemap.SetTile(vecInt, tiles[0]); //원하는 위치를 타일 A로 세팅함.            
        }
        else if (Input.GetMouseButtonDown(1)) //마우스 오른쪽 클릭을 하면
        {
            vec = camera.ScreenToWorldPoint(Input.mousePosition);
            vecInt = tilemap.WorldToCell(vec);            
            tilemap.SetTile(vecInt, null); //원하는 위치를 타일 A로 세팅함.            
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            tilemap.SwapTile(tiles[0], tiles[1]); //0번 타일들을 1번타일로 바꿔라.
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            tilemap.color = colors[Random.Range(0, colors.Length)];
        }
    }
}
