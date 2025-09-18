using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
    public Button exitGameBtn;

    public Button refreshTileUIBtn;
    // Start is called before the first frame update
    void Start()
    {
       exitGameBtn.onClick.AddListener(() =>
       {
           Application.Quit();
#if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
#endif

       }); 
       
       refreshTileUIBtn.onClick.AddListener(() =>
       {
           UIManager.Instance.tilePanel.UpdateTilesUIForDebug();
       });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
