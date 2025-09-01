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

    [Header("Camera Settings")]
    public Camera vrCamera; // CenterEyeAnchor の Camera をInspectorで割り当て

    void Start()
    {
        mrToggle.onValueChanged.AddListener(OnMRChanged);
        vrToggle.onValueChanged.AddListener(OnVRChanged);

        if (mrToggle.isOn) OnMRChanged(true);
        else if (vrToggle.isOn) OnVRChanged(true);
    }

    void OnMRChanged(bool isOn)
    {
        if (isOn)
        {
            StopAllCoroutines();
            StartCoroutine(FadePassthrough(true));

            // MR時は背景を透明に
            if (vrCamera != null)
            {
                vrCamera.clearFlags = CameraClearFlags.SolidColor;
                vrCamera.backgroundColor = new Color(0, 0, 0, 0);
            }

            currentModeText.text = "Current Mode: MR";
        }
    }

    [Header("Skybox Settings")]
public Material vrSkybox; // VR用スカイボックスマテリアルをInspectorで割り当て

void OnVRChanged(bool isOn)
{
    if (isOn)
    {
        StopAllCoroutines();
        StartCoroutine(FadePassthrough(false));

        if (vrCamera != null)
        {
            vrCamera.clearFlags = CameraClearFlags.Skybox;
            if (vrSkybox != null)
            {
                RenderSettings.skybox = vrSkybox;
            }
        }

        currentModeText.text = "Current Mode: VR";
    }
}

    IEnumerator FadePassthrough(bool enable)
    {
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

        if (!enable)
            passthroughLayer.enabled = false;
    }
}
