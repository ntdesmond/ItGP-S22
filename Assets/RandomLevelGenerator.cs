using UnityEngine;
using UnityEngine.Assertions;

public class RandomLevelGenerator : MonoBehaviour
{
    public uint pathPiecesCount;
    public Transform pathPiecePrefab;   
    public Transform levelPath;
    public Transform finishPrefab;
    public Transform coinPrefab;
    private void Awake()
    {
        Assert.IsTrue(pathPiecesCount > 0, "pathPiecesCount must be greater than 0");
        
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
            newScale.x = Random.Range(3, 10);
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
        var coinCount = (int)Random.Range(0, pathPiece.localScale.x / 3);
        for (var i = 1; i <= coinCount; i++)
        {
            var coin = Instantiate(coinPrefab, pathPiece);
            coin.localPosition = new Vector3(i * (1.0f / (coinCount + 1)) - 0.5f, 1.2f, 0);
            // Scale relatively to the whole level
            coin.parent = pathPiece.parent;
            coin.localScale = Vector3.one;
        }
    }
}
