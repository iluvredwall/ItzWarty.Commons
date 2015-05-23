﻿using System;
using System.Collections.Generic;

namespace Dargon.Commons.Collections {
   public interface ICollectionFactory {
      IConcurrentDictionary<K, V> CreateConcurrentDictionary<K, V>();
      IConcurrentDictionary<K, V> CreateConcurrentDictionary<K, V>(IEnumerable<KeyValuePair<K, V>> collection);
      IConcurrentDictionary<K, V> CreateConcurrentDictionary<K, V>(IEqualityComparer<K> comparer);
      IConcurrentDictionary<K, V> CreateConcurrentDictionary<K, V>(IEnumerable<KeyValuePair<K, V>> collection, IEqualityComparer<K> comparer);

      IConcurrentSet<T> CreateConcurrentSet<T>();
      IConcurrentSet<T> CreateConcurrentSet<T>(IEnumerable<T> collection);
      IConcurrentSet<T> CreateConcurrentSet<T>(IEqualityComparer<T> comparer);
      IConcurrentSet<T> CreateConcurrentSet<T>(IEnumerable<T> collection, IEqualityComparer<T> comparer);

      IConcurrentBag<T> CreateConcurrentBag<T>();
      IConcurrentBag<T> CreateConcurrentBag<T>(IEnumerable<T> collection);

      IHashSet<T> CreateHashSet<T>();
      IHashSet<T> CreateHashSet<T>(IEnumerable<T> collection);
      IHashSet<T> CreateHashSet<T>(IEqualityComparer<T> comparer);
      IHashSet<T> CreateHashSet<T>(IEnumerable<T> collection, IEqualityComparer<T> comparer);

      ISortedSet<T> CreateSortedSet<T>();
      ISortedSet<T> CreateSortedSet<T>(IEnumerable<T> collection);
      ISortedSet<T> CreateSortedSet<T>(IComparer<T> comparer);
      ISortedSet<T> CreateSortedSet<T>(IEnumerable<T> collection, IComparer<T> comparer);

      IReadOnlyCollection<T> CreateImmutableCollection<T>();
      IReadOnlyCollection<T> CreateImmutableCollection<T>(params T[] args);

      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>();
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1);
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1, K k2, V v2);
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1, K k2, V v2, K k3, V v3);
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1, K k2, V v2, K k3, V v3, K k4, V v4);
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1, K k2, V v2, K k3, V v3, K k4, V v4, K k5, V v5);
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1, K k2, V v2, K k3, V v3, K k4, V v4, K k5, V v5, K k6, V v6);
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1, K k2, V v2, K k3, V v3, K k4, V v4, K k5, V v5, K k6, V v6, K k7, V v7);
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1, K k2, V v2, K k3, V v3, K k4, V v4, K k5, V v5, K k6, V v6, K k7, V v7, K k8, V v8);
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1, K k2, V v2, K k3, V v3, K k4, V v4, K k5, V v5, K k6, V v6, K k7, V v7, K k8, V v8, K k9, V v9);
      IReadOnlyDictionary<K, V> CreateImmutableDictionary<K, V>(K k1, V v1, K k2, V v2, K k3, V v3, K k4, V v4, K k5, V v5, K k6, V v6, K k7, V v7, K k8, V v8, K k9, V v9, K k10, V v10);

      IReadOnlySet<T> CreateImmutableSet<T>();
      IReadOnlySet<T> CreateImmutableSet<T>(params T[] args);

      IMultiValueDictionary<K, V> CreateMultiValueDictionary<K, V>();
      IMultiValueDictionary<K, V> CreateMultiValueDictionary<K, V>(IEqualityComparer<K> comparer);

      IMultiValueSortedDictionary<K, V> CreateMultiValueSortedDictionary<K, V>();
      IMultiValueSortedDictionary<K, V> CreateMultiValueSortedDictionary<K, V>(IComparer<K> comparer);

      IOrderedDictionary<K, V> CreateOrderedDictionary<K, V>();
      IOrderedDictionary<K, V> CreateOrderedDictionary<K, V>(IEqualityComparer<K> comparer);
         
      IOrderedMultiValueDictionary<K, V> CreateOrderedMultiValueDictionary<K, V>(ValuesSortState valuesSortState = ValuesSortState.Unsorted);

      IQueue<T> CreateQueue<T>();
      IPriorityQueue<TValue, TPriority> CreatePriorityQueue<TValue, TPriority>(int capacity)
         where TPriority : IComparable<TPriority>, IEquatable<TPriority>
         where TValue : class, IPriorityQueueNode<TPriority>;
      IConcurrentQueue<T> CreateConcurrentQueue<T>();
      IConcurrentQueue<T> CreateSingleConsumerSingleProducerConcurrentQueue<T>() where T : class;

      IUniqueIdentificationSet CreateUniqueIdentificationSet(bool filled);
      IUniqueIdentificationSet CreateUniqueIdentificationSet(uint low, uint high);

      IListDictionary<K, V> CreateListDictionary<K, V>();

      IDictionary<K, V> CreateDictionary<K, V>();
      IDictionary<K, V> CreateDictionary<K, V>(IEqualityComparer<K> comparer);

      IDictionary<K, V> CreateSortedDictionary<K, V>();
      IDictionary<K, V> CreateSortedDictionary<K, V>(IComparer<K> comparer);
   }
}
