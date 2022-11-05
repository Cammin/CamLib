using System.Collections.Generic;
using UnityEngine;

//https://answers.unity.com/questions/620318/sprite-layer-order-determined-by-y-value.html

 namespace CamLib
 {
     public class SortableManager : MonoBehaviour
     {
         [SerializeField] private int onlyUpdateCountPerFrame = 1;

         private static HashSet<ISortable> _sortables = null;

         private void Start()
         {
             FormulateManagedSortables();
             UpdateSortOrder();
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

             Debug.Log($"{nameof(SortableManager)}: Actively updating order of {_sortables.Count} sortables and staggering {onlyUpdateCountPerFrame} per frame\n{includedObjects}");
         }

         private void LateUpdate()
         {
             UpdateSortOrder();
         }

         private static void UpdateSortOrder()
         {
             foreach (ISortable sortable in _sortables)
             {
                 sortable.UpdateOrder();
             }
         }
         
         
         public static void Add(ISortable sortable)
         {
             _sortables ??= new HashSet<ISortable>();
             _sortables.Add(sortable);
         }
     }
 }
