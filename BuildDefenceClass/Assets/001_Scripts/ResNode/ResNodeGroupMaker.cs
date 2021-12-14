using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResNodeGroupMaker : MonoBehaviour
{
    [Header("리소스 노드 프리팹")]
    [SerializeField] private GameObject[] _resNodePrefabs;

    enum eNodeType : int
    {
        Wood = 0,
        Stone,
        Gold,
    }

    [Header("리소스 노드 생성 옵션")]
    [SerializeField] bool _bNodeTypeRandom = true;
    [SerializeField] eNodeType _nodeType;

    [Range(1, 3)]
    [SerializeField] int _nodeAmountMin = 1;

    [Range(5, 7)]
    [SerializeField] int _nodeAmountMax = 5;

    [Range(1.0f, 2.0f)]
    [SerializeField] float _nodeDistance = 1.0f;

    private void Awake()
    {
        RandomMakeNodeGroup();

    }

    private void RandomMakeNodeGroup()
    {
        GameObject nodeObj = null;
        float randomPosX;
        float randomPosY;

        if(_resNodePrefabs == null)
        {
            Debug.LogWarning("RandomMakeGroup > _resNodePrefabs is null");
            return;
        }

        // 노드 타입
        if(_bNodeTypeRandom)
        {
            int nodeTypeMax = (int)System.Enum.GetValues(typeof(eNodeType)).Length;
            _nodeType = (eNodeType)Random.Range(0, nodeTypeMax);
        }

        // 노드 수 만큼 생성
        int nodeLength = Random.Range(_nodeAmountMin, _nodeAmountMax + 1);
        for (int i = 0; i < nodeLength; ++i)
        {
            nodeObj = Instantiate(_resNodePrefabs[(int)_nodeType], transform);
            
            randomPosX = transform.position.x + Random.Range(-_nodeDistance, _nodeDistance);
            randomPosY = transform.position.y + Random.Range(-_nodeDistance, _nodeDistance);

            nodeObj.transform.SetPositionAndRotation(new Vector3(randomPosX, randomPosY), Quaternion.identity);
            
        }
    }


}
