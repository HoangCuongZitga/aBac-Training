  using UnityEditor.SceneManagement;
    void ResetGame()
    {
        EditorSceneManager.LoadScene(0);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
        
#endif
       
    }
