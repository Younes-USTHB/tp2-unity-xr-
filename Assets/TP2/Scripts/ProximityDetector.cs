
using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    
    [SerializeField] private Transform controleur;
    [SerializeField] private GameObject[] cubes;

    [Header("Effets visuels")]
    [SerializeField] private Color couleurProximite = Color.green;
    [SerializeField] private float multiplicateurEchelle = 1.2f;
    [SerializeField] private float seuilDetection = 0.2f;

    private Color[] couleursInitiales;
    private Vector3[] echellesInitiales;

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
    }

    private void Update()
    {
        float distanceMinimale = float.MaxValue;
        int indiceCubeLePlusProche = -1;

        // Calculer les distances et gérer les couleurs
        for (int i = 0; i < cubes.Length; i++)
        {

            float distance = Vector3.Distance(controleur.position, cubes[i].transform.position );

            Renderer rendu = cubes[i].GetComponent<Renderer>();

            if (distance < seuilDetection)
            {
                rendu.material.color = couleurProximite;
            }
            else
            {
                rendu.material.color = couleursInitiales[i];
            }

            // Identifier le cube le plus proche
            if (distance < distanceMinimale)
            {
                distanceMinimale = distance;
                indiceCubeLePlusProche = i;
            }
        }

        // Agrandir uniquement le cube le plus proche
        for (int i = 0; i < cubes.Length; i++)
        {

            if (i == indiceCubeLePlusProche &&
                distanceMinimale < seuilDetection)
            {
                cubes[i].transform.localScale =
                    echellesInitiales[i] * multiplicateurEchelle;
            }
            else
            {
                cubes[i].transform.localScale =
                    echellesInitiales[i];
            }

        }
    }
}
