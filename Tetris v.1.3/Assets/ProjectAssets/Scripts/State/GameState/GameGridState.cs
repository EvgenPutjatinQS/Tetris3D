using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tetris
{

public class GameGridState : AppState {

    private GameGridData _nextTexture;
    private Texture2D nextTex;

    private GameGridData _curModels;
    private GameObject curMod;
    
    private GameGridData _rawImg;
    private RawImage rawImg;

    private Vector2 instPos = new Vector2(4, 18);

    public static int Height = 20;       
    public static int Width = 10;        
    public static Transform[,] gridTransform = new Transform[Width, Height];

    private float fall = 0;
    public float speedFull = 1f;

    private GameGridData _lineTxt;
    private GameGridData _levelTxt;
    private GameGridData _scoreTxt;

    private Text lineText;
    private Text scoreText;              
    private Text levelText;

    private int curScore = 0;           
    private int curLine = 0;            
    private int curLevel = 1;            
    private int countRow = 0;           

    private int scoreOneLine = 1;       
    private int scoreTwoLine = 3;        
    private int scoerThreeLine = 5;

    private float speedLevel_2 = 0.85f;
    private float speedLevel_3 = 0.7f;
    private float speedLevel_4 = 0.55f;
    private float speedLevel_5 = 0.4f;
    private float speedLevel_6 = 0.3f;
    private float speedLevel_7 = 0.25f;
    private float speedLevel_8 = 0.2f;
    private float speedLevel_9 = 0.1f;

    private int Level_2 = 2;
    private int Level_3 = 3;
    private int Level_4 = 4;
    private int Level_5 = 5;
    private int Level_6 = 6;
    private int Level_7 = 7;
    private int Level_8 = 8;
    private int Level_9 = 9;

    private string Mod_I = "ModI";
    private string Mod_J = "ModJ";
    private string Mod_L = "ModL";
    private string Mod_O = "ModO";
    private string Mod_S = "ModS";
    private string Mod_T = "ModT";
    private string Mod_Z = "ModZ";

    private GameObject _color;
    private Color color;

    private bool _fadeDelta = false;

    private GameObject moveMod;

    private GameObject curModel;


        public GameGridState() : base()
        {
            _sceneName = "GridGame";
            _id = EAppStateId.GridGame;
        }

        public override void Initialize() {}

        public override void Deactivate() {}

        public override void Update() {

            moveMod = GameObject.FindGameObjectWithTag("Model");

            if (moveMod != null)
            {
                if (_fadeDelta == false)
                {
                    if (moveMod.tag == "Model")
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            MoveLeft();
                        }
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            MoveRight();
                        }
                        if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= speedFull)
                        {
                            MoveDown();
                        }
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            Rotation();
                        }
                    }
                }
                AppRoot.Instance.StartCoroutine(Fade());
                UpdateScore();
                UpdateLevel();
            }
            
        }

        /// <summary>
        /// Инициализирует игровую модель и генерирует методом случайного выбора модель для следующей инициализации
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator LoadSceneData(AsyncOperation op)
        {
            yield return base.LoadSceneData(op);
            _ModelGO = GameObject.Find(_sceneName);
            if (_ModelGO != null)
            {
                _curModels = _ModelGO.GetComponent<GameGridData>();
                _nextTexture = _ModelGO.GetComponent<GameGridData>();
                _rawImg = _ModelGO.GetComponent<GameGridData>();
            }
            curMod = _curModels._models[Random.Range(0, _curModels._models.Length)];
            UnityEngine.GameObject.Instantiate(curMod, instPos, Quaternion.identity);
            nextTex = _nextTexture._modelsTex[Random.Range(0, _nextTexture._modelsTex.Length)];
            rawImg = _rawImg._rawImg;
            rawImg.texture = nextTex;

        }
 
        /// <summary>
        /// Получение случайной модели 
        /// </summary>
        protected void GetRandomNewModel()
        {
            string curTexture = nextTex.name;
            InstantiateModel(curTexture);
            nextTex = _nextTexture._modelsTex[Random.Range(0, _nextTexture._modelsTex.Length)];
            rawImg.texture = nextTex;
        }
        
        /// <summary>
        /// Инициализация игроговой модели
        /// </summary>
        /// <param name="GO"></param>
        protected void InstantiateModel(string GO)
        {
            if (GO == Mod_I)
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[0], instPos, Quaternion.identity);
            }
            if (GO == Mod_J)
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[1], instPos, Quaternion.identity);
            }
            if (GO == Mod_L)
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[2], instPos, Quaternion.identity);
            }
            if (GO == Mod_O)
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[3], instPos, Quaternion.identity);
            }
            if (GO == Mod_S)
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[4], instPos, Quaternion.identity);
            }
            if (GO == Mod_T)
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[5], instPos, Quaternion.identity);
            }
            if (GO == Mod_Z)
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[6], instPos, Quaternion.identity);
            }
        }

        /// <summary>
        /// Плавное удаление ряда 
        /// </summary>
        protected void DeleteRow()
        {
            for (int x = 0; x < Height; ++x)
            {
                if (IsFullRowm(x))
                {
                    _Fade(x);          
                }
            }
        }

        /// <summary>
        /// Удаление игровой модели и сдвиг ряда после плавного удаления модели
        /// </summary>
        protected void DelRow()
        {
            for (int x = 0; x < Height; ++x)
            {
                if (IsFullRowm(x))
                {
                    DeleteModel(x);
                    AllRownDown(x + 1);
                    --x;
                }
            }
        }
        
        /// <summary>
        /// Плавное удаление ряда
        /// </summary>
        /// <returns>Возращает значение Alpha для цвета</returns>
        protected override IEnumerator Fade()
        {
            if (_fadeDelta == true)
            {
                for (int x = 0; x < Width; ++x)
                {
                    color = curModel.GetComponentInChildren<Renderer>().material.color;
                    color.a -= Time.deltaTime;
                    curModel.GetComponentInChildren<Renderer>().material.color = color;
                    Debug.Log(color.a);
                }
                if (color.a <= 0)
                {
                    DelRow();
                    _fadeDelta = false;

                }
            }
            yield return null;
        }

        /// <summary>
        /// Проверка заполнения ряда
        /// </summary>
        /// <param name="y">Кол-во заполненых рядов</param>
        /// <returns>Возвращает значение заполнения ряда</returns>
        protected bool IsFullRowm(int y)
        {
            for (int x = 0; x < Width; ++x)
            {
                if (gridTransform[x, y] == null)
                {
                    return false;
                }
            } 
            return true;
        }

        /// <summary>
        /// Присвоение переменной значений игровой модели 
        /// </summary>
        /// <param name="y">Кол-во заполненых рядов</param>
        protected void _Fade(int y)
        {
            for (int x = 0; x < Width; ++x)
            {
                if (gridTransform[x, y] != null)
                {
                    curModel = gridTransform[x, y].gameObject;
                    _fadeDelta = true;
                    AppRoot.Instance.StartCoroutine(Fade());
                }
            } 
        }

        /// <summary>
        /// Удаление игровых моделей по кол-ву рядов
        /// </summary>
        /// <param name="y">Кол-во заполненых рядов</param>
        protected void DeleteModel(int y)
        {
            for (int x = 0; x < Width; ++x)
            {
                UnityEngine.GameObject.Destroy(gridTransform[x, y].gameObject);
                gridTransform[x, y] = null;
                _fadeDelta = false;
            }
            countRow++;
        }

        /// <summary>
        /// Сдвиг рядов после удаления игровых моделей
        /// </summary>
        /// <param name="y">Кол-во заполненых рядов</param>
        protected void AllRownDown(int y)
        {
            for (int i = y; i < Height; ++i)
            {
                RownDown(i);
            }
        }

        /// <summary>
        /// Сдвиг ряда
        /// </summary>
        /// <param name="y">Кол-во заполненых рядов</param>
        protected void RownDown(int y)
        {
            for (int x = 0; x < Width; ++x)
            {
                if (gridTransform[x, y] != null)
                {
                    gridTransform[x, y - 1] = gridTransform[x, y];
                    gridTransform[x, y] = null;
                    gridTransform[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
        
        /// <summary>
        /// Реализация перемещения игровой модели влево
        /// </summary>
        protected void MoveLeft()
        {
            moveMod.transform.position += new Vector3(-1, 0, 0);
            if (LimitedMove())
            {
                UpdatetGrid();
            }
            else
                moveMod.transform.position += new Vector3(1, 0, 0);
        }

        /// <summary>
        /// Реализация перемещения игровой модели вправо
        /// </summary>
        protected void MoveRight()
        {
            moveMod.transform.position += new Vector3(1, 0, 0);
            if (LimitedMove())
            {
                UpdatetGrid();
            }
            else
                moveMod.transform.position += new Vector3(-1, 0, 0);
        }

        /// <summary>
        /// Реализация перемещения игровой модели вниз
        /// </summary>
        protected void MoveDown()
        {
            moveMod.transform.position += new Vector3(0, -1, 0);
            if (LimitedMove())
            {
                UpdatetGrid();
            }
            else
            {
                moveMod.transform.position += new Vector3(0, 1, 0);
                moveMod.tag = "NoModel";

                    GetRandomNewModel();
                    DeleteRow();

                if (UpLimitGrid())
                {
                    AppRoot.Instance.SetState(EAppStateId.GameOver);
                }

            }
            fall = Time.time;
        }

        /// <summary>
        /// Вращение игровой модели
        /// </summary>
        protected void Rotation()
        {
            moveMod.transform.Rotate(0, 0, 90);
            if (LimitedMove())
            {
                UpdatetGrid();
            }
            else
                moveMod.transform.Rotate(0, 0, -90);
        }

        /// <summary>
        /// Устанавливает границы игрового поля
        /// </summary>
        /// <returns>Возвращает значение расположения игровой модели на поле </returns>
        protected bool LimitedMove()
        {
            foreach (Transform tran in moveMod.transform)
            {
                Vector2 pos = Round(tran.position);
                if (!Border(pos))
                {
                    return false;
                }
                if (gridTransform[(int)pos.x, (int)pos.y] != null &&
                    gridTransform[(int)pos.x, (int)pos.y].parent != moveMod.transform)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Обновление значений расположения игрой модели на поле 
        /// </summary>
        protected void UpdatetGrid()
        {
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    if (gridTransform[x, y] != null)
                    {
                        if (gridTransform[x, y].parent == moveMod.transform)
                        {
                            gridTransform[x, y] = null;
                        }
                    }
                }
            }
            foreach (Transform tran in moveMod.transform)
            {
                Vector2 pos = Round(tran.position);
                gridTransform[(int)pos.x, (int)pos.y] = tran;
            }
        }

        /// <summary>
        /// Определение верхнего предела расположения игровой модели
        /// </summary>
        /// <returns>Возвращает значение расположения игровой модели на поле</returns>
        protected bool UpLimitGrid()
        {
            for (int x = 0; x < Width; ++x)
            {
                foreach (Transform trans in moveMod.transform)
                {
                    Vector2 pos = Round(trans.position);
                    if (pos.y > Height - 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Определение границ игрового поля
        /// </summary>
        /// <param name="pos">Принимает значения расположения игровой модели на поле</param>
        /// <returns>Возвращает значение расположения игровой модели на поле</returns>
        protected bool Border(Vector2 pos)
        {
            return (pos.x >= 0 && (int)pos.x < Width && pos.y >= 0);
        }

        /// <summary>
        /// Округление значений к целому числу
        /// </summary>
        /// <param name="pos">Принимает значения расположения игровой модели на поле</param>
        /// <returns>Возвращает целое число</returns>
        protected Vector2 Round(Vector2 pos)
        {
            return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
        }

        /// <summary>
        /// Счетчик полученых очков за удаление ряда и кол-во удаленных рядов
        /// </summary>
        protected void UpdateScore()
        {
            _ModelGO = GameObject.Find(_sceneName);
            if (_ModelGO != null)
            {
                _scoreTxt = _ModelGO.GetComponent<GameGridData>();
                _lineTxt = _ModelGO.GetComponent<GameGridData>();
            }
            scoreText = _scoreTxt._scoreText;
            lineText = _lineTxt._lineText;

            lineText.text = "Lines: " + curLine.ToString();
            scoreText.text = "Score: " + curScore.ToString();
            if (countRow > 0)
            {
                if (countRow == 1)
                {
                    curScore += scoreOneLine;
                    curLine += countRow;
                }
                else if (countRow == 2)
                {
                    curScore += scoreTwoLine;
                    curLine += countRow;
                }
                else if (countRow == 3)
                {
                    curScore += scoerThreeLine;
                    curLine += countRow;
                }
                else if (countRow == 4)
                {
                    curScore += scoerThreeLine;
                    curLine += countRow;
                }
            }
            countRow = 0;
        }

        /// <summary>
        /// Счетчик уровня сложности игры
        /// </summary>
        protected void UpdateLevel()
        {
            _ModelGO = GameObject.Find(_sceneName);
            if (_ModelGO != null)
            {
                _levelTxt = _ModelGO.GetComponent<GameGridData>();
            }
            levelText = _levelTxt._levelText;
            levelText.text = "Level: " + curLevel.ToString();

            if (curScore == Mathf.Clamp(curScore, 11, 20))
            {
                curLevel = Level_2;
                speedFull = speedLevel_2;
            }
            else if (curScore == Mathf.Clamp(curScore, 21, 30))
            {
                curLevel = Level_3;
                speedFull = speedLevel_3;
            }
            else if (curScore == Mathf.Clamp(curScore, 41, 50))
            {
                curLevel = Level_4;
                speedFull = speedLevel_4;
            }
            else if (curScore == Mathf.Clamp(curScore, 51, 60))
            {
                curLevel = Level_5;
                speedFull = speedLevel_5;
            }
            else if (curScore == Mathf.Clamp(curScore, 61, 70))
            {
                curLevel = Level_6;
                speedFull = speedLevel_6;
            }
            else if (curScore == Mathf.Clamp(curScore, 71, 80))
            {
                curLevel = Level_7;
                speedFull = speedLevel_7;
            }
            else if (curScore == Mathf.Clamp(curScore, 81, 90))
            {
                curLevel = Level_8;
                speedFull = speedLevel_8;
            }
            else if (curScore == Mathf.Clamp(curScore, 91, 100))
            {
                curLevel = Level_9;
                speedFull = speedLevel_9;
            }
        } 
    }
}