using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public PlayerMovement playerMovement;  // Asumimos que tienes un script llamado "PlayerMovement"

    private bool isInventoryOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(false);  // Asegúrate de que el inventario esté oculto al iniciar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
    }
}

