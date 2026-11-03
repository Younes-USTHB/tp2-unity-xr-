
using UnityEngine;
using TMPro;

public class FloatingMenu : MonoBehaviour
{

    private Transform panneau;
    private TextMeshPro texteMenu;

    private void Start()
    {
        // Récupérer le panneau Quad
        panneau = transform.Find("Quad");

        if (panneau == null)
        {
            Debug.LogError("Le panneau Quad est introuvable !");
            return;
        }

        // Récupérer le texte TextMeshPro enfant du Quad
        texteMenu = panneau.GetComponentInChildren<TextMeshPro>(true);

        if (texteMenu == null)
        {
            Debug.LogError("Le composant TextMeshPro est introuvable !");
            return;
        }

        // Positionner le menu à 15 cm devant la main gauche
        transform.localPosition = new Vector3(0, 0, 0.15f);

        // Configurer le panneau
        panneau.localPosition = Vector3.zero;
        panneau.localRotation = Quaternion.identity;
        panneau.localScale = new Vector3(0.2f, 0.1f, 1f);

        // Créer un matériau gris pour le Quad
        Material materiauGris = new Material(Shader.Find("Sprites/Default"));
        materiauGris.color = new Color(0.4f, 0.4f, 0.4f, 1f);

        Renderer renduPanneau = panneau.GetComponent<Renderer>();
        renduPanneau.material = materiauGris;

        // Configurer le texte
        texteMenu.color = Color.white;
        texteMenu.alignment = TextAlignmentOptions.Center;
        texteMenu.fontSize = 2;

        // Positionner le texte devant le panneau
        texteMenu.transform.localPosition = new Vector3(0, 0, -0.002f);

        // Corriger le texte inversé
        texteMenu.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    private void Update()
    {
        if (panneau == null || texteMenu == null)
            return;

        // Récupérer les positions locale et globale du panneau
        Vector3 positionLocale = panneau.localPosition;
        Vector3 positionGlobale = panneau.position;

        // Afficher les positions
        texteMenu.text =
            "Local: (" +
            positionLocale.x.ToString("F2") + ", " +
            positionLocale.y.ToString("F2") + ", " +
            positionLocale.z.ToString("F2") + ")\n" +

            "Global: (" +
            positionGlobale.x.ToString("F2") + ", " +
            positionGlobale.y.ToString("F2") + ", " +
            positionGlobale.z.ToString("F2") + ")";

        // Orienter le menu vers la caméra
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
