using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class RotSystem : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Image targetImage;

    [SerializeField] private string sceneToLoad = "NextScene";

    [SerializeField] private float drainDuration = 5f;
    [SerializeField, Range(0f, 1f)] private float restoreAmount = 0.3f;
    [SerializeField] private float restoreDuration = 0.5f;

    [SerializeField] private bool changeUIColorByFill = true;
    [SerializeField] private Color fullColor = Color.green;
    [SerializeField] private Color emptyColor = Color.red;

    public static RotSystem Instance { get; private set; }

    private Color _origimnalObjectColor = Color.white;
    private Tween _tween;
    private bool _isSceneChanging;
    private float _progress = 1f;


    private void Awake()
    {
        Instance = this;

        if(targetRenderer != null) 
            _origimnalObjectColor = targetRenderer.material.color;

        if (targetImage != null)
            targetImage.type = Image.Type.Filled;

        ApplyProgress(_progress);
    }

    private void Start()
    {
        StartDraining(drainDuration);
    }

    private void StartDraining(float duration)
    {
        _tween?.Kill();
        _tween = DOTween.To(() => _progress, SetProgress, 0f, duration)
            .SetEase(Ease.Linear)
            .OnComplete(OnEmpty);
    }

    private void SetProgress(float value)
    {
        _progress = value;
        ApplyProgress(value);
    }

    private void ApplyProgress(float value) 
        {
            if (targetRenderer != null)
                targetRenderer.material.color = Color.Lerp(Color.black, _origimnalObjectColor, value);

            if (targetImage != null)
            {
                targetImage.fillAmount = value;
            if (changeUIColorByFill)
                targetImage.color = Color.Lerp(emptyColor, fullColor, value);
            }
        }
    private void OnEmpty()
    {
        if (_isSceneChanging) return;
        _isSceneChanging = true;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Restore()
    {
        if (_isSceneChanging) return ;

        _tween?.Kill();
        float targetValue = Mathf.Clamp01(_progress + restoreAmount);

        _tween = DOTween.To(() => _progress, SetProgress, targetValue, restoreDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                float remainingDuration = drainDuration * targetValue;
                StartDraining(remainingDuration);
            });
    }
    private void OnDestroy()
    {
        _tween?.Kill();
    }

}