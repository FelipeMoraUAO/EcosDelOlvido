using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<string> hechizosRecolectados = new List<string>();
    public List<string> recuerdosRecolectados = new List<string>();

    public int totalRecuerdosNecesarios = 4; // ✅ Cantidad requerida para ganar

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🌀 Métodos para HECHIZOS
    public void AgregarHechizo(string nombre)
    {
        if (!hechizosRecolectados.Contains(nombre))
        {
            hechizosRecolectados.Add(nombre);
            Debug.Log($"🪄 Hechizo agregado: {nombre} | Total: {hechizosRecolectados.Count}");
        }
    }

    // 🧩 Métodos para RECUERDOS
    public void AgregarRecuerdo(string nombre)
    {
        if (!recuerdosRecolectados.Contains(nombre))
        {
            recuerdosRecolectados.Add(nombre);
            Debug.Log($"💭 Recuerdo agregado: {nombre} | Total: {recuerdosRecolectados.Count}");

            // ✅ Si ya los tiene todos, cargar Victoria
            if (recuerdosRecolectados.Count >= totalRecuerdosNecesarios)
            {
                Debug.Log("🏆 ¡Todos los recuerdos recolectados! Cargando escena de Victoria...");
                SceneManager.LoadScene("7Victoria");
            }
        }
    }

    // 🔎 Métodos para comprobar progreso
    public int GetHechizos() => hechizosRecolectados.Count;
    public int GetRecuerdos() => recuerdosRecolectados.Count;
}
