using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Instancia estática para usar este script como singleton en todas las escenas
    public static GameManager Instance;

    // Lista de hechizos recolectados por el jugador
    public List<string> hechizosRecolectados = new List<string>();

    // Lista de recuerdos recolectados por el jugador
    public List<string> recuerdosRecolectados = new List<string>();

    // Cantidad total de recuerdos requeridos para activar la victoria
    public int totalRecuerdosNecesarios = 4;

    // Se ejecuta antes de Start. Configura el patrón Singleton.
    void Awake()
    {
        // Si no existe otra instancia, esta se vuelve la principal
        if (Instance == null)
        {
            Instance = this;

            // Evita que este objeto sea destruido al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia previa, destruye esta duplicada
            Destroy(gameObject);
        }
    }

    // Agrega un hechizo a la lista si aún no ha sido recolectado
    public void AgregarHechizo(string nombre)
    {
        if (!hechizosRecolectados.Contains(nombre))
        {
            hechizosRecolectados.Add(nombre);
            Debug.Log($"Hechizo agregado: {nombre} | Total: {hechizosRecolectados.Count}");
        }
    }

    // Agrega un recuerdo y verifica si se completó la cantidad necesaria para ganar
    public void AgregarRecuerdo(string nombre)
    {
        if (!recuerdosRecolectados.Contains(nombre))
        {
            recuerdosRecolectados.Add(nombre);
            Debug.Log($"Recuerdo agregado: {nombre} | Total: {recuerdosRecolectados.Count}");

            // Si el jugador reúne todos los recuerdos, carga la escena de victoria
            if (recuerdosRecolectados.Count >= totalRecuerdosNecesarios)
            {
                Debug.Log("Todos los recuerdos recolectados. Cargando escena de Victoria.");
                SceneManager.LoadScene("7Victoria");
            }
        }
    }

    // Retorna la cantidad de hechizos recolectados
    public int GetHechizos() => hechizosRecolectados.Count;

    // Retorna la cantidad de recuerdos recolectados
    public int GetRecuerdos() => recuerdosRecolectados.Count;
}
