using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    public GameObject kartP2;   // R�f�rence au kart du joueur 2
    public Camera cameraP1;     // Cam�ra du joueur 1
    public Camera cameraP2;     // Cam�ra du joueur 2
    public Canvas canvasP2;     // UI du joueur 2
    private bool isSplitScreen = false;

    void Start()
    {
        // D�sactiver tout ce qui concerne le joueur 2 au d�but
        kartP2.SetActive(false);
        cameraP2.gameObject.SetActive(false);
        canvasP2.gameObject.SetActive(false);
    }

    void Update()
    {
        // Si on appuie sur �, on active le mode 2 joueurs
        if (Input.GetKeyDown(KeyCode.BackQuote)) // `�` est le BackQuote sur un clavier AZERTY
        {
            ActivateSplitScreen();
        }
    }

    void ActivateSplitScreen()
    {
        if (isSplitScreen) return; // �vite de r�activer plusieurs fois

        isSplitScreen = true;
        kartP2.SetActive(true);
        cameraP2.gameObject.SetActive(true);
        canvasP2.gameObject.SetActive(true);

        // Scinder l'�cran en deux
        cameraP1.rect = new Rect(0, 0, 0.5f, 1); // Cam�ra 1 � gauche
        cameraP2.rect = new Rect(0.5f, 0, 0.5f, 1); // Cam�ra 2 � droite
    }
}