using System.Collections;
using TMPro;
using UnityEngine;

public class ConfirmPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMesh;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void SetText(string text) => _textMesh.SetText($"{text}");

    public IEnumerator WaitConfirm(KeyCode keyCode)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(keyCode));
    }
}