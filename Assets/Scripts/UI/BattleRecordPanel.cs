using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Manager;

public class BattleRecordPanel : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemPrefab;

    public GameObject mask;

    private void OnEnable()
    {
        Refresh();
    }

    public void Close()
    {
        mask.SetActive(false);
    }

    public void Show()
    {
        mask.SetActive(true);
    }

    public void Refresh()
    {
        // 清空旧的
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        var records = BattleRecordManager.Instance.BattleRecords;

        // 倒序显示（最新在上面）
        for (int i = records.Count - 1; i >= 0; i--)
        {
            CreateItem(records[i]);
        }
    }

    private void CreateItem(BattleRecord record)
    {
        var go = Instantiate(itemPrefab, content);

        var item = go.GetComponent<BattleRecordItem>();

        string time = DateTimeOffset
            .FromUnixTimeSeconds(record.timestamp)
            .LocalDateTime
            .ToString("yyyy-MM-dd HH:mm");

        Debug.Log(item);
        item.timeText.text = time;
        item.title.text = record.isHumanWined ? "玩家胜利" : "AI胜利";
        item.title.color = record.isHumanWined ? Color.blue : Color.red;
    }
}