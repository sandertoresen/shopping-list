import React from 'react';
import ShoppingItem from './ShoppingItem';
import AddShoppingItemForm from '../forms/AddShoppingItem.form';

function ShoppingList({ shoppingList, setList, apiService }) {
    async function UpdateItem(id, count) {
        setList(await apiService.updateItem(id, count));
    }

    let displayList = []

    async function AddItem(itemName, count) {
        //add item
        setList(await apiService.addItem(itemName, count));
    }

    async function RemoveItem(id) {
        //remove item
        setList(await apiService.deleteItem(id));
    }

    for (let i = 0; i < shoppingList.length; i++) {
        //each arr item one line
        displayList.push(<ShoppingItem
            key={shoppingList[i].id}
            shoppingObj={shoppingList[i]}
            updateItem={UpdateItem}
            removeItem={RemoveItem} />);
    }

    return (
        <>
            <AddShoppingItemForm addItem={AddItem} />
            {displayList}
        </>
    );
}

export default ShoppingList;