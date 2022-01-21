using UnityEngine;
using System.Collections.Generic;

public interface IItemDelivearable {
    bool ReceiveItems(List<Item> items, ItemStorage storage);
}