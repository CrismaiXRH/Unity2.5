using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI DialogueTXT; // Referencia al texto del diálogo
    public GameObject dialoguePanel; // Referencia al panel de diálogo
    public string[] lines; // Líneas de diálogo
    public float textSpeed = 0.1f; // Velocidad de aparición de las letras
    private int index;
    private bool jugadorCerca = false; // Variable para verificar si el jugador está cerca
    private bool isWriting = false; // Variable para controlar si el texto está escribiéndose

    void Start()
    {
        DialogueTXT.text = string.Empty;
        dialoguePanel.SetActive(false); // Asegúrate de que el panel de diálogo esté oculto al inicio
    }

    void Update()
    {
        // Tecla F para avanzar en el diálogo solo si el jugador está cerca y el texto ha terminado de escribirse
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F) && !isWriting)
        {
            NextLine(); // Pasa a la siguiente línea
        }
    }

    // Cuando el jugador entra en el área del NPC
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tiene el tag "Player"
        {
            jugadorCerca = true;
            dialoguePanel.SetActive(true); // Muestra el panel de diálogo
            StartDialogue(); // Inicia el diálogo
        }
    }

    // Cuando el jugador sale del área del NPC
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            dialoguePanel.SetActive(false); // Oculta el panel de diálogo
            DialogueTXT.text = string.Empty; // Limpia el texto del diálogo
            StopAllCoroutines(); // Detiene cualquier Coroutine activa
            isWriting = false; // Reinicia la variable isWriting
        }
    }

    public void StartDialogue()
    {
        index = 0;
        DialogueTXT.text = string.Empty;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        isWriting = true; // Indica que la línea se está escribiendo
        DialogueTXT.text = string.Empty;

        foreach (char letra in lines[index].ToCharArray())
        {
            DialogueTXT.text += letra;
            yield return new WaitForSeconds(textSpeed);
        }

        isWriting = false; // Marca que la línea ha terminado de escribirse
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            DialogueTXT.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            dialoguePanel.SetActive(false); // Oculta el panel de diálogo al terminar las líneas
            DialogueTXT.text = string.Empty; // Limpia el texto del diálogo al finalizar
        }
    }
}
