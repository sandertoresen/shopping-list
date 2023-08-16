import styled from "styled-components";

/*
content
padding
border
margin
*/
export const StyledShoppingItem = styled.section`
    /* position */
    display: flex;
    justify-content: flex-end;
    margin: .5%;
    padding: 0px;
    padding-left: 1%;
    padding-right: 1%;
    
    /* size */
    width: 40%;
    
    /* color/style */
    background: ${props => props.theme.colors.listBackground};
    border-radius: 2.5%;

    p.name{
        /* position */
        align-self: center;
        margin-right: .5%;
        padding: 0px;

        /* size */
        width: 16ch;
        max-width: 16ch;
        font-size: 1.5rem;
    }
    p.count{
        /* position */
        align-self: center;
        margin-right: .5%;

        /* size */
        width: 4ch;
        font-size: 1.5rem;
    }
`;