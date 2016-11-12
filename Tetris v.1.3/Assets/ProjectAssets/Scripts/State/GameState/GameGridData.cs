using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGridData : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Models;          //Массив игровых моделей
   
    [SerializeField]
    private Texture2D[] NexModelTexture;  //Массив изображений моделей

    [SerializeField]
    private RawImage RawImage;

    [SerializeField]
    private Text ScoreText;

    [SerializeField]
    private Text LevelText;

    [SerializeField]
    private Text LineText;  

    public GameObject[] _models
    {
        get {
            return Models;
        }
    }

    public Texture2D[] _modelsTex
    {
        get
        {
            return NexModelTexture;
        }
    }

    public RawImage _rawImg
    {
        get
        {
            return RawImage;
        }
    }

    public Text _scoreText
    {
        get
        {
            return ScoreText;
        }
    }

    public Text _levelText
    {
        get
        {
            return LevelText;
        }
    }

    public Text _lineText
    {
        get
        {
            return LineText;
        }
    }
}