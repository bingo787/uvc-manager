using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LionSDKDotDemo
{
    public class BlockingQueue<T>
    {
        private readonly Queue<T> queue = new Queue<T>();
        private readonly int maxSize;
        bool closing;

        /// <summary>
        /// CBlockQueue init
        /// </summary>
        /// <param >maxSize</param>
        public BlockingQueue(int maxSize)
        {
            this.maxSize = maxSize;
        }

        /// <summary>
        /// Enqueue
        /// </summary>
        /// <param >item</param>
        public void Enqueue(T item)
        {
            lock (queue)
            {
                while (queue.Count >= maxSize)
                {
                    Monitor.Wait(queue);
                }
                queue.Enqueue(item);
                if (queue.Count == 1)
                {
                    // wake up any blocked dequeue
                    Monitor.PulseAll(queue);
                }
            }
        }

        /// <summary>
        /// TryDequeue 
        /// </summary>
        /// <param >value</param>
        public bool TryDequeue(out T value)
        {
            lock (queue)
            {
                while (queue.Count == 0)
                {
                    if (closing)
                    {
                        value = default(T);
                        return false;
                    }
                    Monitor.Wait(queue);
                }
                value = queue.Dequeue();
                if (queue.Count == maxSize - 1)
                {
                    // wake up any blocked enqueue
                    Monitor.PulseAll(queue);
                }
                return true;
            }
        }

        /// <summary>
        /// Get Queue Count 
        /// </summary>
        /// <param ></param>
        public int Count()
        {
            lock (queue)
            {
                return queue.Count;
            }
        }

        /// <summary>
        /// Close Queue 
        /// </summary>
        /// <param ></param>
        public void Close()
        {
            lock (queue)
            {
                closing = true;
                Monitor.PulseAll(queue);
            }
        }

    }
}
