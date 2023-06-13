using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    static T instance;
    static bool isShutDown = false;

    public static T Inst {
        get {
            if (isShutDown)
            {
                Debug.LogWarning(typeof(T).Name + " Deleted");
                return null;
            }
            if (instance == null) {

                T sigleton = FindObjectOfType<T>();
                if (sigleton == null)
                {
                    GameObject gameObj = new GameObject();
                    gameObj.name = typeof(T).Name + " Singleton";
                    sigleton = gameObj.AddComponent<T>();
                }
                instance = sigleton;
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    void Awake() {
        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        } else {
            if (instance != this) {
                Destroy(this.gameObject);
            }
        }
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Init();
    }

    void OnApplicationQuit() {
        isShutDown = true;
    }

    void OnDestroy() {
        isShutDown = true;
    }

    protected virtual void Init() {
    }
}
