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

    public static int Height = 20;       //Высота игравой сетки
    public static int Width = 10;        //Ширина игравой сетки
    public static Transform[,] gridTransform = new Transform[Width, Height];

    private float fall = 0;
    public float speedFull = 1f;

    private GameGridData _lineTxt;
    private GameGridData _levelTxt;
    private GameGridData _scoreTxt;

    private Text lineText;
    private Text scoreText;               //Переменна для вывода кол-ва очков на экран
    private Text levelText;

    private int curScore = 0;            //Установка начального значения счетчика очков
    private int curLine = 0;             //Установка начального значения счетчика удаленных линий
    private int curLevel = 1;            //Установка начального значения счетчика уровня сложности
    private int countRow = 0;            //Устанавливаем начальное значение кол-ва удаленных рядов 

    private int scoreOneLine = 1;        //Кол-во очков за удаление одной линии
    private int scoreTwoLine = 3;        //Кол-во очков за удаление двух линий
    private int scoerThreeLine = 5;

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

        public override void Activate(IStateData data, bool reset)
        {
            Debug.Log("OnActivate");
            SceneManager.LoadScene(_sceneName);
            AppRoot.Instance.StartCoroutine(LoadSceneData());
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
        protected override IEnumerator LoadSceneData()
        {
            yield return null;
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
        public void GetRandomNewModel()
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
        public void InstantiateModel(string GO)
        {
            if (GO == "ModI")
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[0], instPos, Quaternion.identity);
            }
            if (GO == "ModJ")
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[1], instPos, Quaternion.identity);
            }
            if (GO == "ModL")
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[2], instPos, Quaternion.identity);
            }
            if (GO == "ModO")
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[3], instPos, Quaternion.identity);
            }
            if (GO == "ModS")
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[4], instPos, Quaternion.identity);
            }
            if (GO == "ModT")
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[5], instPos, Quaternion.identity);
            }
            if (GO == "ModZ")
            {
                UnityEngine.GameObject.Instantiate(_curModels._models[6], instPos, Quaternion.identity);
            }
        }

        /// <summary>
        /// Плавное удаление ряда 
        /// </summary>
        public void DeleteRow()
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
        void DelRow()
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
        public bool IsFullRowm(int y)
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
        public void _Fade(int y)
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
        public void DeleteModel(int y)
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
        public void AllRownDown(int y)
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
        public void RownDown(int y)
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
        public void MoveLeft()
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
        public void MoveRight()
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
        public void MoveDown()
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
        public void Rotation()
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
        public bool LimitedMove()
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
        public void UpdatetGrid()
        {
            for (int y = 0; y < Height; ++y)
                for (int x = 0; x < Width; ++x)
                    if (gridTransform[x, y] != null)
                        if (gridTransform[x, y].parent == moveMod.transform)
                            gridTransform[x, y] = null;
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
        public bool UpLimitGrid()
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
        public bool Border(Vector2 pos)
        {
            return ((int)pos.x >= 0 && (int)pos.x < GameGridState.Width && (int)pos.y >= 0);
        }

        /// <summary>
        /// Округление значений к целому числу
        /// </summary>
        /// <param name="pos">Принимает значения расположения игровой модели на поле</param>
        /// <returns>Возвращает целое число</returns>
        public Vector2 Round(Vector2 pos)
        {
            return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
        }

        /// <summary>
        /// Счетчик полученых очков за удаление ряда и кол-во удаленных рядов
        /// </summary>
        void UpdateScore()
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
        void UpdateLevel()
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
                curLevel = 2;
                speedFull = 0.85f;
            }
            else if (curScore == Mathf.Clamp(curScore, 21, 30))
            {
                curLevel = 3;
                speedFull = 0.7f;
            }
            else if (curScore == Mathf.Clamp(curScore, 41, 50))
            {
                curLevel = 4;
                speedFull = 0.55f;
            }
            else if (curScore == Mathf.Clamp(curScore, 51, 60))
            {
                curLevel = 5;
                speedFull = 0.4f;
            }
            else if (curScore == Mathf.Clamp(curScore, 61, 70))
            {
                curLevel = 6;
                speedFull = 0.3f;
            }
            else if (curScore == Mathf.Clamp(curScore, 71, 80))
            {
                curLevel = 7;
                speedFull = 0.25f;
            }
            else if (curScore == Mathf.Clamp(curScore, 81, 90))
            {
                curLevel = 8;
                speedFull = 0.2f;
            }
            else if (curScore == Mathf.Clamp(curScore, 91, 100))
            {
                curLevel = 9;
                speedFull = 0.1f;
            }
        } 
    }
}