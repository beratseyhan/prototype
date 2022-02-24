using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantContainer : MonoBehaviour
{
    public enum GameStates { Begin, InGame, LevelEnd }
    public enum NPCTarget {  Food,Nearest }
    public class LayerNames
    {
        public const string PLAYER_LAYER = "Player";
        public const string NPCC_LAYER = "Npc";
        public const string INTERACTION_LAYER = "PlayerInteraction";
    }
    public class AnimationsName
    {
        public const string RUN_TRIGGER = "RunTrigger";
       
        public const string IDLE_TRIGGER = "IdleTrigger";
      

    }

}
