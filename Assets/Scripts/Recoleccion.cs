using UnityEngine;
using UnityEngine.UI; // Para trabajar con la UI

public class Recoleccion : MonoBehaviour
{
    public GameObject[] celdasInventario; // Referencia a las celdas de inventario (asígnalas desde el Inspector)
    public Sprite maderaSprite; // Sprite que representará la madera
    public Sprite rocaSprite; // Sprite que representará la roca
    public Sprite setaSprite; // Sprite que representará la seta
    public Sprite fondoPredeterminado; // Sprite que representará el fondo predeterminado

    void Update()
    {
        // Detectar clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            // Realizar un raycast desde la cámara
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit impacto;

            if (Physics.Raycast(rayo, out impacto))
            {
                // Verificar si el objeto tocado es un recurso
                madera madera = impacto.collider.GetComponent<madera>();
                if (madera != null)
                {
                    CambiarImagenInventario(maderaSprite);
                    return; // Salimos después de recoger madera
                }

                roca roca = impacto.collider.GetComponent<roca>();
                if (roca != null)
                {
                    CambiarImagenInventario(rocaSprite);
                    return; // Salimos después de recoger roca
                }

                seta seta = impacto.collider.GetComponent<seta>();
                if (seta != null)
                {
                    CambiarImagenInventario(setaSprite);
                    return; // Salimos después de recoger seta
                }
            }
        }
    }

    void CambiarImagenInventario(Sprite spriteRecurso)
    {
        // Busca la primera celda vacía
        foreach (GameObject celda in celdasInventario)
        {
            Image imagenCelda = celda.GetComponentInChildren<Image>();

            // Verificar si la celda está vacía (o tiene el fondo predeterminado)
            if (imagenCelda != null && imagenCelda.sprite == fondoPredeterminado) // Verifica el sprite por defecto
            {
                // Cambiar la imagen de la celda según el recurso recogido
                imagenCelda.sprite = spriteRecurso;
                Debug.Log("Has recogido un recurso: " + spriteRecurso.name);
                return; // Salimos después de añadir
            }
            else
            {
                Debug.Log("Celda no vacía: " + imagenCelda.sprite.name); // Debug para verificar las celdas ocupadas
            }
        }

        // Si no hay espacio en el inventario
        Debug.Log("No hay espacio en el inventario.");
    }
}
