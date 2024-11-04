using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI DialogueTXT; // Referencia al texto del diálogo
    public GameObject dialoguePanel; // Referencia al panel de diálogo
    public string[] lines; // Líneas de diálogo
    public float textSpeed = 0.1f; // Velocidad de aparición de las letras
    private int index;
    private bool jugadorCerca = false; // Variable para verificar si el jugador está cerca

    void Start()
    {
        DialogueTXT.text = string.Empty; // Limpia el texto al inicio
        dialoguePanel.SetActive(false); // Asegúrate de que el panel de diálogo esté oculto al inicio
    }

    void Update()
    {
        // Presiona F para continuar con el diálogo
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F))
        {
            if (DialogueTXT.text == lines[index])
            {
                NextLine(); // Ir a la siguiente línea
            }
            else
            {
                StopCoroutine(WriteLine()); // Detiene la escritura en curso
                DialogueTXT.text = lines[index]; // Muestra la línea completa
            }
        }
    }

    // Cuando el jugador entra en el área del NPC
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que entra tiene el tag "Player"
        {
            jugadorCerca = true; // Marca que el jugador está cerca
            dialoguePanel.SetActive(true); // Muestra el panel de diálogo
            StartDialogue(); // Inicia el diálogo
        }
    }

    // Cuando el jugador sale del área del NPC
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que sale tiene el tag "Player"
        {
            jugadorCerca = false; // Marca que el jugador ya no está cerca
            dialoguePanel.SetActive(false); // Oculta el panel de diálogo
            DialogueTXT.text = string.Empty; // Limpia el texto del diálogo
            StopAllCoroutines(); // Detiene cualquier Coroutine activa
        }
    }

    public void StartDialogue()
    {
        index = 0; // Reinicia el índice de las líneas de diálogo
        DialogueTXT.text = string.Empty; // Limpia el texto al inicio del diálogo
        StartCoroutine(WriteLine()); // Comienza a escribir la primera línea
    }
    
    IEnumerator WriteLine()
    {
        foreach (char letra in lines[index].ToCharArray())
        {
            DialogueTXT.text += letra; // Añade cada letra al texto del diálogo
            yield return new WaitForSeconds(textSpeed); // Espera antes de añadir la siguiente letra
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1) // Verifica si hay más líneas de diálogo
        {
            index++; // Avanza al siguiente índice
            DialogueTXT.text = string.Empty; // Limpia el texto para la nueva línea
            StartCoroutine(WriteLine()); // Escribe la nueva línea
        }
        else
        {
            dialoguePanel.SetActive(false); // Oculta el panel de diálogo al terminar las líneas
        }
    }
}
