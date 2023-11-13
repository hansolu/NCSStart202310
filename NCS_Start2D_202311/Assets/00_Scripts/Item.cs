using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item 
{
    public AllEnum.ItemType itemType = AllEnum.ItemType.End;
    public string Name { get; private set; }
    public int Count { get; private set; } = 1;
    public Sprite spr; //또는 그림 고유번호...
    public Item(string name, AllEnum.ItemType _type, Sprite spr )
    {
        this.Name = name;
        this.itemType = _type;
        this.spr = spr;
    }
    public void AddCount(int count)
    {
        this.Count += count;
    }
    public bool SubCount(int count)
    {
        if (this.Count > count)
        {
            this.Count += count;
            return true;
        }
        else
        {
            return false;
        }
    }

}
