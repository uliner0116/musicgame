using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Common;
using Common.Data;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace Game
{
    public class SceneController : MonoBehaviour
    {
        public GameObject page;
        public string mainMenuSceneName;
        public string gameName;
        private bool pauseEnabled = false;
        public GameObject gameOverCanvasPrefab;

        public const float PRE_NOTE_SPAWN_TIME = 3f;
        public const float PERFECT_BORDER = 0.05f;
        public const float GREAT_BORDER = 0.1f;
        public const float GOOD_BORDER = 0.2f;
        public const float BAD_BORDER = 0.5f;
        public Vector2 m_screenPos = new Vector2();
        int listNumber = 0;
        string[] songList = new string[]{
            "butterfly" ,"Don't say lazy" ,"Im sorry" ,"LATATA" ,"LOVE" ,"Mirotic" ,"Oh!" ,"One Night In 北京" ,"PON PON PON" ,"Roly Poly" ,"SORRY SORRY" ,"Trouble Maker" ,"Tunak Tunak Tun" ,
             "YES or YES" ,"三國戀" ,"千年之戀" ,"不得不愛" ,"月牙灣" ,"回レ! 雪月花" ,"我不配" ,"我還年輕 我還年輕" ,"牡丹江" ,"東區東區" ,"直感" ,"星空" ,"夏祭り"  ,"恋は渾沌の隷也" ,
             "恋愛サーキュレーション" ,"夠愛" ,"將軍令" ,"華陽炎" ,"極楽浄土" ,"憂愁" ,"憨人" ,"樹枝孤鳥" ,"恋"
        };

        [SerializeField]
        AudioManager audioManager;
        [SerializeField]
        Button[] noteButtons;
        [SerializeField]
        Color defaultButtonColor;
        [SerializeField]
        Color highlightButtonColor;
        [SerializeField]
        TextAsset songDataAsset;
        [SerializeField]
        Transform noteObjectContainer;
        [SerializeField]
        NoteObject noteObjectPrefab;
        [SerializeField]
        Transform messageObjectContainer;
        [SerializeField]
        MessageObject messageObjectPrefab;
        [SerializeField]
        Transform baseLine;
        [SerializeField]
        GameObject gameOverPanel;
        [SerializeField]
        Button retryButton;
        [SerializeField]
        Button stopButton;
        [SerializeField]
        Button UnStopButton;
        [SerializeField]
        Button[] Hits;
        [SerializeField]
        Text scoreText;
        [SerializeField]
        Text lifeText;
        [SerializeField]
        Text comboText;
        [SerializeField]
        Text TimeText;

        float previousTime = 0f;
        SongData song;
        Dictionary<Button, int> lastTappedMilliseconds = new Dictionary<Button, int>();
        List<NoteObject> noteObjectPool = new List<NoteObject>();
        List<MessageObject> messageObjectPool = new List<MessageObject>();
        int maxLife;
        int life;
        public int Line;
        public int score;
        public int combo;
        public int maxCombo = 0;
        public int noteQuantity;
        public int perfectNum;
        public int greatNum;
        public int goodNum;
        public int badNum;
        public int missNum;
        public string songName;
        MatchCollection mc = null;
        Boolean inOver = false;
        float time1 = 0;
        public int maxScore;
        float volume;
        AudioClip noteAudio;



        /*Button[] Hits = new Button[]
        {
            Hit0, Hit1,Hit2
        };*/
        KeyCode[] keys = new KeyCode[]
       {
            KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K, KeyCode.L
       };


        int Life
        {
            set
            {
                life = value;
                if (life <= 0)
                {
                    life = 0;
                    gameOverPanel.SetActive(true);
                }
                lifeText.text = string.Format("Life:" + life);
            }
            get { return life; }
        }

        public int Score
        {
            set
            {
                score = value;
                scoreText.text = string.Format("Score: {0}", score);
            }
            get { return score; }
        }

        public int Combo
        {
            set
            {
                combo = value;
                if (combo >= 50 && combo % 5 == 0)//生命回復
                {
                    if (life < maxLife)
                    {
                        Life++;
                    }
                }
                if (maxCombo < combo)
                {
                    maxCombo = combo;
                }
                comboText.text = string.Format("Combo: {0}", combo);
            }
            get { return combo; }
        }




        void Start()
        {
            Debug.Log("star");
            if(Line == 3)
            {
                this.songDataAsset = songData.Line3SongDataAsset;
            }else if (Line == 6)
            {
                this.songDataAsset = songData.Line6SongDataAsset;
            }
            audioManager.bgm.clip = songData.audio;
            Life = 1000;
            //note音量設定
            loadVolume("audioNote");
            audioManager.note.volume = volume;
Life = 2500;
            //note音設定
            loadNoteAudio("notePlay");
            audioManager.note.clip = noteAudio;
            
            Debug.Log("bgm" + audioManager.bgm.volume);
            Debug.Log("note" + audioManager.note.volume);
            songName = audioManager.bgm.clip.name;
            
            //bgm音量設定
            string name = songName;
            /*for (int i = 1; i <= songList.Length; i++)
            {
                if (string.Compare(songList[i], songName) == 0)
                {
                    listNumber = i;
                    name = "song" + listNumber.ToString("D3");
                    Debug.Log("name: " + name);
                    break;
                }
            } */
            string txtName;
            txtName = name + " Audio";
            Debug.Log("txtName " + txtName);
            loadVolume(txtName);
            audioManager.bgm.volume = volume;

            pauseEnabled = false;
            Time.timeScale = 1;


            // フレームレート設定
            Application.targetFrameRate = 60;

            Score = 0;
            Life = 3000;
            maxLife = 30;
            Combo = 0;
            retryButton.onClick.AddListener(OnRetryButtonClick);
            stopButton.onClick.AddListener(Stop);



            // ボタンのリスナー設定と最終タップ時間の初期化
            for (var i = 0; i < noteButtons.Length; i++)
            {
                noteButtons[i].onClick.AddListener(GetOnNoteButtonClickAction(i));
                lastTappedMilliseconds.Add(noteButtons[i], 0);
            }
            Debug.Log("Hits.Length" + Hits.Length);
            for (int i = 0; i < Hits.Length; i++)
            {
                Debug.Log("set" + i);
                Hits[i].onClick.AddListener(GetOnNoteButtonClickAction(i));
                lastTappedMilliseconds.Add(Hits[i], 0);
            }
            // ノートオブジェクトのプール
            for (var i = 0; i < 100; i++)
            {
                var obj = Instantiate(noteObjectPrefab, noteObjectContainer);
                obj.baseY = baseLine.localPosition.y;
                obj.gameObject.SetActive(false);
                noteObjectPool.Add(obj);
            }
            noteObjectPrefab.gameObject.SetActive(false);

            // メッセージオブジェクトのプール
            for (var i = 0; i < 50; i++)
            {
                var obj = Instantiate(messageObjectPrefab, messageObjectContainer);
                obj.baseY = baseLine.localPosition.y;
                obj.gameObject.SetActive(false);
                messageObjectPool.Add(obj);
            }
            messageObjectPrefab.gameObject.SetActive(false);

            // 楽曲データのロード
            song = SongData.LoadFromJson(songDataAsset.text);
            Regex re = new Regex("time");
            mc = re.Matches(songDataAsset.text);
            noteQuantity = mc.Count;
            setMaxScore();
            // Debug.Log("noteQuantity" + noteQuantity);
            audioManager.bgm.PlayDelayed(1f);
            //audioManager.bgm.time = 30;


        }

        void Update()
        {
            time1 = Time.timeSinceLevelLoad;
            TimeText.text = string.Format("Time: {0}", time1);
            //Debug.Log("遊戲時間為:"+Time.time);
            // キーボード入力も可能に
            for (var i = 0; i < keys.Length; i++)
            {
                //接收觸及改這邊
                if (Input.GetKeyDown(keys[i]))
                {
                    noteButtons[i].onClick.Invoke();
                }
            }
            /*if (MobileInput())
            {
                Debug.Log("touch");
                int i;
                i = Collision();
                if (i != 5)
                {
                    Debug.Log(i);
                    noteButtons[i].onClick.Invoke();
                }
            }*/

            // ノートを生成
            var audioLength = audioManager.bgm.clip.length;
            var bgmTime = audioManager.bgm.time;
            if (time1 >= audioLength + 3 && inOver == false)
            {
                inOver = true;
                Debug.Log("prefect:" + perfectNum);
                Debug.Log("great:" + greatNum);
                Debug.Log("good:" + goodNum);
                Debug.Log("bad:" + badNum);
                Debug.Log("miss:" + missNum);
                Instantiate(gameOverCanvasPrefab, Vector2.zero, Quaternion.identity);
            }
            else
            {
                foreach (var note in song.GetNotesBetweenTime(previousTime + PRE_NOTE_SPAWN_TIME, bgmTime + PRE_NOTE_SPAWN_TIME))
                {
                    var obj = noteObjectPool.FirstOrDefault(x => !x.gameObject.activeSelf);
                    var positionX = noteButtons[note.NoteNumber].transform.localPosition.x;
                    obj.Initialize(this, audioManager.bgm, note, positionX);
                }
                previousTime = bgmTime;
            }
        }
        public void loadVolume(string name)
        {
            string loadJson;
            //讀取json檔案並轉存成文字格式
#if UNITY_EDITOR
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, name);
            Debug.Log("filePath:" + filePath);
#elif UNITY_ANDROID
            string filePath = Path.Combine("jar:file://" + Application.dataPath + "!assets/", name);

#endif

#if UNITY_EDITOR
            StreamReader file = new StreamReader(filePath);
            loadJson = file.ReadToEnd();
            file.Close();

#elif UNITY_ANDROID
            WWW reader = new WWW (filePath);
            while (!reader.isDone) {
            }
            loadJson = reader.text;
#endif
            Life = 2000;
            //新增一個物件類型為playerState的變數 loadData
            volumeState loadData = new volumeState();

            //使用JsonUtillty的FromJson方法將存文字轉成Json
            loadData = JsonUtility.FromJson<volumeState>(loadJson);

            //驗證用，將sammaru的位置變更為json內紀錄的位置
            volume = loadData.volume;
        }
        public void loadNoteAudio(string name)
        {
            string loadJson;
            //讀取json檔案並轉存成文字格式
#if UNITY_EDITOR
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, name);
            Debug.Log("filePath:" + filePath);
#elif UNITY_ANDROID
            string filePath = Path.Combine("jar:file://" + Application.dataPath + "!assets/", name);

#endif

#if UNITY_EDITOR
            StreamReader file = new StreamReader(filePath);
            loadJson = file.ReadToEnd();
            file.Close();
#elif UNITY_ANDROID
            WWW reader = new WWW (filePath);
            while (!reader.isDone) {
            }
            loadJson = reader.text;
#endif

            //新增一個物件類型為playerState的變數 loadData
            noteState loadData = new noteState();

            //使用JsonUtillty的FromJson方法將存文字轉成Json
            loadData = JsonUtility.FromJson<noteState>(loadJson);

            //驗證用，將sammaru的位置變更為json內紀錄的位置
            noteAudio = loadData.noteAudio;
        }
        public class volumeState
        {
            public float volume;
        }
        public class noteState
        {
            public AudioClip noteAudio = null;
        }
        void setMaxScore()
        {
            
            for (int i = 0; i < noteQuantity; i++)
            {
                float v = 1+((float)i/(float)noteQuantity);
                maxScore = Convert.ToInt32(maxScore + ( 1000 * v));
            }
            Debug.Log("maxScore:" + maxScore);
        }

        void Stop()
        {
            page.SetActive(true);
            Time.timeScale = 0;
            audioManager.bgm.Pause();
            Debug.Log("Stop");
            pauseEnabled = true;
        }





        
        public void BackMenu()//Make Main Menu button
        {
            Application.LoadLevel(mainMenuSceneName);
        }

        public void Retry()
        {
            if (Line == 3)
            {
                SceneManager.LoadScene("Game 3ver");
            }
            else if (Line == 6)
            {
                SceneManager.LoadScene("Game 6ver");
            }
        }
        public void BackGame()
        {
            page.SetActive(false);
            pauseEnabled = false;          
            Time.timeScale = 1;
            //Thread.Sleep(3000);
            audioManager.bgm.UnPause();
            Debug.Log("UnStop");
            
        }
         
        void OnNotePerfect(int noteNumber)
        {
            ShowMessage("Perfect", Color.yellow, noteNumber);
            //Score += 1000;
            ScoreDouble(1000);
            Combo++;
            perfectNum++;
        }

        void OnNoteGreat(int noteNumber)
        {
            ShowMessage("Great", Color.magenta, noteNumber);
            //Score += 500;
            ScoreDouble(500);
            Combo++;
            greatNum++;
        }


        void OnNoteGood(int noteNumber)
        {
            ShowMessage("Good", Color.green, noteNumber);
            //Score += 300;
            ScoreDouble(300);
            Combo++;
            goodNum++;
        }

        void OnNoteBad(int noteNumber)
        {
            ShowMessage("Bad", Color.gray, noteNumber);
            Life--;
            Combo = 0;
            badNum++;
        }

        void ScoreDouble(int up)//依combo高低調整分數上升幅度
        {
            Score = (score + up) * (1 + combo / noteQuantity);
        }

        public void OnNoteMiss(int noteNumber)
        {
            ShowMessage("Miss", Color.black, noteNumber);
            Life--;
            Combo = 0;
            missNum++;
        }

        void ShowMessage(string message, Color color, int noteNumber)
        {
            if (gameOverPanel.activeSelf)
            {
                return;
            }

            var positionX = noteButtons[noteNumber].transform.localPosition.x;
            var obj = messageObjectPool.FirstOrDefault(x => !x.gameObject.activeSelf);
            obj.Initialize(message, color, positionX);
        }

        /// <summary>
        /// ボタンのフォーカスを外します
        /// </summary>
        /// <returns>The coroutine.</returns>
        /// <param name="button">Button.</param>
        IEnumerator DeselectCoroutine(Button button)
        {
            yield return new WaitForSeconds(0.01f);
            if (lastTappedMilliseconds[button] <= DateTime.Now.Millisecond - 100)
            {
                button.image.color = defaultButtonColor;
            }
        }

        /// <summary>
        /// ノート（音符）に対応したボタン押下時のアクションを返します
        /// </summary>
        /// <returns>The on note button click action.</returns>
        /// <param name="noteNo">Note no.</param>
        UnityAction GetOnNoteButtonClickAction(int noteNo)
        {
            Debug.Log("GetOnNoteButtonCli");
            return () =>
            {
                if (gameOverPanel.activeSelf)
                {
                    return;
                }

                audioManager.note.Play();
                noteButtons[noteNo].image.color = highlightButtonColor;
                StartCoroutine(DeselectCoroutine(noteButtons[noteNo]));
                lastTappedMilliseconds[noteButtons[noteNo]] = DateTime.Now.Millisecond;

                var targetNoteObject = noteObjectPool.Where(x => x.NoteNumber == noteNo)
                                                     .OrderBy(x => x.AbsoluteTimeDiff)
                                                     .FirstOrDefault(x => x.AbsoluteTimeDiff <= BAD_BORDER);
                if (null == targetNoteObject)
                {
                    return;
                }

                var timeDiff = targetNoteObject.AbsoluteTimeDiff;
                Debug.Log("hit time: " + targetNoteObject.bgm.time);
                if (timeDiff <= PERFECT_BORDER)
                {
                    OnNotePerfect(targetNoteObject.NoteNumber);
                }
                else if (timeDiff <= GREAT_BORDER)
                {
                    OnNoteGreat(targetNoteObject.NoteNumber);
                }
                else if (timeDiff <= GOOD_BORDER)
                {
                    OnNoteGood(targetNoteObject.NoteNumber);
                }
                else
                {
                    OnNoteBad(targetNoteObject.NoteNumber);
                }
                targetNoteObject.gameObject.SetActive(false);
            };
        }

        /*UnityAction AcriveNoteBotton(int noteNum)
        {
            noteButtons[noteNum].onClick.Invoke();
            return ;
        }*/

        void OnRetryButtonClick()
        {
            SceneManager.LoadScene("Game");
        }
        Boolean MobileInput()
        {
            if (Input.touchCount <= 0)
                return false;

            //1個手指觸碰螢幕
            if (Input.touchCount == 1)
            {
                //開始觸碰
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    Debug.Log("Began");
                    //紀錄觸碰位置
                    m_screenPos = Input.touches[0].position;

                    return true;
                    //手指移動
                }
                /*else if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    Debug.Log("Moved");
                    //移動攝影機
                    //Camera.main.transform.Translate (new Vector3 (-Input.touches [0].deltaPosition.x * Time.deltaTime, -Input.touches [0].deltaPosition.y * Time.deltaTime, 0));
                }*/
                //手指離開螢幕
                /*if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    //Debug.Log("Ended");
                    Vector2 pos = Input.touches[0].position;
                    //移動 gDefine.Direction mDirection = HandDirection(m_screenPos, pos);
                    //移動 Debug.Log("mDirection: " + mDirection.ToString());
                }*/
                //攝影機縮放，如果1個手指以上觸碰螢幕
            }
            return false;
            /*else if (Input.touchCount > 1)
            {

                //記錄兩個手指位置
                Vector2 finger1 = new Vector2();
                Vector2 finger2 = new Vector2();

                //記錄兩個手指移動距離
                Vector2 move1 = new Vector2();
                Vector2 move2 = new Vector2();

                //是否是小於2點觸碰
                for (int i = 0; i < 2; i++)
                {
                    UnityEngine.Touch touch = UnityEngine.Input.touches[i];

                    if (touch.phase == TouchPhase.Ended)
                        break;

                    if (touch.phase == TouchPhase.Moved)
                    {
                        //每次都重置
                        float move = 0;

                        //觸碰一點
                        if (i == 0)
                        {
                            finger1 = touch.position;
                            move1 = touch.deltaPosition;
                            //另一點
                        }
                        else
                        {
                            finger2 = touch.position;
                            move2 = touch.deltaPosition;

                            //取最大X
                            if (finger1.x > finger2.x)
                            {
                                move = move1.x;
                            }
                            else
                            {
                                move = move2.x;
                            }

                            //取最大Y，並與取出的X累加
                            if (finger1.y > finger2.y)
                            {
                                move += move1.y;
                            }
                            else
                            {
                                move += move2.y;
                            }

                            //當兩指距離越遠，Z位置加的越多，相反之
                            Camera.main.transform.Translate(0, 0, move * Time.deltaTime);
                        }
                    }
                }//end for
            }//end else if */
        }
        int Collision()
        {
            //在这里进行碰撞检测
            //检测的原理是点与圆形的碰撞
            //利用数学公事　(x1 – x2)2 + (y1 – y2)2 < (r1 + r2)2
            //判断点是在蓝盘中还是红盘中

            int radius = 50 * 50;
            if ((((-360 - m_screenPos.x) * (-360 - m_screenPos.x)) + ((-m_screenPos.y) * (0 - m_screenPos.y))) < radius)
            {
                return 0;
            }
            /*else if ((((-180 - m_screenPos.x) * (-180 - m_screenPos.x)) + ((0 - m_screenPos.y) * (0 - m_screenPos.y))) < radius)
            {
                return 1;
            }*/
            else if ((((0 - m_screenPos.x) * (0 - m_screenPos.x)) + ((0 - m_screenPos.y) * (0 - m_screenPos.y))) < radius)
            {
                return 2;
            }
            /*else if ((((180 - m_screenPos.x) * (180 - m_screenPos.x)) + ((0 - m_screenPos.y) * (0 - m_screenPos.y))) < radius)
            {
                return 3;
            }*/
            else if ((((360 - m_screenPos.x) * (360 - m_screenPos.x)) + ((0 - m_screenPos.y) * (0 - m_screenPos.y))) < radius)
            {
                return 4;
            }
            else
            {
                return 5;
            }

        }
    }
}

