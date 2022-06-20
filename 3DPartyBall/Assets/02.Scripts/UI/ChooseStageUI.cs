using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStageUI : MonoBehaviour
{
    private Transform _content;     // ��� �������� ������Ʈ���� �θ�
    private Button _rightButton;    // ������ ���������� �̵��ϴ� ��ư
    private Button _leftButton;     // ���� ���������� �̵��ϴ� ��ư

    private int _contentCount;      // _content�� �ڽ� ��
    private int _currentStage = 0;      // ���� �̵��ϰ��� �ϴ� ��������

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
    /// ���������� UI �̵�
    /// ���� �̵� ���ϴ� ��� ��Ȱ��ȭ
    /// TODO : �ڿ������� �̵��� �ϰ� �ʹٸ� ��Ʈ�� ���� �� �̿�
    /// </summary>
    private void MoveToRightStage()
    {
        if(_currentStage + 1 == _contentCount)
        {
            Debug.Log("�̵� �Ұ�");
            _rightButton.gameObject.SetActive(false);
            return;
        }

        _currentStage++;
        _content.position += new Vector3(-750, 0, 0); // 750 ��ŭ ���� �������� �̵�

        if (!_leftButton.gameObject.activeSelf)
            _leftButton.gameObject.SetActive(true);

        if (_currentStage + 1 == _contentCount) _rightButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// �������� UI �̵�
    /// ���� �̵� ���ϴ� ��� ��Ȱ��ȭ
    /// TODO : �ڿ������� �̵��� �ϰ� �ʹٸ� ��Ʈ�� ���� �� �̿�
    /// </summary>
    private void MoveToLeftStage()
    {
        if (_currentStage - 1 < 0)
        {
            Debug.Log("�̵� �Ұ�");
            _leftButton.gameObject.SetActive(false);
            return;
        }

        _currentStage--;
        _content.position += new Vector3(750, 0, 0); // 750 ��ŭ ���ؼ� �������� �̵�

        if(!_rightButton.gameObject.activeSelf)
            _rightButton.gameObject.SetActive(true);

        if (_currentStage == 0) _leftButton.gameObject.SetActive(false);
    }

}
