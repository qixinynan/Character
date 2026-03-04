
using System.Collections.Generic;
using Configs;
using Game;
using Game.GameRule;
using Game.Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] private bool forceVsAIMode = false;
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
            bool isVsAIMode = IsVsAIMode();
            // Init GameRule
            _tileDataManager.ReadTileData();
            _gameRule = isVsAIMode ? new VsAIGameRule() : new BasicGameRule();
            _gameRule.Init(_tileDataManager.GetCharacterResources());            
            if (isVsAIMode)
            {
                PlayerManager.InitVsAI(_gameRule);
                EnsureAIBattlePanelExistsInScene();
            }
            else
            {
                PlayerManager.Init(_gameRule);
            }
            
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
            if (player == null)
            {
                Debug.LogError("没有可用玩家, 无法开始回合");
                return;
            }

            player.OnRoundEnd -= EndRound;
            player.OnRoundEnd += EndRound;
            player.StartRound();
            EventManager.OnAnyRoundStart?.Invoke();
            if (player is AIPlayer aiPlayer)
            {
                aiPlayer.PlayAutoTurn();
            }
        }

        void EndRound()
        {
            Debug.Log("GameManager.EndRound");
            // 检测是否胜利
            var currentPlayer = PlayerManager.GetCurrentPlayer();
            if (currentPlayer != null && _gameRule.CheckWin(currentPlayer))
            {
                EventManager.OnGameOver?.Invoke(currentPlayer);
                _gameRule.OnGameOver(currentPlayer);
                return;
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
            if (result.IsOk)
            {
                if(result.Data != "")
                    EventManager.OnPlayerCharacterComposed?.Invoke(result.Data);
                PlayerManager.GetCurrentPlayer().PlayTiles(tiles);
            }
            else
            {
                Debug.LogWarning("出牌检测失败:"+ result.Message);
            }
        }

        private bool IsVsAIMode()
        {
            if (forceVsAIMode)
            {
                return true;
            }

            return SceneManager.GetActiveScene().name.Contains("VsAI");
        }

        private void EnsureAIBattlePanelExistsInScene()
        {
            if (FindFirstObjectByType<AIBattlePanel>() == null)
            {
                Debug.LogWarning("VsAI场景缺少 AIBattlePanel，请在 Canvas 下配置该面板。");
            } 
        }


    }
}
