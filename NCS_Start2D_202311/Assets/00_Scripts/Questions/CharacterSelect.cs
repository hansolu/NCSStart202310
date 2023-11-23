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
        //시작하자마자 캐릭터 선택창의 캐릭터 선택그림을 UI매니저에서 가져와서 세팅해둠.
        characterSelect[0].transform.GetChild(0).GetComponent<Image>().sprite = UIManager_Test.Instance.characterselectImg[0];
        characterSelect[1].transform.GetChild(0).GetComponent<Image>().sprite = UIManager_Test.Instance.characterselectImg[1];
    }
    public void CreateCharacter() //생성클릭시
    {
        //string name
        GameManager_Test.Instance.CreateMyCharacter(new CharacterInfo(inputfield.text,
            100, 10, characterSelect[0].isOn? CharacterType.Near : CharacterType.Far));
    }
}
