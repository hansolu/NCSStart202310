using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Ŵ����� ���������� �Ѿ������
//�������� �����ϰ�
//���ο������ �����ؾ��Ұ��� �������ٵ�

public abstract class BaseScene : MonoBehaviour 
{
    public AllEnum.SceneKind SceneType;
    public abstract AllEnum.SceneKind GetSceneType();
    
    public abstract void Init();//������, �ʱ�ȭ �Լ���

    public abstract void ClearScene(); //�����ϴ� �Լ��� ������ �ǰ���..?
}
