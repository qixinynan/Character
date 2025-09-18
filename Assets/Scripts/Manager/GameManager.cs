
using System.Collections.Generic;
using Configs;
using Game;
using Game.GameRule;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public UIManager uiManager;
        public GameConfig gameConfig;

        private readonly TileDataManager _tileDataManager = new TileDataManager();
        public readonly PlayerManager PlayerManager = new PlayerManager();
        private IGameRule _gameRule;
        // private List<TileData> _tileDatas;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            // Init GameRule
            _tileDataManager.ReadTileData(gameConfig.characterDataPath);
            _gameRule = new BasicGameRule();
            _gameRule.Init(_tileDataManager.GetCharacterResources());            
            PlayerManager.Init(_gameRule);
            
            // Init EventManager
            EventManager.OnTilesPlayed += PlayTilesHandler;
            
            InitTiles();
            StartRound();
            
            
        }
        
        // 发牌
        void InitTiles()
        {
            PlayerManager.InitTiles(); 
        }
        
        void StartRound()
        {
            Debug.Log("GameManager.StartRound");
            var player = PlayerManager.NextPlayer();
            player.StartRound();
            player.OnRoundEnd += EndRound; // 注意是等于,不然越来越多
            EventManager.OnAnyRoundStart?.Invoke();
        }

        void EndRound()
        {
            Debug.Log("GameManager.EndRound");
            // 检测是否胜利
            if (_gameRule.CheckWin(PlayerManager.GetCurrentPlayer()))
            {
                EventManager.OnGameOver?.Invoke();
            } 
            
            
            EventManager.OnAnyRoundEnd?.Invoke();
            StartRound();
        }

        void PlayTilesHandler(List<TileData> tiles)
        {
            if (!PlayerManager.IsHumanPlayerRound())
            {
                Debug.LogError("不在玩家回合");
                return;
            }
            var result = _gameRule.IsTilesPlayable(tiles);
            if (result.IsOk())
            {
                PlayerManager.GetCurrentPlayer().PlayTiles(tiles);
            }
            else
            {
                Debug.LogWarning("出牌检测失败:"+ result.Message);
            }
        }


    }
}
