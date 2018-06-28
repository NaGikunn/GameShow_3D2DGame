﻿using UnityEngine;
using Dimension.Stage;

namespace Dimension.Player
{
    public struct KeyState
    {
        public bool Action;
        public bool Jump;
        public bool Dash;

        public Vector2 Axis;
    }

    public class TestPlayer : MonoBehaviour
    {
        Transform transformCache;
        Rigidbody rigidbodyCache;

        PlayerMover pMover;

        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------
        // Transform
        public Vector3 Position {
            get { return transformCache.position; }
            set { transformCache.position = value; }
        }
        public Vector3 LocalPosition {
            get { return transformCache.localPosition; }
            set { transformCache.localPosition = value; }
        }
        public Vector3 Center { get { return transformCache.position + Vector3.up * 0.5f; } }
        public Vector3 Forward {
            get { return transformCache.forward; }
            set { transformCache.forward = value; }
        }
        // ステータス
        public bool IsStop { get; set; }
        public bool IsGround { get { return Mathf.Abs(rigidbodyCache.velocity.y) <= 0.01f; } }
        public bool IsFall { get { return transformCache.localPosition.y + 2.0f < HeightMin; } }    // 落下判定
        public bool IsRight {
            get
            {   // ステージ中央より右にいるか
                Vector3 playerVec = LocalPosition - StageCenter;
                float side = StageForward.z * playerVec.x - StageForward.x * playerVec.z;
                return side >= 0;
            } }   

        //
        public GameController GController { get; private set; }
        public StageController SController { get; private set; }

        //
        public Vector3 StageForward { get; private set; }
        public Vector3 StageRight { get; private set; }
        public Vector3 StageCenter { get; private set; }
        public float HeightMax { get; private set; }        // 復活する高さ
        public float HeightMin { get; private set; }        // 落下判定の位置

        //
        public Vector3 SpawnPosition { get; private set; }
        public float SaveAccel { get; set; }
        //=====================================================
        void Awake()
        {
            transformCache = transform;
            rigidbodyCache = GetComponent<Rigidbody>();
            IsStop = false;
            SaveAccel = 0;
            if (pMover == null) ChangeMover<PlayerMover3D>();
        }
        void Update()
        {
            if (IsStop) return;
            pMover.Move(InputKey());

            if (IsGround) SpawnPosition = transformCache.localPosition;
            // 落下判定
            if (IsFall) pMover.ReSpawn(SpawnPosition);
        }
        //-----------------------------------------------------
        //  ゲームコントローラー受け取り
        //-----------------------------------------------------
        public void SetGameController(GameController gCon)
        {
            GController = gCon;
            SController = gCon.sController;

            StageForward = SController.StageForward;
            StageRight = SController.StageRight;
            StageCenter = SController.StageCenter;
            HeightMax    = SController.StageCenter.y + SController.StageHeight * 0.5f + 1;
            HeightMin    = SController.StageCenter.y - SController.StageHeight * 0.5f - 1;
        }
        //-----------------------------------------------------
        //  操作の変更
        //-----------------------------------------------------
        public void ChangeMover<PM>() where PM : PlayerMover
        {
            Destroy(pMover);
            pMover = gameObject.AddComponent<PM>();
        }
        //-----------------------------------------------------
        // 入力
        //-----------------------------------------------------
        KeyState InputKey()
        {
            KeyState key = new KeyState();

            if(Input.GetJoystickNames().Length == 0)
            {   // キーボード
                key.Action = Input.GetKeyDown(KeyCode.Z);
                key.Jump   = Input.GetKeyDown(KeyCode.Space);
                key.Dash = Input.GetKeyDown(KeyCode.LeftControl);

                key.Axis.x += (Input.GetKey(KeyCode.RightArrow)) ? 1 : 0;
                key.Axis.x -= (Input.GetKey(KeyCode.LeftArrow))  ? 1 : 0;
                key.Axis.y += (Input.GetKey(KeyCode.UpArrow))    ? 1 : 0;
                key.Axis.y -= (Input.GetKey(KeyCode.DownArrow))  ? 1 : 0;
                key.Axis = key.Axis.normalized;
            } else
            {   // ゲームパッド
                key.Action = Input.GetButtonDown("Action");
                key.Jump   = Input.GetButtonDown("Jump");
                key.Dash   = Input.GetButtonDown("Dash");

                key.Axis = new Vector2(
                    Input.GetAxis("Horizontal"),
                    Input.GetAxis("Vertical")
                    );
            }

            return key;
        }
    }
}