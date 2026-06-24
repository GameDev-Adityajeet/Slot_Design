using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI messageText;
    public Button spinButton;
    public Button restartButton;

    [Header("Win Panel")]
    public GameObject winPanel;
    public TextMeshProUGUI winAmountText;
    public Button continueButton;

    private SlotManager slotManager;

    private void Start()
    {
        slotManager = SlotManager.Instance;

        spinButton.onClick.AddListener(OnSpinPressed);
        continueButton.onClick.AddListener(OnContinuePressed);
        restartButton.onClick.AddListener(OnRestartPressed);

        slotManager.OnSpinComplete += OnSpinComplete;

        // Hide panels and restart button at start
        winPanel.SetActive(false);
        winPanel.transform.localScale = Vector3.one;
        restartButton.gameObject.SetActive(false);

        messageText.text = "Press SPIN!";
        UpdateBalanceDisplay();
    }

    private void OnDestroy()
    {
        if (slotManager != null)
            slotManager.OnSpinComplete -= OnSpinComplete;
    }

    private void OnSpinPressed()
    {
        if (slotManager.IsSpinning) return;

        AudioManager.Instance.PlayClick();
        spinButton.interactable = false;
        messageText.text = "Spinning...";
        slotManager.Spin();
    }

    private void OnSpinComplete(bool isWin, int payout)
    {
        UpdateBalanceDisplay();

        // Balance hit 0 — show restart button, lock spin
        if (slotManager.playerBalance <= 0)
        {
            spinButton.interactable = false;
            messageText.text = "Out of coins!";
            restartButton.gameObject.SetActive(true);

            // Small bounce on restart button to draw attention
            restartButton.transform.localScale = Vector3.zero;
            restartButton.transform
                .DOScale(1f, 0.4f)
                .SetEase(Ease.OutBack);
            return;
        }

        spinButton.interactable = true;

        if (isWin)
        {
            messageText.text = "YOU WIN!";

            foreach (Reel r in slotManager.reels)
                r.GetMiddleSymbol().PlayWinAnimation();

            winAmountText.text = $"You won {payout} coins!";
            winPanel.SetActive(true);
        }
        else
        {
            messageText.text = "No win. Try again!";
        }
    }

    private void OnContinuePressed()
    {
        foreach (Reel r in slotManager.reels)
            r.GetMiddleSymbol().StopWinAnimation();

        winPanel.SetActive(false);
        messageText.text = "Press SPIN!";
    }

    private void OnRestartPressed()
    {
        AudioManager.Instance.PlayClick();

        slotManager.RestartGame();
        UpdateBalanceDisplay();

        restartButton.gameObject.SetActive(false);
        spinButton.interactable = true;
        messageText.text = "Press SPIN!";
    }

    private void UpdateBalanceDisplay()
    {
        balanceText.text = $"Balance: {slotManager.playerBalance}";
    }
}