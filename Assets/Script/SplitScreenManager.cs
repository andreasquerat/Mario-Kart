using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    public GameObject kartP2;   // Référence au kart du joueur 2
    public Camera cameraP1;     // Caméra du joueur 1
    public Camera cameraP2;     // Caméra du joueur 2
    public Canvas canvasP2;     // UI du joueur 2
    private bool isSplitScreen = false;

    void Start()
    {
        // Désactiver tout ce qui concerne le joueur 2 au début
        kartP2.SetActive(false);
        cameraP2.gameObject.SetActive(false);
        canvasP2.gameObject.SetActive(false);
    }

    void Update()
    {
        // Si on appuie sur ², on active le mode 2 joueurs
        if (Input.GetKeyDown(KeyCode.BackQuote)) // `²` est le BackQuote sur un clavier AZERTY
        {
            ActivateSplitScreen();
        }
    }

    void ActivateSplitScreen()
    {
        if (isSplitScreen) return; // Évite de réactiver plusieurs fois

        isSplitScreen = true;
        kartP2.SetActive(true);
        cameraP2.gameObject.SetActive(true);
        canvasP2.gameObject.SetActive(true);

        // Scinder l'écran en deux
        cameraP1.rect = new Rect(0, 0, 0.5f, 1); // Caméra 1 à gauche
        cameraP2.rect = new Rect(0.5f, 0, 0.5f, 1); // Caméra 2 à droite
    }
}