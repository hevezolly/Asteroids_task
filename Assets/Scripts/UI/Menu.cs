using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private string pouseText;
    [SerializeField]
    private string gameOverText;
    [SerializeField]
    private string menuText;

    [SerializeField]
    private TextMeshProUGUI menuTextField;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private ScriptableValue<bool> canContinueGame;

    [SerializeField]
    private SelectedInputSchema selectedSchema;

    [SerializeField]
    private GameState gameState;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private TextMeshProUGUI controllText;

    [SerializeField]
    private List<InputVariant> possibleInputs;

    [SerializeField]
    private List<GameObject> objectsToDisable;

    public bool IsMenuShown { get; private set; }
    private int selectedControllSchema;

    private void Awake()
    {
        selectedControllSchema = 0;
        for (var i = 0; i < possibleInputs.Count; i++)
        {
            possibleInputs[i].InputSchema.Initiate(player);
            if (selectedSchema.CurrentSchema != null && possibleInputs[i].InputSchema == selectedSchema.CurrentSchema)
            {
                selectedControllSchema = i;
            }
        }

        controllText.text = possibleInputs[selectedControllSchema].SchemaName;
    }

    public void SwitchInputSchema()
    {
        selectedControllSchema = (selectedControllSchema + 1) % possibleInputs.Count;
        selectedSchema.SetSchema(possibleInputs[selectedControllSchema].InputSchema);
        controllText.text = possibleInputs[selectedControllSchema].SchemaName;
    }

    private void SetMenuActive(bool active)
    {
        canvas.enabled = active;
        foreach (var o in objectsToDisable)
        {
            o.SetActive(active);
        }
    }

    public void Pause()
    {
        if (IsMenuShown)
            return;
        menuTextField.text = pouseText;
        ShowMenu();
    }

    public void GameOver()
    {
        if (IsMenuShown)
            return;
        menuTextField.text = gameOverText;
        ShowMenu();
    }

    public void DisplayMenu()
    {
        if (IsMenuShown)
            return;
        menuTextField.text = menuText;
        ShowMenu();
    }

    private void ShowMenu()
    {
        selectedSchema.InputEnabled = false;
        continueButton.interactable = canContinueGame.Value;
        SetMenuActive(true);
        Time.timeScale = 0;
        IsMenuShown = true;
    }

    public void StartNewGame()
    {
        canContinueGame.Value = true;
        HideMenu();
        gameState.RestartScene();
    }

    public void HideMenu()
    {
        selectedSchema.InputEnabled = true;
        SetMenuActive(false);
        Time.timeScale = 1;
        IsMenuShown = false;
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        selectedSchema.SetSchema(possibleInputs[0].InputSchema);
    }
#endif


    [System.Serializable]
    private class InputVariant
    {
        public string SchemaName;
        public PlayerInputSchema InputSchema;
    }
}
