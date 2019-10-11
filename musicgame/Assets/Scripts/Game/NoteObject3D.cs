using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Data;
using System;
using Common;

namespace Game
{
    public class NoteObject3D : MonoBehaviour
    {
        public float moveTime;
        public GameObject destination;
        public GameObject lostPosition;
        //GameScene3D sceneController;
        private new Transform transforma;
        //public AudioSource bgm;
        //SongData.Note note;
        private float time = 0f;
        private Vector3 v_start;
        private Vector3 v_destination;

   
       /*public int NoteNumber
        {
            get { return gameObject.activeSelf ? note.NoteNumber : int.MinValue; }
        }*/

        /*public float AbsoluteTimeDiff
        {
            get { return Mathf.Abs(note.Time - bgm.time); }
        }*/

         void Start()
        {
            Debug.Log("in start");
            transforma = GetComponent<Transform>();
            v_destination = lostPosition.transform.position;
            v_start = this.transform.position;
        }

        void Update()
        {
            /*var timeDiff = note.Time - bgm.time;

            if (timeDiff < -SceneController.BAD_BORDER)
            {
                sceneController.OnNoteMiss(NoteNumber);
                gameObject.SetActive(false);
            }

            GetComponent<Transform>().localPosition = new Vector3(positionX,
                                                baseY + timeDiff * 100f,
                                                baseZ + timeDiff * 100f);*/
            Debug.Log("in update");
            var v = time / moveTime;
            Debug.Log(" v:" + v);
            this.transform.position = Vector3.Lerp(v_start, v_destination, v);
            time += Time.deltaTime;

        }
        private void OnCollisionEnter(Collision collision)
        {
            if(destination == collision.gameObject)
            {
                v_start = this.transform.position;
                destination = lostPosition;
                v_destination = destination.transform.position;
                time = 0f;
            }
            if(lostPosition == collision.gameObject)
            {
                Debug.Log("test");
                Destroy(gameObject);
            }
        }

        /*public void Initialize(GameScene3D sceneController, AudioSource bgm, SongData.Note note, float positionX)
        {
            gameObject.SetActive(true);

            this.sceneController = sceneController;
            this.bgm = bgm;
            this.note = note;
            //this.transform.localPosition.x = positionX;
            /*switch (note.NoteNumber)
            {
                case 1:
               /* case 3:
                    image.color = Color.green;
                    break;
                default:
                    image.color = Color.white;
                    break;
            }

            Update();
        }*/
    }
}
