using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField] private Image _loadingCircle;
    [SerializeField] private TMP_Text _text;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void ShowMessage(string text) => _text.SetText($"{text}");

    private void Update() => _loadingCircle.transform.Rotate(_loadingCircle.transform.forward * Time.deltaTime * 100, Space.World);
}