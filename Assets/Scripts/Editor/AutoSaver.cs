using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CXUtils
{
    [InitializeOnLoad]
    public class AutoSaver
    {
        public const string MENU_NAME  = "Tools/玩，自动保存";
        public const string LOG_PREFIX = "[自动保存]";

        static bool _enabled;

        static AutoSaver() => EditorApplication.delayCall += OnDelayCall;

        static void OnDelayCall()
        {
            _enabled = EditorPrefs.GetBool( MENU_NAME, false );
            Menu.SetChecked( MENU_NAME, _enabled );
            SetMode();
        }

        [MenuItem( MENU_NAME )]
        static void ToggleMode()
        {
            _enabled = !_enabled;
            Menu.SetChecked( MENU_NAME, _enabled );
            EditorPrefs.SetBool( MENU_NAME, _enabled );
            SetMode();

            Log( _enabled ? "开启自动保存!" : "关闭自动保存!" );
        }

        static void SetMode()
        {
            if ( _enabled )
                EditorApplication.playModeStateChanged += AutoSaveOnRun;
            else
                EditorApplication.playModeStateChanged -= AutoSaveOnRun;
        }

        static void AutoSaveOnRun( PlayModeStateChange state )
        {
            if ( !EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying ) return;

            var scene = SceneManager.GetActiveScene();

            if ( !scene.isDirty )
            {
                Log( "当前游玩场景未改变,不会保存." );
                return;
            }

            Log( "正在游玩前自动保存场景..." );

            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();

            Log( "保存完成！" );
        }

        static void Log( string message ) => Debug.Log( LOG_PREFIX + message );
    }
}
