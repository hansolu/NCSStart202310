using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//��Ƶ� Ÿ���� �ڵ忡�� ���� �ؾ��� ��..    
public class ForTilemapTest : MonoBehaviour
{
    public Camera camera;
    public Tilemap tilemap; //Ÿ�ϵ��� �׸� �� Ÿ�� ��
    public TileBase[] tiles; //Ÿ�� ��������... �ȷ�Ʈ�� �÷ȴ� Ÿ�ϵ� �ϳ��ϳ�..
    Vector3 vec = Vector3.zero; //int ���� float�� ��.. �� ��¼�ٺ��� 
    Vector3Int vecInt = Vector3Int.zero;
    Color[] colors = new Color[] { Color.white, Color.red, Color.blue, Color.green};

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //���콺 ���� Ŭ���� �ϸ�
        {
            vec = camera.ScreenToWorldPoint(Input.mousePosition);
            vecInt = tilemap.WorldToCell(vec);
            //tilemap.SwapTile(Ÿ�� A, Ÿ�� B); //AŸ�ϵ��� BŸ�Ϸ� �ٲ�
            tilemap.SetTile(vecInt, tiles[0]); //���ϴ� ��ġ�� Ÿ�� A�� ������.            
        }
        else if (Input.GetMouseButtonDown(1)) //���콺 ������ Ŭ���� �ϸ�
        {
            vec = camera.ScreenToWorldPoint(Input.mousePosition);
            vecInt = tilemap.WorldToCell(vec);            
            tilemap.SetTile(vecInt, null); //���ϴ� ��ġ�� Ÿ�� A�� ������.            
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            tilemap.SwapTile(tiles[0], tiles[1]); //0�� Ÿ�ϵ��� 1��Ÿ�Ϸ� �ٲ��.
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            tilemap.color = colors[Random.Range(0, colors.Length)];
        }
    }
}
