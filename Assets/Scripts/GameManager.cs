using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text populationText;
    public TMP_Text potatoText;
    public TMP_Text stabilityText;
    public TMP_Text mentalStateText;

    ColonyManager colony;
    GeneManager genes;
    MentalStateManager mental;
    CrisisManager crisis;

    float timer;

    void Start()
    {
        colony = FindObjectOfType<ColonyManager>();
        genes = FindObjectOfType<GeneManager>();
        mental = FindObjectOfType<MentalStateManager>();
        crisis = FindObjectOfType<CrisisManager>();

        mental.RollState();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10f)
        {
            timer = 0f;
            Tick();
        }

        UpdateUI();
    }

    void Tick()
    {
        colony.ApplyTick(genes);
        mental.RollState();

        if (Random.value < 0.5f)
            crisis.TriggerCrisis();
    }

    void UpdateUI()
    {
        populationText.text = $"Население: {colony.population}";
        potatoText.text = $"Картошка: {colony.potato}";
        stabilityText.text = $"Стабильность: {colony.stability}";
        mentalStateText.text = $"Состояние: {mental.currentState}";
    }
}
