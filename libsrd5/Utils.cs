using System;

namespace srd5 {
    public enum RemoveResult {
        NOT_FOUND,
        REMOVED_AND_GONE,
        REMOVED_BUT_REMAINS
    }
    public class Utils {
        public static void Push<T>(ref T[] array, params T[] elements) {
            int i = array.Length;
            Array.Resize(ref array, array.Length + elements.Length);
            foreach (T element in elements)
                array[i++] = element;
        }

        /*
        Adds a new element to an array.
        Returns whether the element is new in the array or already contained.
        */
        public static bool PushUnique<T>(ref T[] array, T element) {
            bool isNew = Array.IndexOf(array, element) == -1;
            Push<T>(ref array, element);
            return isNew;
        }

        /*
        Removes a single element from an array if the array contains it.
        Return value indicates, whether nothing was removed, or something was removed
        and whether the element is still in the array or not
        */
        public static RemoveResult RemoveSingle<T>(ref T[] array, T element) {
            int index = Array.IndexOf(array, element);
            if (index == -1) return RemoveResult.NOT_FOUND;
            for (int i = index; i < array.Length - 1; i++) {
                array[i] = array[i + 1];
            }
            Array.Resize(ref array, array.Length - 1);
            if (Array.IndexOf(array, element) == -1) {
                return RemoveResult.REMOVED_AND_GONE;
            } else {
                return RemoveResult.REMOVED_BUT_REMAINS;
            }
        }

        public static T Max<T>(T[] array) where T : IComparable {
            if (array == null || array.Length == 0) return default(T);
            T max = array[0];
            for (int i = 0; i < array.Length; i++) {
                if (array[i].CompareTo(max) > 0)
                    max = array[i];
            }
            return max;
        }

        // 
        public static T[] Expand<T>(T element, int n) {
            T[] elements = new T[n];
            for (int i = 0; i < n; i++)
                elements[i] = element;
            return elements;
        }
    }

    public abstract class GuidClass {
        public Guid Guid { get; private set; } = Guid.NewGuid();

        public override bool Equals(object obj) {
            if (obj is GuidClass other) {
                return this.Guid.Equals(other.Guid);
            } else {
                return false;
            }
        }

        public override int GetHashCode() {
            return Guid.GetHashCode();
        }
    }
}