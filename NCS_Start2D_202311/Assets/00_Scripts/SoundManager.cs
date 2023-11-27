using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip[] allSoundSource_BGM; //��� �Ҹ� ���ҽ� ������ �ְ�
    public AudioClip[] allSoundSource_Effect; //��� �Ҹ� ���ҽ� ������ �ְ�

    public AudioSource BGM;
    public AudioSource[] Effects;
    bool IsMute = false;
    //enum���� ���� �����ϴ���, Ȥ�� ���� ���صΰ�...
    //0���� ���� �Ҹ�
    //1���� ���� �Ҹ�//.....

    public void SetSoundBGM(int songNum, Vector3 vec) //
    {
        if (IsMute)
        {
            return;
        }
        BGM.clip = allSoundSource_BGM[songNum];
        BGM.transform.position = vec;
        BGM.gameObject.SetActive(true);
        BGM.Play();
        //Effects[0].Play(); //��������� �����ų� �÷��̰� �߾ȵǰų�... �����Ÿ��ų� ���� ������ ������..                
    }

    public void StopSound()
    {
        BGM.Stop();
    }

    public void Mute(bool onoff)
    {
        IsMute = onoff;
        if (IsMute)
        {
            BGM.Stop();
            Effects[0].Stop();
            Effects[1].Stop();
        }        
    }
    public void Pause(bool ison)
    {
        if (ison)
        {
            BGM.Pause();
        }
        else
        {
            BGM.UnPause();
        }
    }

    public void SetSoundEffect(int songNum, Vector3 vec) //
    {
        if (IsMute)
        {
            return;
        }

        if (Effects[0].isPlaying)
        {
            Effects[1].transform.position = vec;
            Effects[1].gameObject.SetActive(true);
            //Effects[0].Play(); //��������� �����ų� �÷��̰� �߾ȵǰų�... �����Ÿ��ų� ���� ������ ������..
            //����Ʈ ����� ����
            Effects[1].PlayOneShot(allSoundSource_Effect[songNum]);
        }
        else
        {
            Effects[0].transform.position = vec;
            Effects[0].gameObject.SetActive(true);
            //Effects[0].Play(); //��������� �����ų� �÷��̰� �߾ȵǰų�... �����Ÿ��ų� ���� ������ ������..
            //����Ʈ ����� ����
            Effects[0].PlayOneShot(allSoundSource_Effect[songNum]);
        }
        
    }
}
