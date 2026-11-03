
using UnityEngine;
using TMPro;

public class ProximityDetector : MonoBehaviour
{

    [SerializeField] private Transform controleur;
    [SerializeField] private GameObject[] cubes;

    [Header("Effets visuels")]
    [SerializeField] private Color couleurProximite = Color.green;
    [SerializeField] private float multiplicateurEchelle = 1.2f;
    [SerializeField] private float seuilDetection = 0.2f;

    [Header("Détection de direction")]
    [SerializeField] private float seuilAngle = 20f;
    [SerializeField] private float fontSize = 20f;
    private LineRenderer rayon;

    private Color[] couleursInitiales;
    private Vector3[] echellesInitiales;

    private TextMeshPro texteInfo;

    private void Start()
    {
        // Mémoriser les couleurs et les échelles initiales
        couleursInitiales = new Color[cubes.Length];
        echellesInitiales = new Vector3[cubes.Length];

        for (int i = 0; i < cubes.Length; i++)
        {
            Renderer rendu = cubes[i].GetComponent<Renderer>();

            couleursInitiales[i] = rendu.material.color;
            echellesInitiales[i] = cubes[i].transform.localScale;
        }

        // Créer un texte 3D attaché au contrôleur
        GameObject objetTexte = new GameObject("TexteInformations");
        objetTexte.transform.SetParent(controleur);

        objetTexte.transform.localPosition = new Vector3(0, 0.1f, 0.2f);

        texteInfo = objetTexte.AddComponent<TextMeshPro>();
        texteInfo.fontSize = fontSize;
        texteInfo.transform.localScale = Vector3.one * 0.01f;
        texteInfo.alignment = TextAlignmentOptions.Center;
        texteInfo.color = Color.white;

        // Créer et paramétrer le Line Renderer
        rayon = gameObject.AddComponent<LineRenderer>();

        rayon.positionCount = 2;
        rayon.startWidth = 0.005f;
        rayon.endWidth = 0.005f;
        rayon.useWorldSpace = true;

        // Créer un matériau blanc
        Material materiauBlanc = new Material(Shader.Find("Sprites/Default"));
        materiauBlanc.color = Color.white;

        rayon.material = materiauBlanc;

        rayon.enabled = false;
    }

    private void Update()
    {
        float distanceMinimale = float.MaxValue;
        int indiceCubeLePlusProche = -1;

        // Calculer les distances et identifier le cube le plus proche
        for (int i = 0; i < cubes.Length; i++)
        {

            float distance = Vector3.Distance(controleur.position, cubes[i].transform.position);

            if (distance < distanceMinimale)
            {
                distanceMinimale = distance;
                indiceCubeLePlusProche = i;
            }
        }

        // Gérer la couleur uniquement pour le cube le plus proche
        for (int i = 0; i < cubes.Length; i++)
        {
            Renderer rendu = cubes[i].GetComponent<Renderer>();

            if (i == indiceCubeLePlusProche && distanceMinimale < seuilDetection)
            {
                rendu.material.color = couleurProximite;
            }
            else
            {
                rendu.material.color = couleursInitiales[i];
            }
        }

        // Détecter l'orientation vers le cube le plus proche
        bool estOrienteVersCube = false;
        float angle = 0f;

        if (indiceCubeLePlusProche != -1)
        {
            Vector3 directionCube =
                (cubes[indiceCubeLePlusProche].transform.position - controleur.position).normalized;

            angle = Vector3.Angle(controleur.forward, directionCube);

            estOrienteVersCube = angle < seuilAngle;

            // Afficher le rayon uniquement si l'angle est inférieur au seuil
            if (estOrienteVersCube)
            {
                rayon.enabled = true;

                rayon.SetPosition(0, controleur.position);
                rayon.SetPosition(1, cubes[indiceCubeLePlusProche].transform.position);
            }
            else
            {
                rayon.enabled = false;
            }

            // Afficher la distance et l'angle
            texteInfo.text =
                "Distance : " + distanceMinimale.ToString("F2") + " m\n" +
                "Angle : " + angle.ToString("F1") + "°";
        }
        else
        {
            rayon.enabled = false;
            texteInfo.text = "";
        }

        // Orienter le texte vers la caméra
        if (Camera.main != null)
        {
            texteInfo.transform.rotation = Quaternion.LookRotation(
                texteInfo.transform.position - Camera.main.transform.position
            );
        }

        // Agrandir uniquement le cube le plus proche
        // si le contrôleur est proche ET orienté vers lui
        for (int i = 0; i < cubes.Length; i++)
        {

            if (i == indiceCubeLePlusProche && distanceMinimale < seuilDetection && estOrienteVersCube)
            {
                cubes[i].transform.localScale = echellesInitiales[i] * multiplicateurEchelle;
            }
            else
            {
                cubes[i].transform.localScale = echellesInitiales[i];
            }

        }
    }
}
