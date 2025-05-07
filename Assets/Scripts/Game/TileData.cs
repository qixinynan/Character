namespace Game
{
    // 牌的数据
    public class TileData
    {
        public readonly string Content;
        public int Id;

        public TileData(int id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}