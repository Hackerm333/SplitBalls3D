using System.Collections.Generic;
using UnityEngine;

namespace MirkoZambito
{
    public enum IngameState
    {
        Ingame_Playing = 1,
        Ingame_Revive = 2,
        Ingame_GameOver = 3,
        Ingame_CompletedLevel = 4,
    }

    public enum PlayerState
    {
        Player_Prepare = 1,
        Player_Living = 2,
        Player_Died = 3,
        Player_CompletedLevel = 4,
    }


    public enum StageType
    {
        GREEN = 1,
        BLUE = 2,
        GRAY = 3,
        RED = 4,
        WHITE = 5,
    }

    public enum BallSize
    {
        BIG = 1,
        MEDIUM = 2,
        NORMAL = 3,
        SMALL = 4,
    }


    public enum RotateDirection
    {
        LEFT = 1,
        RIGHT = 2,
    }

    public enum TextObjectType
    {
        DUPLICATOR = 1,
        LOCKER = 2,
    }



    [System.Serializable]
    public class StageSelectionData
    {
        [SerializeField] private StageType stageType = StageType.GREEN;
        public StageType StageType { get { return stageType; } }
        [SerializeField] private Mesh stageMesh = null;
        public Mesh StageMesh { get { return stageMesh; } }
        [SerializeField] private Material stageMaterial = null;
        public Material StageMaterial { get { return stageMaterial; } }
    }




    [System.Serializable]
    public class LevelConfig
    {
        [Header("Level Number Configuration")]
        [SerializeField] private int levelNumber = 1;
        public int LevelNumber { get { return levelNumber; } }


        [Header("Background Colors Configuration")]
        [SerializeField] private Color backgroundTopColor = Color.white;
        public Color BackgroundTopColor { get { return backgroundTopColor; } }
        [SerializeField] private Color backgroundBottomColor = Color.white;
        public Color BackgroundBottomColor { get { return backgroundBottomColor; } }


        [Header("Background Music Configuration")]
        [SerializeField] private SoundClip musicClip = null;
        public SoundClip MusicClip { get { return musicClip; } }


        [Header("Other Colors Configuration")]
        [SerializeField] private Color ballColor = Color.blue;
        public Color BallColor { get { return ballColor; } }
        [SerializeField] private Color blockerColor = Color.blue;
        public Color BlockerColor { get { return blockerColor; } }
        [SerializeField] private Color duplicatorColor = Color.blue;
        public Color DuplicatorColor { get { return duplicatorColor; } }
        [SerializeField] private Color lockerColor = Color.blue;
        public Color LockerColor { get { return lockerColor; } }
        [SerializeField] private Color ballContainerColor = Color.blue;
        public Color BallContainerColor { get { return ballContainerColor; } }


        [Header("Stages Prefab Configuration")]
        [SerializeField] private List<StageController> listStageControlPrefab = new List<StageController>();
        public List<StageController> ListStageControlPrefab { get { return listStageControlPrefab; } }
    }
}
