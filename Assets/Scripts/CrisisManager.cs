using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrisisManager : MonoBehaviour
{
    public GameObject crisisPanel;
    public TMP_Text crisisText;

    ColonyManager colony;
    GeneManager genes;

    void Start()
    {
        colony = FindObjectOfType<ColonyManager>();
        genes = FindObjectOfType<GeneManager>();
    }

    public void TriggerCrisis()
    {
        crisisPanel.SetActive(true);
        crisisText.text = "Радиационная буря на Марсе";
    }

    public void OptionMutatePeople()
    {
        genes.MutateGene(GeneType.Skin, 1);
        Close();
    }

    public void OptionSacrifice()
    {
        colony.population -= 10;
        Close();
    }

    public void OptionWait()
    {
        colony.stability -= 5;
        Close();
    }

    void Close()
    {
        crisisPanel.SetActive(false);
    }
}
