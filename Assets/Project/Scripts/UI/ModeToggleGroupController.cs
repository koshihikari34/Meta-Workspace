using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ModeToggleGroupController : MonoBehaviour
{
    [Header("UI References")]
    public Toggle mrToggle;
    public Toggle vrToggle;
    public TextMeshProUGUI currentModeText;

    [Header("Passthrough Settings")]
    public OVRPassthroughLayer passthroughLayer;
    [Range(0.1f, 2f)]
    public float fadeDuration = 0.5f;

    void Start()
    {
        // リスナー登録
        mrToggle.onValueChanged.AddListener(OnMRChanged);
        vrToggle.onValueChanged.AddListener(OnVRChanged);

        // 初期状態反映
        if (mrToggle.isOn) OnMRChanged(true);
        else if (vrToggle.isOn) OnVRChanged(true);
    }

    void OnMRChanged(bool isOn)
    {
        if (isOn)
        {
            StopAllCoroutines();
            StartCoroutine(FadePassthrough(true));
            currentModeText.text = "Current Mode: MR";
        }
    }

    void OnVRChanged(bool isOn)
    {
        if (isOn)
        {
            StopAllCoroutines();
            StartCoroutine(FadePassthrough(false));
            currentModeText.text = "Current Mode: VR";
        }
    }

    IEnumerator FadePassthrough(bool enable)
    {
        // Passthrough を有効化する場合は先に ON にしてからフェード
        if (enable && !passthroughLayer.enabled)
            passthroughLayer.enabled = true;

        float start = passthroughLayer.textureOpacity;
        float end = enable ? 1f : 0f;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            passthroughLayer.textureOpacity = Mathf.Lerp(start, end, t);
            yield return null;
        }

        // 完全にフェードアウトしたら OFF にする
        if (!enable)
            passthroughLayer.enabled = false;
    }
}
