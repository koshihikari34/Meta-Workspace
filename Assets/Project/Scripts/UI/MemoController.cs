using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MemoController : MonoBehaviour
{
    public TextMeshProUGUI memoText;

    [Header("Buttons in Panel")]
    public Button editButton;
    public Button deleteButton;

    [Header("Edit UI Prefab")]
    public GameObject editPanelPrefab;

    private GameObject editPanelInstance;

    void Awake()
    {
        if (editButton != null) editButton.onClick.AddListener(OpenEditPanel);
        if (deleteButton != null) deleteButton.onClick.AddListener(DeleteMemo);
    }

    void OpenEditPanel()
    {
        if (editPanelInstance == null)
        {
            editPanelInstance = Instantiate(editPanelPrefab);
        }

        var inputField = editPanelInstance.GetComponentInChildren<TMP_InputField>(true);
        if (inputField != null)
        {
            inputField.text = memoText.text;

            // フォーカスを当てるとQuestのシステムキーボードが出る
            inputField.Select();
            inputField.ActivateInputField();
        }

        // 子階層すべての Button を検索
        var buttons = editPanelInstance.GetComponentsInChildren<Button>(true);

        var confirmButton = buttons.FirstOrDefault(b => b.name == "ConfirmButton");
        var cancelButton  = buttons.FirstOrDefault(b => b.name == "CancelButton");

        if (confirmButton != null)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(() =>
            {
                if (inputField != null) memoText.text = inputField.text;
                CloseEditPanel();
            });
        }

        if (cancelButton != null)
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(CloseEditPanel);
        }

        editPanelInstance.SetActive(true);
    }

    void CloseEditPanel()
    {
        if (editPanelInstance != null) editPanelInstance.SetActive(false);
    }

    void DeleteMemo()
    {
        Destroy(gameObject);
    }
}
