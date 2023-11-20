public class AllEnum //Enum만 담아두는 클래스...밖에서 Enum접근을 원활하게 하기 위함.
{
    public enum PokeType
    {        
        불,
        풀,
        물,

        END
    }
    public enum ItemType
    { 
        Potion,
        Sword,
        Armor,

        End
    }

    public enum TileKind
    { 
        Coin_Bronze,
        ICE,
        MUD,

        End
    }

    public enum SceneKind
    { 
        Game,
        Second,
        
        Load,

        End
    }
}
