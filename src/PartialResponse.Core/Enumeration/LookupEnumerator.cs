using System;
using System.Collections;
using System.Collections.Generic;

namespace PartialResponse.Core.Enumeration
{
    public class LookupEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;

        private EnumerableItemContainer<T> previous = null;
        private EnumerableItemContainer<T> current = null;
        private EnumerableItemContainer<T> next = null;
        public T Current => current.Value;
        public bool Finished => current.Empty;
        public int Index { get; private set; } = -1;
        object IEnumerator.Current => current.Value;

        public LookupEnumerator(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
        }

        public T GetPrevious()
        {
            if (previous == null)
            {
                throw new InvalidOperationException();
            }
            return previous.Value;
        }

        public T GetNext()
        {
            if (current?.Empty != false)
            {
                throw new InvalidOperationException();
            }

            if (next == null)
            {
                next = ReadNextElement();
            }

            return next.Value;
        }

        public void Dispose()
        {
            enumerator.Dispose();
        }

        public bool MoveNext()
        {
            previous = current;

            if (next != null)
            {
                current = next;
                next = null;
            }
            else
            {
                current = ReadNextElement();
            }

            Index++;
            return !current.Empty;
        }

        private EnumerableItemContainer<T> ReadNextElement()
        {
            bool exists = enumerator.MoveNext();
            return exists ? EnumerableItemContainer.Create(enumerator.Current) : EnumerableItemContainer.Empty<T>();
        }

        public void Reset()
        {
            Index = -1;
            enumerator.Reset();
            previous = null;
            current = null;
            next = null;
        }
    }
}
