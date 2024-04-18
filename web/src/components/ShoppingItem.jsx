import React from 'react';
import styled from 'styled-components'
import { StyledShoppingItem} from './styled/ShoppingItem.styled';
import { StyledPrimaryButton, StyledSecondButton, StyledThirdButton } from './styled/Buttons.styled';

function ShoppingItem({ shoppingObj, updateItem, removeItem }) {
    const { id, name, count } = shoppingObj;

    //add
    //subtract
    const handleDecrement = () => {
        updateItem(id, -1);
    };
    const handleIncrement = () => {
        updateItem(id, 1);
    };
    //delete
    //rename
    return (
        <StyledShoppingItem>
            <p className='name'>{name}</p>
            <p className='count'>{count}</p>
            <StyledPrimaryButton onClick={handleIncrement}>+</StyledPrimaryButton>
            <StyledSecondButton onClick={handleDecrement}>-</StyledSecondButton>
            <StyledThirdButton onClick={() => { removeItem(id) }}>Remove</StyledThirdButton>
        </StyledShoppingItem>);
}

export default ShoppingItem;