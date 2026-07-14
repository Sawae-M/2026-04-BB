using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RotSystem : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private string colorPropertyName = "_Color";
    [SerializeField] private Slider targetSlider;

    [SerializeField] private string sceneToLoad = "NextScene";
    [SerializeField] private float drainDuration = 5f;

    [SerializeField, Range(0f, 1f)] private float restoreAmount = 0.3f;
    [SerializeField] private float restoreDuration = 0.5f;

    [SerializeField] private bool changeUIColorByFill = true;
    [SerializeField] private Image sliderFillImage;
    [SerializeField] private Color fullColor = Color.green;
    [SerializeField] private Color emptyColor = Color.red;

    public static RotSystem Instance { get; private set; }

    private Color _origimnalObjectColor = Color.white;
    private SpriteRenderer _targetSpriteRenderer;
    private Material[] _targetMaterials;
    private Tween _tween;
    private bool _isSceneChanging;
    private float _progress = 1f;


    private void Awake()
    {
        Instance = this;

        if (targetRenderer != null )
        {
            _targetSpriteRenderer = targetRenderer as SpriteRenderer;
            if (_targetSpriteRenderer != null)
            {
                _origimnalObjectColor = _targetSpriteRenderer.color;
            }
            else
            {
                _targetMaterials = targetRenderer.materials;

                _origimnalObjectColor = _targetMaterials.Length > 0
                    ? _targetMaterials[0].color
                    : Color.white;
            }
        }

            if (targetSlider != null)
        {
            targetSlider.minValue = 0f;
            targetSlider.maxValue = 1f;
        }
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
            {
                Color newColor = Color.Lerp(Color.black, _origimnalObjectColor, value);
                if (_targetSpriteRenderer != null)
                {
                    _targetSpriteRenderer.color = newColor;
                }
                else if (_targetMaterials != null)
                {
                    foreach (var mat in _targetMaterials)
                    {
                         mat.color = newColor;
                    }
                }
            }
            if (targetSlider != null)
            {
                targetSlider.value = value;
                    if (changeUIColorByFill && sliderFillImage != null)
                        sliderFillImage.color = Color.Lerp(emptyColor, fullColor, value);
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