using UnityEngine;
using DG.Tweening;

public class Symbol : MonoBehaviour
{
    public string symbolName;
    public int payoutValue;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogError("No SpriteRenderer on " + gameObject.name);
    }

    public void SetSymbol(SymbolData data)
    {
        if (data == null) { Debug.LogError("SymbolData is null!"); return; }

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        symbolName  = data.symbolName;
        payoutValue = data.payoutValue;

        if (data.sprite != null)
        {
            spriteRenderer.sprite = data.sprite;
            spriteRenderer.color  = Color.white; // show sprite in full color
        }
        else
        {
            // No sprite — use a colored square so it's always visible
            spriteRenderer.sprite = null;
            spriteRenderer.color  = data.color;
        }

        gameObject.name = data.symbolName;
    }

    public void PlayLandAnimation()
    {
        transform.DOKill();
        transform.localScale = Vector3.one;
        transform.DOPunchScale(new Vector3(0.3f, -0.2f, 0.3f), 0.4f, 5, 0.5f);
    }

    public void PlayWinAnimation()
    {
        transform.DOKill();
        transform.DOScale(1.15f, 0.3f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void StopWinAnimation()
    {
        transform.DOKill();
        transform.DOScale(1f, 0.15f);
    }
}