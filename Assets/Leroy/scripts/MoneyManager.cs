using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public int money = 0;
    public TMP_Text moneyText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (amount <= 0)
            return true;

        if (money < amount)
            return false;

        money -= amount;
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = money.ToString();
    }
}
