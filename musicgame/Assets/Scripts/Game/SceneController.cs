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

namespace Game
{
    public class SceneController : MonoBehaviour
    {
        
        public const float PRE_NOTE_SPAWN_TIME = 3f;
        public const float PERFECT_BORDER = 0.05f;
        public const float GREAT_BORDER = 0.1f;
        public const float GOOD_BORDER = 0.2f;
        public const float BAD_BORDER = 0.5f;
        public Vector2 m_screenPos = new Vector2();

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

        float previousTime = 0f;
        SongData song;
        Dictionary<Button, int> lastTappedMilliseconds = new Dictionary<Button, int>();
        List<NoteObject> noteObjectPool = new List<NoteObject>();
        List<MessageObject> messageObjectPool = new List<MessageObject>();
        int maxLife;
        int life;
        int score;
        int combo;
        int maxCombo=0;
        int noteQuantity;
        MatchCollection mc = null;


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
                lifeText.text = string.Format("Life: {0}", life);
            }
            get { return life; }
        }

        int Score
        {
            set
            {
                score = value;
                scoreText.text = string.Format("Score: {0}", score);
            }
            get { return score; }
        }

        int Combo
        {
            set
            {
                combo = value;
                if(combo >= 50 && combo % 5 == 0)//生命回復
                {
                    if(life< maxLife)
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
            // フレームレート設定
            Application.targetFrameRate = 60;

            Score = 0;
            Life = 3000;
            maxLife = 30;
            Combo = 0;
            retryButton.onClick.AddListener(OnRetryButtonClick);
            stopButton.onClick.AddListener(Stop);
            UnStopButton.onClick.AddListener(UnStop);


            // ボタンのリスナー設定と最終タップ時間の初期化
            for (var i = 0; i < noteButtons.Length; i++)
            {
                noteButtons[i].onClick.AddListener(GetOnNoteButtonClickAction(i));
                lastTappedMilliseconds.Add(noteButtons[i], 0);
            }
            Debug.Log("Hits.Length" + Hits.Length);
            for (int i = 0; i < Hits.Length; i=i+2)
            {
                Debug.Log("set"+i);
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
            Debug.Log("noteQuantity" + noteQuantity);
            audioManager.bgm.PlayDelayed(1f);

        }

        void Update()
        {
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
                if (Time.time >= audioLength+2)
                {
                    Debug.Log("END");
                    Debug.Log("maxCombo:"+ maxCombo);
                    SceneManager.LoadScene("Score");
                    Score += 0;
                    //scoreText.text = string.Format("Score: {0}", score);
                    comboText.text = string.Format("Combo: {0}", maxCombo);
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
          
        void Stop()
             {
                Time.timeScale = 0;
                audioManager.bgm.Pause();
                Debug.Log("Stop");

        }
        void UnStop()
        {
            Time.timeScale = 1;
            audioManager.bgm.UnPause();
            Debug.Log("UnStop");

        }
        void OnNotePerfect(int noteNumber)
        {
            ShowMessage("Perfect", Color.yellow, noteNumber);
            //Score += 1000;
            ScoreDouble(1000);
            Combo++;
        }

        void OnNoteGreat(int noteNumber)
        {
            ShowMessage("Great", Color.magenta, noteNumber);
            //Score += 500;
            ScoreDouble(500);
            Combo++;
        }

        void OnNoteGood(int noteNumber)
        {
            ShowMessage("Perfect", Color.green, noteNumber);
            //Score += 300;
            ScoreDouble(300);
            Combo++;
        }

        void OnNoteBad(int noteNumber)
        {
            ShowMessage("Bad", Color.gray, noteNumber);
            Life--;
            Combo = 0;
        }

        void ScoreDouble(int up)//依combo高低調整分數上升幅度
        {
            Score = (score+up) * (1 + combo / 150);
        }

        public void OnNoteMiss(int noteNumber)
        {
            ShowMessage("Miss", Color.black, noteNumber);
            Life--;
            Combo = 0;
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

                audioManager.notes[noteNo].Play();
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
            if ((((-360 - m_screenPos.x) * (-360 - m_screenPos.x)) + (( - m_screenPos.y) * (0 - m_screenPos.y))) < radius)
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
