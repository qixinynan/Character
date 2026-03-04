using System;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    [Serializable]
    public class BattleRecord
    {
        public long timestamp;
        public bool isHumanWined;
    }

    [Serializable]
    internal class BattleRecordContainer
    {
        public List<BattleRecord> records = new List<BattleRecord>();
    }

    public class BattleRecordManager
    {
        private const string BattleRecordKey = "BATTLE_RECORDS";
        private const int MaxRecordCount = 200;

        private static BattleRecordManager _instance;
        public static BattleRecordManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattleRecordManager();
                }
                return _instance;
            }
        }

        private BattleRecordContainer _container;

        public IReadOnlyList<BattleRecord> BattleRecords => _container.records;

        // 私有构造，防止外部 new
        private BattleRecordManager()
        {
            Init();
        }

        private void Init()
        {
            if (!PlayerPrefs.HasKey(BattleRecordKey))
            {
                _container = new BattleRecordContainer();
                Save();
                return;
            }

            string json = PlayerPrefs.GetString(BattleRecordKey);
            _container = JsonUtility.FromJson<BattleRecordContainer>(json);

            if (_container == null)
                _container = new BattleRecordContainer();
        }

        public void AddRecord(bool isHumanWined)
        {
            var record = new BattleRecord
            {
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                isHumanWined = isHumanWined
            };

            _container.records.Add(record);

            if (_container.records.Count > MaxRecordCount)
            {
                _container.records.RemoveAt(0);
            }

            Save();
        }

        public void Clear()
        {
            _container.records.Clear();
            Save();
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(_container);
            PlayerPrefs.SetString(BattleRecordKey, json);
            PlayerPrefs.Save();
        }
    }
}