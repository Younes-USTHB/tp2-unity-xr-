
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [Header("Feedback d'échec")]
    [SerializeField] private WallFeedback _feedbackMur;

    private Vector3 _positionInitiale;
    private Quaternion _rotationInitiale;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _positionInitiale = transform.position;
        _rotationInitiale = transform.rotation;

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile touché par : " + collision.gameObject.name);

        // Afficher le feedback d'échec sur le mur
        if (_feedbackMur != null)
        {
            _feedbackMur.AfficherEchec();
        }

        Reinitialiser();
    }

    private void Reinitialiser()
    {
        // Arrêter le mouvement physique
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        // Revenir à la position initiale
        _rigidbody.position = _positionInitiale;
        _rigidbody.rotation = _rotationInitiale;

        Debug.Log("Projectile réinitialisé !");
    }
}
