using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Dimension.Stage
{
    public class StageController : MonoBehaviour
    {
        //public StageData stageData;

        [Space]
        // ステージ
        //public MeshSetter stageMesh;
        public MeshCollider stageMesh;
        public MeshCollider rightSecond;
        public MeshCollider leftSecond;

        //[Space]
        //// ギミック
        //public StageGoal goalPre;

        Mesh renderMesh;     // レンダリングされるメッシュ
        Mesh colliderMesh;   // コライダー作成用のメッシュ

        // 戻る位置
        //BackLine[] backLinesRight;
        //BackLine[] backLinesLeft;
        // リスポーン地点
        Vector3[] ReSpawrnList;
        //-----------------------------------------------------
        //  プロパティ
        //-----------------------------------------------------
        public Vector3 StageForward { get; private set; }   // ステージの正面方向
        public Vector3 StageRight   { get; private set; }   // ステージの右方向
        public Vector3 StageCenter  { get; private set; }   // ステージの中心
        public Vector3 StartPoint   { get; private set; }   // スタート地点
        public Vector3 GoalPoint    { get; private set; }   // ゴール地点
        public float StageWidth     { get; private set; }   // ステージの幅
        public float StageHeight    { get; private set; }   // ステージの高さ
        public float StageDepth     { get; private set; }   // ステージの奥行き

        //=====================================================
        void Awake()
        {
            //if (stageData == null) return;

            StageForward = new Vector3(0, 0, -1);

            Initialize();
        }
        //-----------------------------------------------------
        //  ステージ情報の設定
        //-----------------------------------------------------
        //public void SetStageData(StageData data)
        //{
        //    stageData = data;
        //    Initialize();
        //}
        //-----------------------------------------------------
        //  リスポーン地点を返す
        //-----------------------------------------------------
        public Vector3 GetReSpawrnPoint(Vector3 playerPos)
        {
            Vector3 bestPoint = new Vector3(0, 0, 0);
            float lengthMin = 100;
            foreach(Vector3 point in ReSpawrnList) {
                float length = (playerPos - point).magnitude;
                if (length > lengthMin) continue;
                lengthMin = length;
                bestPoint = point;
            }
            return bestPoint;
        }
        //-----------------------------------------------------
        //  2Dから3Dに変換するときの位置
        //-----------------------------------------------------
        public Vector3 GetBackPoint(Vector3 playerPos)
        {
            Vector3 rePoint = playerPos;
            //// プレイヤーの位置が右かどうか
            //bool isRight = Conversion.Vector3ToFloat(Vector3.Scale(StageCenter, StageRight)) <
            //               Conversion.Vector3ToFloat(Vector3.Scale(playerPos, StageRight));
            //// 使用するほうを選択
            //BackLine[] backLines = (isRight) ? backLinesRight : backLinesLeft;
            //Vector3 forwardAxis = new Vector3(Mathf.Abs(StageForward.x), 0, Mathf.Abs(StageForward.z));
            //float playerDepth = Conversion.Vector3ToFloat(Vector3.Scale(forwardAxis, playerPos));
            //// 
            //for(int i = 0; i < backLines.Length; ++i) {
            //    if(playerDepth < backLines[i].endDepth)
            //    {

            //    }
            //}

            return rePoint;
        }
        //-----------------------------------------------------
        //  初期化
        //-----------------------------------------------------
        void Initialize()
        {
            //ReadStageData();    // 読み込み
            DataCalculation();  // 計算
            CreateStage();      // 生成
        }
        //-----------------------------------------------------
        //  データの読み込み
        //-----------------------------------------------------
        void ReadStageData()
        {
            //renderMesh = stageData.renderMesh;
            //colliderMesh = stageData.colliderMesh;

            //// 読み込み
            //StringReader reader = new StringReader(stageData.data.text);

            //StageForward = Conversion.StringToVector3(reader.ReadLine());
            //StartPoint   = Conversion.StringToVector3(reader.ReadLine());
            //GoalPoint    = Conversion.StringToVector3(reader.ReadLine());

            //// 2Dから3Dに変換するときに戻る位置 右
            //backLinesRight = ReadBackLines(ref reader);

            //// 2Dから3Dに変換するときに戻る位置 左
            //backLinesLeft = ReadBackLines(ref reader);

            //// リスポーン地点
            //int num = int.Parse(reader.ReadLine());
            //ReSpawrnList = new Vector3[num];
            //for (int i = 0; i < ReSpawrnList.Length; ++i) {
            //    ReSpawrnList[i] = Conversion.StringToVector3(reader.ReadLine());
            //}

            //reader.Close();
        }

        //BackLine[] ReadBackLines(ref StringReader sr)
        //{
        //    int num = int.Parse(sr.ReadLine());
        //    BackLine[] backLines = new BackLine[num];
        //    for (int i = 0; i < backLines.Length; ++i)
        //    {
        //        backLines[i] = new BackLine {
        //            endDepth = float.Parse(sr.ReadLine())
        //        };
        //        backLines[i].highSide.Clear();
        //        int highNum = int.Parse(sr.ReadLine());
        //        for (int j = 0; j < highNum; ++j) {
        //            backLines[i].highSide.Add(Conversion.StringToVector2(sr.ReadLine()));
        //        }
        //    }
        //    return backLines;
        //}
        //-----------------------------------------------------
        //  読み込んだデータから計算
        //-----------------------------------------------------
        void DataCalculation()
        {
            StageRight = Quaternion.Euler(new Vector3(0, 90, 0)) * StageForward;
            colliderMesh = stageMesh.sharedMesh;

            // ステージのサイズ
            Vector3 vecMax = new Vector3(0, 0, 0);
            Vector3 vecMin = new Vector3(0, 0, 0);

            foreach (Vector3 vec in colliderMesh.vertices)
            {
                vecMax = Vector3.Max(vec, vecMax);
                vecMin = Vector3.Min(vec, vecMin);
            }

            // ステージの中央
            Vector3 centerVec = (vecMax - vecMin).normalized;
            float dis = (vecMax - vecMin).magnitude * 0.5f;
            StageCenter = centerVec * dis;

            StageWidth  = vecMax.x - vecMin.x;
            StageHeight = vecMax.y - vecMin.y;
            StageDepth  = vecMax.z - vecMin.z;
        }
        //-----------------------------------------------------
        //  ステージ生成
        //-----------------------------------------------------
        void CreateStage()
        {
            // 三次元用
            //stageMesh.SetMesh(renderMesh, colliderMesh);

            // 二次元用
            rightSecond.transform.position = StageRight  * StageWidth * 2;
            leftSecond.transform.position  = -StageRight * StageWidth * 2;
            CreateSecondCollider();

            // ゴールの配置
            //GameObject goalObj = Instantiate(goalPre.gameObject, GoalPoint, Quaternion.identity);
            //goalObj.transform.parent = transform;
        }
        //-----------------------------------------------------
        //  2次元用Collider(3D)を用意
        //-----------------------------------------------------
        public class Plane
        {
            const int VERTEX_NUM = 4;   // 頂点数
            public Vector3[] vertices;  // 頂点
            public Vector3 normalVector;// 法線ベクトル

            public Vector3 Center
            {
                get {
                    return vertices[0] + (vertices[2] - vertices[0]) * 0.5f;
                }
            }

            // コンストラクタ
            public Plane() {
                vertices = new Vector3[VERTEX_NUM];
            }
            public Plane(Vector3 one, Vector3 two, Vector3 three, Vector3 fowr) {
                vertices = new Vector3[] { one, two, three, fowr };
            }

            public static bool operator ==(Plane p1, Plane p2) { return p1.Equals(p2); }
            public static bool operator !=(Plane p1, Plane p2) { return !p1.Equals(p2); }

            public override bool Equals(object obj) {
                if (!(obj is Plane)) return false;

                Plane other = (Plane)obj;
                for (int i = 0; i < VERTEX_NUM; ++i)
                    if (vertices[i] != other.vertices[i]) return false;
                
                return true;
            }
            public override int GetHashCode() { return base.GetHashCode(); }

            // 法線ベクトルを計算
            public Vector3 GetNormalVector() {
                normalVector = Vector3.Cross(vertices[1] - vertices[0], vertices[2] - vertices[0]);
                return normalVector;
            }
            // 引数の軸の値が同一かどうか
            public bool CheckAxisSame(Vector3 axis) {
                return Vector3.Scale(axis, vertices[0]) == Vector3.Scale(axis, vertices[1]) &&
                       Vector3.Scale(axis, vertices[1]) == Vector3.Scale(axis, vertices[2]) &&
                       Vector3.Scale(axis, vertices[2]) == Vector3.Scale(axis, vertices[3]);
            }
        }
        public class Cube
        {
            const int VERTEX_NUM = 8;   // 頂点数
            public Vector3[] vertices;  // 頂点
            public int[]     triangles; // 三角形

            public Cube(Plane plane)
            {
                // 頂点
                vertices = new Vector3[] {
                    plane.vertices[0] + plane.normalVector * 0.5f,
                    plane.vertices[1] + plane.normalVector * 0.5f,
                    plane.vertices[2] + plane.normalVector * 0.5f,
                    plane.vertices[3] + plane.normalVector * 0.5f,
                    plane.vertices[1] - plane.normalVector * 0.5f,
                    plane.vertices[0] - plane.normalVector * 0.5f,
                    plane.vertices[3] - plane.normalVector * 0.5f,
                    plane.vertices[2] - plane.normalVector * 0.5f,
                };
                // 三角形
                triangles = new int[] {
                    0, 1, 2, 0, 2, 3,   // 正面
                    4, 5, 6, 4, 6, 7,   // 背後
                    3, 2, 7, 3, 7, 6,   // 上
                    1, 0, 5, 1, 5, 4,   // 下
                    5, 0, 3, 5, 3, 6,   // 右
                    1, 4, 7, 1, 7, 2    // 左
                };
            }

            public void CreateTriangle(int start) {
                for(int i = 0; i < triangles.Length; i++) {
                    triangles[i] += start;
                }
            }
        }
        void CreateSecondCollider()
        {
            List<Plane>     planes    = new List<Plane>();      // 平面のリスト
            List<Cube>      cubes     = new List<Cube>();       // 立方体のリスト
            List<Vector3>   vertices  = new List<Vector3>();    // 頂点のリスト
            List<int>       triangles = new List<int>();        // 三角形のリスト

            // 必要な平面を取得
            planes = GetMeshToPlane(colliderMesh, StageRight);

            // 平面を立方体に
            foreach(Plane plane in planes) {
                cubes.Add(new Cube(plane));
            }

            //頂点と三角形を取得
            foreach(Cube cube in cubes) {
                cube.CreateTriangle(vertices.Count);

                vertices.AddRange(cube.vertices);
                triangles.AddRange(cube.triangles);
            }

            // Mesh作成
            Mesh secondMesh = new Mesh
            {
                name = "Second Mesh",
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray()
            };

            secondMesh.RecalculateBounds();
            secondMesh.RecalculateNormals();

            rightSecond.sharedMesh = secondMesh;
            leftSecond.sharedMesh = secondMesh;

            return;
        }
        //-----------------------------------------------------
        //  指定された軸を向いた平面を取得
        //-----------------------------------------------------
        List<Plane> GetMeshToPlane(Mesh mesh, Vector3 axis)
        {
            List<Plane> planeList = new List<Plane>();

            Vector3 getAxis = new Vector3(
                (Mathf.Round(axis.x) == 0) ? 1 : 0,
                (Mathf.Round(axis.y) == 0) ? 1 : 0,
                (Mathf.Round(axis.z) == 0) ? 1 : 0
                );

            for (int i = 0; i < colliderMesh.vertexCount / 4; ++i)
            {
                // 平面を取得
                Plane plane = new Plane(
                    Vector3.Scale(getAxis, colliderMesh.vertices[(i * 4) + 0]),
                    Vector3.Scale(getAxis, colliderMesh.vertices[(i * 4) + 1]),
                    Vector3.Scale(getAxis, colliderMesh.vertices[(i * 4) + 2]),
                    Vector3.Scale(getAxis, colliderMesh.vertices[(i * 4) + 3])
                    );

                // 頂点の順序
                if (plane.GetNormalVector() != axis) continue;

                //リストに追加
                if (planeList.Count == 0) planeList.Add(plane);
                else if (CheckNotListSame(planeList, plane)) planeList.Add(plane);
            }

            return planeList;
        }
        //-----------------------------------------------------
        //  List内にvalueと同じ値がないとき、true
        //-----------------------------------------------------
        bool CheckNotListSame(List<Plane> list, Plane value)
        {
            foreach (var item in list)
            {
                if (value == item) return false;
            }
            return true;
        }
    }
}