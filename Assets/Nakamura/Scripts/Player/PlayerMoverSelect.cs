using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dimension.Player
{
    public class PlayerMoverSelect : PlayerMover
    {
        //-----------------------------------------------------
        //  初期化
        //-----------------------------------------------------
        public override void Initialize()
        {
            
        }
        //-----------------------------------------------------
        //  行動
        //-----------------------------------------------------
        public override void Move(KeyState key)
        {
            Vector3 inputVec = Vector3.forward * key.Axis.y + Vector3.right * key.Axis.x;

            transformCache.localPosition += inputVec * DEFAULT_SPEED * Time.deltaTime;
        }
        //-----------------------------------------------------
        //  リスポーン
        //-----------------------------------------------------
        public override void ReSpawn(Vector3 position) { return; }
    }
}