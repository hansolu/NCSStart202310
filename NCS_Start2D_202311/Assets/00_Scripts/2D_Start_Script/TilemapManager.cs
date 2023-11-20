using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileInfo
{
    AllEnum.TileKind kind;
    public float speedval { get; private set; } = 0.5f;

    public TileInfo(AllEnum.TileKind kind,float speedval =1)
    {
        this.kind = kind;
        this.speedval = speedval;
    }
    
}

public class TilemapManager : Singleton<TilemapManager>
{    
    public Tilemap tilemap; //여러개라면... 얘도 배열로 여러개...
    public TileBase[] tiles;

    Dictionary<Vector3Int, TileInfo> tilesinfo = new Dictionary<Vector3Int, TileInfo>();


    //데이터 세팅 예시
    //protected override void Awake()
    //{
    //    base.Awake();                
        //foreach (var item in tilemap.cellBounds.allPositionsWithin)
        //{
        //    if (tilemap.HasTile(item))
        //    {
        //        var tile = tilemap.GetTile<TileBase>(item);
        //        if (tile == /*내가 지정한 얼음타일*/tiles[(int)AllEnum.TileKind.ICE])
        //        {
        //            tilesinfo.Add(item, new TileInfo(AllEnum.TileKind.ICE, 2));
        //        }
        //        else if(tile == /*내가 지정한 진흙타일*/tiles[(int)AllEnum.TileKind.MUD])
        //        {
        //            tilesinfo.Add(item, new TileInfo(AllEnum.TileKind.MUD, 2));
        //        }
                
        //    }
        //    else
        //    {
                
        //    }
        //}
    //}

    public float GetTileSpeed(Vector3 _pos)
    {
        vecint = tilemap.WorldToCell(_pos);
        return tilesinfo[vecint].speedval;
    }
    Vector3Int vecint = Vector3Int.zero;

    public void SetTile(Vector3 _pos, AllEnum.TileKind _kind)
    {        
        vecint = tilemap.WorldToCell(_pos); //월드포지션으로 셀 포지션을 얻는거고
            //tilemap.LocalToCell //로컬포지션으로 셀포지션을 받아요...
                
        if (_kind == AllEnum.TileKind.End)
        {
            tilemap.SetTile(vecint, null);
        }
        else
         tilemap.SetTile(vecint, tiles[(int)_kind]);
    }
    public void SwapTile(AllEnum.TileKind _kind1, AllEnum.TileKind _kind2)
    {
        if (_kind1 == AllEnum.TileKind.End && _kind2 == AllEnum.TileKind.End) //둘다 비면..?
        {
            return;
        }

        if (_kind1 == AllEnum.TileKind.End)
        {
            tilemap.SwapTile(null, tiles[(int)_kind2]);
        }
        else if (_kind2 == AllEnum.TileKind.End)
        {
            tilemap.SwapTile(tiles[(int)_kind1], null);
        }
        else
        {
            tilemap.SwapTile(tiles[(int)_kind1], tiles[(int)_kind2]);
        }        
    }
    public void SwapTile(TileBase tile, TileBase tile2)
    {
        if (tile == null)
        {            
            return;
        }
        tilemap.SwapTile(tile, tile2);       
    }

    public TileBase GetTile(Vector3 _pos)
    {
        vecint = tilemap.WorldToCell(_pos);
        return tilemap.GetTile(vecint);
    }
}
