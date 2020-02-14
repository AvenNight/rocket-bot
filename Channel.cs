using System.Collections.Generic;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        private List<T> list;

        public Channel()
        {
            list = new List<T>();
        }
        /// <summary>
        /// Возвращает элемент по индексу или null, если такого элемента нет.
        /// При присвоении удаляет все элементы после.
        /// Если индекс в точности равен размеру коллекции, работает как Append.
        /// </summary>
        public T this[int index]
        {
            get
            {
                lock (list)
                    return index < 0 || index >= list.Count ? null : list[index];
            }
            set
            {
                lock (list)
                    if (index == Count)
                        list.Add(value);
                    else if (index >= 0 && index < list.Count - 1)
                    {
                        list.RemoveRange(index + 1, Count - index - 1);
                        list[index] = value;
                    }
            }
        }

        /// <summary>
        /// Возвращает последний элемент или null, если такого элемента нет
        /// </summary>
        public T LastItem()
        {
            lock (list)
                return Count > 0 ? list[Count - 1] : null;
        }

        /// <summary>
        /// Добавляет item в конец только если lastItem является последним элементом
        /// </summary>
        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (list)
                if (LastItem() == knownLastItem)
                    list.Add(item);
        }

        /// <summary>
        /// Возвращает количество элементов в коллекции
        /// </summary>
        public int Count => list.Count;
    }
}