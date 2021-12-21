using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResNodePositionManager : MonoBehaviour
{
    [Header("리소스 노드그룹 프리팹 링크")]
    [SerializeField]
    private ResNodeGroupMaker _resNodeGroupPrefab;

    [Header("리소스 노드 배치 옵션")]

    [SerializeField]
    [Range(5, 7)]
    int _nodeGroupAmountMin = 5;

    [SerializeField]
    [Range(9, 11)]
    int _nodeGroupAmountMax = 9;

    [SerializeField]
    [Range(10f, 20f)]
    float _nodeRandomDistance = 10f;

    float _checkMinDistance = 3f;

    public static Dictionary<string, GameObject> _resNodeObjDic;
    string _NodeKeyName;

    private void Awake()
    {
        _resNodeObjDic = new Dictionary<string, GameObject>();

        _NodeKeyName = this.gameObject.name;
    }

    void Start()
    {
        SetResNodeGroup();
    }

    void SetResNodeGroup()
    {
        GameObject nodeGroupObj = null;
        float randomPosX, randomPosY;

        if (_resNodeGroupPrefab == null)
        {
            Debug.LogWarning("_resNodeGroupPrefab is null");
            return;
        }

        // 노드그룹 갯수 랜덤 세팅
        int nodeLength = Random.Range(_nodeGroupAmountMin, _nodeGroupAmountMax + 1);

        // 그룹을 생성
        string nodeObjKeyStr = "";
        _resNodeObjDic.Clear();

        for(int i = 0; i < nodeLength; i++)
        {
            nodeGroupObj = Instantiate(_resNodeGroupPrefab.gameObject, transform);
            nodeGroupObj.SetActive(false);

            nodeObjKeyStr = string.Format("{0}{1}", _NodeKeyName, i);
            _resNodeObjDic.Add(nodeObjKeyStr, nodeGroupObj);
        }

        print("리소스 노드 그룹 수 : " + _resNodeObjDic.Count);

        for(int i = 0; i < _resNodeObjDic.Count; i++)
        {
            bool bChanged = false;
            while(!bChanged)
            {
                randomPosX = transform.position.x + Random.Range(-_nodeRandomDistance, _nodeRandomDistance);
                randomPosY = transform.position.y + Random.Range(-_nodeRandomDistance, _nodeRandomDistance);

                nodeObjKeyStr = string.Format("{0}{1}", _NodeKeyName, i);
                _resNodeObjDic[nodeObjKeyStr].transform.position = new Vector3(randomPosX, randomPosY, 0f);

                // 노드그룹 중심부와 너무 가까우면 리포지셔닝
                if((_resNodeObjDic[nodeObjKeyStr].transform.position - this.transform.position).magnitude < _checkMinDistance)
                {
                    continue;
                }
                else
                {
                    bChanged = true;
                    _resNodeObjDic[nodeObjKeyStr].SetActive(true);
                }
            }
        }
    }
}
