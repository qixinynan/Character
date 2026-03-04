using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
   public Button practiceButton;
   public Button singleButton;
   public Button quitButton;

   private void Awake()
   {
       practiceButton.onClick.AddListener(() =>
       {
           SceneManager.LoadScene("Game");
       });
       
       singleButton.onClick.AddListener(() =>
       {
           SceneManager.LoadScene("GameVsAI");
       });
       
       quitButton.onClick.AddListener(() =>
       {
           Application.Quit();
#if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
#endif
       });
   }
}
