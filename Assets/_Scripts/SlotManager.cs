using UnityEngine;
using System.Collections;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance { get; private set; }

    [Header("Reels")]
    public Reel[] reels;

    [Header("Symbol Pool")]
    public SymbolData[] allSymbols;

    [Header("Game State")]
    public int playerBalance = 100;
    public int spinCost = 10;

    public bool IsSpinning { get; private set; } = false;

    // Fired when spin ends — UI listens to this
    public System.Action<bool, int> OnSpinComplete;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Spin()
    {
        if (IsSpinning) return;
        if (playerBalance < spinCost)
        {
            Debug.Log("Not enough balance!");
            return;
        }

        playerBalance -= spinCost;
        IsSpinning = true;
        
        AudioManager.Instance.PlaySpin();

        // Pick results for all reels upfront
        SymbolData[] results = new SymbolData[reels.Length];
        for (int i = 0; i < reels.Length; i++)
            results[i] = allSymbols[Random.Range(0, allSymbols.Length)];

        // Start each reel with a small staggered delay
        for (int i = 0; i < reels.Length; i++)
            reels[i].StartSpin(results[i], i * 0.15f);

        // Wait for all reels to finish then check win
        StartCoroutine(WaitForResult(results));
    }

    private IEnumerator WaitForResult(SymbolData[] results)
    {
        float lastReelDelay = (reels.Length - 1) * 0.15f;
        float totalSpinTime = lastReelDelay + reels[0].spinDuration;

        yield return new WaitForSeconds(totalSpinTime);

        IsSpinning = false;
        CheckWin(results);
    }

    private void CheckWin(SymbolData[] results)
    {
        bool allMatch = results[0].symbolName == results[1].symbolName
                     && results[1].symbolName == results[2].symbolName;

        int payout = 0;
        if (allMatch)
        {
            payout = results[0].payoutValue * reels.Length;
            playerBalance += payout;
            AudioManager.Instance.PlayWin(); 
            Debug.Log($"WIN! {results[0].symbolName} x3 — Payout: {payout}");
        }
        else
        {
            AudioManager.Instance.PlayNoWin();
            Debug.Log($"No win: {results[0].symbolName} | {results[1].symbolName} | {results[2].symbolName}");
        }

        // Notify UI
        OnSpinComplete?.Invoke(allMatch, payout);
    }
    
    public void RestartGame()
    {
        playerBalance = 100;
        IsSpinning = false;
    }
}