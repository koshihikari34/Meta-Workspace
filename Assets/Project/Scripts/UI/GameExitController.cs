using UnityEngine;
using UnityEngine.UI; // Button用

public class GameExitController : MonoBehaviour
{
    [SerializeField] private Button exitButton; // Inspectorでボタンをアサイン

    private void Start()
    {
        // ボタンが設定されていれば、クリック時にExitGameを呼ぶ
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
        else
        {
            Debug.LogWarning("ExitButton がアサインされていません");
        }
    }

    public void ExitGame()
    {
        Debug.Log("ゲーム終了処理を実行");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
