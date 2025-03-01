using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadingView : UIBehaviour
{
    private Slider _progressSlider;
    private TextMeshProUGUI _progressText;
    private float smoothSpeed = 50f;
    private float accelerateSpeed = 100f;

    protected override void ParseComponent()
    {
        _progressSlider = FindComponent<Slider>("Slider_LoadingBar");
        _progressText = FindComponent<TextMeshProUGUI>("Slider_LoadingBar/Text_LoadingValue");
    }

    public override void Show()
    {
        base.Show();
        _progressSlider.value = 0;
        CoroutineManager.Instance.IStartCoroutine(FillProgress());
    }

    public override void Hide()
    {
        if (_progressSlider.value < 100)
        {
            CoroutineManager.Instance.IStartCoroutine(FillToComplete());
        }
        else
        {
            base.Hide();
        }
    }

    private IEnumerator FillProgress()
    {
        while (_progressSlider.value < 100)
        {
            _progressSlider.value = Mathf.MoveTowards(_progressSlider.value, 100, smoothSpeed * Time.deltaTime);
            _progressText.text = "Loading..." + Mathf.RoundToInt(_progressSlider.value) + "%";
            yield return null;
        }
    }

    private IEnumerator FillToComplete()
    {
        while (_progressSlider.value < 100)
        {
            _progressSlider.value = Mathf.MoveTowards(_progressSlider.value, 100, accelerateSpeed * Time.deltaTime);
            _progressText.text = "Loading..." + Mathf.RoundToInt(_progressSlider.value) + "%";
            yield return null;
        }

        base.Hide();
    }
}