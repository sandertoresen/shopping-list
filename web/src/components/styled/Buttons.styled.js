import styled from 'styled-components';

export const StyledBaseButton = styled.button`
    /* position */
    align-self: center;
    margin: .5%;

    /* size */
    height: 2.5rem;
    flex: 1;

    /* style */
    border-radius: 5px;
    border: none;
    font-size: ${props => props.$remove ? '1.5rem':'2rem'};
    cursor: pointer;

    /* animation */
    transition-duration: 0.4s;
`;  


export const StyledPrimaryButton = styled(StyledBaseButton)`
    background: ${props => props.theme.colors.primaryBtn};
    color: ${props => props.theme.colors.primaryBtnActive};

    &:hover{
        background: ${props => props.theme.colors.primaryBtnActive};
        color: ${props => props.theme.colors.primaryBtn};
}
`;

export const StyledSecondButton = styled(StyledBaseButton)`
    background: ${props => props.theme.colors.secondBtn};
    color: ${props => props.theme.colors.secondBtnActive};

    &:hover{
        background: ${props => props.theme.colors.secondBtnActive};
        color: ${props => props.theme.colors.secondBtn};
}
`;

export const StyledThirdButton = styled(StyledBaseButton)`
    background: ${props => props.theme.colors.thirdBtn};
    color: ${props => props.theme.colors.thirdBtnActive};

    &:hover{
        background: ${props => props.theme.colors.thirdBtnActive};
        color: ${props => props.theme.colors.thirdBtn};
}
`;