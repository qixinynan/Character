using System.Collections.Generic;
using Util;

namespace Game.GameRule
{
    public class BasicGameRule : IGameRule
    {
        private CharacterResources _characterResources;

        public void Init(CharacterResources characterResources)
        {
            this._characterResources = characterResources;
        }

        public List<TileData> GenerateTiles(int count)
        {
            List<TileData> tileDatas = new List<TileData>();
            var components = _characterResources.GetComponents(); // 假设返回 HashSet<string>

            // 将 HashSet 转换为列表以便支持随机访问
            var componentList = new List<string>(components);

            for (int i = 0; i < count; i++)
            {
                if (componentList.Count > 0)
                {
                    // 随机选择一个组件
                    int randomIndex = UnityEngine.Random.Range(0, componentList.Count);
                    string randomComponent = componentList[randomIndex];

                    // 创建 TileData 对象（假设 TileData 有一个构造函数接受组件字符串）
                    TileData tileData = new TileData(i, randomComponent);

                    // 添加到结果列表
                    tileDatas.Add(tileData);
                }
            }

            return tileDatas;
        }

        public int GetFirstGenerateTileCount()
        {

            return 13;
        }

        public Result IsTilesPlayable(List<TileData> tiles)
        {
            if (tiles.Count == 1)
            {
                return Result.Ok;
            }

            return new Result(ResultType.Error, "数量很多目前没有实现"); // TODO
        }
    }
}