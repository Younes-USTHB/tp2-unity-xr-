
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text _texteScore;

    private int _score = 0;

    private void Awake()
    {
        Instance = this;
        ActualiserScore();
    }

    public void AjouterPoints(int points)
    {
        _score += points;
        ActualiserScore();
    }

    private void ActualiserScore()
    {
        if (_texteScore != null)
        {
            _texteScore.text = "Score = " + _score;
        }
    }
}
