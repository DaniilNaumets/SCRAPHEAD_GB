using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if(videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }    
    }

    void LateUpdate()
    {
        if (Input.anyKeyDown)
        {
            LoadNextScene(videoPlayer);
        }
        //if (!videoPlayer.isPlaying)
        //{
        //    LoadNextScene(videoPlayer);
        //}
    }

    void LoadNextScene(VideoPlayer vp)
    {
        OnVideoEnd(vp);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
