using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResNodePositionManager : MonoBehaviour
{
    [Header("리소스 노드 그룹 프리팹")]
    [SerializeField] private ResNodeGroupMaker _resNodeGroupPrefab;

    [Header("리소스 노드 배치 옵션")]
    [Range(5, 7)]
    [SerializeField] int _nodeGroupAmountMin = 5;

    [Range(9, 11)]
    [SerializeField] int _nodeGroupAmountMax = 9;

    [Range(10.0f, 20.0f)]
    [SerializeField] float _randomDistance = 10.0f;

    float _checkMinDistance = 3.0f;

    public static Dictionary<string, GameObject> _resNodeObjDict = new Dictionary<string, GameObject>();
    string _nodeKeyName;

    private void Awake()
    {
        _nodeKeyName = this.gameObject.name;
    }

    private void Start()
    {
        SetResourceNodeGroup();
    }

    private void SetResourceNodeGroup()
    {
        GameObject nodeGroupObj = null;
        float randomPosX;
        float randomPosY;

        if (_resNodeGroupPrefab == null)
        {
            Debug.LogWarning("ResaNodePositionManager > _resNodeGroupPrefab is null");
            return;
        }

        // 노드 그룹 갯수 랜덤
        int nodeLength = Random.Range(_nodeGroupAmountMin, _nodeGroupAmountMax + 1);

        // 노드그룹 생
        string nodeObjKeyStr = "";
        _resNodeObjDict.Clear();

        // 생성
        for (int i = 0; i < nodeLength; ++i)
        {
            nodeGroupObj = Instantiate(_resNodeGroupPrefab.gameObject, transform);
            nodeGroupObj.SetActive(false);

            nodeObjKeyStr = string.Format("{0}{1}", _nodeKeyName, i);
            _resNodeObjDict.Add(nodeObjKeyStr, nodeGroupObj);
        }

        print("리소스 노드 그룹 수: " + _resNodeObjDict.Count);

        for (int i = 0; i < _resNodeObjDict.Count; ++i)
        {
            bool bChanged = false;

            while(!bChanged)
            {
                randomPosX = transform.position.x + Random.Range(-_randomDistance, _randomDistance);
                randomPosY = transform.position.y + Random.Range(-_randomDistance, _randomDistance);

                nodeObjKeyStr = string.Format("{0}{1}", _nodeKeyName, i);
                _resNodeObjDict[nodeObjKeyStr].transform.position = new Vector3(randomPosX, randomPosY, 0.0f);

                // 노드 중심부 거리 체크
                if((_resNodeObjDict[nodeObjKeyStr].transform.position - this.transform.position).magnitude < _checkMinDistance)
                {
                    continue;
                }
                else
                {
                    bChanged = true;
                    _resNodeObjDict[nodeObjKeyStr].SetActive(true);
                }
            }
        }
    }



}
