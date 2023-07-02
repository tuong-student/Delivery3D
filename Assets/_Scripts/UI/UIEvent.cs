using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImpossibleOdds;

namespace Game
{
    public static class UIEvent
    {
        public static UIAction<bool> onUIButtonRequester = new UIAction<bool>();
        public static UIAction<RoadDirection> onDirectionButtonPress = new UIAction<RoadDirection>();
        public static UIAction<SceneRoad> onUISetBtnRequest = new UIAction<SceneRoad>();


        public static void PurgeDelegatesOf(object target)
        {
            onUIButtonRequester.PurgeDelegatesOf(target);
            onDirectionButtonPress.PurgeDelegatesOf(target);
            onUISetBtnRequest.PurgeDelegatesOf(target);
        }
    }
}
