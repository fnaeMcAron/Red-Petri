using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class menuUI : MonoBehaviour
{
    public GameObject buttons;
    public GameObject titles;
    InputAction anyInputAction;

    void Awake()
    {
        anyInputAction = new InputAction(binding: "*/<button>");
        anyInputAction.performed += OnAnyInput;
        anyInputAction.Enable();
        InputActionAsset layout = FindAnyObjectByType<InputActionAsset>();
    }

    void OnAnyInput(InputAction.CallbackContext context)
    {
        // Ýעמע לועמה גûחûגאועסÿ ןנט ËÞÁÎÌ גגמהו
        if (!buttons.activeSelf)
        {
            buttons.SetActive(true);
            titles.GetComponent<Animation>()["Titles"].time = -100f;
            titles.GetComponent<Animation>()["Titles"].normalizedTime = -100f;
        }
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void Titles()
    {
        buttons.SetActive(false);
        titles.GetComponent<Animation>().Stop();
        titles.GetComponent<Animation>()["Titles"].time = 0f;
        titles.GetComponent<Animation>()["Titles"].normalizedTime = 0f;
        titles.GetComponent<Animation>().Play();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
