using System.Collections.Generic;
using UnityEngine;

//https://answers.unity.com/questions/620318/sprite-layer-order-determined-by-y-value.html

 namespace CamLib.RendererSorting
 {
     public class SortableManager : MonoBehaviour
     {
         [SerializeField] private float _updateFrequency = 0.1f;
 
         private readonly GameTimer _updateTimer = new GameTimer();

         private static List<ISortable> _sortables = null;

         private void Start()
         {
             FormulateManagedSortables();
             UpdateSortOrder();
             SetTimer();
         }
         
         private void OnDisable()
         {
             _sortables = null;
         }

         private void FormulateManagedSortables()
         {
             string includedObjects = "";

             foreach (ISortable s in _sortables)
             {
                includedObjects += s.ToString();
                includedObjects += "\n";
             }

             Debug.Log($"{nameof(SortableManager)}: Actively updating order of {_sortables.Count} sortables in interval {_updateFrequency}\n{includedObjects}");
         }

         private void LateUpdate ()
         {
             if (_updateTimer.IsRunning) return;
             SetTimer();
         
             UpdateSortOrder();
         }

         private static void UpdateSortOrder()
         {
             foreach (ISortable sortable in _sortables)
             {
                 sortable.UpdateOrder();
             }
         }
     
         private void SetTimer() => _updateTimer.Set(_updateFrequency);
         
         public static void Add(ISortable sortable)
         {
             if (_sortables == null)
             {
                 _sortables = new List<ISortable>();
             }
             
             if (_sortables.Contains(sortable)) return;
         
             _sortables.Add(sortable);
         }
     }
 }
