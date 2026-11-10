
using System.Collections;
using UnityEngine;

public class CibleManager : MonoBehaviour
{
    [Header("Feedback audio")]
    [SerializeField] private AudioClip _sonReussite;

    [Header("Feedback visuel")]
    [SerializeField] private Color _couleurReussite = Color.green;
    [SerializeField] private float _dureeClignotement = 0.5f;

    private AudioSource _audioSource;
    private Renderer _rendererCible;

    private Color _couleurInitiale;
    private Coroutine _coroutineClignotement;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rendererCible = GetComponent<Renderer>();

        if (_rendererCible != null)
        {
            _couleurInitiale = _rendererCible.material.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'objet entrant est un projectile
        if (!other.CompareTag("Projectile"))
            return;

        Debug.Log("Cible touchée : " + gameObject.name);

        // Feedback audio
        if (_audioSource != null && _sonReussite != null)
        {
            _audioSource.PlayOneShot(_sonReussite);
        }

        // Feedback visuel
        if (_rendererCible != null)
        {
            if (_coroutineClignotement != null)
            {
                StopCoroutine(_coroutineClignotement);
            }

            _coroutineClignotement = StartCoroutine(Clignoter());
        }

        // Ajouter 10 points au score
        ScoreManager.Instance.AjouterPoints(10);

    }

    private IEnumerator Clignoter()
    {
        _rendererCible.material.color = _couleurReussite;

        yield return new WaitForSeconds(_dureeClignotement);

        _rendererCible.material.color = _couleurInitiale;

        _coroutineClignotement = null;
    }
}
