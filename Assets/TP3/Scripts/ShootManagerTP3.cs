
using UnityEngine;
using UnityEngine.XR;

public class ShootManagerTP3 : MonoBehaviour
{
    [Header("Configuration du tir")]
    [SerializeField] private Transform _manetteDroiteTransform;
    [SerializeField] private float _toleranceAimAngle = 20f;

    [Header("Feedback")]
    [SerializeField] private ParticleSystem _prefabParticules;
    [SerializeField] private AudioClip _shotAudio;

    private InputDevice _rightController;
    private GameObject[] _targets;

    private bool _gachettePrecedente = false;

    void Start()
    {
        // Récupération de la manette droite
        _rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Récupération de toutes les cibles
        _targets = GameObject.FindGameObjectsWithTag("Target");

        Debug.Log("Nombre de cibles : " + _targets.Length);
    }

    void Update()
    {
        // MODE META QUEST : vérification de la manette
        if (!_rightController.isValid)
        {
            _rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        // Détection de la gâchette Meta Quest
        if (_rightController.isValid)
        {
            bool gachetteAppuyee;

            if (_rightController.TryGetFeatureValue(
                CommonUsages.triggerButton,
                out gachetteAppuyee))
            {
                if (gachetteAppuyee && !_gachettePrecedente)
                {
                    Shoot();
                }

                _gachettePrecedente = gachetteAppuyee;
            }
        }

        // MODE SIMULATEUR : clic gauche de la souris
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        foreach (GameObject target in _targets)
        {
            // Calcul du vecteur vers la cible
            Vector3 directionVersCible = target.transform.position - _manetteDroiteTransform.position;

            // Calcul de l'angle de visée
            float angleVisee = Vector3.Angle( _manetteDroiteTransform.forward, directionVersCible );

            // Vérification de la tolérance angulaire
            if (angleVisee <= _toleranceAimAngle)
            {
                // Instanciation des particules
                ParticleSystem particules = Instantiate(
                    _prefabParticules,
                    target.transform.position,
                    Quaternion.identity
                );

                // Lecture des particules
                particules.Play();

                // Destruction automatique
                Destroy(
                    particules.gameObject,
                    particules.main.duration +
                    particules.main.startLifetime.constantMax
                );

                // Récupération de l'AudioSource
                AudioSource audioSource = target.GetComponent<AudioSource>();

                // Lecture du son
                if (audioSource != null && _shotAudio != null)
                {
                    audioSource.PlayOneShot(_shotAudio);
                }

                // Une seule cible par tir
                break; // pour arreter la boucle si la cible est trouvée
            }
        }
    }
}
