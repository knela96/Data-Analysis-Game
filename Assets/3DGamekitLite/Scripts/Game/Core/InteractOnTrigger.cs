using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class InteractOnTrigger : MonoBehaviour
    {
        public LayerMask layers;
        public UnityEvent OnEnter, OnExit;
        new Collider collider;
        public InventoryController.InventoryChecker[] inventoryChecks;

        public float timerSwitch1 = 0;
        public float timerSwitch2 = 0;
        public float timerSwitch3 = 0;
        public float finalTimer = 0;
        public float timerKey = 0;

        public delegate void SwitchTimer(int id, float time);
        public static SwitchTimer SwitchTimerEvent;

        public delegate void LevelComplete(float time);
        public static LevelComplete LevelCompleteEvent;

        public delegate void KeyTimer(float time);
        public static KeyTimer KeyTimerEvent;

        void Reset()
        {
            layers = LayerMask.NameToLayer("Everything");
            collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                ExecuteOnEnter(other);

                if (gameObject.name == "DoorSwitch1")
                {
                    timerSwitch1 = Time.time - EventHandler.eventhandler.GetStartTime();
                    SwitchTimerEvent?.Invoke(1, timerSwitch1);
                }

                if (gameObject.name == "Switch")
                {
                    timerSwitch2 = Time.time - EventHandler.eventhandler.GetStartTime();
                    SwitchTimerEvent?.Invoke(2, timerSwitch2);
                }

                if (gameObject.name == "Switch (1)")
                {
                    timerSwitch3 = Time.time - EventHandler.eventhandler.GetStartTime();
                    SwitchTimerEvent?.Invoke(3, timerSwitch2);
                }

                if (gameObject.name == "InfoZone_InsideBox")//Key
                {
                    timerKey = Time.time - EventHandler.eventhandler.GetStartTime();
                    KeyTimerEvent?.Invoke(timerKey);
                }

                // Porta 1 = "PressurePad"
                // Switch 1 = "DoorSwitch1"
                // Switch 2 = "Switch"
                // Switch 3 = "Switch (1)"

                if (gameObject.name == "InfoZone_End")//End Level
                {
                    finalTimer = Time.time - EventHandler.eventhandler.GetStartTime();
                    LevelCompleteEvent?.Invoke(finalTimer);
                }
            }
        }

        protected virtual void ExecuteOnEnter(Collider other)
        {
            OnEnter.Invoke();
            for (var i = 0; i < inventoryChecks.Length; i++)
            {
                inventoryChecks[i].CheckInventory(other.GetComponentInChildren<InventoryController>());
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                ExecuteOnExit(other);
            }
        }

        protected virtual void ExecuteOnExit(Collider other)
        {
            OnExit.Invoke();
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
        }

        void OnDrawGizmosSelected()
        {
            //need to inspect events and draw arrows to relevant gameObjects.
        }

    } 
}
