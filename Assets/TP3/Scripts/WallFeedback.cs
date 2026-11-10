
using System.Collections;
using UnityEngine;

public class WallFeedback : MonoBehaviour
{
    [Header("Feedback d'échec")]
    [SerializeField] private Color _couleurEchec = Color.red;
    [SerializeField] private float _dureeEchec = 1f;

    private Renderer _rendererMur;
    private Color _couleurInitiale;
    private Coroutine _coroutineEchec;

    private void Start()
    {
        _rendererMur = GetComponent<Renderer>();

        if (_rendererMur != null)
        {
            _couleurInitiale = _rendererMur.material.color;
        }
    }

    public void AfficherEchec()
    {
        if (_rendererMur == null)
            return;

        if (_coroutineEchec != null)
        {
            StopCoroutine(_coroutineEchec);
        }

        _coroutineEchec = StartCoroutine(ClignoterRouge());
    }

    private IEnumerator ClignoterRouge()
    {
        _rendererMur.material.color = _couleurEchec;

        yield return new WaitForSeconds(_dureeEchec);

        _rendererMur.material.color = _couleurInitiale;

        _coroutineEchec = null;
    }
}
