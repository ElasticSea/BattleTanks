using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _Framework.Scripts.Extensions
{
    public static class MonoBehaviorExtensions
    {
        public static Task<AudioClip> LoadClipAtPath(this MonoBehaviour mono, string path)
        {
            var tcs = new TaskCompletionSource<AudioClip>();
            mono.StartCoroutine(LoadClipAtPath(path, 
                clip => tcs.SetResult(clip),
                exception => tcs.SetException(exception)));
            return tcs.Task;
        }

        private static IEnumerator LoadClipAtPath(this string path, Action<AudioClip> callback, Action<Exception> exception)
        {
            UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = null;
            UnityWebRequest www = null;
            bool exit = false;
            try
            {
                path = Path.GetFullPath(path);
                www = UnityWebRequestMultimedia.GetAudioClip("file://"+path, AudioType.UNKNOWN);
                unityWebRequestAsyncOperation = www.SendWebRequest();
            }
            catch (Exception e)
            {
                exception(e);
                exit = true;
            }    

            if (exit)
            {
                yield return null;
            }
            
            yield return unityWebRequestAsyncOperation;

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                callback(null);
            }
            else
            {
                try
                {
                    AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                    callback(myClip);
                }
                catch (Exception e)
                {
                    exception(e);
                }
            }
        }
    }
}