using UnityEngine;
using UnityEngine.InputSystem;

public class DocController : MonoBehaviour
{
    public GameObject menuUI;
    public PlayerInput playerInput;
    public StudentMovement player;

    bool menuOpen;
    void Update()
    {
        if (playerInput.currentActionMap != null)
            Debug.Log(playerInput.currentActionMap.name);
    }

    public void OnOpenMenu(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        menuOpen = !menuOpen;

        if (menuOpen)
        {
            menuUI.SetActive(true);
            playerInput.SwitchCurrentActionMap("UI");
        }
        else
        {
            CloseMenu();
        }
    }

    public void CloseMenu()
    {
        menuUI.SetActive(false);
        playerInput.SwitchCurrentActionMap("Student");
        menuOpen = false;
        Debug.Log("ÊÍÎÏÊÀ ÆÈÂÀ");
    }

    // ÊÍÎÏÊÈ ÌÎÄÈÔÈÊÀÖÈÉ
    public void AddSpeed() => player.speed += 1f;
    public void AddJump() => player.jumpForce += 1f;
    public void AddScale() => player.transform.localScale += Vector3.one * 0.1f;
}
