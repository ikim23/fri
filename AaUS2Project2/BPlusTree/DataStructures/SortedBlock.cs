﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BPlusTree.Writables;

namespace BPlusTree.DataStructures
{
    public class SortedBlock<TK, TV> : IEnumerable<TV>, IWritable where TK : IComparable<TK>, IWritable, new() where TV : IWritable, new()
    {
        public int ByteSize => _items.ByteSize;
        public int MaxSize => _items.MaxSize;
        public int Count => _items.Count;
        private SortedIndex<SortedBlockItem<TK, TV>> _items;

        public SortedBlock(int size)
        {
            _items = new SortedIndex<SortedBlockItem<TK, TV>>(size);
        }

        public SortedBlock(int size, IEnumerable<Tuple<TK, TV>> items) : this(size)
        {
            foreach (var tuple in items) Insert(tuple.Item1, tuple.Item2);
        }

        public bool IsFull() => _items.IsFull();

        public bool Contains(TK key)
        {
            var item = new SortedBlockItem<TK, TV> { Key = key };
            return _items.Contains(item);
        }

        public TV Find(TK key)
        {
            var itemToFind = new SortedBlockItem<TK, TV> { Key = key };
            var foundItem = _items.Find(itemToFind);
            return foundItem.Value;
        }

        public SortedBlock<TK, TV> Split(TK key, TV value, out TK middle)
        {
            var item = new SortedBlockItem<TK, TV>(key, value);
            var rightSplit = _items.Split(item, out var middleTuple);
            var rightBlock = new SortedBlock<TK, TV>(MaxSize);
            rightBlock._items = rightSplit;
            rightBlock.Insert(middleTuple.Key, middleTuple.Value);
            middle = middleTuple.Key;
            return rightBlock;
        }

        public void Insert(TK key, TV value)
        {
            var item = new SortedBlockItem<TK, TV>(key, value);
            _items.Insert(item);
        }

        public TV Remove(TK key)
        {
            var itemToRemove = new SortedBlockItem<TK, TV> { Key = key };
            var removedItem = _items.Remove(itemToRemove);
            return removedItem.Value;
        }

        public TK ShiftMaxFromLeft(SortedBlock<TK, TV> left) => _items.ShiftMaxFromLeft(left._items).Key;

        public TK ShiftMinFromRight(SortedBlock<TK, TV> right) => _items.ShiftMinFromRight(right._items).Key;

        public void Merge(SortedBlock<TK, TV> right)
        {
            _items.Merge(right._items);
        }

        public TK MinKey() => _items.Min.Key;

        public int FindInsertionIndex(TK key)
        {
            var item = new SortedBlockItem<TK, TV> { Key = key };
            return _items.FindInsertionIndex(item);
        }

        public byte[] GetBytes() => _items.GetBytes();

        public void FromBytes(byte[] bytes, int index = 0) => _items.FromBytes(bytes, index);

        public IEnumerator<Tuple<TK, TV>> GetKeyValueEnumerator() => _items.Select(i => Tuple.Create(i.Key, i.Value)).GetEnumerator();

        public IEnumerator<TV> GetEnumerator() => _items.Select(i => i.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => string.Join(",", this);

        public void Update(TK key, TV value)
        {
            var item = new SortedBlockItem<TK, TV>(key, value);
            var index = _items.FindInsertionIndex(item);
            _items.Update(index, item);
        }
    }

    internal class SortedBlockItem<TK, TV> : IComparable<SortedBlockItem<TK, TV>>, IWritable where TK : IComparable<TK>, IWritable, new() where TV : IWritable, new()
    {
        public int ByteSize => ByteUtils.ByteSize(Key, Value);
        public TK Key { get; internal set; }
        public TV Value { get; internal set; }

        public SortedBlockItem() : this(new TK(), new TV())
        {
        }

        public SortedBlockItem(TK key, TV value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(SortedBlockItem<TK, TV> other) => Key.CompareTo(other.Key);

        public byte[] GetBytes() => ByteUtils.Join(Key, Value);

        public void FromBytes(byte[] bytes, int index = 0) => ByteUtils.FromBytes(bytes, index, Key, Value);

        public override string ToString() => Key.ToString();
    }
}
