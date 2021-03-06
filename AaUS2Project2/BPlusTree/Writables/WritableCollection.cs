﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BPlusTree.Writables
{
    public class WritableCollection<T> : IWritable, IEnumerable<T> where T : IWritable, new()
    {
        public int ByteSize { get; }
        public int Count { get; set; }
        public int Length => _value.Length;
        private readonly T[] _value;

        public WritableCollection(int length)
        {
            ByteSize = sizeof(int) + length * new T().ByteSize;
            _value = new T[length];
        }

        public void Add(T item) => _value[Count++] = item;

        public T Get(int i) => _value[i];

        public IEnumerator<T> GetEnumerator() => _value.Take(Count).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public byte[] GetBytes()
        {
            var bytes = new byte[ByteSize];
            var dstIdx = sizeof(int);
            Array.Copy(BitConverter.GetBytes(Count), 0, bytes, 0, dstIdx);
            for (var i = 0; i < Count; i++)
            {
                var item = _value[i];
                Array.Copy(item.GetBytes(), 0, bytes, dstIdx, item.ByteSize);
                dstIdx += item.ByteSize;
            }
            return bytes;
        }

        public void FromBytes(byte[] bytes, int index = 0)
        {
            Count = BitConverter.ToInt32(bytes, index);
            var srcIdx = index + sizeof(int);
            for (var i = 0; i < Count; i++)
            {
                var item = _value[i] != null ? _value[i] : new T();
                item.FromBytes(bytes, srcIdx);
                _value[i] = item;
                srcIdx += item.ByteSize;
            }
        }
    }
}
