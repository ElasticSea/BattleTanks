using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _Framework.Scripts.Extensions
{
    public static class EventTriggerExtension
    {
        public static void PointerEnter(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.PointerEnter, callback);
        }

        public static void PointerExit(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.PointerExit, callback);
        }

        public static void PointerDown(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.PointerDown, callback);
        }

        public static void PointerUp(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.PointerUp, callback);
        }

        public static void PointerClick(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.PointerClick, callback);
        }

        public static void Drag(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.Drag, callback);
        }

        public static void Drop(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.Drop, callback);
        }

        public static void Scroll(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.Scroll, callback);
        }

        public static void UpdateSelected(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.UpdateSelected, callback);
        }

        public static void Select(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.Select, callback);
        }

        public static void Deselect(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.Deselect, callback);
        }

        public static void Move(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.Move, callback);
        }

        public static void InitializePotentialDrag(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.InitializePotentialDrag, callback);
        }

        public static void BeginDrag(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.BeginDrag, callback);
        }

        public static void EndDrag(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.EndDrag, callback);
        }

        public static void Submit(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.Submit, callback);
        }

        public static void Cancel(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
        {
            eventTrigger.Trigger(EventTriggerType.Cancel, callback);
        }
    }
}