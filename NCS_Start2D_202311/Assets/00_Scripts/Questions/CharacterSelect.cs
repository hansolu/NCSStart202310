using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public InputField inputfield;
    public Toggle[] characterSelect;

    void Start()
    {
        //�������ڸ��� ĳ���� ����â�� ĳ���� ���ñ׸��� UI�Ŵ������� �����ͼ� �����ص�.
        characterSelect[0].transform.GetChild(0).GetComponent<Image>().sprite = UIManager_Test.Instance.characterselectImg[0];
        characterSelect[1].transform.GetChild(0).GetComponent<Image>().sprite = UIManager_Test.Instance.characterselectImg[1];
    }
    public void CreateCharacter() //����Ŭ����
    {
        //string name
        GameManager_Test.Instance.CreateMyCharacter(new CharacterInfo(inputfield.text,
            100, 10, characterSelect[0].isOn? CharacterType.Near : CharacterType.Far));
    }
}
