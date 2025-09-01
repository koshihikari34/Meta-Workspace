using UnityEngine;
using UnityEngine.UI;

public class MemoSpawner : MonoBehaviour
{
    public GameObject memoPrefab;
    public Button spawnButton; // ← UIボタンをInspectorで割り当て

    void Start()
    {
        // ボタンが存在する場合、リスナーを追加
        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(SpawnMemo);
        }
    }

    public void SpawnMemo()
    {
        Transform cam = Camera.main.transform;
        Vector3 spawnPos = cam.position + cam.forward * 2f;
        Quaternion spawnRot = Quaternion.LookRotation(cam.forward, Vector3.up);
        Instantiate(memoPrefab, spawnPos, spawnRot);

        // Transform cam = Camera.main.transform;

        // // 出現位置：カメラ正面0.8m、胸の高さにオフセット
        // Vector3 forwardOffset = cam.forward * 0.8f;
        // Vector3 heightOffset = Vector3.down * 0.2f; // 少し下げる（胸の高さ）
        // Vector3 spawnPos = cam.position + forwardOffset + heightOffset;

        // // プレイヤーの向きに合わせる
        // Quaternion spawnRot = Quaternion.LookRotation(
        //     Vector3.ProjectOnPlane(cam.forward, Vector3.up), // 水平成分だけ
        //     Vector3.up
        // );

        // GameObject memo = Instantiate(memoPrefab, spawnPos, spawnRot);
    }
}
