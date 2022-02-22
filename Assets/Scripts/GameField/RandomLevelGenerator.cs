using UnityEngine;
using UnityEngine.Assertions;

namespace GameField
{
    public class RandomLevelGenerator : MonoBehaviour
    {
        [Min(1)]
        public int pathPiecesCount;
        
        [Min(1)]
        public int minPathPieceLength;
        
        [Min(1)]
        public int maxPathPieceLength;
        
        [Min(1)]
        public float coinSpawnHeight;
        
        [Range(0, 1)]
        public float maxCoinRatio;
        
        public Transform pathPiecePrefab;   
        public Transform levelPath;
        public Transform finishPrefab;
        public Transform coinPrefab;

        private void Awake()
        {
            Assert.IsTrue(
                minPathPieceLength <= maxPathPieceLength, 
                "Minimal path piece length should not exceed the maximum path piece length"
            );
            var lastPathPiece = CreatePath();
            PlaceFinishObject(lastPathPiece);
        }

        private Transform CreatePath()
        {
            var initialPathPieceScale = pathPiecePrefab.localScale;
            var lastPieceEndPosition = Vector3.zero;
            var offsets = new[]
            {
                Vector3.Scale(initialPathPieceScale, new Vector3(0, -1, 0)),
                Vector3.Scale(initialPathPieceScale, new Vector3(1, -1, -1))
            };
            Transform lastPathPiece;
            uint i = 0;
            do
            {
                var newScale = initialPathPieceScale;
                newScale.x = Random.Range(minPathPieceLength, maxPathPieceLength);
                lastPathPiece = Instantiate(pathPiecePrefab, levelPath);
                lastPathPiece.localScale = newScale;
                var rotation = i % 2 == 0 ? Quaternion.identity : Quaternion.AngleAxis(-90, Vector3.up);
                newScale = rotation * newScale;
                lastPathPiece.SetPositionAndRotation(lastPieceEndPosition + newScale / 2, rotation);
                lastPieceEndPosition += newScale + offsets[i % 2];
                PlaceCoins(lastPathPiece);
            } while (++i < pathPiecesCount);
        
            return lastPathPiece;
        }

        private void PlaceFinishObject(Transform pathPiece)
        {
            var pathPieceScale = pathPiece.localScale;
            var finishInstance = Instantiate(finishPrefab, pathPiece);
            var finishScale = finishInstance.localScale;
            finishScale.x = pathPieceScale.z / pathPieceScale.x;
            finishInstance.localScale = finishScale;
            finishInstance.localPosition = new Vector3(1 - finishScale.x, 1.01f, 0) / 2;
        }

        private void PlaceCoins(Transform pathPiece)
        {
            var maxCoinCount = pathPiece.localScale.x * maxCoinRatio;
            var coinCount = (int)Random.Range(0.0f, maxCoinCount);
            for (var i = 1; i <= coinCount; i++)
            {
                var coin = Instantiate(coinPrefab, pathPiece);
                coin.localPosition = new Vector3(i * (1.0f / (coinCount + 1)) - 0.5f, coinSpawnHeight, 0);
                // Scale relatively to the whole level
                coin.parent = pathPiece.parent;
                coin.localScale = Vector3.one;
            }
        }
    }
}
