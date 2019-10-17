using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Data;
using System;
using Common;


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

   
 

         void Start()
        {
           // Debug.Log("in start");
            transforma = GetComponent<Transform>();
            v_destination = destination.transform.position;
            v_start = this.transform.position;
        }

        void Update()
        {           
            var v = time / moveTime;
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

