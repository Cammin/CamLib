using System.Collections.Generic;
using System.Linq;

namespace CamLib
{
    /// <summary>
    /// A useful class that aims to "guarantee randomness" where all elements are guaranteed to be selected before any are repeated, including when the list is restocked to prevent the same element from being selected twice in a row.
    /// </summary>
    public class RandomStock<T>
    {
        public List<T> Items = new();
        public List<int> Remaining = new();

        public int LastElementCollected;
        
        public RandomStock() { }

        public RandomStock(IEnumerable<T> items)
        {
            Items = items.ToList();
            Restock();
        }
        
        public void Restock()
        {
            Remaining.Clear();
            for (int i = 0; i < Items.Count; i++)
            {
                Remaining.Add(i);
            }
            
            Remaining.Shuffle();
            
            if (Remaining.Count > 1 && Remaining.Last() == LastElementCollected)
            {
                int random = UnityEngine.Random.Range(0, Remaining.Count - 1);
                (Remaining[random], Remaining[^1]) = (Remaining[^1], Remaining[random]);
            }
        }

        public T GetRandom()
        {
            if (Remaining.IsNullOrEmpty())
            {
                Restock();
            }

            int i = Remaining.Pop();
            LastElementCollected = i;
            return Items[i];
        }

        public void RemoveRemaining(T remove)
        {
            int i = Items.IndexOf(remove);
            if (i >= 0)
            {
                Remaining.Remove(i);
            }
        }
    }
}