using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStageUI : MonoBehaviour
{
    private Transform _content;     // 모든 스테이지 오브젝트들의 부모
    private Button _rightButton;    // 오른쪽 스테이지로 이동하는 버튼
    private Button _leftButton;     // 왼쪽 스테이지로 이동하는 버튼

    private int _contentCount;      // _content의 자식 수
    private int _currentStage = 0;      // 현재 이동하고자 하는 스테이지

    private void Start()
    {
        _content = transform.GetComponentInChildren<GridLayoutGroup>().transform;
        _rightButton = transform.Find("RightButton").GetComponent<Button>();
        _leftButton = transform.Find("LeftButton").GetComponent<Button>();

        _contentCount = _content.childCount;

        _rightButton.onClick.AddListener(() =>
        {
            MoveToRightStage();
        });

        _leftButton.onClick.AddListener(() =>
        {
            MoveToLeftStage();
        });

        _leftButton.gameObject.SetActive(false);

        if (_contentCount == 0) _rightButton.gameObject.SetActive(false);

    }

    /// <summary>
    /// 오른쪽으로 UI 이동
    /// 만약 이동 못하는 경우 비활성화
    /// TODO : 자연스러운 이동이 하고 싶다면 다트윈 같은 것 이용
    /// </summary>
    private void MoveToRightStage()
    {
        if(_currentStage + 1 == _contentCount)
        {
            Debug.Log("이동 불가");
            _rightButton.gameObject.SetActive(false);
            return;
        }

        _currentStage++;
        _content.position += new Vector3(-750, 0, 0); // 750 만큼 빼서 다음으로 이동

        if (!_leftButton.gameObject.activeSelf)
            _leftButton.gameObject.SetActive(true);

        if (_currentStage + 1 == _contentCount) _rightButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 왼쪽으로 UI 이동
    /// 만약 이동 못하는 경우 비활성화
    /// TODO : 자연스러운 이동이 하고 싶다면 다트윈 같은 것 이용
    /// </summary>
    private void MoveToLeftStage()
    {
        if (_currentStage - 1 < 0)
        {
            Debug.Log("이동 불가");
            _leftButton.gameObject.SetActive(false);
            return;
        }

        _currentStage--;
        _content.position += new Vector3(750, 0, 0); // 750 만큼 더해서 다음으로 이동

        if(!_rightButton.gameObject.activeSelf)
            _rightButton.gameObject.SetActive(true);

        if (_currentStage == 0) _leftButton.gameObject.SetActive(false);
    }

}
