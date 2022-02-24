using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager
{
    public static event Action OnSceneStart;
    public static event Action OnGameStart;
    public static event Action OnWin;
    public static event Action OnLose;
    public static event Action OnSceneEnd;





    //Oyun sahnesi acildiginda cagirilacak event.
    public static void SceneStartEvent()
    {
        if (OnSceneStart != null)
        {
            OnSceneStart();
        }
    }

    //Oyuncu oynamaya basladiginda cagirilacak event.
    public static void GameStartEvent()
    {
        if (OnGameStart != null)
        {
            OnGameStart();
        }
    }

    //Oyuncu win condition i sagladiginda cagirilacak event. 
    public static void WinEvent()
    {
        if (OnWin != null)
        {
            OnWin();

        }


    }

    //Oyuncu lose condition i sagladiginda cagirilacak event.
    public static void LoseEvent()
    {
        if (OnLose != null)
        {
            OnLose();
        }
    }

    //Oyun sahnelerinin sonunda cagirilacak event.
    public static void SceneEndEvent()
    {
        if (OnSceneEnd != null)
        {
            OnSceneEnd();
        }
    }

   
}
