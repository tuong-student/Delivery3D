using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImpossibleOdds;

namespace Game
{
    public static class UIEvent
    {
        public static UIAction<bool> onDirectionButtonOnOffRequester = new UIAction<bool>();
        public static UIAction<RoadDirection> onDirectionButtonPress = new UIAction<RoadDirection>();
        public static UIAction<SceneRoad> onSetDirectionBtnRequest = new UIAction<SceneRoad>();

        public static UIAction<string> onEventPlaceSetRequest = new UIAction<string>();
        public static UIAction<bool> onEventPlaceButtonPress = new UIAction<bool>();
        public static UIAction<bool> onEventPlaceButtonOnOffRequest = new UIAction<bool>();


        public static void PurgeDelegatesOf(object target)
        {
            onDirectionButtonOnOffRequester.PurgeDelegatesOf(target);
            onDirectionButtonPress.PurgeDelegatesOf(target);
            onSetDirectionBtnRequest.PurgeDelegatesOf(target);

            onEventPlaceSetRequest.PurgeDelegatesOf(target);
            onEventPlaceButtonPress.PurgeDelegatesOf(target);
            onEventPlaceButtonOnOffRequest.PurgeDelegatesOf(target);
        }
    }
}
