using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Reel : MonoBehaviour
{
    [Header("References")]
    public GameObject symbolPrefab;
    public SymbolData[] possibleSymbols;

    [Header("Settings")]
    public float symbolSpacing = 1.1f;
    public float spinDuration  = 1.5f;
    public float spinSpeed     = 15f;

    // Exactly 5: index 0=below-screen, 1=bottom, 2=middle, 3=top, 4=above-screen
    private List<Symbol> symbols = new List<Symbol>();

    // Visible window: only indices 1, 2, 3 show — 0 and 4 are off-screen buffers
    private float topY    =>  1 * symbolSpacing;   //  1.1
    private float midY    =>  0f;                   //  0.0
    private float botY    => -1 * symbolSpacing;   // -1.1
    private float bufTopY =>  2 * symbolSpacing;   //  2.2  (off screen above)
    private float bufBotY => -2 * symbolSpacing;   // -2.2  (off screen below)

    // Recycle threshold: if symbol goes below this, send to top buffer
    private float recycleBottom => -2.5f * symbolSpacing;

    public SymbolData CurrentSymbol { get; private set; }
    public bool IsSpinning { get; private set; }

    private void Start()
    {
        SpawnSymbols();
        SetupCameraMask();
    }

    private void SpawnSymbols()
    {
        if (symbolPrefab == null || possibleSymbols == null || possibleSymbols.Length == 0)
        {
            Debug.LogError($"{gameObject.name}: Missing prefab or symbols!");
            return;
        }

        float[] positions = { bufBotY, botY, midY, topY, bufTopY };

        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(symbolPrefab, transform);
            obj.transform.localPosition = new Vector3(0, positions[i], 0);

            Symbol sym = obj.GetComponent<Symbol>();
            sym.SetSymbol(possibleSymbols[Random.Range(0, possibleSymbols.Length)]);
            symbols.Add(sym);
        }

        CurrentSymbol = possibleSymbols[Random.Range(0, possibleSymbols.Length)];
    }

    // Creates a SpriteMask child so only 3 symbols show
    private void SetupCameraMask()
    {
        GameObject maskObj = new GameObject("ReelMask");
        maskObj.transform.SetParent(transform.parent); // sibling of reel, not child
        maskObj.transform.position = new Vector3(transform.position.x, 0, 0);

        SpriteMask mask = maskObj.AddComponent<SpriteMask>();
        mask.sprite = GetSquareSprite();
        // Scale: x=symbol width, y=3 symbols tall with small padding
        maskObj.transform.localScale = new Vector3(0.95f, symbolSpacing * 3f, 1f);

        // Tell every symbol's SpriteRenderer to respect this mask
        foreach (Symbol s in symbols)
        {
            SpriteRenderer sr = s.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }

    // Gets Unity's built-in square sprite for the mask
    private Sprite GetSquareSprite()
    {
        // Use the same sprite your symbol prefab uses
        SpriteRenderer sr = symbolPrefab.GetComponent<SpriteRenderer>();
        if (sr != null && sr.sprite != null)
            return sr.sprite;

        // Fallback: create a white square sprite programmatically
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
    }

    public void StartSpin(SymbolData result, float delay)
    {
        foreach (Symbol s in symbols) s.StopWinAnimation();
        StartCoroutine(SpinRoutine(result, delay));
    }

    private IEnumerator SpinRoutine(SymbolData result, float delay)
    {
        yield return new WaitForSeconds(delay);
        IsSpinning = true;

        float elapsed    = 0f;
        float accelTime  = 0.25f;
        float decelStart = spinDuration - 0.35f;
        float speed      = 0f;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;

            if (elapsed < accelTime)
                speed = Mathf.Lerp(0, spinSpeed, elapsed / accelTime);
            else if (elapsed > decelStart)
                speed = Mathf.Lerp(spinSpeed, 1f,
                    (elapsed - decelStart) / (spinDuration - decelStart));
            else
                speed = spinSpeed;

            foreach (Symbol s in symbols)
                s.transform.localPosition += Vector3.down * speed * Time.deltaTime;

            Recycle();
            yield return null;
        }

        ApplyResult(result);
        IsSpinning = false;
    }

    private void Recycle()
    {
        foreach (Symbol s in symbols)
        {
            if (s.transform.localPosition.y < recycleBottom)
            {
                // Send off-screen to the top buffer position
                s.transform.localPosition = new Vector3(0, bufTopY, 0);
                s.SetSymbol(possibleSymbols[Random.Range(0, possibleSymbols.Length)]);
            }
        }
    }

    private void ApplyResult(SymbolData result)
    {
        CurrentSymbol = result;

        // Snap all 5 back to exact positions
        float[] positions = { bufBotY, botY, midY, topY, bufTopY };
        for (int i = 0; i < 5; i++)
        {
            symbols[i].transform.localPosition = new Vector3(0, positions[i], 0);
            symbols[i].SetSymbol(i == 2
                ? result
                : possibleSymbols[Random.Range(0, possibleSymbols.Length)]);
        }

        // DOTween punch on middle symbol
        symbols[2].PlayLandAnimation();
    }

    public Symbol GetMiddleSymbol() => symbols[2];
    public void SetResult(SymbolData result) => CurrentSymbol = result;
}