import React, { useState } from "react";
import { StyledPrimaryButton, StyledSecondButton } from "../components/styled/Buttons.styled";
import { StyledPrimaryLabel } from "../components/styled/Labels.styled";
import { StyledPrimaryInput, StyledSecondInput } from "../components/styled/Inputs.styled";

function AddShoppingItemForm({ addItem }) {

    async function handleSubmit(e) {
        e.preventDefault();

        // Read the form data
        const form = e.target;
        const formData = new FormData(form);

        const formJson = Object.fromEntries(formData.entries());

        await addItem(formJson.itemName, formJson.count).then(
            form.reset()
        );



    }
    return (<>
        <form method="post" onSubmit={handleSubmit}>
            <StyledPrimaryLabel>
                Name:<StyledPrimaryInput name="itemName" defaultValue={''} />
            </StyledPrimaryLabel>
            <StyledPrimaryLabel>Count:<StyledSecondInput name="count" type="number" defaultValue={0} /></StyledPrimaryLabel>
            <StyledPrimaryButton type="reset">Reset form</StyledPrimaryButton>
            <StyledSecondButton type="submit">Submit</StyledSecondButton>
        </form >
    </>);
}

export default AddShoppingItemForm;