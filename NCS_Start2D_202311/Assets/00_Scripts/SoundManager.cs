using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip[] allSoundSource_BGM; //모든 소리 리소스 가지고 있고
    public AudioClip[] allSoundSource_Effect; //모든 소리 리소스 가지고 있고

    public AudioSource BGM;
    public AudioSource[] Effects;
    bool IsMute = false;
    //enum으로 먼저 정의하던지, 혹은 숫자 정해두고...
    //0번은 무슨 소리
    //1번은 무슨 소리//.....

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
        //Effects[0].Play(); //어느한쪽이 씹히거나 플레이가 잘안되거나... 지직거리거나 뭔가 문제가 좀있음..                
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
            //Effects[0].Play(); //어느한쪽이 씹히거나 플레이가 잘안되거나... 지직거리거나 뭔가 문제가 좀있음..
            //이펙트 사운드는 보통
            Effects[1].PlayOneShot(allSoundSource_Effect[songNum]);
        }
        else
        {
            Effects[0].transform.position = vec;
            Effects[0].gameObject.SetActive(true);
            //Effects[0].Play(); //어느한쪽이 씹히거나 플레이가 잘안되거나... 지직거리거나 뭔가 문제가 좀있음..
            //이펙트 사운드는 보통
            Effects[0].PlayOneShot(allSoundSource_Effect[songNum]);
        }
        
    }
}
