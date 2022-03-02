using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayer : MonoBehaviour
{
    //ȸ���� ������Ʈ�̴�. -> �ַ� ������ ���͸� �����ϸ� �ȴ�.
    public Transform center;

    //�ٶ� ��ü�̴�. - > �ַ� �÷��̾ �����ϸ� �ȴ�.
    private Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        //���� �y���� ���� ������ ����
        var rot = target.position - center.position;

        //x,y�� ���� �����Ͽ� Z���� ������ ������. -> ~�� ������ ����
        var angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        //�ش� Ÿ�� �������� ȸ���Ѵ�.
        center.rotation = Quaternion.Euler(0, 0, angle);
    }
}
